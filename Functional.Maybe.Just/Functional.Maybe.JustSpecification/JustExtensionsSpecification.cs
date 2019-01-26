using System;
using Functional.Maybe.Just;
using NUnit.Framework;

namespace Functional.Maybe.JustSpecification
{
  public class JustExtensionsSpecification
  {
    [Test]
    public void Test1()
    {
      string s = null;
      Assert.Throws<ArgumentNullException>(() => s.Just());
      Assert.AreEqual("a".ToMaybe(), "a".Just());
      Assert.AreEqual("a", "a".Just().Value);
    }
  }
}