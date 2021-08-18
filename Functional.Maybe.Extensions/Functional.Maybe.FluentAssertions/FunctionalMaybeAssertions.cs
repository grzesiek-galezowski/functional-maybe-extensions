using FluentAssertions;
using FluentAssertions.Common;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Functional.Maybe.FluentAssertions
{
  public class MaybeAssertions<T> : ReferenceTypeAssertions<Maybe<T>, MaybeAssertions<T>>
  {
    public MaybeAssertions(Maybe<T> maybe)
    : base(maybe)
    {
    }

    protected override string Identifier { get; } = "Maybe<" + typeof(T).Name + ">";

    public AndConstraint<MaybeAssertions<T>> BeJust(T expected)
    {
      Execute.Assertion.ForCondition(Subject.HasValue).FailWith("Expected a value of type " + typeof(T).Name + ", but got Nothing.");
      Execute.Assertion.ForCondition(
        ReferenceEquals(Subject.Value, expected) || Equals(Subject.Value, expected)
        ).FailWith("Expected {context:object} to be {0}{reason}, but found {1}.", expected, this.Subject);
      return new AndConstraint<MaybeAssertions<T>>(this);
    }

    public AndConstraint<MaybeAssertions<T>> BeSomething()
    {
      Execute.Assertion.ForCondition(Subject.HasValue).FailWith(() => new FailReason("Expected Maybe<T> to be Something, but got Nothing."));
      return new AndConstraint<MaybeAssertions<T>>(this);
    }

    public AndConstraint<MaybeAssertions<T>> BeNothing()
    {
      Execute.Assertion.ForCondition(!Subject.HasValue).FailWith(() => new FailReason("Expected a Nothing, but got a value of {0}", this.Subject.Value));
      return new AndConstraint<MaybeAssertions<T>>(this);
    }
  }

  public static class FunctionalMaybeAssertions
  {
    public static MaybeAssertions<T> Should<T>(this Maybe<T> maybe)
    {
      return new MaybeAssertions<T>(maybe);
    }
  }
}
