using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XEReversiLib.Core
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
            for (int i = 0; i < items.Count(); i++)
            {
                var leftside = new T[] { items.ElementAt(i) };
                List<T> unused = new List<T>(items);
                unused.RemoveAt(i);
                foreach (var rightside in GetPermutation(unused))
                {
                    yield return leftside.Concat(rightside).ToArray();
                }
            }
        }
    }
}
