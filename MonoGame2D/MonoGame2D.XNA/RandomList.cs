using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame2D.Utils
{
    /// <summary>
    /// Random List 
    /// </summary>
    public class RandomList
    {
        private static readonly Random _rand = new Random();

        /// <summary>
        /// Get randomized list
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int[] GetList(int count)
        {
            int[] list = new int[count];
            for (int i = 0; i < count; i++)
            {
                list[i] = i;
                // swap the item with any previous item, or itself
                int swap = _rand.Next(i + 1);
                if (swap != i)
                {
                    int temp = list[i];
                    list[i] = list[swap];
                    list[swap] = temp;
                }
            }
            return list;
        }

        public static int[] GetContinueNoiseIntList(int Count)
        {
            int[] list = new int[Count];
            list[0] = 0;
            for (int i = 1; i < Count; i++)
            {
                if (_rand.NextDouble() > 0.5)
                    list[i] = list[i - 1] + 1;
                else
                    list[i] = list[i - 1] - 1;
            }
            return list;
        }

        public static int[] GetRandomList(int Count, int range)
        {
            int[] list = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                list[i] = _rand.Next(range * 2) - range;

            }
            return list;
        }
    }
}
