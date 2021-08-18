using System;
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

  public static class BasicNullableExtensions
  {
    public static T OrThrow<T>(this T? instance)
    {
      return instance.OrThrow(nameof(instance));
    }

    public static T OrThrow<T>(this T? instance, string instanceName)
    {
      return instance ??
             throw new InvalidOperationException(
               $"Could not convert {instanceName} " +
               "to non-nullable reference type because it is null");
    }

    public static async Task<T> OrThrowAsync<T>(this Task<T?> instance)
    {
      return OrThrow(await instance);
    }

    public static async Task<T> OrThrowAsync<T>(this Task<T?> instance, string instanceName)
    {
      return OrThrow(await instance, instanceName);
    }

    public static TResult? SelectOrNull<T, TResult>(this T? instance, Func<T, TResult> fn) 
      where TResult : class
    {
      return instance == null ? null : fn(instance);
    }

    public static TResult SelectOrElse<T, TResult>(this T? instance, Func<T, TResult> fn, TResult defaultResult)
    {
      return instance == null ? defaultResult : fn(instance);
    }

    public static TResult SelectOrElse<T, TResult>(this T? instance, Func<T, TResult> fn, Func<TResult> defaultFn)
    {
      return instance == null ? defaultFn() : fn(instance);
    }

    public static T OrElse<T>(this T? instance, Func<T> @default)
    {
      return instance ?? @default();
    }

    public static T OrElse<T>(this T? instance, T @default)
    {
      return instance ?? @default;
    }

    public static T OrElse<T, TException>(this T? instance, Func<TException> @default) where TException : Exception
    {
      return instance ?? throw @default();
    }

    public static string ReturnToString<T>(this T? instance, string @default)
    {
      return instance != null ? instance.ToString() : @default;
    }

    public static async Task<TR?> SelectOrNullAsync<T, TR>(
      this T? @this,
      Func<T?, Task<TR?>> res) where TR : class
    {
      TR? maybe;
      if (@this != null)
        maybe = await res(@this);
      else
        maybe = null;
      return maybe;
    }

    public static async Task<T> OrElseAsync<T>(this Task<T?> instance, Func<Task<T>> orElse)
    {
      var maybe = await instance;
      T obj;
      if (maybe != null)
        obj = maybe;
      else
        obj = await orElse();
      return obj;
    }
  }
}
