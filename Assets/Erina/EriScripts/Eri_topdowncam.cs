using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eri_topdowncam : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;  //var

    private Transform parent; //ref

    //public gameobject character;
    //public vector3 positionoffset;
    //// update is called once per frame
    //void update()
    //{
    //    transform.position = vector3.lerp(transform.position, character.transform.position + positionoffset, time.deltatime * 5);
    //}

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


}
