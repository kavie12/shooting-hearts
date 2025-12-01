using UnityEngine;

[System.Serializable]
// Represents a range of float values with minimum and maximum bounds
public class FloatRange
{
    public float Min;
    public float Max;

    public FloatRange(float min, float max)
    {
        this.Min = min;
        this.Max = max;
    }

    public float RandomValue()
    {
        return Random.Range(Min, Max);
    }
}
