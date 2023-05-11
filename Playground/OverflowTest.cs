namespace Playground;

public static class OverflowTest
{
    // unchecked by default
    public static void Perform()
    {
        int a = int.MaxValue;
        Decimal d = Decimal.MaxValue;

        unchecked
        {
            a++;
        }

        // Decimal uses it's own overridden operators - throw on overflow
        unchecked
        {
            
        //d++;
        }
        
        UInt32 invalid = unchecked((UInt32) (-1));  // OK
        invalid = 0;
        invalid--;

        Console.WriteLine(a);
        Console.WriteLine(d);
        Console.WriteLine(invalid);
        
        // (un)checked doesn't propagate inside method

    }
}