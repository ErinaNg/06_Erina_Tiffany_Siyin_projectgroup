using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eri_playerscript : MonoBehaviour
{
    public float moveSpeed = 7;
    public float smoothMoveTime = .1f;

    float smoothInputMagnitude;
    float smoothMoveVelocity;

    public Animator anim;
    void Update()
    {
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));

        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);
        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * targetAngle;

        transform.Translate(transform.forward * moveSpeed * Time.deltaTime * smoothInputMagnitude, Space.World);
    }
}
