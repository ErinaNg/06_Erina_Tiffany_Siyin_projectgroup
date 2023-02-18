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
    public GameObject FoodTray;
    public float DestinationTimer;
    public GameObject Door;
    public Text NarratorText;
    private bool TimerStart;

    public Transform PlayerTransform;
    public static bool PlayerInSightRange, PlayerInAttackRange;
    public LayerMask Ground, WhatIsPlayer, Obstacles;
    public float Distance;
    [Range(0, 360)]
    public float angle;

    public float SlightRadius;
    public float HearRadius;
    public float AttackRadius;


    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        IsActive = true;
        TutorialKnightCode = this;
        FoodTray.active = false;
        DestinationTimer = 0;
        NarratorText.text = "Hello Timmy II! It seems you are in prison, like usual. Press WASD to move around.";
        Invoke("KnightShowUpText", 3.5f);
        TimerStart = true;
        StartCoroutine(FOVRoutine());
    }

    void KnightShowUpText()
    {
        NarratorText.text = "Oh Look! The Guard here to give your your daily lunch.";
        TutorialKnightCode.KnightWalksToDestination();
    }

    public void KnightWalksToDestination()
    {
        Door.transform.Rotate(Door.transform.rotation.x, 90f, Door.transform.rotation.y);
        float DestinationZ = -8.63f;
        float DestinationX = 23.98f;
        Vector3 Destination = new Vector3(DestinationX, transform.position.y, DestinationZ);
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
        Invoke("TurnBehind", 1.5f);
    }

    void TurnBehind()
    {
        float DestinationZ = -8.6f;
        float DestinationX = 22f;
        Vector3 NewDestination = new Vector3(DestinationX, transform.position.y, DestinationZ);
        navMeshAgent.SetDestination(NewDestination);
        NarratorText.text = "Wait a min...The guard has turn behind! Quick! Sneak behind him and press Q to knock him out!";
    }

    public void TutorialKnightKnockOut()
    {
        NarratorText.text = "Good Job! Now Quick get out of here! To that teleporter!";
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
            TutorialKnightKnockOut();
        }

        if(TimerStart)
        {
            DestinationTimer += Time.deltaTime;

            if (DestinationTimer >= 8f)
            {
                StopMoving();
                TimerStart = false;
            }
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] RangeChecks = Physics.OverlapSphere(transform.position, SlightRadius, WhatIsPlayer);

        //We only want the first one because we only have one player but
        //there mulitple objects in the layer so
        if (RangeChecks.Length != 0)
        {
            for (int i = 0; i < RangeChecks.Length; i++)
            {
                Transform target = RangeChecks[i].transform;
                //Normalized to get a value between 0 and 1
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                //We divide it to two cause the enemy is facing forward, so you got
                //half the angle to the left and half to the right so we want to narrow by half
                //to do a detail angle check
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float DistanceToTarget = Vector3.Distance(transform.position, target.position);

                    //Check if there no obstacles in the way for the boss to see the target
                    //If there obstacles the boss line of view will be block by it
                    if (!Physics.Raycast(transform.position, directionToTarget, DistanceToTarget, Obstacles))
                    {
                        PlayerInSightRange = true;

                        //Check for attack range
                        float AttackDistance = Vector3.Distance(transform.position, PlayerTransform.position);
                        if (AttackDistance < AttackRadius)
                        {
                            PlayerInAttackRange = true;
                        }
                    }
                    else
                    {
                        NoRange();
                    }
                }
                else
                {
                    NoRange();
                }
            }
        }
    }

    private void NoRange()
    {
        PlayerInSightRange = false;
        PlayerInAttackRange = false;
    }
}

