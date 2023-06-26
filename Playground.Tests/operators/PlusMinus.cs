namespace Playground.Tests.operators;

public class PlusMinus
{
    class Complex
    {
        public readonly int _r;
        public readonly int _i;

        public Complex(int r, int i)
        {
            _r = r;
            _i = i;
        }

        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a._r + b._r, a._i + b._i);
        }
        public static Complex operator +(Complex a, object b)
        {
            return new Complex(-1, -1);// broken on purpose
        }        
        public static Complex operator +(object a, Complex b)
        {
            return new Complex(-2, -2);// broken on purpose
        }

        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a._r - b._r, a._i - b._i);
        }
        public static Complex operator -(Complex a, object b)
        {
            return new Complex(-5, -5);// broken on purpose
        }

        public static bool operator ==(Complex? a, object? b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            var bb = b as Complex;
            
            if (a == null || bb == null)
            {
                return false;
            }

            return a._r == bb._r && a._i == bb._i;
        }

        public static bool operator !=(Complex a, object b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            var c = obj as Complex;

            if (c is null)
            {
                return false;
            }

            return c._i == _i && c._r == _r;
        }
    }

    class ComplexChild : Complex
    {
        public ComplexChild(int r, int i) : base(r, i)
        {
        }
        
        public static Complex operator +(ComplexChild a, Complex b)
        {
            return new Complex(100, 100);
        }
    }
    
    [Fact]
    public void Plus1()
    {
        Complex a = new Complex(1, 1);
        Complex b = new Complex(2, 2);

        Complex c = a + b;
        Assert.Equal(new Complex(3,3), c);
    }    
    
    [Fact]
    public void Plus2()
    {
        Complex a = new Complex(1, 1);
        object b = new Complex(2, 2);

        Complex c = a + b;
        Assert.Equal(new Complex(-1,-1), c);
    }    
    
    [Fact]
    public void Plus3()
    {
        object a = new Complex(1, 1);
        Complex b = new Complex(2, 2);

        Complex c = a + b;
        Assert.Equal(new Complex(-2,-2), c);
    }    

    // child class
    [Fact]
    public void Plus4()
    {
        ComplexChild a = new ComplexChild(1, 1);
        ComplexChild b = new ComplexChild(2, 2);

        Complex c = a + b;
        Assert.Equal(new Complex(100,100), c);
    }    
    
    [Fact]
    public void Plus5()
    {
        ComplexChild a = new ComplexChild(1, 1);
        ComplexChild b = new ComplexChild(2, 2);

        Complex c = a + b;
        Assert.Equal(new Complex(100,100), c);
    }        
    
    [Fact]
    public void Plus6()
    {
        Complex a = new ComplexChild(1, 1);
        ComplexChild b = new ComplexChild(2, 2);

        Complex c = a + b;
        Assert.Equal(new Complex(3,3), c);
    }    
    
    
    // Minus
    [Fact]
    public void Minus1()
    {
        Complex a = new Complex(3, 3);
        Complex b = new Complex(2, 2);

        Complex c = a - b;
        Assert.Equal(new Complex(1,1), c);
    }    
    
    [Fact]
    public void Minus2()
    {
        Complex a = new Complex(1, 1);
        object b = new Complex(2, 2);

        Complex c = a - b;
        Assert.Equal(new Complex(-5,-5), c);

        //c = b - a doesn't compile as appropriate op not found
    }    
}