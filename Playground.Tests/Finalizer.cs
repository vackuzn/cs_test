using System.Runtime.CompilerServices;

namespace Playground.Tests;

public class Finalizer
{
    class Window
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Window(Model model)
        {
            Console.WriteLine("ctor");
        }

        ~Window()
        {
            Console.WriteLine("fin");
        }
    }

    class Model
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Model build()
        {
            throw new Exception();
        }
    }
    
    [Fact]
    public void Test1()
    {
        try
        {
            var content = Model.build();
            var window = new Window(content);
        }
        catch
        {
        }
    }
}