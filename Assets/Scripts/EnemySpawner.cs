using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // D��man prefab'�n�z� buraya s�r�kleyin
    public float spawnRate = 2f; // Her 2 saniyede bir d��man spawn edecek
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
                Instantiate(enemyPrefabs[0], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan d��man� spawn eder
                Instantiate(enemyPrefabs[1], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan d��man� spawn eder
                Instantiate(enemyPrefabs[2], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan d��man� da spawn eder
                Instantiate(enemyPrefabs[3], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan d��man� da spawn eder
                Instantiate(enemyPrefabs[4], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan d��man� da spawn eder
                break;
            case 3:
                Instantiate(enemyPrefabs[0], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan d��man� spawn eder
                Instantiate(enemyPrefabs[1], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan d��man� spawn eder
                Instantiate(enemyPrefabs[2], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan d��man� da spawn eder
                Instantiate(enemyPrefabs[3], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan d��man� da spawn eder
                break;
            case 2:
                Instantiate(enemyPrefabs[0], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan d��man� spawn eder
                Instantiate(enemyPrefabs[1], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan d��man� spawn eder
                Instantiate(enemyPrefabs[2], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan d��man� da spawn eder
                break;
            case 1:
                Instantiate(enemyPrefabs[0], GetSpawnPosition(), Quaternion.identity); // indexi 1 olan d��man� spawn eder
                Instantiate(enemyPrefabs[1], GetSpawnPosition(), Quaternion.identity); // indexi 2 olan d��man� da spawn eder
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
        spawnRate = spawnRate - 0.3f; // spawnRate'i 1 ile 5 aras�nda rastgele bir de�ere ayarlar
        if (spawnRate <= 0)
        {
            spawnRate = 2;
        }

        // InvokeRepeating'i durdurun ve yeni spawn oran�yla ba�lat�n
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
        StartCoroutine(ChangeSpawnRate());
    }

    IEnumerator ChangeEnemyType()
    {
        yield return new WaitForSeconds(70); // 70 saniye bekler
        currentEnemyIndex = (currentEnemyIndex + 1) % enemyPrefabs.Length; // D��man t�r�n� s�radaki ile de�i�tirir
        StartCoroutine(ChangeEnemyType()); // Coroutine'i tekrar ba�lat�r
    }
}