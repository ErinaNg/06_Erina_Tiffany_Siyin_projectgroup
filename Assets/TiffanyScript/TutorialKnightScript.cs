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
    public GameObject Destination;
    public GameObject FoodTray;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        IsActive = true;
        TutorialKnightCode = this;
        FoodTray.active = false;
    }

    public void KnightWalksToDestination()
    {
        navMeshAgent.SetDestination(Destination.transform.position);
        animator.SetBool("EnemyRunning", true);

        Vector3 distanceToDestination = transform.position - Destination.transform.position;
        if (distanceToDestination.magnitude < 1f)
        {
            animator.SetBool("EnemyRunning", false);
            FoodTray.active = true;
        }
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
    }
}
