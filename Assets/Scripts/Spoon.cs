using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : MonoBehaviour
{

    public float ForwardVelocity;
    public float Damage;
    public int HitsUntilDestroy = 1;

    private int remainingHits;
    void Start()
    {
        remainingHits = HitsUntilDestroy;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * ForwardVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Environment"))
            Destroy(gameObject);
        else if (other.CompareTag("Enemy"))
        {
            remainingHits--;
            if(remainingHits == 0)
                Destroy(gameObject);
        }
    }

}
