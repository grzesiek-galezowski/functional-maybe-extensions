using Functional.Maybe.JsonConverter;
using Newtonsoft.Json;

namespace Functional.Maybe.JsonConverterSpecification
{
  public class ObjectWithMaybePrimitiveType
  {
    [JsonConverter(typeof(MaybeConverter<int>))]
    public Maybe<int> AnInteger { get; set; }
  }
}