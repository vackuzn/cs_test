using Xunit.Abstractions;

namespace Playground.Tests;

public class ValueTypes
{
    private struct Point
    {
        private int aaa = 6; // wasn't allowed in earlier C# versions
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

    private sealed class Rectangle
    {
        public Point TopLeft, BottomRight;

        public Rectangle()
        {
            // In C#, new on a value type calls the constructor to
            // initialize the value type's fields.

            // TopLeft = new Point(); if commented - will have default value - Point(0, 0), not null
            BottomRight = new Point();
        }
    }

    [Fact]
    public void StructBehavior()
    {
        var r = new Rectangle();

        Assert.Equal(0, r.TopLeft.m_y); // no explicit constructor call, will default to Point(0,0)
        Assert.Equal(5, r.BottomRight.m_y);
    }
}