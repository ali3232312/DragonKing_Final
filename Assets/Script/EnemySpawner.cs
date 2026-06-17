using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración del spawner")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;
    public int maxEnemies = 10;
    public bool spawnOnStart = true;

   
    private Coroutine spawnCoroutine;

    private void Start()
    {
        if (spawnOnStart)
            StartSpawning();
    }
    public void StartSpawning()
    {
        if (spawnCoroutine == null)
            spawnCoroutine = StartCoroutine(SpawnRoutine());
    }
    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
    public void SpawnOnce()
    {
        if (enemyPrefab == null)
            return; // No hay prefab, no hacemos nada.

        if (spawnPoints == null || spawnPoints.Length == 0)
            return; // No hay puntos de spawn configurados.

        int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (currentEnemies >= maxEnemies)
            return; // Ya hay suficientes enemigos en la escena.

        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawn.position, spawn.rotation);
    }
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnOnce();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnPoints == null)
            return;

        Gizmos.color = Color.red;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i] == null)
                continue;

            Gizmos.DrawWireSphere(spawnPoints[i].position, 0.5f);
        }
    }
}
