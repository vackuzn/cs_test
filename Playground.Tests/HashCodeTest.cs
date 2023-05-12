using Xunit.Abstractions;

namespace Playground.Tests;

public class HashCodeTest
{
    private class HashKeyObject
    {
        private readonly bool _breakHash;
        private readonly bool _breakEquals;

        public int Value { get; }

        public HashKeyObject(int i, bool breakHash = false, bool breakEquals = false)
        {
            _breakHash = breakHash;
            _breakEquals = breakEquals;
            Value = i;
        }

        public override int GetHashCode()
        {
            return _breakHash ? new Random().Next() : Value.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (_breakEquals)
            {
                return false;
            }

            var casted = obj as HashKeyObject;
            if (casted is null)
            {
                return false;
            }

            return casted.Value == Value;
        }
    }


    [Fact]
    public void ExpectedBehavior()
    {
        var keys = BuildKeyList(10, breakHash: false, breakEquals: false);
        var dict = BuildDictionary(keys);

        foreach (var key in keys)
        {
            Assert.True(dict.ContainsKey(key));
        }
    }

    [Fact]
    public void RandomizedHash()
    {
        var keys = BuildKeyList(size: 10, breakHash: true, breakEquals: false);
        var dict = BuildDictionary(keys);

        var itemsFound = keys.Count(key => dict.ContainsKey(key));
        
        Assert.True(itemsFound == 0);
    }
    
    [Fact]
    public void BrokenEquals()
    {
        var keys = BuildKeyList(size: 10, breakHash: false, breakEquals: true);
        var dict = BuildDictionary(keys);

        var itemsFound = keys.Count(key => dict.ContainsKey(key));
        
        Assert.True(itemsFound == 0);
    }

    private List<HashKeyObject> BuildKeyList(int size, bool breakHash, bool breakEquals)
    {
        List<HashKeyObject> result = new List<HashKeyObject>();

        for (int i = 0; i < size; i++)
        {
            result.Add(new HashKeyObject(i, breakHash, breakEquals));
        }

        return result;
    }

    private Dictionary<HashKeyObject, int> BuildDictionary(List<HashKeyObject> keyList)
    {
        var res = new Dictionary<HashKeyObject, int>();

        foreach (var key in keyList)
        {
            res[key] = key.Value;
        }

        return res;
    }
}