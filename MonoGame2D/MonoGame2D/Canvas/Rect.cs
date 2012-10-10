using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    /// <summary>
    /// Implementation of flexible rectangle class for Vortex2D.NET
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect : IEquatable<Rect>
    {
        #region Constants

        /// <summary>Empty rectangle, all of coordinates = 0</summary>
        public static readonly Rect Empty = new Rect(Vector2.Zero, Vector2.Zero);
        /// <summary>Unit rectangle at zero point with edge size = 1</summary>
        public static readonly Rect Unit = new Rect(Vector2.Zero, Vector2.UnitX);

        #endregion

        #region Public Fields

        ///<summary>Left X coordinate of rectangle</summary>
        [System.Xml.Serialization.XmlAttribute]
        public float Left;
        ///<summary>Top Y coordinate of rectangle</summary>
        [System.Xml.Serialization.XmlAttribute]
        public float Top;
        ///<summary>Right X coordinate of rectangle</summary>
        [System.Xml.Serialization.XmlAttribute]
        public float Right;
        ///<summary>Bottom Y coordinate of rectangle</summary>
        [System.Xml.Serialization.XmlAttribute]
        public float Bottom;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public float Width
        {
            get { return Right - Left; }
            set { Right = Left + value; }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public float Height
        {
            get { return Bottom - Top; }
            set { Bottom = Top + value; }
        }

        /// <summary>
        /// Gets or sets the size of rectangle.
        /// </summary>
        /// <value>The size of rectangle.</value>
        public Vector2 Size
        {
            get { return new Vector2(Width, Height); }
            set { Width = value.X; Height = value.Y; }
        }

        /// <summary>
        /// Gets or sets the width half.
        /// </summary>
        /// <value>The half of width .</value>
        public float HalfWidth
        {
            get { return Width * .5f; }
            set { Width = value * 2f; }
        }

        /// <summary>
        /// Gets or sets the half of height .
        /// </summary>
        /// <value>The half of height.</value>
        public float HalfHeight
        {
            get { return Height * .5f; }
            set { Height = value * 2f; }
        }

        /// <summary>
        /// Gets or sets the half of size of rectangle.
        /// </summary>
        /// <value>The half of size of rectangle.</value>
        public Vector2 HalfSize
        {
            get { return new Vector2(HalfWidth, HalfHeight); }
            set { HalfWidth = value.X; HalfHeight = value.Y; }
        }

        /// <summary>
        /// Gets or sets the center X.
        /// </summary>
        /// <value>The center X.</value>
        public float CenterX
        {
            get { return (Left + Right) * 0.5f; }
            set { float delta = value - CenterX; Left += delta; Right += delta; }
        }

        /// <summary>
        /// Gets or sets the center Y.
        /// </summary>
        /// <value>The center Y.</value>
        public float CenterY
        {
            get { return (Top + Bottom) * 0.5f; }
            set { float delta = value - CenterY; Top += delta; Bottom += delta; }
        }

        /// <summary>
        /// Gets or sets the center of rectangle.
        /// </summary>
        /// <value>The center of rectangle.</value>
        public Vector2 Center
        {
            get { return new Vector2(CenterX, CenterY); }
            set { CenterX = value.X; CenterY = value.Y; }
        }

        /// <summary>
        /// Gets or sets the left top point.
        /// </summary>
        /// <value>The left top point.</value>
        public Vector2 LeftTop
        {
            get { return new Vector2(Left, Top); }
            set { Left = value.X; Top = value.Y; }
        }

        /// <summary>
        /// Gets or sets the right top  point.
        /// </summary>
        /// <value>The right top point.</value>
        public Vector2 RightTop
        {
            get { return new Vector2(Right, Top); }
            set { Right = value.X; Top = value.Y; }
        }

        /// <summary>
        /// Gets or sets the right bottom point.
        /// </summary>
        /// <value>The right bottom point.</value>
        public Vector2 RightBottom
        {
            get { return new Vector2(Right, Bottom); }
            set { Right = value.X; Bottom = value.Y; }
        }

        /// <summary>
        /// Gets or sets the left bottom point.
        /// </summary>
        /// <value>The left bottom point.</value>
        public Vector2 LeftBottom
        {
            get { return new Vector2(Left, Bottom); }
            set { Left = value.X; Bottom = value.Y; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is normalized (Left less or equal Right and Top less or equal Bottom).
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is normalized; otherwise, <c>false</c>.
        /// </value>
        public bool IsNormalized
        {
            get { return Left <= Right && Top <= Bottom; }
        }

        #endregion

        #region Constructors

        public Rect(float left, float top, float right, float bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public Rect(Vector2 leftTop, Vector2 rightBottom)
        {
            this.Left = leftTop.X;
            this.Top = leftTop.Y;
            this.Right = rightBottom.X;
            this.Bottom = rightBottom.Y;
        }

        public Rect(float left, float top, SizeF size)
        {
            this.Left = left;
            this.Top = top;
            this.Right = Left + size.Width;
            this.Bottom = Top + size.Height;
        }

        public Rect(Vector2 leftTop, SizeF size)
        {
            this.Left = leftTop.X;
            this.Top = leftTop.Y;
            this.Right = Left + size.Width;
            this.Bottom = Top + size.Height;
        }

        public Rect(PointF leftTop, SizeF size)
        {
            this.Left = leftTop.X;
            this.Top = leftTop.Y;
            this.Right = Left + size.Width;
            this.Bottom = Top + size.Height;
        }

        public Rect(Microsoft.Xna.Framework.Point leftTop, Size size)
        {
            this.Left = leftTop.X;
            this.Top = leftTop.Y;
            this.Right = Left + size.Width;
            this.Bottom = Top + size.Height;
        }

        public Rect(Microsoft.Xna.Framework.Rectangle rect)
        {
            this.Left = rect.Left;
            this.Top = rect.Top;
            this.Right = rect.Right;
            this.Bottom = rect.Bottom;
        }

        public Rect(RectangleF rect)
        {
            this.Left = rect.Left;
            this.Top = rect.Top;
            this.Right = rect.Right;
            this.Bottom = rect.Bottom;
        }

        #endregion

        #region Constructor Methods

        /// <summary>
        /// Creates rectangle from top left point and size
        /// </summary>
        /// <param name="x">The x of left top corner.</param>
        /// <param name="y">The y of left top corner.</param>
        /// <param name="width">The width of rectangle.</param>
        /// <param name="height">The height of rectangle.</param>
        /// <returns>Initialized rectangle</returns>
        public static Rect FromBox(float x, float y, float width, float height)
        {
            return new Rect(x, y, x + width, y + height);
        }

        /// <summary>
        /// Creates rectangle from top left point and size
        /// </summary>
        /// <param name="leftTop">The left top point.</param>
        /// <param name="size">The size.</param>
        /// <returns>Initialized rectangle</returns>
        public static Rect FromBox(Vector2 leftTop, Vector2 size)
        {
            return new Rect(leftTop, leftTop + size);
        }

        /// <summary>
        /// Creates rectangle from central point and size
        /// </summary>
        /// <param name="centerX">The center X.</param>
        /// <param name="centerY">The center Y.</param>
        /// <param name="width">The width of rectangle.</param>
        /// <param name="height">The height of rectangle.</param>
        /// <returns>Initialized rectangle</returns>
        public static Rect FromPoint(float centerX, float centerY, float width, float height)
        {
            float halfX = width * 0.5f, halfY = height * 0.5f;
            return new Rect(centerX - halfX, centerY - halfY, centerX + halfX, centerY + halfY);
        }

        /// <summary>
        /// Creates rectangle from central point and size
        /// </summary>
        /// <param name="center">The center point.</param>
        /// <param name="size">The size.</param>
        /// <returns>Initialized rectangle</returns>
        public static Rect FromPoint(Vector2 center, Vector2 size)
        {
            Vector2 half = size.Half();
            return new Rect(center.X - half.X, center.Y - half.Y, center.X + half.X, center.Y + half.Y);
        }

        #endregion

        #region IEquatable<Rect> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Rect other)
        {
            return Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;
        }

        #endregion

        #region Maintain methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object other)
        {
            return other is Rect ? Equals((Rect)other) : false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (int)(Left + Top + Right + Bottom);
        }

        /// <summary>
        /// Compares two instances of <see cref="Vortex.Drawing.Rect"/>.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns><c>true</c> if values of type <see cref="Vortex.Drawing.Rect"/> are equal; otherwise, <c>false</c>.</returns>
        public static bool Equals(ref Rect value1, ref Rect value2)
        {
            return value1.Left == value2.Left && value1.Top == value2.Top && value1.Right == value2.Right && value1.Bottom == value2.Bottom;
        }

        #endregion

        #region ToXXX Methods

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this rect.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this rect.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{{{0},{1},{2},{3}}}", Left, Top, Right, Bottom);
        }

        /// <summary>
        /// Returns the <see cref="System.Drawing.Rectangle"/> equivalent this.
        /// </summary>
        /// <returns>A <see cref="System.Drawing.Rectangle"/></returns>
        public Microsoft.Xna.Framework.Rectangle ToRectangle()
        {
            return new Microsoft.Xna.Framework.Rectangle((int)Left, (int)Top, (int)Width, (int)Height);
        }

        /// <summary>
        /// Returns the <see cref="System.Drawing.RectangleF"/> equivalent this.
        /// </summary>
        /// <returns>A <see cref="System.Drawing.RectangleF"/></returns>
        public RectangleF ToRectangleF()
        {
            return new RectangleF(Left, Top, Width, Height);
        }

        #endregion

        #region Basic Operations

        public Rect Add(Rect value)
        {
            Add(ref this, ref value, out value);
            return value;
        }

        public static Rect Add(Rect value1, Rect value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Add(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left + value2.Left;
            result.Top = value1.Top + value2.Top;
            result.Right = value1.Right + value2.Right;
            result.Bottom = value1.Bottom + value2.Bottom;
        }

        public Rect Add(Vector2 value)
        {
            Rect result;
            Add(ref this, ref value, out result);
            return result;
        }

        public static Rect Add(Rect value1, Vector2 value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Add(ref Rect value1, ref Vector2 value2, out Rect result)
        {
            result.Left = value1.Left + value2.X;
            result.Top = value1.Top + value2.Y;
            result.Right = value1.Right + value2.X;
            result.Bottom = value1.Bottom + value2.Y;
        }

        public Rect Subtract(Rect value)
        {
            Subtract(ref this, ref value, out value);
            return value;
        }

        public static Rect Subtract(Rect value1, Rect value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Subtract(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left - value2.Left;
            result.Top = value1.Top - value2.Top;
            result.Right = value1.Right - value2.Right;
            result.Bottom = value1.Bottom - value2.Bottom;
        }

        public Rect Subtract(Vector2 value)
        {
            Rect result;
            Subtract(ref this, ref value, out result);
            return result;
        }

        public static Rect Subtract(Rect value1, Vector2 value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Subtract(ref Rect value1, ref Vector2 value2, out Rect result)
        {
            result.Left = value1.Left - value2.X;
            result.Top = value1.Top - value2.Y;
            result.Right = value1.Right - value2.X;
            result.Bottom = value1.Bottom - value2.Y;
        }

        public Rect Multiply(Rect value)
        {
            Multiply(ref this, ref value, out value);
            return value;
        }

        public static Rect Multiply(Rect value1, Rect value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Multiply(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left * value2.Left;
            result.Top = value1.Top * value2.Top;
            result.Right = value1.Right * value2.Right;
            result.Bottom = value1.Bottom * value2.Bottom;
        }

        public Rect Multiply(Vector2 value)
        {
            Rect result;
            Multiply(ref this, ref value, out result);
            return result;
        }

        public static Rect Multiply(Rect value1, Vector2 value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Multiply(ref Rect value1, ref Vector2 value2, out Rect result)
        {
            result.Left = value1.Left * value2.X;
            result.Top = value1.Top * value2.Y;
            result.Right = value1.Right * value2.X;
            result.Bottom = value1.Bottom * value2.Y;
        }

        public static void Multiply(ref Rect value1, float value2, out Rect result)
        {
            result.Left = value1.Left * value2;
            result.Top = value1.Top * value2;
            result.Right = value1.Right * value2;
            result.Bottom = value1.Bottom * value2;
        }

        public Rect Divide(Rect value)
        {
            Divide(ref this, ref value, out value);
            return value;
        }

        public static Rect Divide(Rect value1, Rect value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Divide(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left / value2.Left;
            result.Top = value1.Top / value2.Top;
            result.Right = value1.Right / value2.Right;
            result.Bottom = value1.Bottom / value2.Bottom;
        }

        public Rect Divide(Vector2 value)
        {
            Rect result;
            Divide(ref this, ref value, out result);
            return result;
        }

        public static Rect Divide(Rect value1, Vector2 value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Divide(ref Rect value1, ref Vector2 value2, out Rect result)
        {
            result.Left = value1.Left / value2.X;
            result.Top = value1.Top / value2.Y;
            result.Right = value1.Right / value2.X;
            result.Bottom = value1.Bottom / value2.Y;
        }


        public static void Divide(ref Rect value1, float value2, out Rect result)
        {
            result.Left = value1.Left / value2;
            result.Top = value1.Top / value2;
            result.Right = value1.Right / value2;
            result.Bottom = value1.Bottom / value2;
        }

        public Rect Inflate(Rect value)
        {
            Inflate(ref this, ref value, out value);
            return value;
        }

        public static Rect Inflate(Rect value1, Rect value2)
        {
            Inflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Inflate(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left - value2.Left;
            result.Top = value1.Top - value2.Top;
            result.Right = value1.Right + value2.Right;
            result.Bottom = value1.Bottom + value2.Bottom;
        }

        public Rect Inflate(float inflateX, float inflateY)
        {
            return Inflate(new Vector2(inflateX, inflateY));
        }

        public Rect Inflate(Vector2 value)
        {
            Rect result;
            Inflate(ref this, ref value, out result);
            return result;
        }

        public static Rect Inflate(Rect value1, Vector2 value2)
        {
            Inflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Inflate(ref Rect value1, ref Vector2 value2, out Rect result)
        {
            result.Left = value1.Left - value2.X;
            result.Top = value1.Top - value2.Y;
            result.Right = value1.Right + value2.X;
            result.Bottom = value1.Bottom + value2.Y;
        }

        public Rect Deflate(Rect value)
        {
            Deflate(ref this, ref value, out value);
            return value;
        }

        public static Rect Deflate(Rect value1, Rect value2)
        {
            Deflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Deflate(ref Rect value1, ref Rect value2, out Rect result)
        {
            result.Left = value1.Left + value2.Left;
            result.Top = value1.Top + value2.Top;
            result.Right = value1.Right - value2.Right;
            result.Bottom = value1.Bottom - value2.Bottom;
        }

        public Rect Deflate(float deflateX, float deflateY)
        {
            return Deflate(new Vector2(deflateX, deflateY));
        }

        public Rect Deflate(Vector2 value)
        {
            Rect result;
            Deflate(ref this, ref value, out result);
            return result;
        }

        public static Rect Deflate(Rect value1, Vector2 value2)
        {
            Deflate(ref value1, ref value2, out value1);
            return value1;
        }

        public static void Deflate(ref Rect value1, ref Vector2 value2, out Rect result)
        {
            result.Left = value1.Left + value2.X;
            result.Top = value1.Top + value2.Y;
            result.Right = value1.Right - value2.X;
            result.Bottom = value1.Bottom - value2.Y;
        }

        #endregion

        #region Advanced Operations

        /// <summary>
        /// Flips rectangle horizontally.
        /// </summary>
        /// <returns>Flipped rectangle</returns>
        public Rect FlipHorizontal()
        {
            return new Rect(Right, Top, Left, Bottom);
        }

        /// <summary>
        /// Flips rectangle vertically.
        /// </summary>
        /// <returns>Flipped rectangle</returns>
        public Rect FlipVertical()
        {
            return new Rect(Left, Bottom, Right, Top);
        }

        /// <summary>
        /// Flips rectangle horizontally.
        /// </summary>
        /// <param name="source">The source rect.</param>
        /// <param name="result">The flipped rect.</param>
        public static void FlipHorizontal(ref Rect source, out Rect result)
        {
            result.Left = source.Right;
            result.Top = source.Top;
            result.Right = source.Left;
            result.Bottom = source.Bottom;
        }

        /// <summary>
        /// Flips rectangle vertically.
        /// </summary>
        /// <param name="source">The source rect.</param>
        /// <param name="result">The flipped rect.</param>
        public static void FlipVertical(ref Rect source, out Rect result)
        {
            result.Left = source.Left;
            result.Top = source.Bottom;
            result.Right = source.Right;
            result.Bottom = source.Top;
        }

        /// <summary>
        /// Normalizes rectangle. Ensures top left coordinate is top left point
        /// </summary>
        /// <returns>Normalized copy of rectangle</returns>
        public Rect Normalize()
        {
            return new Rect(
                MathHelper.Min(Left, Right),
                MathHelper.Min(Top, Bottom),
                MathHelper.Max(Left, Right),
                MathHelper.Max(Top, Bottom)
            );
        }

        public bool Contains(float pointX, float pointY)
        {
            return Contains(new Vector2(pointX, pointY));
        }

        public bool Contains(Vector2 point)
        {
            return GeometryHelper.Contains(ref this, ref point);
        }

        public bool Contains(Rect rect)
        {
            return GeometryHelper.Contains(ref this, ref rect);
        }

        public bool Intersects(Rect rect)
        {
            return GeometryHelper.Intersects(ref this, ref rect);
        }

        public bool Intersects(Vector2 start, Vector2 end)
        {
            return GeometryHelper.Intersects(ref this, ref start, ref end);
        }

        #endregion

        #region Operators

        public static bool operator ==(Rect value1, Rect value2)
        {
            return Equals(ref value1, ref value2);
        }

        public static bool operator !=(Rect value1, Rect value2)
        {
            return !Equals(ref value1, ref value2);
        }

        public static Rect operator +(Rect value1, Rect value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator +(Rect value1, Vector2 value2)
        {
            Add(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator +(Rect value1, float value2)
        {
            value1.Left += value2;
            value1.Top += value2;
            value1.Right += value2;
            value1.Bottom += value2;
            return value1;
        }

        public static Rect operator -(Rect value1, Rect value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator -(Rect value1, Vector2 value2)
        {
            Subtract(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator -(Rect value1, float value2)
        {
            value1.Left -= value2;
            value1.Top -= value2;
            value1.Right -= value2;
            value1.Bottom -= value2;
            return value1;
        }

        public static Rect operator *(Rect value1, Rect value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator *(Rect value1, Vector2 value2)
        {
            Multiply(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator *(Rect value1, float value2)
        {
            Multiply(ref value1, value2, out value1);
            return value1;
        }

        public static Rect operator *(Vector2 value1, Rect value2)
        {
            Multiply(ref value2, ref value1, out value2);
            return value2;
        }

        public static Rect operator *(float value1, Rect value2)
        {
            Multiply(ref value2, value1, out value2);
            return value2;
        }

        public static Rect operator /(Rect value1, Rect value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator /(Rect value1, Vector2 value2)
        {
            Divide(ref value1, ref value2, out value1);
            return value1;
        }

        public static Rect operator /(Rect value1, float value2)
        {
            Divide(ref value1, value2, out value1);
            return value1;
        }

        #endregion

        #region Implicit operators

        public static implicit operator Rect(Microsoft.Xna.Framework.Rectangle rect)
        {
            return new Rect(rect);
        }

        public static implicit operator Rect(RectangleF rect)
        {
            return new Rect(rect);
        }

        public static implicit operator Microsoft.Xna.Framework.Rectangle(Rect rect)
        {
            return rect.ToRectangle();
        }

        public static implicit operator RectangleF(Rect rect)
        {
            return rect.ToRectangleF();
        }

        #endregion
    }
}
