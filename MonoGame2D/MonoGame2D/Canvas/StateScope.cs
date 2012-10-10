using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    /// <summary>
	/// Contains list of operators which provides easy-to-use canvas state scoped shortcuts '<='
	/// </summary>
    public abstract class StateScope : IDisposable
    {
        ///<summary>Previous scope in scope chain</summary>
        protected StateScope _prevScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateScope"/> class.
        /// </summary>
        protected StateScope()
        {
        }

        /// <summary>
        /// Initializes a <see cref="StateScope"/> with a previous scope.
        /// </summary>
        /// <param name="prevScope">The previous scope in scope chain.</param>
        protected StateScope(StateScope prevScope)
        {
            _prevScope = prevScope;
        }

        /// <summary>
        /// Gets the referred canvas .
        /// </summary>
        /// <value>The canvas instance.</value>
        protected virtual Canvas2D ReferredCanvas
        {
            get { return _prevScope.ReferredCanvas; }
        }

        /// <summary>
        /// Closes the scope: setting initial canvas state, closing previous scope in chain...
        /// </summary>
        protected virtual void CloseScope()
        {
            _prevScope.CloseScope();
        }

        #region IDisposable Members

        /// <summary>
        /// Tricky method for using canvas state changes with operator 
        /// </summary>
        public void Dispose()
        {
            CloseScope();
        }

        #endregion


        public static StateScope operator <=(StateScope canvas, ITransform tran)
        {

            Matrix max = canvas.ReferredCanvas.Matrix * tran.Matrix;
            return new TransformScope(canvas, ref max, false);// new Canvas2D(canvas.Batch, ref max);
        }


        public static StateScope operator >=(StateScope canvas, ITransform tran)
        {

            throw new NotImplementedException();
        }
    }
}
