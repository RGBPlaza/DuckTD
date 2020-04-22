using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Duck : MonoBehaviour
{

    public float Range = 25f;
    public float FireRate = 2f;
    public float RotationSpeed = 10f;
    public float SpoonVelocity = 48f;
    public float SpoonDamage = 1f;
    public float FoVAngle = 60f;
    public bool RightHanded = true;

    public Transform ProjectilePrefeb;

    private Transform target;
    private bool targetSet = false;

    private Animator animator;

    void Start()
    {
        RightHanded = Random.value >= 0.1;
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.2f);
        animator = GetComponent<Animator>();
        animator.SetBool("Right_Handed", RightHanded);
        animator.SetFloat("Speed", FireRate);
    }

    private bool isLookingAtTarget = false;
    void Update()
    {
        if (target != null)
        {
            Vector3 targetDir = target.position - transform.position;
            targetDir.y = 0;
            isLookingAtTarget = Vector3.Angle(transform.forward, targetDir) <= FoVAngle / 2f;
            Quaternion lookRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space)) animator.SetTrigger("Throw");
    }

    private void UpdateTarget()
    {
        int nearestEnemyIndex = -1;
        float shortestDistanceFromEnemy = Range; 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (dist <= shortestDistanceFromEnemy)
            {
                shortestDistanceFromEnemy = dist;
                nearestEnemyIndex = i;
            }
        }
        target = nearestEnemyIndex >= 0 ? enemies[nearestEnemyIndex].transform : null;
        if (nearestEnemyIndex >= 0 && !targetSet)
        {
            StartCoroutine(nameof(SendProjectiles));
            targetSet = true;
        }
    }

    private List<(Vector3, Quaternion)> projectileInstantiationData = new List<(Vector3, Quaternion)>();
    private IEnumerator SendProjectiles()
    {
        while (target != null)
        {
            Vector3 projectilePosition = transform.position + (transform.rotation * (1.8f * new Vector3(RightHanded ? 0.879f : -0.879f, 2.1f, -0.072f)));
            Vector3 projectDir = target.position - projectilePosition;
            Quaternion projectileRotation = Quaternion.LookRotation(projectDir);
            if (isLookingAtTarget)
            {
                projectileInstantiationData.Add((projectilePosition, projectileRotation));
                Invoke(nameof(InstantiateProjectile), 0.556f / FireRate);
                animator.SetTrigger("Throw");
            }
            yield return new WaitForSeconds(1f / FireRate);
        }
        isLookingAtTarget = false;
        targetSet = false;
    }

    private void InstantiateProjectile()
    {
        (Vector3 position, Quaternion rotation) = projectileInstantiationData[0];
        Spoon spoon = Instantiate(ProjectilePrefeb, position, rotation).GetComponent<Spoon>();
        spoon.Velocity = SpoonVelocity;
        spoon.Damage = SpoonDamage;
        projectileInstantiationData.RemoveAt(0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
