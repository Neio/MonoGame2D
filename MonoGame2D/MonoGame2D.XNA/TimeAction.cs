using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Script
{
    /// <summary>
    /// Implements base time action
    /// </summary>
    abstract class TimeAction : ITimeActionChain
    {

        /// <summary>
        /// Gets or sets the next action in chain.
        /// </summary>
        /// <value>The next action in chain.</value>
        public TimeAction NextAction { get; private set; }

        /// <summary>
        /// Updates the specified time action with frame time delta.
        /// </summary>
        /// <param name="timeDelta">The time delta in seconds.</param>
        /// <returns><c>true</c> if action is totally complete and can't be processed more</returns>
        public abstract bool Update(float timeDelta);


        #region ITimeActionChain Members

        /// <summary>
        /// Waits the specified time before start executing next chain element.
        /// </summary>
        /// <param name="timeToWait">The time to wait before start executing next chained element.</param>
        /// <returns>
        /// Time action chain successing current action.
        /// </returns>
        public ITimeActionChain Wait(float timeToWait)
        {
            return NextAction = new WaitTimeAction(timeToWait);
        }

        /// <summary>
        /// Invokes the specified method.
        /// </summary>
        /// <param name="actionCallback"></param>
        /// <returns>
        /// Time action chain successing current action.
        /// </returns>
        public ITimeActionChain Invoke(CallbackHandler actionCallback)
        {
            return NextAction = new InvokeTimeAction(actionCallback);
        }

        /// <summary>
        /// Repeats the call of action status callback for the specified period.
        /// </summary>
        /// <param name="timeToRepeat">The time to repeat action update.</param>
        /// <param name="actionCallback">The action callback.</param>
        /// <returns>
        /// Time action chain successing current action.
        /// </returns>
        public ITimeActionChain Repeat(float timeToRepeat, TimeActionCallbackHandler actionCallback)
        {
            return NextAction = new RepeatableTimeAction(timeToRepeat, 0, actionCallback);
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
        public ITimeActionChain Pulse(float timeToRepeat, float pulsePeriod, TimeActionCallbackHandler actionCallback)
        {
            return NextAction = new RepeatableTimeAction(timeToRepeat, pulsePeriod, actionCallback);
        }

        #endregion


        public ITimeActionChain RepeatUntil(ActionCallBack action)
        {
            return NextAction = new RepeatCallbackAction(action);
        }
    }

    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Implements wait action
    /// </summary>
    class WaitTimeAction : TimeAction
    {
        private float _waitTime;

        public WaitTimeAction(float waitTime)
        {
            _waitTime = waitTime;
        }

        public override bool Update(float timeDelta)
        {
            _waitTime -= timeDelta;
            return _waitTime <= 0f;
        }
    }

    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Implements method invokation action
    /// </summary>
    class InvokeTimeAction : TimeAction
    {
        private CallbackHandler _callbackHandler;

        public InvokeTimeAction(CallbackHandler callbackHandler)
        {
            _callbackHandler = callbackHandler;
        }

        public override bool Update(float timeDelta)
        {
            _callbackHandler();
            return true;
        }
    }

    //---------------------------------------------------------------------------------------------

    /// <summary>
    /// Repeatable time action. Can be used for repeatable and pulsing events.
    /// </summary>
    class RepeatableTimeAction : TimeAction, IActionStatus
    {
        private int _eventCount;
        private float _duration;
        private float _pulseTime;
        private float _timeEllapsed;
        private float _pulseTimeEllapsed;
        private TimeActionCallbackHandler _timeCallbackHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatableTimeAction"/> class.
        /// </summary>
        /// <param name="duration">The duration of action.</param>
        /// <param name="pulseTime">The pulse time.</param>
        /// <param name="timeCallbackHandler">The time callback handler.</param>
        public RepeatableTimeAction(float duration, float pulseTime, TimeActionCallbackHandler timeCallbackHandler)
        {
            _duration = duration;
            _pulseTime = pulseTime;
            _timeCallbackHandler = timeCallbackHandler;
        }

        public override bool Update(float timeDelta)
        {
            _timeEllapsed += timeDelta;
            _pulseTimeEllapsed += timeDelta;

            if (_pulseTimeEllapsed >= _pulseTime)
            {
                if (_pulseTime > 0)
                {
                    _pulseTimeEllapsed %= _pulseTime;
                }
                //invoke callback
                _timeCallbackHandler(this);
                _eventCount++;
            }

            return _timeEllapsed >= _duration;
        }

        #region IActionStatus Members

        /// <summary>
        /// Gets the number of invoked events before this one.
        /// </summary>
        /// <value>The action event count before this one.</value>
        public int EventCount
        {
            get { return _eventCount; }
        }

        /// <summary>
        /// Gets the duration of action.
        /// </summary>
        /// <value>The duration of action.</value>
        public float Duration
        {
            get { return _duration; }
        }

        /// <summary>
        /// Gets the ellapsed action time.
        /// </summary>
        /// <value>The ellapsed action time.</value>
        public float Ellapsed
        {
            get { return _timeEllapsed; }
        }

        /// <summary>
        /// Gets the progress of action.
        /// </summary>
        /// <value>The progress of action.</value>
        public float Progress
        {
            get { return _timeEllapsed / _duration; }
        }

        #endregion
    }


    /// <summary>
    /// Repeat until Callback Action, extented by Neio Zhou(neio.zhou@gmail.com)
    /// </summary>
    class RepeatCallbackAction : TimeAction
    {
        //private bool _isExecuting = false;
        private bool _finished = false;

        private ActionCallBack _callbackHandler;

        public RepeatCallbackAction(ActionCallBack callbackHandler)
        {

            _callbackHandler = callbackHandler;
        }

        public override bool Update(float timeDelta)
        {
            // if (!_isExecuting) {
            _callbackHandler(
                new CallbackHandler(() =>
                {
                    _finished = true;
                }));
            //  _isExecuting = true;
            //}
            return _finished;
        }



    }

    public delegate void ActionCallBack(CallbackHandler Ended);
}
