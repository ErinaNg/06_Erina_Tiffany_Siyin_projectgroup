using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EriDoorScript : MonoBehaviour
{
    public Animator animatorDoor;
    private AudioSource audioSource;
    [SerializeField] private AudioSource doorSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        animatorDoor = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animatorDoor.SetBool("Open",true);
         
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animatorDoor.SetBool("Open", false);
            doorSound.Play();
        }
    }
}
