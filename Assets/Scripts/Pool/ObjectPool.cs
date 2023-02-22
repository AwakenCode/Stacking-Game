using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


[Serializable]
public class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
{
    public const uint DefaultMaxSize = 1000;
    public const uint DefaultCapacity = 10;

    [SerializeField] private uint _maxSize = DefaultMaxSize;

    private List<T> _list;

    public ObjectPool(Func<T> createNew, Action<T> actionOnDestroyed = null,
        Action<T> actionOnGot = null,
        Action<T> actionOnReleased = null,
        uint defaultCapacity = DefaultCapacity,
        uint maxSize = DefaultMaxSize)
    {
        SetList(defaultCapacity);
        _createNew = createNew;
        _destroyed = actionOnDestroyed;
        _got = actionOnGot;
        _released = actionOnReleased;
        MaxSize = maxSize;
    }

    private readonly Func<T> _createNew;
    private readonly Action<T> _destroyed;
    private readonly Action<T> _got;
    private readonly Action<T> _released;

    public int CountActive => CountAll - _list.Count;
    public int CountInactive => _list.Count;
    public int CountAll { get; private set; }
    public uint MaxSize
    {
        get => _maxSize;
        set
        {
            if (value == 0)
                throw new ArgumentOutOfRangeException(nameof(_maxSize));

            _maxSize = value;
        }
    }

    public void Dispose()
    {
        if (_destroyed != null)
            foreach (T entity in _list)
                _destroyed(entity);

        _list.Clear();
        CountAll = 0;
    }

    public virtual T Get()
    {
        if (_list.Count == 0)
            return GetNew();

        T entity = _list[_list.Count - 1];

        _list.Remove(entity);

        _got?.Invoke(entity);

        return entity;
    }

    public void Release(T entity)
    {
        _released?.Invoke(entity);

        if (_list.Count < _maxSize)
            _list.Add(entity);
        else
            _destroyed?.Invoke(entity);
    }

    public T GetNew()
    {
        T entity = CreateNew();
        ++CountAll;
        return entity;
    }

    private T CreateNew() => _createNew();

    private void SetList(uint capacity)
    {
        Assert.IsNull(_list);
        _list = new List<T>((int)capacity);
    }
}