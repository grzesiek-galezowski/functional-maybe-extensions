using System.Threading.Tasks;
using NullableReferenceTypesExtensions;
using NUnit.Framework;

namespace Functional.Maybe.JustSpecification
{
  class NullableReferenceTypeExtensionsSpecification
  {
    [Test]
    public async Task METHOD()
    {
      (await Task.FromResult("")).OrThrow();
      await (Task.FromResult<string?>("").OrThrowAsync());
      await Task.FromResult("").OrThrow();
    }
  }
}
