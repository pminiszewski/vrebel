using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Helper class for late initialization
/// </summary>
/// <typeparam name="T"></typeparam>
public class Lazy<T> where T : class, new()
{
    private Func<T> _InitFunc;
    private T _Value;
    public T Value
    {
        get
        {
            if (_Value == null)
            {
                if (_InitFunc != null)
                {
                    _Value = _InitFunc();
                }
                else
                {
                    _Value = new T();
                }

            }
            return _Value;
        }

    }
    public Lazy()
    {
        _Value = new T();
    }
    public Lazy(Func<T> initFunc)
    {
        _InitFunc = initFunc;
    }
    public static implicit operator T(Lazy<T> d)
    {
        return d.Value;
    }
}

