namespace DataStructures;

public class HashMap<TValue>
{
    private readonly Bucket?[] _buckets;
    private readonly int _capacity;

    public HashMap(int capacity = 10)
    {
        _capacity = capacity;
        _buckets = new Bucket[capacity];
    }

    public void Set(string key, TValue value)
    {
        var index = Hash(key);
        GetOrCreateBucket(index).Set(key, value);
    }

    public TValue Get(string key)
    {
        var index = Hash(key);
        return GetOrCreateBucket(index).Get(key);
    }
    
    private Bucket GetOrCreateBucket(int index) => _buckets[index] ??= new Bucket();

    private int Hash(string key) => 
        Math.Abs(key.GetHashCode()) % _capacity;

    private sealed class Bucket
    {
        private readonly LinkedList<(string key, TValue value)> _list;

        public Bucket() =>
            _list = new LinkedList<(string key, TValue value)>();

        public void Set(string key, TValue value) =>
            _list.AddLast((key, value));

        public TValue Get(string key)
        {
            var keyValue = _list.FirstOrDefault(tuple => tuple.key == key);

            if (keyValue.key == null)
                throw new KeyNotFoundException(key);
            
            return keyValue.value;
        }
    }
}