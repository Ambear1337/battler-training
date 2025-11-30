using UnityEngine;
using System;

public delegate void CurrentValueChanged(int oldValue, int newValue);
public delegate void MinValueChanged(int oldMinValue, int newMinValue);
public delegate void MaxValueChanged(int oldMaxValue, int newMaxValue);

public class ValueComponent: MonoBehaviour
{
    public event CurrentValueChanged CurrentValueChanged;
    public event MinValueChanged MinValueChanged;
    public event MaxValueChanged MaxValueChanged;

    [SerializeField] private int _minValue;
    [SerializeField] private int _currentValue;
    [SerializeField] private int _maxValue;

    public int MinValue
    {
        get => _minValue;
        set
        {
            var oldValue = _minValue;
            _minValue = value;
            MinValueChanged.Invoke(oldValue, _minValue);
        }
    }
    public int CurrentValue
    {
        get => _currentValue;
        set
        {
            var oldValue = _currentValue;
            _currentValue = Math.Clamp(value, _minValue, _maxValue);
            CurrentValueChanged.Invoke(oldValue, _currentValue);
        }
    }

    public int MaxValue
    {
        get => _maxValue;
        set
        {
            var oldValue = _maxValue;
            _maxValue = value;
            MaxValueChanged.Invoke(oldValue, _maxValue);
        }
    }

    public void Set(int value)
    {
        CurrentValue = value;
    }

    public void SetMin(int value)
    {
        _minValue = value;
    }
    
    public void SetMax(int value)
    {
        _maxValue = value;
    }

    public void Add(int value)
    {
        CurrentValue += value;
    }

    public void Sub(int value)
    {
        CurrentValue -= value;
    }

    public void AddMax(int value)
    {
        _maxValue += value;
    }

    public void SubMax(int value)
    {
        _maxValue -= value;
    }
}
