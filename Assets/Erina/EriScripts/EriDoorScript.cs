using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EriDoorScript : MonoBehaviour
{
    public Animator animatorDoor;
    private AudioSource audioSource;
    [SerializeField] private AudioSource doorSound;

    private void Start()
    {
        animatorDoor = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up

    }

    IEnumerator waitBeforePlayAudio()
    {
        yield return new WaitForSeconds(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animatorDoor.SetBool("Open",true);
            StartCoroutine(waitBeforePlayAudio());
            doorSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animatorDoor.SetBool("Open", false);
            doorSound.Stop();
        }
    }
}
