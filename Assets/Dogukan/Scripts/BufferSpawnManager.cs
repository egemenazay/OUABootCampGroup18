using UnityEngine;

public class BufferSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bufferPrefab; // Prefab of the buffer

    [SerializeField]
    private float spawnInterval = 5f; // Spawn interval in seconds

    [SerializeField]
    private Vector2 spawnAreaSize = new Vector2(10, 10); // Size of the spawn area

    private float timeSinceLastSpawn;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnBuffer();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnBuffer()
    {
        Vector3 randomPosition = GetRandomPosition();
        Instantiate(bufferPrefab, randomPosition, Quaternion.identity);
        spawnInterval = Random.Range(3f, 10f); // 3 ile 10 saniye arasında rastgele bir süre
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(spawnAreaSize.x, 1, spawnAreaSize.y));
    }


    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float z = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        return new Vector3(x, 0, z); // y eksenini 0 olarak tutarak zeminde kalmasını sağlıyoruz
    }
}
