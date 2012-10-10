using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    //---------------------------------------------------------------------
    /// <summary>
    /// Implementation of Transform scope
    /// </summary>
    public class TransformScope : StateScope
    {
        public TransformScope(StateScope scope, ref Matrix transform, bool overrideTransform)
            : base(scope)
        {
            Canvas2D canvas = ReferredCanvas;
            canvas.PushTransform();
            if (overrideTransform)
            {
                canvas.SetTransform(ref transform);
            }
            else
            {
                canvas.ApplyTransformBefore(ref transform);
            }
        }

        /// <summary>
        /// Closes the scope: setting initial z value.
        /// </summary>
        protected override void CloseScope()
        {
            ReferredCanvas.PopTransform();
            base.CloseScope();
        }
    }
}
