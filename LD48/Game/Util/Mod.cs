using System;
using System.IO;

namespace LD48
{
    struct MathUtil {
        public static int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public static int Round(float x)
        {
            return (int) Math.Round(x);
        }
    }
}
