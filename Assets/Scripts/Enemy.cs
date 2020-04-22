using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float Speed = 10f;
    public float RotationalSpeed = 180f;

    private Transform Target { get => WayPoints.Points[wayPointIndex]; }
    private int wayPointIndex = 0;

    private float hp = 6;

    private void Update()
    {
        Vector3 rot = new Vector3(1, 1, 1) * RotationalSpeed * Time.deltaTime;
        Vector3 dir = Target.position - transform.position;

        transform.Rotate(rot, Space.World);
        if (dir.magnitude < 0.1)
            ReachedWayPoint();
        else
            transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
    }

    private void ReachedWayPoint()
    {
        if (wayPointIndex >= WayPoints.Points.Length - 1)
            Destroy(gameObject);
        else
            wayPointIndex++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Spoon spoon = collision.gameObject.GetComponent<Spoon>();
        hp -= spoon.Damage;
        Destroy(collision.gameObject);
        if (hp <= 0)
            Destroy(gameObject);
    }

}
