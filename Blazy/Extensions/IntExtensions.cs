using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazy.Extensions
{
    public static class IntExtensions
    {
        public static int Clamp(this int number, int min, int max) => Math.Max(min, Math.Min(number, max));
    }
}
