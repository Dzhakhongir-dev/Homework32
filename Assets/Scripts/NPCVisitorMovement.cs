using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCVisitorMovement : MonoBehaviour
{
    [Range(1f, 2f)]
    [SerializeField] private float minAgentSpeed;
    [Range(2f, 3f)]
    [SerializeField] private float maxAgentSpeed;

    private float randomSpeed;
    private WayPoints nextWaypoint;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        nextWaypoint = FindObjectOfType<WayPoints>();
    }

    private void Start()
    {
        randomSpeed = Random.Range(minAgentSpeed, maxAgentSpeed);
        agent.speed = randomSpeed;
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(DelayWhileDancing());
        }
    }

    private IEnumerator DelayWhileDancing()
    {
        while (agent.remainingDistance == agent.stoppingDistance)
        {
            nextWaypoint = nextWaypoint.nextWaypoint;

            yield return new WaitForSeconds(0.01f);
            agent.speed = randomSpeed;
            agent.SetDestination(nextWaypoint.GetPosition());
        }
    }
}
