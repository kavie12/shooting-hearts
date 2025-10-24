using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public SpawnConfig heartSpawnConfig;
    public SpawnConfig carrotSpawnConfig;
    public FloatRange spawnRangeX = new FloatRange(-8.5f, 8.5f);
    public FloatRange spawnRangeY = new FloatRange(6f, 10f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnObject(heartSpawnConfig);
        spawnObject(carrotSpawnConfig);
    }

    // Update is called once per frame
    void Update()
    {
        spawnObject(heartSpawnConfig);
        spawnObject(carrotSpawnConfig);
    }

    void spawnObject(SpawnConfig sc)
    {
        if (sc.getTimer() < sc.getSpawnInterval())
        {
            sc.countTimer();
        }
        else
        {
            Instantiate
            (
                sc.prefab,
                new Vector3(spawnRangeX.RandomValue(), spawnRangeY.RandomValue(), transform.position.z),
                transform.rotation
            );
            sc.resetTimer();
            sc.renewSpawnInterval();
        }
    }
}
