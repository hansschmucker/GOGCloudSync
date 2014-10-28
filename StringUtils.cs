using System.Collections.Generic;
namespace GOGCloud
{

    public class StringUtils
    {
        /**
         * Returns a normalized a path by removing a trailing slash if there is one.
         */
        public static string stripTrailingBackslash(string path)
        {
            if (path.Length == 0 || path[path.Length - 1] != '\\')
                return path;
            else
                return path.Substring(0, path.Length - 1);
        }

        /*
         * Returns a new list containing all of a, followed by any item of b that is not already part of a.
         */
        public static List<string> mergeList(List<string> a, List<string> b)
        {
            var r = new List<string>(a);

            for (var i = 0; i < a.Count; i++)
            {
                if (!r.Contains(a[i]))
                    r.Add(a[i]);
            }
            return r;
        }
    }
}