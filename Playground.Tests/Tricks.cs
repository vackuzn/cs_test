using System.Collections;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using Xunit.Abstractions;

namespace Playground.Tests;

public class Tricks
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Tricks(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        bool a, b, c;

        a = true;
        c = (b = a);

        Assert.True(c);

        if (b = a)
        {
            //...
        }
    }

    [Fact]
    public void Test2()
    {
        var s = (String)null;

        s.GetType();
    }

    [Fact]
    public void Test3()
    {
        String s1 = "Hello";
        String s2 = new StringBuilder().Append("Hel").Append("lo").ToString();

        object os1 = s1;
        object os2 = s2;

        Assert.True(s1 == s2);
        Assert.False(os1 == os2);
        Assert.False(s1 == os2);
        Assert.False(os1 == s2);
    }

    [Fact]
    public void Test4()
    {
        String s1 = "Hello";
        String s2 = String.Intern(new StringBuilder().Append("Hel").Append("lo").ToString());

        object os1 = s1;
        object os2 = s2;

        Assert.True(s1 == s2);
        Assert.True(os1 == os2);
        Assert.True(s1 == os2);
        Assert.True(os1 == s2);
    }

    class MyCollection<T> : IEnumerable<T>
    {
        private readonly List<T> _lst;

        public MyCollection(List<T> lst)
        {
            _lst = lst;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyCollectionEnumerator<T>(_lst);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyCollectionEnumerator<T>(_lst);
        }
    }

    class MyCollectionEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _e;

        public MyCollectionEnumerator(List<T> src)
        {
            _e = src.GetEnumerator();
        }

        public bool MoveNext()
        {
            return _e.MoveNext();
        }

        public void Reset()
        {
        }

        public T Current => _e.Current;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _e.Dispose();
        }

        void IDisposable.Dispose()
        {
            _e.Dispose();
        }
    }

    [Fact]
    public void Test5()
    {
        var a = new List<String>
        {
            "a", "b", "c"
        };

        foreach (var VARIABLE in a)
        {
            _testOutputHelper.WriteLine(VARIABLE);
        }
    }

    [Fact]
    public void Test6()
    {
        var l = new List<String>
        {
            "a", "b", "c"
        };
        
        var a = new MyCollection<String>(l);

        foreach (var VARIABLE in a)
        {
            _testOutputHelper.WriteLine(VARIABLE);
        }
    }
}