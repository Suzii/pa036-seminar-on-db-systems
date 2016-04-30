using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Extensions
{
    public static class LinqCustomExtensions
    {
        public static IEnumerable<double> ZippedAverage(this IEnumerable<IEnumerable<double>> listsOfValues)
        {
            return listsOfValues.LeveledReduce(list => list.Average());
        }


        public static IEnumerable<T> LeveledReduce<T>(this IEnumerable<IEnumerable<T>> sequence, Func<IEnumerable<T>, T> function)
        {
            var minLength = sequence.Min(list => list.Count());

            var result = Enumerable
               .Range(0, minLength)
               .Select(i => function(sequence.Select(list => list.ElementAt(i))))
               .ToList();

            return result;
        }
    }
}
