using System.Collections.Generic;
using System.Linq;

namespace dev.sthorne.AdventOfCode.Utils
{
	public static class IEnumerableExtensions
	{
		public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
				yield break;

			var list = enumerable.ToList();
			if (!list.Any())
				yield return Enumerable.Empty<T>();
			
			var startIndex = 0;
			foreach (var startElem in list)
			{
				var index = startIndex;
				var rest = list.Where((e, i) => i != index);

				foreach (var restPermutations in rest.Permute())
				{
					yield return restPermutations.Prepend(startElem);
				}

				startIndex++;
			}
		}
	}
}
