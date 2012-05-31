using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame2D.Script
{
    /// <summary>
	/// Time line controller class. It allows stacking (chaining) events and control their execution
	/// </summary>
	public class TimeLine : IUpdatable, ITimeActionChain {
		///<summary>List of currently active time events</summary>
		List<TimeAction> _events = new List<TimeAction>();


		#region IUpdatable Members

		/// <summary>
		/// Updates the specified time line with frame time delta.
		/// </summary>
		/// <param name="timeDelta">The time delta in seconds.</param>
		public void Update(float timeDelta) {
			if (_events.Count > 0) {
				for (int n = 0; n < _events.Count; ++n) {
					TimeAction oldAction = _events[n];
					TimeAction action = UpdateAction(oldAction, timeDelta);
					if (action != oldAction) {
						if (action != null) {
							_events[n] = action;
						} else {
							_events.RemoveAt(n--);
						}
					}
				}
			}
		}

		/// <summary>
		/// Updates the time action.
		/// </summary>
		/// <param name="timeAction">The time action to update.</param>
		/// <returns><see cref="Vortex.TimeAction"/> instance which replaces old one (can be the same if action is not instant) or <c>null</c> if time chain is executed completely</returns>
		private TimeAction UpdateAction(TimeAction timeAction, float timeDelta) {
			while (null != timeAction && timeAction.Update(timeDelta)) {
				timeAction = timeAction.NextAction;
			}
			return timeAction;
		}

		#endregion

		#region IEventChain Members

		/// <summary>
		/// Waits the specified time before start executing next chain element.
		/// </summary>
		/// <param name="timeToWait">The time to wait before start executing next chained element.</param>
		/// <returns>
		/// Time action chain successing current action.
		/// </returns>
		public ITimeActionChain Wait(float timeToWait) {
			TimeAction action = new WaitTimeAction(timeToWait);
			_events.Add(action);
			return action;
		}

		/// <summary>
		/// Invokes the specified method to invoke.
		/// </summary>
		/// <param name="methodToInvoke">The method to invoke.</param>
		/// <returns></returns>
		public ITimeActionChain Invoke(CallbackHandler methodToInvoke) {
			TimeAction action = new InvokeTimeAction(methodToInvoke);
			_events.Add(action);
			return action;
		}

		/// <summary>
		/// Repeats the call of action status callback for the specified period.
		/// </summary>
		/// <param name="timeToRepeat">The time to repeat action update.</param>
		/// <param name="actionCallback">The action callback.</param>
		/// <returns>
		/// Time action chain successing current action.
		/// </returns>
		public ITimeActionChain Repeat(float timeToRepeat, TimeActionCallbackHandler actionCallback) {
			TimeAction action = new RepeatableTimeAction(timeToRepeat, 0, actionCallback);
			_events.Add(action);
			return action;
		}

		/// <summary>
		/// Repeats the call of action status callback for the specified period each pulsePeriod time.
		/// </summary>
		/// <param name="timeToRepeat">The time to repeat action update.</param>
		/// <param name="pulsePeriod">The pulsation period.</param>
		/// <param name="actionCallback">The action callback.</param>
		/// <returns>
		/// Time action chain successing current action.
		/// </returns>
		public ITimeActionChain Pulse(float timeToRepeat, float pulsePeriod, TimeActionCallbackHandler actionCallback) {
			TimeAction action = new RepeatableTimeAction(timeToRepeat, pulsePeriod, actionCallback);
			_events.Add(action);
			return action;
		}

		#endregion

        /// <summary>
        /// Repeat until Callback is called
        /// </summary>
        /// <param name="TAction"></param>
        /// <returns></returns>
        public ITimeActionChain RepeatUntil(ActionCallBack TAction)
        {
            TimeAction action = new RepeatCallbackAction(TAction);
            _events.Add(action);
            return action;
        }
    }
}

