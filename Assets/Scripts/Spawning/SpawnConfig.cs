using UnityEngine;

[System.Serializable]
public class SpawnConfig
{
    public GameObject prefab;
    public FloatRange spawnIntervalRange = new FloatRange(5f, 10f);

    private float spawnInterval = 0f;
    private float timer = 0f;

    public float getSpawnInterval()
    {
        if (spawnInterval == 0f)
        {
            renewSpawnInterval();
        }
        return spawnInterval;
    }

    public void renewSpawnInterval()
    {
        spawnInterval = spawnIntervalRange.RandomValue();
    }

    public float getTimer()
    {
        return timer;
    }

    public void countTimer()
    {
        timer += Time.deltaTime;
    }

    public void resetTimer()
    {
        timer = 0f;
    }
}
