using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class NPCMovement : MonoBehaviour
{
    [Range(1f, 3f), SerializeField] private float minAgentSpeed;
    [Range(3f, 5f), SerializeField] private float maxAgentSpeed;

    private float randomSpeed;
    private WayPoints nextWaypoint;
    private NavMeshAgent agent;
    private NPCAnimations nPCAnimations;

    private void Awake()
    {
        nPCAnimations = GetComponent<NPCAnimations>();
        agent = GetComponent<NavMeshAgent>();
        nextWaypoint = FindObjectOfType<WayPoints>();
    }

    private void Start()
    {
        randomSpeed = Random.Range(minAgentSpeed, maxAgentSpeed);
        agent.speed = randomSpeed;
        nextWaypoint = nextWaypoint.nextWaypoint;
        nPCAnimations.ChangeAnimationState("Walking");
        agent.SetDestination(nextWaypoint.GetPosition());
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            StopCoroutine(DelayWhileDancing());
            StartCoroutine(DelayWhileDancing());
        }

        //Debug.Log(agent.remainingDistance);
    }

    private IEnumerator DelayWhileDancing()
    {
        while (agent.remainingDistance == agent.stoppingDistance)
        {
            nPCAnimations.ChangeAnimationState("Soul Spin Dance");
            nextWaypoint = nextWaypoint.nextWaypoint;
            yield return new WaitForSeconds(nPCAnimations.currentState.Length * 2);
            //����� �������� �� ��������� � ������ ������������� ��������� 
            //�� ����� ������� �� ��� (* 2) ��� �� �������� �������� ���� �������� ������� ("Dancing")
            nPCAnimations.ChangeAnimationState("Walking");
            agent.speed = randomSpeed;
            agent.SetDestination(nextWaypoint.GetPosition());

        }
    }

}
