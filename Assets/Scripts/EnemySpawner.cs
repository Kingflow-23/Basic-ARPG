using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public float spawnRadius = 60f;
    public int maxEnemies = 10;

    private int currentEnemyCount = 0;
    private ObjectiveManager objectiveManager;

    void Start()
    {
        objectiveManager = Object.FindFirstObjectByType<ObjectiveManager>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies && objectiveManager != null)
            {
                Vector3 center = objectiveManager.GetBoxPosition();

                Vector3 randomPos = center + Random.insideUnitSphere * spawnRadius;
                randomPos.y = center.y;

                NavMeshHit hit;
                if (
                    NavMesh.SamplePosition(randomPos, out hit, 5f, NavMesh.AllAreas) &&
                    Vector3.Distance(hit.position, center) > 5f
                )
                {
                    Instantiate(enemyPrefab, hit.position, Quaternion.identity);
                    currentEnemyCount++;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void OnEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
