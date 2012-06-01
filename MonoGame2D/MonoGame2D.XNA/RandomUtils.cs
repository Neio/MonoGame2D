using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Source Code modified from Vortex2D.NET
namespace MonoGame2D.Utils
{
    ///<summary>Provides better global interface for getting Random values</summary>
    public static class RandomUtils
    {
        private static Random _random = new Random();

        /// <summary>
        /// Shuffles random.
        /// </summary>
        public static void Shuffle()
        {
            _random = new Random((int)DateTime.Now.Ticks);
        }

        /// <summary>
        /// Return next int from range 0..maxValue
        /// </summary>
        /// <param name="maxValue">The maximal value of range.</param>
        /// <returns>Returns next random from range</returns>
        public static int NextInt(int maxValue)
        {
            return _random.Next(maxValue);
        }

        /// <summary>
        /// Return next int from range minValue to maxValue
        /// </summary>
        /// <param name="minValue">The min value of range.</param>
        /// <param name="maxValue">The max value of range.</param>
        /// <returns>Returns next random from range</returns>
        public static int NextInt(int minValue, int maxValue)
        {
            return minValue + NextInt(maxValue - minValue);
        }

        /// <summary>
        /// Return random float from range 0..1
        /// </summary>
        /// <returns>Returns next float from range</returns>
        public static float NextFloat()
        {
            return (float)_random.NextDouble();
        }

        /// <summary>
        /// Return random float from range 0..maxValue
        /// </summary>
        /// <param name="maxValue">The max value of range.</param>
        /// <returns>Returns next float from range</returns>
        public static float NextFloat(float maxValue)
        {
            return maxValue * NextFloat();
        }

        /// <summary>
        /// Return random float from range 0..maxValue
        /// </summary>
        /// <param name="minValue">The min value of range.</param>
        /// <param name="maxValue">The max value of range.</param>
        /// <returns>Returns next float from range</returns>
        public static float NextFloat(float minValue, float maxValue)
        {
            return minValue + NextFloat(maxValue - minValue);
        }

        /// <summary>
        /// Return random double from range 0..1
        /// </summary>
        /// <returns>Returns next double from range</returns>
        public static float NextDouble()
        {
            return (float)_random.NextDouble();
        }

        /// <summary>
        /// Return random double from range 0..maxValue
        /// </summary>
        /// <param name="maxValue">The max value of range.</param>
        /// <returns>Returns next double from range</returns>
        public static float NextDouble(float maxValue)
        {
            return maxValue * NextDouble();
        }

        /// <summary>
        /// Return random double from range 0..maxValue
        /// </summary>
        /// <param name="minValue">The min value of range.</param>
        /// <param name="maxValue">The max value of range.</param>
        /// <returns>Returns next double from range</returns>
        public static float NextDouble(float minValue, float maxValue)
        {
            return minValue + NextDouble(maxValue - minValue);
        }
    }
}
