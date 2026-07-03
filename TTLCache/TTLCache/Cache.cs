using System;
using System.Collections.Generic;

public class Cache<T>
{
    private readonly Dictionary<string, (T Value, DateTime AddedAt)> _storage;
    private readonly TimeSpan _ttl;

    public Cache(TimeSpan ttl)
    {
        _ttl     = ttl;
        _storage = new Dictionary<string, (T Value, DateTime AddedAt)>();
    }

    public void Set(string key, T value)
    {
        _storage[key] = (value, DateTime.Now);
    }

    public T? Get(string key)
    {
        if (!_storage.ContainsKey(key))
            return default;

        var entry = _storage[key];

        if (DateTime.Now - entry.AddedAt > _ttl)
        {
            _storage.Remove(key);
            return default;
        }

        return entry.Value;
    }

    public bool IsExpired(string key)
    {
        if (!_storage.ContainsKey(key))
            return true;

        return DateTime.Now - _storage[key].AddedAt > _ttl;
    }
}