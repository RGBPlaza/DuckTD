using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public Transform Duck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        Instantiate(Duck, transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
    }

}
