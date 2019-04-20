using System;
using Newtonsoft.Json;

namespace Functional.Maybe.JsonConverter
{
  public class MaybeConverter<T> : JsonConverter<Maybe<T>>
  {
    public override void WriteJson(JsonWriter writer, Maybe<T> value, JsonSerializer serializer)
    {
        if (value.HasValue)
        {
            serializer.Serialize(writer, value.Value);
        }
        else
        {
            serializer.Serialize(writer, null);
        }
    }

    public override Maybe<T> ReadJson(JsonReader reader, Type objectType, Maybe<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null)
      {
        return Maybe<T>.Nothing;
      }

      var value = serializer.Deserialize<T>(reader);
      return value.ToMaybe();
    }
  }}
