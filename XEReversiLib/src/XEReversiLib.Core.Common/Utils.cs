using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XEReversiLib.Core.Common
{
    class Utils
    {
        public static IEnumerable<T[]> GetPermutation<T>(IEnumerable<T> items)
        {
            if (items.Count() == 1)
            {
                yield return new T[] { items.First() };
                yield break;
            }
            foreach (var item in items)
            {
                var leftside = new T[] { item };
                var unused = items.Except(leftside);
                foreach (var rightside in GetPermutation(unused))
                {
                    yield return leftside.Concat(rightside).ToArray();
                }
            }
        }
    }
}
