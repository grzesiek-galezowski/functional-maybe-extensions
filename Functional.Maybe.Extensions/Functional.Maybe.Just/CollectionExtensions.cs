using System.Collections.Generic;

namespace Functional.Maybe.Just
{
  public static class CollectionExtensions
  {
    public static Maybe<TValue> MaybeValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary,
      TKey key)
    {
      var result = dictionary.TryGetValue(key, out var value);
      return result ? value.Just() : Maybe<TValue>.Nothing;
    }

    public static Maybe<TValue> MaybeValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
      TKey key)
    {
      var result = dictionary.TryGetValue(key, out var value);
      return result ? value.Just() : Maybe<TValue>.Nothing;
    }
  }
}