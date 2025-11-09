using UnityEngine;

[System.Serializable]
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
