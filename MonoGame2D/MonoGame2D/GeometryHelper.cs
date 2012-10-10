using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame2D
{
    /// <summary>
    /// Contains helper methods for geometry processing algorithms
    /// </summary>
    public static class GeometryHelper
    {

        /// <summary>
        /// Determines whether the specified rect contains specified point.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="point">The point.</param>
        /// <returns>
        /// 	<c>true</c> if rect contains point; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(ref Rect rect, ref Vector2 point)
        {
            return rect.Left <= point.X && rect.Top <= point.Y && rect.Right > point.X && rect.Bottom > point.Y;
        }

        /// <summary>
        /// Determines whether the specified rect contains other rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="other">The other rect.</param>
        /// <returns>
        /// 	<c>true</c> if the specified rect contains other rect; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(ref Rect rect, ref Rect other)
        {
            return
                rect.Left <= other.Left &&
                rect.Top <= other.Top &&
                rect.Right >= other.Right &&
                rect.Bottom >= other.Bottom;
        }


        /// <summary>
        /// Determines whether the specified rect intersects with another rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="point">The another rect.</param>
        /// <returns>
        /// 	<c>true</c> if rect intersects with another rect; otherwise, <c>false</c>.
        /// </returns>
        public static bool Intersects(ref Rect rect, ref Rect other)
        {
            return rect.Left <= other.Right &&
                   rect.Top <= other.Bottom &&
                   rect.Right >= other.Left &&
                   rect.Bottom >= other.Top;
        }

        public static bool Intersects(ref Rect rect, ref Vector2 start, ref Vector2 end)
        {
            throw new NotImplementedException();
        }

        #region Line Checkings

        /// <summary>
        /// Check point hits polyline with specified accuracy
        /// </summary>
        /// <param name="point">Checked point</param>
        /// <param name="vertices">Polyline vertex set</param>
        /// <param name="accuracy">Accuracy threshold</param>
        /// <returns>
        /// Returns true if point hists line otherwise - false
        /// </returns>
        public static bool PointInLine(Vector2 point, Vector2[] vertices, float accuracy)
        {
            //if (point == null || (vertices == null || vertices.Length < 2))
             //   return false;

            for (int i = 0; i < vertices.Length - 1; i++)
            {
                Rect pointRect = new Rect();
                pointRect.Center = point;
                pointRect.Width = accuracy;
                pointRect.Height = accuracy;

                if (LineIntersectRect(vertices, pointRect))
                {
                    return true;
                }

            }

            return false;
        }

        /// <summary>
        /// Check intersection of polyline with rectangular area
        /// </summary>
        /// <param name="vertices">Polyline vertex set</param>
        /// <param name="rect">Rectangular area</param>
        /// <returns>
        /// Returns true if polyline intersects rectangular area, otherwise - false
        /// </returns>
        public static bool LineIntersectRect(Vector2[] vertices, Rect rect)
        {
            if ((vertices == null || vertices.Length < 2) || rect == Rect.Empty)
                return false;
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                if (CheckRectLine(vertices[i], vertices[i + 1], rect))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks intersection of line with rectangular area
        /// </summary>
        /// <param name="start">Line start point</param>
        /// <param name="end">Line end point</param>
        /// <param name="rect">Rectangular area</param>
        /// <returns>
        /// Returns true if line intersects with rectangle otherwise - false
        /// </returns>
        private static bool CheckRectLine(Vector2 start, Vector2 end, Rect rect)
        {
            bool result = false;
            if (rect.Contains(start) || rect.Contains(end))
                result = true;
            else
            {
                result |= CheckRectLineH(start, end, rect.LeftTop.Y, rect.LeftTop.X, rect.RightBottom.X);
                result |= CheckRectLineH(start, end, rect.RightBottom.Y, rect.LeftTop.X, rect.RightBottom.X);
                result |= CheckRectLineV(start, end, rect.LeftTop.X, rect.LeftTop.Y, rect.RightBottom.Y);
                result |= CheckRectLineV(start, end, rect.RightBottom.X, rect.LeftTop.Y, rect.RightBottom.Y);
            }
            return result;
        }

        private static bool CheckRectLineH(Vector2 start, Vector2 end, float y0, float x1, float x2)
        {
            if ((y0 < start.Y) && (y0 < end.Y))
                return false;
            if ((y0 > start.Y) && (y0 > end.Y))
                return false;
            if (start.Y == end.Y)
            {
                if (y0 == start.Y)
                {
                    if ((start.X < x1) && (end.X < x1))
                        return false;
                    if ((start.X > x2) && (end.X > x2))
                        return false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            float x = (end.X - start.X) * (y0 - start.Y) / (end.Y - start.Y) + start.X;
            return ((x >= x1) && (x <= x2));
        }

        private static bool CheckRectLineV(Vector2 start, Vector2 end, float x0, float y1, float y2)
        {
            if ((x0 < start.X) && (x0 < end.X))
                return false;
            if ((x0 > start.X) && (x0 > end.X))
                return false;
            if (start.X == end.X)
            {
                if (x0 == start.X)
                {
                    if ((start.Y < y1) && (end.Y < y1))
                        return false;
                    if ((start.Y > y2) && (end.Y > y2))
                        return false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            float y = (end.Y - start.Y) * (x0 - start.X) / (end.X - start.X) + start.Y;
            return ((y >= y1) && (y <= y2));
        }

        #endregion
    }
}
