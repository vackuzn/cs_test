using Xunit.Abstractions;

namespace Playground.Tests.inheritance
{
    public class Puzzle
    {
        class ArgBase
        { }
        class ArgChild : ArgBase
        { }

        class HostBase
        {
            protected ITestOutputHelper testOutputHelper;
            public HostBase(ITestOutputHelper testOutputHelper) {
                this.testOutputHelper = testOutputHelper;
            }
            public Type inheritedMethod(ArgChild _)
            {
                testOutputHelper.WriteLine($"{nameof(inheritedMethod)} from {nameof(HostBase)}");
                return typeof(HostBase);
            }
            public virtual Type methodWithOverride(ArgChild _)
            {
                testOutputHelper.WriteLine($"{nameof(methodWithOverride)}({nameof(ArgChild)}) from {nameof(HostBase)}");
                return typeof(ArgChild);
            }
            public Type hiddenMethod(ArgChild _)
            {
                testOutputHelper.WriteLine($"{nameof(hiddenMethod)}({nameof(ArgChild)}) from {nameof(HostBase)}");
                return typeof(ArgChild);
            }
        }

        class HostChild : HostBase
        {
            public HostChild(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }
            
            // test 1
            public Type inheritedMethod(ArgBase _)
            {
                testOutputHelper.WriteLine($"{nameof(inheritedMethod)} from {nameof(HostChild)}");
                return typeof(HostChild);
            }

            // test 2
            public override Type methodWithOverride(ArgChild _)
            {
                testOutputHelper.WriteLine($"{nameof(methodWithOverride)}({nameof(ArgChild)}) from {nameof(HostChild)}");
                return typeof(ArgChild);
            }
            public Type methodWithOverride(ArgBase _)
            {
                testOutputHelper.WriteLine($"{nameof(methodWithOverride)}({nameof(ArgBase)}) from {nameof(HostChild)}");
                return typeof(ArgBase);
            }

            // test 3
            public new Type hiddenMethod(ArgChild _)
            {
                testOutputHelper.WriteLine($"{nameof(hiddenMethod)}({nameof(ArgChild)}) from {nameof(HostChild)}");
                return typeof(ArgChild);
            }
            public Type hiddenMethod(ArgBase _)
            {
                testOutputHelper.WriteLine($"{nameof(hiddenMethod)}({nameof(ArgBase)}) from {nameof(HostChild)}");
                return typeof(ArgBase);
            }
        }

        private readonly ITestOutputHelper _testOutputHelper;

        public Puzzle(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void LookInCurrentClassBeforeSearchingParent()
        {
            HostChild b = new HostChild(_testOutputHelper);
            Type runBy = b.inheritedMethod(new ArgChild());

            // We would expect parent class method to be called, as it's signature is closer to the argument,
            // BUT the resolution is carried out on child class level first, checking base classes only if no suitable method found
            Assert.Equal(typeof(HostChild), runBy);
        }

        [Fact]
        public void SameShitForOverriddenMethods()
        {
            HostChild b = new HostChild(_testOutputHelper);
            var argCompileType = b.methodWithOverride(new ArgChild());
            // Even if method is overridden, so technically speaking it is located on child class level, it will only be chosen
            // if no other suitable non overridden methods found

            Assert.Equal(typeof(ArgBase), argCompileType);
        }

        [Fact]
        public void ButWorksAsExpectedForHiddenMethods()
        {
            HostChild b = new HostChild(_testOutputHelper);
            var argCompileType = b.hiddenMethod(new ArgChild());
            // As we're starting to get used to this weird behavior, another surprise is waiting for us
            // If method hiding is used, then child method has nothing to do with the parent and it will work as expected

            Assert.Equal(typeof(ArgChild), argCompileType);
        }

        // Morale: do not use it in your code, as it will confuse other developers and even you after a while
    }
}
