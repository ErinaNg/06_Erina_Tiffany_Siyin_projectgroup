using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {   //Eri gem movement


    public bool isRotating = false;

    public Vector3 rotationAngle;
    public float rotationSpeed;
   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (isRotating)
        {
            transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
        }

	}
}
