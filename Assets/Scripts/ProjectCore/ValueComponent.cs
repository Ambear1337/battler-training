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

    [SerializeField] private int minValue;
    [SerializeField] private int currentValue;
    [SerializeField] private int maxValue;

    public int MinValue => minValue;
    public int CurrentValue
    {
        get => currentValue;
        set
        {
            var oldValue = currentValue;
            currentValue = Math.Clamp(value, minValue, maxValue);
            CurrentValueChanged.Invoke(oldValue, currentValue);
        }
    }
    public int MaxValue => maxValue;

    //ПОМЕНЯТЬ ВСЕ СЕТТЕРЫ

    public void Set(int value)
    {
        CurrentValue = value;
    }

    public void SetMin(int value)
    {
        minValue = value;
    }
    
    public void SetMax(int value)
    {
        maxValue = value;
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
        maxValue += value;
    }

    public void SubMax(int value)
    {
        maxValue -= value;
    }
}
