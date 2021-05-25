using System.Threading.Tasks;

namespace Functional.Maybe.Just
{
  public static class AsyncExtensions
  {
    public static async Task<Maybe<T>> JustAsync<T>(this Task<T?> value)
    {
      return (await value).Just();
    }

    public static async Task<Maybe<T>> JustAsync<T>(this Task<T?> value) where T : struct
    {
      return (await value).Just();
    }
      
    public static async Task<Maybe<T>> ToMaybeAsync<T>(this Task<T?> value)
    {
      return (await value)!.ToMaybe();
    }

    public static async Task<Maybe<T>> ToMaybeAsync<T>(this Task<T?> value) where T : struct
    {
      return (await value).ToMaybe();
    }
  }
}