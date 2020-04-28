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
    public bool RandomSpin = false;
    public bool TurnToFaceWayPoint = true;
    public Transform PlaceToHit;
    public GameObject HealthBarGO;
    public Transform dropPrefab;

    public float MaxHP = 6f;

    private float currentHP;
    private HealthBar healthBar;
    private Vector3 randomSpinDirection;

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
        if (RandomSpin)
        {
            SpinningBit.rotation = Random.rotation;
            Vector2 xzSpinDirection;
            do xzSpinDirection = Random.insideUnitCircle;
            while (xzSpinDirection.magnitude == 0);
            xzSpinDirection = xzSpinDirection.normalized;
            randomSpinDirection = new Vector3(xzSpinDirection.x, Random.value, xzSpinDirection.y);
        }
    }

    private void Update()
    {
        if (RandomSpin)
        {
            Vector3 rot = randomSpinDirection * RotationalSpeed * Time.deltaTime;
            SpinningBit.Rotate(rot);
        }

        Vector3 dir = Target.position - transform.position;
        if (dir.magnitude < 0.1)
            ReachedWayPoint();
        else
        {
            transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
            if(TurnToFaceWayPoint)
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
        if (other.CompareTag("Spoon"))
        {
            Spoon spoon = other.GetComponent<Spoon>();
            currentHP -= spoon.Damage;
            healthBar.CurrentHP = currentHP;
            if (currentHP <= 0)
            {
                if (RandomSpin)
                    Instantiate(dropPrefab, SpinningBit.position, SpinningBit.rotation);
                else
                    Instantiate(dropPrefab, PlaceToHit.position, PlaceToHit.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        DuckManager.Instance.Deselect();
    }

}
