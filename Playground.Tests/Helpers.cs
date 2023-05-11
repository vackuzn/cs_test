using Xunit.Abstractions;

namespace Playground.Tests;

public static class Helpers
{
    public static void WriteLine(this ITestOutputHelper h, Object o)
    {
        h.WriteLine(o.ToString());
    }
}