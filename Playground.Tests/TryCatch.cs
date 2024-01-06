using Xunit.Abstractions;

namespace Playground.Tests;

public class TryCatch
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TryCatch(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        try
        {
            throw null;
        }
        catch (Exception e)
        {
            _testOutputHelper.WriteLine(e.ToString());
        }
    }
    [Fact]
    public void TryFinally()
    {
        var l = () =>
        {
            try
            {
                throw new MyException();
            }
            finally
            {
            }
        };

        Assert.Throws<MyException>(l);
    }
    
    [Fact]
    public void TryCatchWhen()
    {
        try
        {
            throw new MyException("aaa");
        }
        catch (MyException e) when (e.Message == "aab")
        {
            _testOutputHelper.WriteLine("aab");
        }
        catch (MyException e) when (e.Message == "aaa")
        {
            _testOutputHelper.WriteLine("aaa");
        }
        catch (MyException e)
        {
            _testOutputHelper.WriteLine("general");
        }
        catch (Exception e)
        {
            _testOutputHelper.WriteLine("Exception");
        }
        catch
        {
            _testOutputHelper.WriteLine("base");
        }
    }

    [Fact]
    public void TypedExceptionTest()
    {
        try
        {
            throw new TypedException<int>();
        }
        catch (TypedException<String> e)
        {
            _testOutputHelper.WriteLine("string");
        }
        catch (TypedException<int> e)
        {
            _testOutputHelper.WriteLine("int");
        }
    }

    class MyException : Exception
    {
        public MyException(String message = null): base(message) {}
    }
    
    class TypedException<T>: Exception {}

    private int ReturnFinally()
    {
        try
        {
            throw new Exception();
        }
        catch (Exception e)
        {
            return 1;
        }
        finally
        {
            //return 2; won't compile!
        }
    }
}