using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    [System.Serializable]
    public class PowerUp
    {
        public GameObject prefab; // Power-up prefab
        public float spawnProbability; // Spawn olma olasýlýðý
    }

    public PowerUp[] powerUps; // Farklý power-up'lar dizisi
    public Transform[] spawnPoints; // Spawn noktalarý
    public float spawnInterval = 10f; // Spawn süresi

    private List<Transform> availableSpawnPoints; // Kullanýlabilir spawn noktalarý listesi

    void Start()
    {
        availableSpawnPoints = new List<Transform>(spawnPoints); // Tüm spawn noktalarýný listeye ekle
        InvokeRepeating("SpawnPowerUp", 0f, spawnInterval); // Spawn iþlemini belirli aralýklarla çaðýr
    }

    void SpawnPowerUp()
    {
        if (availableSpawnPoints.Count == 0) // Eðer kullanýlabilir spawn noktasý yoksa
        {
            availableSpawnPoints = new List<Transform>(spawnPoints); // Tüm spawn noktalarýný tekrar ekle
        }

        int spawnIndex = Random.Range(0, availableSpawnPoints.Count); // Rastgele bir kullanýlabilir spawn noktasý seç
        Transform spawnPoint = availableSpawnPoints[spawnIndex];
        availableSpawnPoints.RemoveAt(spawnIndex); // Seçilen spawn noktasýný listeden çýkar

        GameObject selectedPowerUp = GetRandomPowerUp(); // Rastgele bir power-up seç
        if (selectedPowerUp != null)
        {
            Quaternion spawnRotation = Quaternion.Euler(30f, 0f, 0f); // Power-up'larý 45 derece açýyla spawn et
            Instantiate(selectedPowerUp, spawnPoint.position, spawnRotation); // Power-up'ý spawn et
        }
    }

    GameObject GetRandomPowerUp()
    {
        float totalProbability = 0f;
        foreach (PowerUp powerUp in powerUps)
        {
            totalProbability += powerUp.spawnProbability;
        }

        float randomPoint = Random.value * totalProbability;

        foreach (PowerUp powerUp in powerUps)
        {
            if (randomPoint < powerUp.spawnProbability)
            {
                return powerUp.prefab;
            }
            else
            {
                randomPoint -= powerUp.spawnProbability;
            }
        }
        return null;
    }
}
