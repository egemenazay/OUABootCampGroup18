using UnityEngine;

public class CageSpawner : MonoBehaviour
{
    public GameObject cagePrefab; // Kafes prefab referansý
    public Vector3[] spawnPositions; // Spawn pozisyonlarýný saklamak için dizi

    private bool isCageSpawned = false; // Kafesin spawnlanýp spawnlanmadýðýný kontrol etmek için bayrak

    void Start()
    {
        SpawnCage();
    }

    public void SpawnCage()
    {
        if (!isCageSpawned)
        {
            int randomIndex = Random.Range(0, spawnPositions.Length); // Rastgele pozisyon seçimi
            Vector3 spawnPosition = spawnPositions[randomIndex];
            Instantiate(cagePrefab, spawnPosition, Quaternion.identity);
            isCageSpawned = true; // Kafesin spawnlandýðýný belirtir
        }
    }
}
