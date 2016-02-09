using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using UIKit;

namespace iOS.Behaviors.Command
{
    public class EventToCommandBehavior : Behavior<UIView>
    {
        private readonly List<Tuple<object, string, Delegate>> _dynamicEventHandlersList;

        private readonly string _eventName;
        private readonly ICommand _command;
        private readonly object _commandParameter;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventToCommandBehavior"/> class.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="command">The command.</param>
        /// <param name="commandParameter">The command parameter.</param>
        public EventToCommandBehavior(string eventName, ICommand command, object commandParameter = null)
        {
            _eventName = eventName;
            _command = command;
            _commandParameter = commandParameter;

            _dynamicEventHandlersList = new List<Tuple<object, string, Delegate>>();
        }

        /// <summary>
        /// Method to override when the behavior is attached to the view.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Exception raised when the specified event is not found on the corresponding type.</exception>
        protected override void OnAttached() 
        {
            var eventInfo = this.AssociatedObject.GetType().GetEvent(_eventName);
            if (eventInfo != null)
            {
                // Add an event handler to the specified event to execute the command when event is raised
                var executeMethodInfo = typeof(ICommand).GetMethod("Execute", new[] { typeof(object) });

                var executeHandler = AddHandler(this.AssociatedObject, _eventName, () =>
                {
                    executeMethodInfo.Invoke(_command, new[] { _commandParameter });
                });

                if (executeHandler != null)
                {
                    _dynamicEventHandlersList.Add(new Tuple<object, string, Delegate>(this.AssociatedObject, _eventName, executeHandler));
                }

                // Add an event handler to manage the CanExecuteChanged event of the command (so we can disable/enable the control attached to the command)
                var enabledProperty = this.AssociatedObject.GetType().GetProperty("Enabled");
                if (enabledProperty != null)
                {
                    enabledProperty.SetValue(this.AssociatedObject, _command.CanExecute(_commandParameter));

                    var canExecuteChangedHandler = AddHandler(_command, "CanExecuteChanged", () => enabledProperty.SetValue(this.AssociatedObject, _command.CanExecute(_commandParameter)));
                    if (canExecuteChangedHandler != null)
                    {
                        _dynamicEventHandlersList.Add(new Tuple<object, string, Delegate>(_command, "CanExecuteChanged", canExecuteChangedHandler));
                    }
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format("The event {0} was not found on the type {1}", _eventName, AssociatedObject.GetType().Name));
            }
        }

        /// <summary>
        /// Method to override when the behavior is removed from the view.
        /// </summary>
        protected override void OnDetaching()
        {
            if (_dynamicEventHandlersList != null && _dynamicEventHandlersList.Any())
            {
                foreach (var tuple in _dynamicEventHandlersList)
                {
                    RemoveHandler(tuple.Item1, tuple.Item2, tuple.Item3);
                }
            }
        }

        /// <summary>
        /// Helper to dynamically add an event handler to a control.
        /// Source: http://stackoverflow.com/questions/5658765/create-a-catch-all-handler-for-all-events-and-delegates-in-c-sharp
        /// </summary>
        /// <param name="target">The control on which we want to add the event handler.</param>
        /// <param name="eventName">The name of the event on which we want to add a handler.</param>
        /// <param name="methodToExecute">The code we want to execute when the handler is raised.</param>
        private static Delegate AddHandler(object target, string eventName, Action methodToExecute)
        {
            var eventInfo = target.GetType().GetEvent(eventName);
            if (eventInfo != null)
            {
                var delegateType = eventInfo.EventHandlerType;
                var dynamicHandler = BuildDynamicHandler(delegateType, methodToExecute);

                //eventInfo.GetAddMethod().Invoke(target, new object[] { dynamicHandler });
                eventInfo.AddEventHandler(target, dynamicHandler);

                return dynamicHandler;
            }

            return null;
        }

        /// <summary>
        /// Helper to dynamically remove an event handler from a control.
        /// </summary>
        /// <param name="target">The control on which we want to add the event handler.</param>
        /// <param name="eventName">The name of the event on which we want to add a handler.</param>
        /// <param name="delegateToRemove">The delegate to remove.</param>
        private static void RemoveHandler(object target, string eventName, Delegate delegateToRemove)
        {
            var eventInfo = target.GetType().GetEvent(eventName);
            if (eventInfo != null)
            {
                eventInfo.RemoveEventHandler(target, delegateToRemove);
            }
        }

        /// <summary>
        /// Build a delegate for a particular type.
        /// </summary>
        /// <param name="delegateType">The type of the object for which we want the delegate.</param>
        /// <param name="methodToExecute">The code we want to execute when the handler is raised.</param>
        /// <returns>A delegate object for the dedicated type, used the execute the specified code.</returns>
        private static Delegate BuildDynamicHandler(Type delegateType, Action methodToExecute)
        {
            var invokeMethod = delegateType.GetMethod("Invoke");
            var parms = invokeMethod.GetParameters().Select(parm => Expression.Parameter(parm.ParameterType, parm.Name)).ToArray();
            var instance = methodToExecute.Target == null ? null : Expression.Constant(methodToExecute.Target);
            var call = Expression.Call(instance, methodToExecute.Method);
            var body = invokeMethod.ReturnType == typeof(void) ? (Expression)call : Expression.Convert(call, invokeMethod.ReturnType);
            var expr = Expression.Lambda(delegateType, body, parms);

            return expr.Compile();
        }
    }
}