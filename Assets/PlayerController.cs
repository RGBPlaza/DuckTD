using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera Camera;
    public Transform HeadBone;

    public float WalkSpeed = 5f;
    public float RotateSpeed = 10f;
    public float Gravity = 9.8f;
    public float JumpVelocity = 10f;
    
    private float verticalVelocity = 0;
    private CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        characterController.Move(transform.up * verticalVelocity * Time.deltaTime);
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = 0;
            if (Input.GetKey(KeyCode.Period))
                characterController.Move(transform.forward * WalkSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.E))
                characterController.Move(-transform.forward * WalkSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.U))
                characterController.Move(transform.right * WalkSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.O))
                characterController.Move(-transform.right * WalkSpeed * Time.deltaTime);
        }
        else
            verticalVelocity -= Gravity * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && verticalVelocity == 0)
            verticalVelocity = JumpVelocity;
        transform.Rotate(transform.up * Input.GetAxis("Mouse X") * RotateSpeed * Time.deltaTime);
    }

}
