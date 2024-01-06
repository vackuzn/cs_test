using System.Runtime.CompilerServices;

namespace Playground.Tests;

public class ClosureTests
{
    delegate int SomeMethod();

    [Fact]
    public void VariableRefCaptured()
    {
        List<SomeMethod> delList = new List<SomeMethod>();
        for (int i = 0; i < 10; i++)
        {
            delList.Add(delegate { Console.WriteLine(i); return i; });
        }
 
        foreach (var del in delList)
        {
            int value = del();
            Assert.Equal(10, value);
        }
    }

    [Fact]
    public void VariableCopied()
    {
        List<SomeMethod> delList = new List<SomeMethod>();
        for (int i = 0; i < 10; i++)
        {
            int iCopy = i;
            delList.Add(delegate { Console.WriteLine(iCopy); return iCopy; });
        }

        for (int idx = 0; idx < delList.Count(); idx++)
        {
            int value = delList[idx]();
            Assert.Equal(idx, value);
        }
    }
}
