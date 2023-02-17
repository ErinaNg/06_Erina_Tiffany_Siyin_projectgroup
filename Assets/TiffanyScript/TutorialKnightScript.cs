using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public GameObject Door;
    public Text NarratorText;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        IsActive = true;
        TutorialKnightCode = this;
        FoodTray.active = false;
        DestinationTimer = 0;
        NarratorText.text = "Hello Timmy II! It seems you are in prison, like usual.";
        Invoke("KnightShowUpText", 3.5f);
    }

    void KnightShowUpText()
    {
        NarratorText.text = "Oh Look! The knight here to give your your daily lunch.";
        TutorialKnightCode.KnightWalksToDestination();
    }

    public void KnightWalksToDestination()
    {
        Door.transform.Rotate(Door.transform.rotation.x, 90f, Door.transform.rotation.y);
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
; 
        Vector3 Turn = new Vector3(transform.rotation.x, 270f, transform.rotation.z);
        Quaternion tempRot = transform.rotation = Quaternion.LookRotation(Turn);
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
