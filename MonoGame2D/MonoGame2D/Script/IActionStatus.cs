using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame2D.Script
{
    /// <summary>
    /// Time action status
    /// </summary>
    public interface IActionStatus
    {

        /// <summary>
        /// Gets the number of invoked events before this one.
        /// </summary>
        /// <value>The action event count before this one.</value>
        int EventCount { get; }

        /// <summary>
        /// Gets the duration of action.
        /// </summary>
        /// <value>The duration of action.</value>
        float Duration { get; }

        /// <summary>
        /// Gets the ellapsed action time.
        /// </summary>
        /// <value>The ellapsed action time.</value>
        float Ellapsed { get; }

        /// <summary>
        /// Gets the progress of action.
        /// </summary>
        /// <value>The progress of action.</value>
        float Progress { get; }
    }
}
