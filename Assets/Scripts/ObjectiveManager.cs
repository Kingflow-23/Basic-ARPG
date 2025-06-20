using UnityEngine;
using UnityEngine.AI;

public class ObjectiveManager : MonoBehaviour
{
    public GameObject boxObject;
    public GameObject goalObject;
    public float spawnRadius = 50f;
    public float minDistance = 30f;

    void Start()
    {
        PlaceObjective();
    }

    public void PlaceObjective()
    {
        Vector3 spawnPos = transform.position;

        for (int i = 0; i < 100; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = transform.position.y;

            if (Vector3.Distance(transform.position, randomPos) >= minDistance &&
                NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                spawnPos = hit.position;
                break;
            }
        }

        if (boxObject != null) boxObject.transform.position = spawnPos;
        if (goalObject != null) goalObject.transform.position = spawnPos;
    }

    public Vector3 GetBoxPosition()
    {
        return boxObject != null ? boxObject.transform.position : transform.position;
    }
}
