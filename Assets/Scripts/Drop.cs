using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour
{
    public Item Item;
    public float RiseSpeed = 20f;
    public float CollectedShrinkSpeed = 4f;
    public float DecayShrinkSpeed = 0.1f;

    private bool collected = false;

    private void Update()
    {
        if (transform.localScale.x <= 0 || transform.localScale.y <= 0 || transform.localScale.z <= 0)
            Destroy(gameObject);
        else
        {
            if (collected)
                transform.localScale -= new Vector3(1, 1, 1) * CollectedShrinkSpeed * Time.deltaTime;
            else
                transform.localScale -= new Vector3(1, 1, 1) * DecayShrinkSpeed * Time.deltaTime;
        }
    }
    

    private void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (!collected)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.detectCollisions = false;
                rb.useGravity = false;
                rb.velocity = new Vector3(0, RiseSpeed / transform.localScale.magnitude, 0);
                collected = true;
                Inventory.Instance.AddItem(Item.ID);
            }
        }
    }
}
