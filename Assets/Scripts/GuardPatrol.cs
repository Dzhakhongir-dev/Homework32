using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]

public class GuardPatrol : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask npcLayer;

    [SerializeField] private float walkingRange = 10f;
    [SerializeField] private float sightRange = 1f;
    [SerializeField] private float argueRange = 3f;
    [SerializeField] private float distanceBetweenGuardAndPoint = 1f;

    private Vector3 destinationPoint;
    private bool walkPointSet;
    private bool nPCInSight;
    private bool nPCInAttackRange;

    private GameObject TheVisitor;
    private NavMeshAgent navMeshAgent;
    private NPCAnimations npcAnimations;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        TheVisitor = GameObject.FindGameObjectWithTag("TheVisitor");
        npcAnimations = GetComponent<NPCAnimations>();
    }

    private void Update()
    {
        nPCInSight = Physics.CheckSphere(transform.position, sightRange, npcLayer);
        nPCInAttackRange = Physics.CheckSphere(transform.position, argueRange, npcLayer);

        if (!nPCInSight && !nPCInAttackRange)
        {
            Patrol();
        }

        if (nPCInSight && !nPCInAttackRange)
        {
            Chase();
        }

        if (nPCInSight && nPCInAttackRange)
        {
            Argue();
        }

    }

    private void Argue()
    {
        npcAnimations.ChangeAnimationState("Argue Duplicate");
        navMeshAgent.SetDestination(transform.position);

        transform.LookAt(TheVisitor.transform.position);

    }

    private void Chase()
    {
        npcAnimations.ChangeAnimationState("Walking");

        navMeshAgent.SetDestination(TheVisitor.transform.position);
    }

    private void Patrol()
    {
        if (!walkPointSet)
        {
            SearchForDestination();
        }

        if (walkPointSet)
        {
            navMeshAgent.SetDestination(destinationPoint);
        }

        if (Vector3.Distance(transform.position, destinationPoint) < distanceBetweenGuardAndPoint)
        {
            walkPointSet = false;
        }
    }

    private void SearchForDestination()
    {
        float z = Random.Range(-walkingRange, walkingRange);
        float x = Random.Range(-walkingRange, walkingRange);

        destinationPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destinationPoint, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        
        Color color = Color.green;
        Debug.DrawLine(transform.position, destinationPoint, color);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, argueRange);
    }
}
