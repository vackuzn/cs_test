using Xunit.Abstractions;

namespace Playground.Tests.value_types
{
    public class StaticConstructorNotCalled
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

        public StaticConstructorNotCalled(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void StructStaticConstructorNotCalled1()
        {
            StructStaticConstructor[] a = new StructStaticConstructor[10];
            a[0].m_x = 123;
            _testOutputHelper.WriteLine(a[0].m_x.ToString());   // Displays 123

            Assert.False(StructStaticCtrCalled);
        }

        [Fact]
        public void StructStaticConstructorNotCalled2()
        {
            StructStaticConstructor a;

            Assert.False(StructStaticCtrCalled);
        }

        [Fact]
        public void StructStaticConstructorNotCalled3()
        {
            StructStaticConstructor a = new StructStaticConstructor
            {
                m_x = 5
            };

            Assert.False(StructStaticCtrCalled);
        }

        [Fact]
        public void StructStaticConstructorNotCalled4()
        {
            StructStaticConstructor a = new StructStaticConstructor();
            a.ToString();

            Assert.False(StructStaticCtrCalled);
        }

        [Fact]
        public void ClassStaticConstructorNotCalled()
        {
            ClassStaticCtor a;

            Assert.False(ClassStaticCtrCalled);
        }
    }
}
