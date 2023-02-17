using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialKnightScript : MonoBehaviour
{
    public static bool IsActive;
    private NavMeshAgent navMeshAgent;
    public static TutorialKnightScript TutorialKnightCode;
    public GameObject Halo;
    private Animator animator;
    private Vector3 Destination;
    public GameObject FoodTray;
    public float DestinationTimer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        IsActive = true;
        TutorialKnightCode = this;
        FoodTray.active = false;
        DestinationTimer = 0;
    }

    public void KnightWalksToDestination()
    {
        float DestinationZ = -8.63f;
        float DestinationX = 23.98f;
        Destination = new Vector3(DestinationX, transform.position.y, DestinationZ);
        Vector3 distanceToDestination = transform.position - Destination;
        navMeshAgent.SetDestination(Destination);
        animator.SetBool("EnemyRunning", true);

        if(distanceToDestination.magnitude <= 1f )
        {
            StopMoving();
        }
    }

    void StopMoving()
    {
        animator.SetBool("EnemyRunning", false);
        FoodTray.active = true;
    }

    void Update()
    {
        if(IsActive)
        {
            animator.SetBool("EnemyIsActive", true);
            Halo.active = false;
        }
        else
        {
            animator.SetBool("EnemyIsActive", false);
            Halo.active = true;
        }
        DestinationTimer += Time.deltaTime;

        if (DestinationTimer >= 8f)
        {
            StopMoving();
        }
    }
}
