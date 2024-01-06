using Xunit.Abstractions;

namespace Playground.Tests.value_types
{
    public class StaticConstructorCalled
    {
        private struct StructStaticConstructor
        {
            static StructStaticConstructor()
            {
                StructStaticCtrCalled = true;
            }

            public StructStaticConstructor(int a)
            {
                m_x = a;
            }

            public void DoSmth()
            {

            }

            public static int a;

            public Int32 m_x;
        }

        private class ClassStaticCtor
        {
            static ClassStaticCtor()
            {
                ClassStaticCtrCalled = true;
            }
        }

        private static bool StructStaticCtrCalled;
        private static bool ClassStaticCtrCalled;

        private readonly ITestOutputHelper _testOutputHelper;

        public StaticConstructorCalled(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void StructStaticConstructorCalled1()
        {
            var a = StructStaticConstructor.a;

            Assert.True(StructStaticCtrCalled);
        }

        [Fact]
        public void StructStaticConstructorCalled2()
        {
            var a = new StructStaticConstructor(1);

            Assert.True(StructStaticCtrCalled);
        }

        [Fact]
        public void StructStaticConstructorCalled3()
        {
            var a = new StructStaticConstructor();
            a.DoSmth();

            Assert.True(StructStaticCtrCalled);
        }

        [Fact]
        public void StructStaticConstructorCalled4()
        {
            var a = new StructStaticConstructor();

            Assert.True(StructStaticCtrCalled);
        }
        // Static ctor for classes

        [Fact]
        public void ClassStaticConstructorCalled()
        {
            ClassStaticCtor a = new ClassStaticCtor();

            Assert.True(ClassStaticCtrCalled);
        }
    }
}
