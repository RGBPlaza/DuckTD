using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform TransformToFollow;
    private Vector3 displacement;

    private void Start()
    {
        displacement = TransformToFollow.position - transform.position;
    }

    private void LateUpdate()
    {
        //transform.position = TransformToFollow.position - displacement;
    }

}
