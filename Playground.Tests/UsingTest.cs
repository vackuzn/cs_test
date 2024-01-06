using Xunit.Abstractions;

namespace Playground.Tests;

public class UsingTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UsingTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    class A : IDisposable
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public A(ITestOutputHelper testOutputHelper, bool throwOnCreate = false)
        {
            if (throwOnCreate)
            {
                throw new Exception("failure");
            }
            _testOutputHelper = testOutputHelper;
            _testOutputHelper.WriteLine("Created");
        }

        public void Dispose()
        {
            _testOutputHelper.WriteLine("Disposed");
        }

        ~A()
        {
            _testOutputHelper.WriteLine("Finalized");
        }
    }
    
    [Fact]
    public void UsingExample()
    {
        A a = new A(_testOutputHelper);

        using (a) 
        {
            _testOutputHelper.WriteLine("using");
        }
        
        _testOutputHelper.WriteLine("done");
    }
    
    [Fact]
    public void ExceptionInUsingInitExample()
    {
        try
        {
            using (A a = new A(_testOutputHelper, true))
            {
                _testOutputHelper.WriteLine("using");
            }
        } catch (Exception e)
        {

        }

        
        _testOutputHelper.WriteLine("done");
    }
}