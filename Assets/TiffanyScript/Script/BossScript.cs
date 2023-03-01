using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
{
    public float MovementSpeed;
    public Transform PlayerTransform;
    public GameObject Halo;
    public LayerMask Ground, WhatIsPlayer, Obstacles;
    public bool CanSeePlayer;
    public float Distance;

    [Range(0, 360)]
    public float angle;

    public float SlightRadius;
    public float HearRadius;
    public float AttackRadius;
    private NavMeshAgent navMeshAgent;
    public  bool IsActive;
    private Animator animator;

    //Patroling
    public Vector3 WalkPoint;
    bool WalkPointSet;
    public float WalkPointRange;
    public float WalkPointTimer;
    //HearPlayerLocation
    public Vector3 SoundPoint;

    //States
    public  bool PlayerInSightRange, PlayerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(FOVRoutine());
        IsActive = true;
        Halo.active = false;
        WalkPointTimer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //WalkPointTimeis for if the AI were to be stuck for about 3 seconds change walkpoint
        WalkPointTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        //&& !GameManager.Instance.isGameOver
        if (PlayerTransform != null && IsActive)
        {
            if (!PlayerInSightRange && !PlayerInAttackRange) Patroling(); HearPlayerLocation();
            if (PlayerInSightRange && !PlayerInAttackRange) ChasePlayer();
            if (PlayerInSightRange && PlayerInAttackRange) AttackPlayer();
            animator.SetBool("EnemyIsActive", true);
        }
        else
        {
            animator.SetBool("EnemyIsActive", false);
            Halo.active = true;
            Invoke("GetUPIn8seconds", 8.0f);
        }
    }

    void GetUPIn8seconds()
    {
        IsActive = true;
        Halo.active = false;
    }

    private void HearPlayerLocation()
    {
        start1:
        float HearDistance = Vector3.Distance(transform.position, PlayerTransform.position);
        if (HearDistance < HearRadius || Eri_malechara.moveSpeed >= 5)  //access script
        {
            //If Got Hear go to the player location but if player move somewhere
            SoundPoint = PlayerTransform.position;
            navMeshAgent.SetDestination(SoundPoint);
            animator.SetBool("EnemyRunning", true);
            //else without making sound the boss/monster will go to the location
            //where the last time makes the sound
            Vector3 distanceToSoundPoint = transform.position - SoundPoint;

            //SoundPoint Reached
            if(distanceToSoundPoint.magnitude < 1f)
            {
                animator.SetBool("EnemyRunning", false);
                goto start1;
            }
        }
        else
        {
            //If Didn't hear anything
            return;
        }
    }

    private void Patroling()
    {
        if (!WalkPointSet) SearchWalkPoint();

        if (WalkPointSet)
        {
            navMeshAgent.SetDestination(WalkPoint);
            Vector3 distanceToWalkPoint = transform.position - WalkPoint;
            animator.SetBool("EnemyRunning", true);

            //Walkpoint reached 
            if (distanceToWalkPoint.magnitude < 1f)
            {
                CannotReachWalkPointOrCompletedWalkPoint();
            }

            //If it cannot reach walkpoint
            if (distanceToWalkPoint.magnitude > 1f && WalkPointTimer >= 3.5f)
            {
                CannotReachWalkPointOrCompletedWalkPoint();
            }
        }
    }

    void CannotReachWalkPointOrCompletedWalkPoint()
    {
        WalkPointTimer = 0;
        WalkPointSet = false;
        animator.SetBool("EnemyRunning", false);
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(WalkPoint, -transform.up, 2f, Ground))
            WalkPointSet = true;
    }

    private void ChasePlayer()
    {
        animator.SetBool("EnemyRunning", true);
        navMeshAgent.SetDestination(PlayerTransform.position);
    }

    private void AttackPlayer() 
    {
        animator.SetTrigger("EnemyAttack");
        transform.LookAt(PlayerTransform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacles" || collision.gameObject.tag == "Enemy")
        {
            CannotReachWalkPointOrCompletedWalkPoint();
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
                            CanSeePlayer = true;
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
        else if (CanSeePlayer)
        {
            NoRange();
        }
    }

    private void NoRange()
    {
        CanSeePlayer = false;
        PlayerInSightRange = false;
        PlayerInAttackRange = false;
    }

    public void OnHit()
    {
        transform.LookAt(PlayerTransform);
    }
}
