using System.Collections;
using UnityEngine;

public class NPCInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject agentPrefab;
    [SerializeField] private int agentCountToSpawn;
    [SerializeField] private float agentSpawnTime;

    private IEnumerator Start()
    {
        for (int i = 0; i < agentCountToSpawn; i++)
        {
            yield return new WaitForSeconds(agentSpawnTime);
            Instantiate(agentPrefab, transform.position, Quaternion.identity);
        }
    }
}
