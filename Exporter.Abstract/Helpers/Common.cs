using System.Collections.Generic;
using System.Linq;

namespace Exporter.Abstract.Helpers
{
    public static class Common
    {
        public static List<List<T>> SplitArray<T>(List<T> src, long maxItem)
        {
            List<List<T>> res = new List<List<T>>();
            for (int i = 0; i < src.Count; i++)
            {
                if (i % maxItem == 0)
                {
                    res.Add(new List<T>());
                }
                res.Last().Add(src[i]);
            }
            return res;
        }
    }
}
