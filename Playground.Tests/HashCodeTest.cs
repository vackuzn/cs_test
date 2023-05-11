namespace Playground.Tests;

public class HashCodeTest
{
    private class MutableHash
    {
        private int _lastHashCode = 0;
        public int IValue { get; set; }
        
        public MutableHash(int i)
        {
            IValue = i;
        }

        public override int GetHashCode()
        {
            return _lastHashCode++;
        }

        public override bool Equals(object? obj)
        {
            return false;
        }
    }
    
    
    
    [Fact]
    public void Test1()
    {
        var item = new MutableHash(0);

        Dictionary<MutableHash, int> d = new Dictionary<MutableHash, int>()
        {
            [item] = item.IValue
        };

        Assert.False(d.ContainsKey(item));
    }
}