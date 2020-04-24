using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float Speed = 10f;
    public float RotationalSpeed = 180f;
    public Transform SpinningBit;
    public Transform PlaceToHit;
    public GameObject HealthBarGO;

    public float MaxHP = 6f;

    private float currentHP;
    private HealthBar healthBar;

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
        healthBar = HealthBarGO.GetComponent<HealthBar>();
        currentHP = MaxHP;
        healthBar.MaxHP = MaxHP; // Automatically sets current HP to max
    }

    private void Update()
    {
        if (SpinningBit != null)
        {
            Vector3 rot = new Vector3(1, 1, 1) * RotationalSpeed * Time.deltaTime;
            SpinningBit.Rotate(rot);
        }

        Vector3 dir = Target.position - transform.position;
        if (dir.magnitude < 0.1)
            ReachedWayPoint();
        else
        {
            transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * RotationalSpeed);
        }
    }

    private void ReachedWayPoint()
    {
        if (wayPointIndex >= WayPoints.Points.Length - 1)
            Destroy(gameObject);
        else
            wayPointIndex++;
    }

    private void OnTriggerEnter(Collider other)
    {
        Spoon spoon = other.gameObject.GetComponent<Spoon>();
        currentHP -= spoon.Damage;
        healthBar.CurrentHP = currentHP;
        if (currentHP <= 0)
            Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        DuckManager.Instance.Deselect();
    }

}
