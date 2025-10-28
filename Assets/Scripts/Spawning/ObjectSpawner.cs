using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private FloatRange spawnRangeX = new FloatRange(-8.5f, 8.5f);
    [SerializeField] private FloatRange spawnRangeY = new FloatRange(6f, 10f);

    [SerializeField] private SpawnConfig[] spawnConfigs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < spawnConfigs.Length; i++)
        {
            spawnObject(spawnConfigs[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spawnConfigs.Length; i++)
        {
            spawnObject(spawnConfigs[i]);
        }
    }

    void spawnObject(SpawnConfig sc)
    {
        if (sc.getTimer() < sc.getSpawnInterval())
        {
            sc.countTimer();
        }
        else
        {
            GameObject obj = ObjectPool.instance.getPooledObject(sc.getObjectType());

            if (obj != null)
            {
                obj.transform.position = new Vector3(spawnRangeX.RandomValue(), spawnRangeY.RandomValue(), transform.position.z);
                obj.SetActive(true);
            }

            sc.resetTimer();
            sc.renewSpawnInterval();
        }
    }
}
