using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Comparators
{
	public class DictionaryComparer<TKey, TValue> : IEqualityComparer<Dictionary<TKey, TValue>>
	{
		private readonly IEqualityComparer<TValue> valueComparer;
		
		public DictionaryComparer(IEqualityComparer<TValue> valueComparer = null)
		{
			this.valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
		}

		public bool Equals(Dictionary<TKey, TValue> x, Dictionary<TKey, TValue> y)
		{
			if (x.Count != y.Count)
				return false;

			if (x.Keys.Except(y.Keys).Any())
				return false;

			if (y.Keys.Except(x.Keys).Any())
				return false;
			
			return x.All(pair => valueComparer.Equals(pair.Value, y[pair.Key]));
		}

		public int GetHashCode(Dictionary<TKey, TValue> obj)
		{
			throw new NotImplementedException();
		}
	}
}
