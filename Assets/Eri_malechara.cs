using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eri_malechara : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private Vector3 moveDirection;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(0, 0, moveZ);
        
        if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))  //Walk
        {
            Walk();
        }
        else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift)) //Run
        {
            Run();
        }
        else if(moveDirection == Vector3.zero) //no movement //idle
        {
            Idle();
        }
        controller.Move(moveDirection * Time.deltaTime);

    }

    private void Idle()
    {

    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
    }

    private void Run()
    {
        moveSpeed = runSpeed;
    }

    private void OnTriggerStay(Collider Other)
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (Other.gameObject.tag == "Tutorial")
            {
                if (TutorialKnightScript.TutorialKnight.IsActive && !TutorialKnightScript.TutorialKnight.PlayerInSightRange)
                {
                    TutorialKnightScript.TutorialKnight.TutorialKnightKnockOut();
                }
            }

            if (Other.gameObject.tag == "Boss")
            {
                if (!BossScript.PlayerInSightRange && BossScript.IsActive && !BossScript.IfAttackPlayer)
                {
                    BossScript.IsActive = false;
                }
            }
        }
    }
}
