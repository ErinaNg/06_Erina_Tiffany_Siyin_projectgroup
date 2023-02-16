using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialKnightScript : MonoBehaviour
{
    public static bool IsActive;
    private NavMeshAgent navMeshAgent;
    public static TutorialKnightScript TutorialKnight;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        IsActive = true;
        TutorialKnight = this;
    }

    public void KnightWalksToDestination()
    {

    }
}
