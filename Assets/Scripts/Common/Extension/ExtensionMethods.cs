using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ExtensionMethods
{
    public static void DoesNothingMethod(this Transform _transform)
    {
        _transform.position = Vector3.zero;
    }

    public static int SortDictByScore(Dictionary<string, long> a, Dictionary<string, long> b)
    {
        return a[NetworkManager.path_Score].CompareTo(b[NetworkManager.path_Score]);
    }

    public static List<IDictionary> SortListIDictionary(this List<IDictionary> list)
    {
        IOrderedEnumerable<IDictionary> orderedEnumerable =
            list.OrderByDescending(score => score[NetworkManager.path_Score]);

        return orderedEnumerable.ToList()   ;
    }
}
