using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame2D.Script
{
    /// <summary>
    /// Defines a per-frame updating model for objects
    /// </summary>
    public interface IUpdatable
    {

        /// <summary>
        /// Updates the specified instance with frame time delta.
        /// </summary>
        /// <param name="timeDelta">The time delta in seconds.</param>
        void Update(float timeDelta);
    }
}
