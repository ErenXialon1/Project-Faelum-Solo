using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Düþman prefab'ýnýzý buraya sürükleyin
    public float spawnRate = 2f; // Her 2 saniyede bir düþman spawn edecek
    public int currentEnemyIndex;
    public Transform playerTransform;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
        StartCoroutine(ChangeEnemyType());
        StartCoroutine(ChangeSpawnRate());
    }

    void SpawnEnemy()
    {
        

        switch (currentEnemyIndex)
        {
            case 4:
                Instantiate(enemyPrefabs[0], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan düþmaný spawn eder
                Instantiate(enemyPrefabs[1], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan düþmaný spawn eder
                Instantiate(enemyPrefabs[2], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan düþmaný da spawn eder
                Instantiate(enemyPrefabs[3], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan düþmaný da spawn eder
                Instantiate(enemyPrefabs[4], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan düþmaný da spawn eder
                break;
            case 3:
                Instantiate(enemyPrefabs[0], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan düþmaný spawn eder
                Instantiate(enemyPrefabs[1], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan düþmaný spawn eder
                Instantiate(enemyPrefabs[2], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan düþmaný da spawn eder
                Instantiate(enemyPrefabs[3], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan düþmaný da spawn eder
                break;
            case 2:
                Instantiate(enemyPrefabs[0], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan düþmaný spawn eder
                Instantiate(enemyPrefabs[1], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan düþmaný spawn eder
                Instantiate(enemyPrefabs[2], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan düþmaný da spawn eder
                break;
            case 1:
                Instantiate(enemyPrefabs[0], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan düþmaný spawn eder
                Instantiate(enemyPrefabs[1], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan düþmaný da spawn eder
                break;

            default:
                Instantiate(enemyPrefabs[currentEnemyIndex], GetSpawnPosition(), Quaternion.identity);
                break;
        }
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 spawnPosition = new Vector2();

        do
        {
            float spawnPointX = Random.Range(-30f, 30f);
            float spawnPointY = Random.Range(-30f, 30f);
            spawnPosition = new Vector2(playerTransform.position.x + spawnPointX, playerTransform.position.y + spawnPointY);
        }
        while (Vector2.Distance(spawnPosition, playerTransform.position) < 15f); // 5 birimlik minimum mesafeyi garanti eder

        return spawnPosition;
    }

    IEnumerator ChangeSpawnRate()
    {
        yield return new WaitForSeconds(10); // 10 saniye bekler
        spawnRate = spawnRate - 0.3f; // spawnRate'i 1 ile 5 arasýnda rastgele bir deðere ayarlar
        if (spawnRate <= 0)
        {
            spawnRate = 2;
        }

        // InvokeRepeating'i durdurun ve yeni spawn oranýyla baþlatýn
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
        StartCoroutine(ChangeSpawnRate());
    }

    IEnumerator ChangeEnemyType()
    {
        yield return new WaitForSeconds(70); // 70 saniye bekler
        currentEnemyIndex = (currentEnemyIndex + 1) % enemyPrefabs.Length; // Düþman türünü sýradaki ile deðiþtirir
        StartCoroutine(ChangeEnemyType()); // Coroutine'i tekrar baþlatýr
    }
}