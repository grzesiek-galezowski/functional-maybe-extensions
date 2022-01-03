using System.Threading.Tasks;
using Functional.Maybe;

namespace NullableReferenceTypesExtensions
{
  public static class MaybeNullableExtensions
  {
    public static Maybe<T> ToMaybeObject<T>(this T? obj)
    {
      return obj!.ToMaybe();
    }

    public static async Task<Maybe<T>> ToMaybeObjectAsync<T>(this Task<T?> value)
    {
      return (await value).ToMaybeObject();
    }

    public static T? OrELseNull<T>(this Maybe<T> obj) where T : class
    {
      return obj.OrElseDefault();
    }
  }
}
