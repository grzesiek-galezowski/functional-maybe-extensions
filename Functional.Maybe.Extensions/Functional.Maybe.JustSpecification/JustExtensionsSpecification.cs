using System;
using System.Threading.Tasks;
using Functional.Maybe.Just;
using NullableReferenceTypesExtensions;
using NUnit.Framework;

namespace Functional.Maybe.JustSpecification
{
  public class JustExtensionsSpecification
  {
    [Test]
    public async Task Test1()
    {
      string? nullString = null;
      Assert.Throws<ArgumentNullException>(() => nullString.Just());
      Assert.AreEqual("a".ToMaybe(), "a".Just());
      Assert.AreEqual("a", "a".Just().Value);
      Assert.AreEqual("a", (await Task.FromResult("a").JustAsync()).Value);
      Assert.AreEqual(1, (await Task.FromResult(1).JustAsync()).Value);
      Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(nullString).JustAsync());
    }

    [Test]
    public async Task Test2()
    {
      string? nullString = null;
      Assert.Throws<ArgumentNullException>(() => nullString.JustObject());
      Assert.AreEqual("a".ToMaybeObject(), "a".JustObject());
      Assert.AreEqual("a", "a".JustObject().Value);
      Assert.AreEqual("a", (await Task.FromResult("a").JustAsync()).Value);
      Assert.AreEqual(1, (await Task.FromResult(1).JustAsync()).Value);
      Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(nullString).JustAsync());
    }
  }
}