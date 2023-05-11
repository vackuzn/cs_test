using System.Runtime.InteropServices.JavaScript;
using Xunit.Abstractions;

namespace Playground.Tests;

public class BoxingUnboxing
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BoxingUnboxing(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private class MyObject
    {
    }

    private struct MyStruct
    {
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(MyStruct? obj)
        {
            return base.Equals(obj);
        }
    }
    
    private interface IChangeBoxedPoint {
       void Change(Int32 x, Int32 y);
    }

    private struct Point : IComparable<Point>, IComparable, IChangeBoxedPoint
    {
        private int _x;
        private int _y;

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }
        
        public void Change(Int32 x, Int32 y) {
            _x = x; 
            _y = y;
        }

        public int CompareTo(object? obj)
        {
            return 0;
        }

        // Implementation of type-safe CompareTo method
        public Int32 CompareTo(Point other)
        {
            // Use the Pythagorean Theorem to calculate
            // which point is farther from the origin (0, 0)
            return Math.Sign(Math.Sqrt(_x * _x + _y * _y) - Math.Sqrt(other._x * other._x + other._y * other._y));
        }

        // When uncommented, no boxing occurs
        // public Type GetType()
        // {
        //     return typeof(object);
        // }
        public override String ToString() {
            return String.Format("({0}, {1})", _x.ToString(), _y.ToString());
        }
    }

    [Fact]
    public void ObjectsCastToNull()
    {
        MyObject o = null;


        var a = (Object)o;
    }

    [Fact]
    public void StructsDontCastToNull()
    {
        MyStruct s = new MyStruct();

        Object o = s;

        o = null;

        Assert.Throws<NullReferenceException>(() =>
        {
            var r = (MyStruct)o;
        });
    }

    [Fact]
    public void UnboxAndCopyIiIl()
    {
        Int32 x = 5;
        Object o = x; // Box x; o refers to the boxed object
        Int16 y = (Int16)(Int32)o;
    }

    [Fact]
    public void BoxingTest()
    {
        MyObject oo = new MyObject();

        Int32 v = 5; // Create an unboxed value type variable.
        Object o = v; // o refers to a boxed Int32 containing 5.
        v = 123; // Changes the unboxed value to 123

        _testOutputHelper.WriteLine(v + ", " + (Int32)o + oo); // Displays "123, 5"
    }

    [Fact]
    public void BoxingTest2()
    {
        Int32 v = 5; // Create an unboxed value type variable.

        // When compiling the following line, v is boxed
        // three times, wasting time and memory.
        _testOutputHelper.WriteLine("{0}, {1}, {2}", v, v, v);

        // The lines below have the same result, execute
        // much faster, and use less memory.
        Object o = v; // Manually box v (just once).

        // No boxing occurs to compile the following line.
        _testOutputHelper.WriteLine("{0}, {1}, {2}", o, o, o);
    }

    [Fact]
    public void BoxingTest3()
    {
        // Create two Point instances on the stack.
        Point p1 = new Point(10, 10);
        Point p2 = new Point(20, 20);

        // p1 does NOT get boxed to call ToString (a virtual method).
        _testOutputHelper.WriteLine(p1.ToString()); // "(10, 10)"

        // p DOES get boxed to call GetType (a non-virtual method).
        _testOutputHelper.WriteLine(p1.GetType()); // "Point"

        // p1 does NOT get boxed to call CompareTo.
        // p2 does NOT get boxed because CompareTo(Point) is called.
        _testOutputHelper.WriteLine(p1.CompareTo(p2)); // "-1"

        // p1 DOES get boxed, and the reference is placed in c.
        IComparable c = p1;
        _testOutputHelper.WriteLine(c.GetType()); // "Point"

        // p1 does NOT get boxed to call CompareTo.
        // Because CompareTo is not being passed a Point variable,
        // CompareTo(Object) is called, which requires a reference to
        // a boxed Point.
        // c does NOT get boxed because it already refers to a boxed Point.
        _testOutputHelper.WriteLine(p1.CompareTo(c)); // "0"

        // c does NOT get boxed because it already refers to a boxed Point.
        // p2 does get boxed because CompareTo(Object) is called.
        _testOutputHelper.WriteLine(c.CompareTo(p2)); // "-1"

        IComparable<Point> d = (IComparable<Point>)c;

        // d does NOT get boxed because it already refers to a boxed Point.
        // p2 does NOT get boxed because CompareTo(Point) is called.
        _testOutputHelper.WriteLine(d.CompareTo(p2)); // "-1"

        // c is unboxed, and fields are copied into p2.
        p2 = (Point)c;

        // Proves that the fields got copied into p2.
        _testOutputHelper.WriteLine(p2.ToString()); // "(10, 10)"
    }

    [Fact]
    public void BoxingTest4()
    {
        Point p = new Point(1, 1);

        _testOutputHelper.WriteLine(p); //1,1

        p.Change(2, 2);
        _testOutputHelper.WriteLine(p); //2,2

        Object o = p;
        _testOutputHelper.WriteLine(o); //2,2

        ((Point) o).Change(3, 3);
        _testOutputHelper.WriteLine(o.ToString()); //2,2

        // Boxes p, changes the boxed object and discards it
        ((IChangeBoxedPoint) p).Change(4, 4); //2,2
        _testOutputHelper.WriteLine(p);

        // Changes the boxed object and shows it
        ((IChangeBoxedPoint) o).Change(5, 5); //5,5
        _testOutputHelper.WriteLine(o);
    }

    [Fact]
    public void OverriddenEqualsCalledBase()
    {
        var a = new MyStruct();
        a.Equals(null);
    }
}