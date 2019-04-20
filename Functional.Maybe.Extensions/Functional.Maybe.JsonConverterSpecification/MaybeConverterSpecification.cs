using FluentAssertions;
using Functional.Maybe.Just;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Functional.Maybe.JsonConverterSpecification
{
    public class Tests
    {
      private const string jsonWithNullPrimitive = "{\"AnInteger\":null}";

      [TestCase(null, null)]
        [TestCase("something", "something")]
        public void ShouldSerializeAndDeserializeComplexDataStructures(string member1, string member2)
        {
            var haveMaybe = new ObjectWithMaybeReferenceType
            {
                Whatever = new ObjectWithMaybeReferenceType.SomeData
                {
                    Member1 = member1,
                    Member2 = member2
                }.ToMaybe()
            };
            var serializedObject = JsonConvert.SerializeObject(haveMaybe);

            var deserializeObject = JsonConvert.DeserializeObject<ObjectWithMaybeReferenceType>(serializedObject);
            deserializeObject.Whatever.Value.Member1.Should().Be(haveMaybe.Whatever.Value.Member1);
            deserializeObject.Whatever.Value.Member2.Should().Be(haveMaybe.Whatever.Value.Member2);
        }

        [Test]
        public void ShouldSerializeNothingOfPrimitiveValueAsNull()
        {
          var obj = new ObjectWithMaybePrimitiveType
            {
                AnInteger = Maybe<int>.Nothing
            };

            var serializedObject = JsonConvert.SerializeObject(obj);
            serializedObject.Should().Be(jsonWithNullPrimitive);
        }

        [Test]
        public void ShouldSerializeJustOfPrimitiveValue()
        {
          var obj = new ObjectWithMaybePrimitiveType
            {
                AnInteger = 2.Just()
            };

            var serializedObject = JsonConvert.SerializeObject(obj);
            serializedObject.Should().Be("{\"AnInteger\":2}");
        }

        [Test]
        public void ShouldDeserializeNullOfPrimitiveTypeAsNothing()
        {
            var deserializeObject = JsonConvert.DeserializeObject<ObjectWithMaybePrimitiveType>(jsonWithNullPrimitive);
            deserializeObject.AnInteger.HasValue.Should().BeFalse();
        }

        [Test] public void ShouldDeserializeValueOfPrimitiveTypeAsJust()
        {
            var deserializeObject = JsonConvert.DeserializeObject<ObjectWithMaybePrimitiveType>("{\"AnInteger\":2}");
            deserializeObject.AnInteger.Should().Be(2.Just());
        }
    }
}