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

    public static async Task<T> OrThrowAsync<T>(this Task<T?> instance) where T : class
    {
      return OrThrow(await instance);
    }

    public static async Task<T> OrThrowAsync<T>(this Task<T?> instance, string instanceName) where T : class
    {
      return OrThrow(await instance, instanceName);
    }

    public static TResult? Select<T, TResult>(this T? instance, Func<T, TResult> fn) 
      where T: class 
      where TResult : class
    {
      return instance == null ? null : fn(instance);
    }

    public static T OrElse<T>(this T? instance, Func<T> @default) where T : class
    {
      return instance ?? @default();
    }

    public static T OrElse<T>(this T? instance, T @default) where T : class
    {
      return instance ?? @default;
    }

    public static T OrElse<T, TException>(this T? instance, Func<TException> @default) where T : class where TException : Exception
    {
      return instance ?? throw @default();
    }

    public static string ReturnToString<T>(this T? instance, string @default) where T : class
    {
      return instance != null ? instance.ToString() : @default;
    }

    public static async Task<TR?> SelectAsync<T, TR>(
      this T? @this,
      Func<T?, Task<TR?>> res) where T : class where TR : class
    {
      TR? maybe;
      if (@this != null)
        maybe = await res(@this!);
      else
        maybe = null;
      return maybe;
    }

    public static async Task<T> OrElseAsync<T>(this Task<T?> instance, Func<Task<T>> orElse) where T : class
    {
      var maybe = await instance;
      T obj;
      if (maybe != null)
        obj = maybe!;
      else
        obj = await orElse();
      return obj;
    }
  }
}
