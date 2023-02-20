using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eri_topdowncam : MonoBehaviour
{
    public GameObject character;
    public Vector3 positionOffSet;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, character.transform.position + positionOffSet, Time.deltaTime * 5);
    }
}
