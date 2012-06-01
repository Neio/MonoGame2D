using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Script
{
    /// <summary>
    /// Time action status callback handler.
    /// </summary>
    public delegate void TimeActionCallbackHandler(IActionStatus status);

    /// <summary>
    /// The simplest possible delegate, someimes it is more preferred than EventHandler
    /// </summary>
    public delegate void CallbackHandler();

    /// <summary>
    /// Implements time chain methods. Each of method return derrived ITimeChain instance which will be excuted after previous one is complete.
    /// </summary>
    public interface ITimeActionChain
    {

        /// <summary>
        /// Waits the specified time before start executing next chain element.
        /// </summary>
        /// <param name="timeToWait">The time to wait before start executing next chained element.</param>
        /// <returns>Time action chain successing current action.</returns>
        ITimeActionChain Wait(float timeToWait);

        /// <summary>
        /// Invokes the specified method.
        /// </summary>
        /// <param name="methodToInvoke">The method to invoke.</param>
        /// <returns>Time action chain successing current action.</returns>
        ITimeActionChain Invoke(CallbackHandler actionCallback);

        /// <summary>
        /// Repeats the call of action status callback for the specified period.
        /// </summary>
        /// <param name="timeToRepeat">The time to repeat action update.</param>
        /// <param name="actionCallback">The action callback.</param>
        /// <returns>Time action chain successing current action.</returns>
        ITimeActionChain Repeat(float timeToRepeat, TimeActionCallbackHandler actionCallback);

        /// <summary>
        /// Repeats the call of action status callback for the specified period each pulsePeriod time.
        /// </summary>
        /// <param name="timeToRepeat">The time to repeat action update.</param>
        /// <param name="pulsePeriod">The pulsation period.</param>
        /// <param name="actionCallback">The action callback.</param>
        /// <returns>Time action chain successing current action.</returns>
        ITimeActionChain Pulse(float timeToRepeat, float pulsePeriod, TimeActionCallbackHandler actionCallback);


        /// <summary>
        /// Repeat until Callback, extented by Neio Zhou(neio.zhou@gmail.com)
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ITimeActionChain RepeatUntil(ActionCallBack action);
    }
}
