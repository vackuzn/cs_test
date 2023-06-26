using System.Net.NetworkInformation;
using Xunit.Abstractions;

namespace Playground.Tests;

//run each test separately
public class ValueTypes
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ValueTypes(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private struct Point
    {
        private int aaa = 6; // wasn't allowed before 
        public Int32 m_x, m_y;

        static Point()
        {
            // Static constructor allowed for structs
        }

        public Point() //before C# 10 this wasn't allowed 
        {
            m_x = m_y = 5;
        }

        public Point(Int32 x)
        {
            m_x = x;
            //m_y = y; // Partial allocation allowed as well now
        }

        public Point(Int32 x, Int32 y)
        {
            this = new Point(y) // not allowed in ref type
            {
                m_x = x
            };

            //m_y = y; // Partial allocation allowed as well now
        }
    }

    class MyClass
    {
        private readonly int a, b;

        public MyClass()
        {
            //this = new MyClass(); Doesn't compile as this is readonly
        }
    }


    private sealed class Rectangle
    {
        public Point TopLeft, BottomRight;

        public Rectangle()
        {
            // In C#, new on a value type calls the constructor to
            // initialize the value type's fields.
            BottomRight = new Point();
        }
    }

    [Fact]
    public void Test1()
    {
        var r = new Rectangle();

        Assert.Equal(0, r.TopLeft.m_y); // no explicit constructor call
        Assert.Equal(5, r.BottomRight.m_y);
    }

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

    private static bool StructStaticCtrCalled;
    
    [Fact]
    public void StructStaticConstructorNotCalled()
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
    public void StructStaticConstructorCalled()
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
    // Static ctor for classes

    class ClassStaticCtor
    {
        static ClassStaticCtor()
        {
            ClassStaticCtrCalled = true;
        }
    }
    
    private static bool ClassStaticCtrCalled;
    
        
    [Fact]
    public void ClassStaticConstructorCalled()
    {
        ClassStaticCtor a = new ClassStaticCtor();
        
        Assert.True(ClassStaticCtrCalled);
    }
    
    [Fact]
    public void ClassStaticConstructorNotCalled()
    {
        ClassStaticCtor a;
        
        Assert.False(ClassStaticCtrCalled);
    }
}