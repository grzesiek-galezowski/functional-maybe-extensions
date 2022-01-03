using System;
using FluentAssertions;
using Functional.Maybe.FluentAssertions;
using Functional.Maybe.Just;
using NUnit.Framework;

namespace Functional.Maybe.FluentAssertionsSpecification
{
  public class FunctionalMaybeAssertionsSpecification
  {
    public class ShouldBeNothingAssertionSpecification
    {
      [Test]
      public void ShouldFailWhenComparingWithJustForReferenceType()
      {
        new object().Should().NotBe(null);
        new Action(() =>
            new AccessViolationException().Just().Should().BeNothing())
          .Should().ThrowExactly<AssertionException>()
          .WithMessage("Expected a Nothing, but got a value of System.AccessViolationException with message \"Attempted to read or write protected memory. This is often an indication that other memory is corrupt.\"");
      }

      [Test]
      public void ShouldFailWhenComparingWithJustForStructType()
      {
        new Action(() =>
            new DateTime(1999,1,1).Just().Should().BeNothing())
          .Should().ThrowExactly<AssertionException>()
          .WithMessage("Expected a Nothing, but got a value of <1999-01-01>");
      }

      [Test]
      public void ShouldFailWhenComparingWithJustForPrimitiveType()
      {
        new Action(() =>
            1.Just().Should().BeNothing())
          .Should().ThrowExactly<AssertionException>()
          .WithMessage("Expected a Nothing, but got a value of 1");
      }

      [Test]
      public void ShouldPassWhenComparingWithNothing()
      {
        Maybe<int>.Nothing.Should().BeNothing();
        Maybe<string>.Nothing.Should().BeNothing();
        Maybe<DateTime>.Nothing.Should().BeNothing();
      }
    }

    public class ShouldBeJustAssertionSpecification
    {
      [Test]
      public void ShouldPassWhenComparingWithJustForReferenceType()
      { 
        var accessViolationException = new AccessViolationException();
        accessViolationException.Just().Should().BeJust(accessViolationException);
      }

      [Test]
      public void ShouldPassWhenComparingWithJustForStructType()
      {
        new DateTime(1999, 1, 1).Just().Should().BeJust(new DateTime(1999, 1, 1));
      }

      [Test]
      public void ShouldPassWhenComparingWithJustForPrimitiveType()
      {
        1.Just().Should().BeJust(1);
      }

      [Test]
      public void ShouldPassWhenComparingWithUnequalJust()
      {
        new Action(() => 1.Just().Should().BeJust(2))
          .Should().ThrowExactly<AssertionException>()
          .WithMessage("Expected object to be 2, but found 1.");
      }

      [Test]
      public void ShouldFailWhenComparingWithNothing()
      {
        new Action(() => Maybe<int>.Nothing.Should().BeJust(1))
          .Should().ThrowExactly<AssertionException>()
          .WithMessage("Expected a value of type Int32, but got Nothing.");
      }

      [Test]
      public void ShouldPassWhenAssertedThatIsSomethingAndIsIndeedSomething()
      {
        12.Just().Invoking(n => n.Should().BeSomething())
          .Should().NotThrow();
      }

      [Test]
      public void ShouldFailWhenAssertedThatIsSomethingButIsNothing()
      {
        Maybe<int>.Nothing.Invoking(n => n.Should().BeSomething())
          .Should().ThrowExactly<AssertionException>()
          .WithMessage("Expected Maybe<T> to be Something, but got Nothing.");
      }


    }
  }
}
