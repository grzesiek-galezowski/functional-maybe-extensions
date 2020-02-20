using System;
using System.Threading.Tasks;
using Functional.Maybe;
using Functional.Maybe.Just;

namespace NullableReferenceTypesExtensions
{
  public static class MaybeNullableExtensions
  {
    public static Maybe<T> ToMaybeObject<T>(this T? obj) where T : class
    {
      return obj!.ToMaybe();
    }

    public static Maybe<T> JustObject<T>(this T? obj) where T : class
    {
      return obj!.Just();
    }

    public static async Task<Maybe<T>> JustObjectAsync<T>(this Task<T?> value) where T : class
    {
      return (await value).JustObject();
    }

    public static async Task<Maybe<T>> ToMaybeObjectAsync<T>(this Task<T?> value) where T : class
    {
      return (await value).ToMaybeObject();
    }

    public static T? OrELseNull<T>(this Maybe<T> obj) where T : class
    {
      return obj.OrElseDefault();
    }
  }

  public static class BasicNullableExtensions
  {
    public static T OrThrow<T>(this T? instance) where T : class
    {
      return instance.OrThrow(nameof(instance));
    }

    public static T OrThrow<T>(this T? instance, string instanceName) where T : class
    {
      return instance ??
             throw new InvalidOperationException(
               $"Could not convert {instanceName} " +
               "to non-nullable reference type because it is null");
    }

    public static TResult? Select<T, TResult>(this T? a, Func<T, TResult> fn) 
      where T: class 
      where TResult : class
    {
      return a == null ? null : fn(a);
    }

    public static T OrElse<T>(this T? a, Func<T> @default) where T : class
    {
      return a ?? @default();
    }
  }
}
