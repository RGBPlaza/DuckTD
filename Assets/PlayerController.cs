using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera Camera;
    public Transform PlayerModel;
    public Transform ProjectilePrefab;
    public Transform HandBone;

    public float WalkSpeed = 5f;
    public float WalkAcceleration = 10f;
    public float HorizontalCameraRotateSpeed = 10f;
    public float VerticalCameraRotateSpeed = 10f;
    public float Gravity = 9.8f;
    public float JumpVelocity = 10f;
    
    private Vector3 velocity = Vector3.zero;
    private CharacterController characterController;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        characterController.Move(velocity * Time.deltaTime);
        if (characterController.isGrounded)
        {

            if (velocity.y < 0)
                velocity.y = 0;

            Vector3 cameraForward = Camera.transform.forward;
            cameraForward.y = 0;

            // x: forward; y: side;
            Vector2 _velocity = Vector2.zero;
            if (Input.GetKey(KeyCode.Period))
            {
                //PlayerModel.LookAt(transform.position + cameraForward);
                //velocity += cameraForward * Time.deltaTime * WalkAcceleration;
                _velocity.x = 1;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                //PlayerModel.LookAt(transform.position - cameraForward);
                //velocity -= cameraForward * Time.deltaTime * WalkAcceleration;
                _velocity.x = -1;
            }
            else
                _velocity.x = 0;

            if (Input.GetKey(KeyCode.U))
            {
                //PlayerModel.LookAt(transform.position + cameraRight);
                //velocity += cameraRight * Time.deltaTime * WalkAcceleration;
                _velocity.y = 1;
            }
            else if (Input.GetKey(KeyCode.O))
            {
                //PlayerModel.LookAt(transform.position - cameraRight);
                //velocity -= cameraRight * Time.deltaTime * WalkAcceleration;
                _velocity.y = -1;
            }
            else
                _velocity.y = 0;

            if (_velocity.sqrMagnitude != 0)
                _velocity = _velocity.normalized;

            Vector3 worldVelocity = new Vector3(_velocity.y, 0, _velocity.x);

            if (_velocity.sqrMagnitude != 0)
                PlayerModel.rotation = Quaternion.Lerp(PlayerModel.rotation, Quaternion.LookRotation(Quaternion.LookRotation(cameraForward) * worldVelocity), Time.deltaTime * WalkAcceleration);
            velocity = Vector3.Lerp(velocity, Quaternion.LookRotation(cameraForward) * worldVelocity * WalkSpeed, Time.deltaTime * WalkAcceleration);

            //if (!(Input.GetKey(KeyCode.Period) || Input.GetKey(KeyCode.E)))
            //    velocity += cameraForward * Time.deltaTime * WalkAcceleration * (forwardVelocity > 0 ? -1 : 1);
            //if (!(Input.GetKey(KeyCode.U) || Input.GetKey(KeyCode.O)))
            //    velocity += cameraRight * Time.deltaTime * WalkAcceleration * (sideVelocity > 0 ? -1 : 1);

            animator.SetFloat("speed", Mathf.Abs(_velocity.x));
        }
        else
            velocity.y -= Gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(velocity.y) < 0.5f)
        {
            velocity.y = JumpVelocity;
            animator.SetFloat("speed", 0f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Invoke(nameof(InstantiateProjectile), 0.33f);
            animator.SetTrigger("throw");
        }

        Camera.transform.RotateAround(transform.position, transform.up, Input.GetAxis("Mouse X") * HorizontalCameraRotateSpeed * Time.deltaTime);
        Camera.transform.Rotate(-Camera.transform.right * Input.GetAxis("Mouse Y") * HorizontalCameraRotateSpeed * Time.deltaTime, Space.World);
    }

    private void InstantiateProjectile()
    {
        Spoon spoon = Instantiate(ProjectilePrefab, HandBone.position, PlayerModel.transform.rotation).GetComponent<Spoon>();
        spoon.ForwardVelocity = 20;
        spoon.Damage = 1;
    }

}
