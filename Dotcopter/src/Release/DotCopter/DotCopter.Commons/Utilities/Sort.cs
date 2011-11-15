using System;
using Microsoft.SPOT;

namespace DotCopter.Commons.Utilities
{
    public static class Sort
    {
        public static void BubbleSort(ref float[] data)
        {
            for (var i = 0; i < data.Length - 1; i++)
            {
                for (var j = data.Length - 1; j > i; j--)
                {
                    if (data[j] >= data[j - 1]) continue;
                    var temp = data[j];
                    data[j] = data[j - 1];
                    data[j - 1] = temp;
                }
            }
        }
    }
}
