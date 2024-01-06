using Xunit.Abstractions;

namespace Playground.Tests;

public class Inheritance
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Inheritance(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    class A
    {
        private int a = 6;
        protected readonly ITestOutputHelper Helper;

        public A(ITestOutputHelper helper)
        {
            Helper = helper;
            VirtualCall2();
        }
        
        public void DoStuff()
        {
            Helper.WriteLine("A::DoStuff");
            DoStuffInternal();
        }

        protected virtual void DoStuffInternal()
        {
            Helper.WriteLine("A::DoStuffInternal");
        }

        public virtual void VirtualCall()
        {
            Helper.WriteLine("A::VirtualCall");
        }
        
        public virtual void VirtualCall2()
        {
            Helper.WriteLine("A::VirtualCall2");
        }
    }

    class B: A
    {
        private readonly String aaa = "abc";
        private readonly String bbb;

        private B(ITestOutputHelper helper, bool garbage) : base(helper)
        {
            bbb = "fff";
        }
        // aaa initialization happens only once in the top this constructor
        // code of current ctor executes after base ctor
        public B(ITestOutputHelper helper) : this(helper, true)
        {
            bbb = "def";
        }

        protected new virtual void DoStuffInternal()
        {
            Helper.WriteLine("B::DoStuffInternal");
        }
        
        public override void VirtualCall()
        {
            Helper.WriteLine("B::VirtualCall");

        }        
        public override void VirtualCall2()
        {
            Helper.WriteLine("B::VirtualCall2");
            Helper.WriteLine(aaa + bbb);
        }
    }
    
    class C: B
    {
        public C(ITestOutputHelper helper) : base(helper) { }

        protected override void DoStuffInternal()
        {
            Helper.WriteLine("C::DoStuffInternal");
        }
        public override void VirtualCall()
        {
            Helper.WriteLine("C::VirtualCall");
            base.VirtualCall();
            // no way to call a.VirtualMethod directly
        }
    }
    
    [Fact]
    public void Test1()
    {
        B a = new B(_testOutputHelper);
        
        a.DoStuff();
    }
    
    [Fact]
    public void BaseCall()
    {
        A c = new C(_testOutputHelper);
        
        c.VirtualCall();
    }
    
    [Fact]
    public void CallVirtualCallFromCtr()
    {
        A b = new B(_testOutputHelper);
        
        b.VirtualCall2();
    }
}