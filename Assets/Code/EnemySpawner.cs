using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float maxSpawnDelay = 3f;
    public float minSpawnDelay =0.5f;

    private Coroutine spawnCoroutine;
    private float timeElapsed;
    private float spawnDelay;


    private void Update()
    {
        if (spawnCoroutine != null)
        {
            timeElapsed += Time.deltaTime;
            float decreasedSpawnDelay = maxSpawnDelay - ((maxSpawnDelay - minSpawnDelay) / 60f * timeElapsed);
            spawnDelay = Mathf.Clamp(decreasedSpawnDelay, minSpawnDelay, maxSpawnDelay);
        }
    }

    private IEnumerator EnemySpawnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);

        Transform spawnPoint = spawnPoints[randomSpawnIndex];
        GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];

        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void StartEndlessSpawning()
    {
        if (spawnCoroutine == null)
        {
            timeElapsed = 0;
            spawnDelay = maxSpawnDelay;
            spawnCoroutine = StartCoroutine(EnemySpawnTimer());
        }
    }

}