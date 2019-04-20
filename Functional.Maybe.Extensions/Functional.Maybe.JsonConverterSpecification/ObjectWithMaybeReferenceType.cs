using Functional.Maybe.JsonConverter;
using Newtonsoft.Json;

namespace Functional.Maybe.JsonConverterSpecification
{
  public class ObjectWithMaybeReferenceType
  {
    [JsonConverter(typeof(MaybeConverter<SomeData>))]
    public Maybe<SomeData> Whatever { get; set; }

    public class SomeData //bug maybe a value object?
    {
      public string Member1 { get; set; }
      public string Member2 { get; set; } //bug use it
    }

  }
}