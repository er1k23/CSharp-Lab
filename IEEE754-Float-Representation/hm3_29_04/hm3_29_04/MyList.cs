using System;

class MyList
{
    private int[] _items;
    private int _count;

    public MyList()
    {
        _items = new int[4]; // начальный размер
        _count = 0;
    }

    public int Count
    {
        get { return _count; }
    }

    public void Add(int item)
    {
        if (_count == _items.Length)
        {
            Resize();
        }

        _items[_count] = item;
        _count++;
    }

    public void AddRange(int[] items)
    {
        if (items == null)
            return;

        for (int i = 0; i < items.Length; i++)
        {
            Add(items[i]);
        }
    }

    public bool Remove(int item)
    {
        int index = IndexOf(item);

        if (index == -1)
            return false;

        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }

        _count--;
        return true;
    }

    public bool TryGet(int index, out int value)
    {
        if (index < 0 || index >= _count)
        {
            value = 0;
            return false;
        }

        value = _items[index];
        return true;
    }

    // ---- дополнительные методы ----

    public int IndexOf(int item)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_items[i] == item)
                return i;
        }

        return -1;
    }

    public bool Contains(int item)
    {
        return IndexOf(item) != -1;
    }

    public void Clear()
    {
        _count = 0;
    }

    public int this[int index]
    {
        get
        {
            return _items[index];
        }
        set
        {
            _items[index] = value;
        }
    }

    private void Resize()
    {
        int newSize = _items.Length * 2;
        int[] newArr = new int[newSize];

        for (int i = 0; i < _items.Length; i++)
        {
            newArr[i] = _items[i];
        }

        _items = newArr;
    }
}