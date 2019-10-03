using System;
using System.Threading.Tasks;

namespace Functional.Maybe.Just
{
  public static class JustExtensions
  {
    public static Maybe<T> Just<T>(this T? value) where T : struct
    {
      if (value.HasValue)
        return value.Value.ToMaybe();
      throw new ArgumentNullException(nameof(value), "Cannot create a Just<" + typeof(T) + "> from null");
    }

    public static Maybe<T> Just<T>(this T value)
    {
      if (value != null)
      {
        return value.ToMaybe();
      }
      throw new ArgumentNullException(nameof(value), "Cannot create a Just<" + typeof(T) + "> from null");
    }
  }

  public static class AsyncExtensions
  {
      public static async Task<Maybe<T>> JustAsync<T>(this Task<T> value)
      {
          return (await value).Just();
      }

      public static async Task<Maybe<T>> JustAsync<T>(this Task<T?> value) where T : struct
      {
          return (await value).Just();
      }
      
      public static async Task<Maybe<T>> ToMaybeAsync<T>(this Task<T> value)
      {
          return (await value).ToMaybe();
      }

      public static async Task<Maybe<T>> ToMaybeAsync<T>(this Task<T?> value) where T : struct
      {
          return (await value).ToMaybe();
      }
  }
}
