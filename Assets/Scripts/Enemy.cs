using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{

    public float Speed = 10f;
    public float RotationalSpeed = 180f;
    public float MaxHP = 6f;

    private float hp;

    public float DistanceFromEnd
    {
        get
        {
            float val = Vector3.Distance(transform.position, Target.position);
            for (int i = wayPointIndex; i < WayPoints.Points.Length - 1; i++)
                val += Vector3.Distance(WayPoints.Points[i].position, WayPoints.Points[i + 1].position);
            return val;
        }
    }

    private Transform Target { get => WayPoints.Points[wayPointIndex]; }
    private int wayPointIndex = 0;

    private void Start()
    {
        hp = MaxHP;
    }

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
