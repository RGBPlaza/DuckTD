using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : MonoBehaviour
{

    public float Velocity;
    public float Damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("hi");
        if (collision.gameObject.CompareTag("Environment"))
            Destroy(gameObject);
    }
}
