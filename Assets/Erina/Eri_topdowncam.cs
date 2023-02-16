using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eri_topdowncam : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;  //var

    private Transform parent; //ref

    private void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        parent.Rotate(Vector3.up, mouseX);

    }

    //public GameObject character;
    //public Vector3 positionOffSet;
    //// Update is called once per frame
    //void Update()
    //{
    //    transform.position = Vector3.Lerp(transform.position, character.transform.position + positionOffSet, Time.deltaTime * 5);
    //}
}
