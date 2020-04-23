using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public Transform Duck;
    public Material DefaultMaterial;
    public Material VacantPlacementMaterial;
    public Material OccupiedPlacementMaterial;

    private MeshRenderer meshRenderer;
    private bool isOccupied = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();   
    }

    private void OnMouseDown()
    {
        if (DuckManager.IsPlacingDuck && !isOccupied)
        {
            Instantiate(Duck, transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
            DuckManager.ToppingBuilt();
            isOccupied = true;
            meshRenderer.material = DefaultMaterial;
        }
        else if (!DuckManager.IsPlacingDuck)
        {

        }
    }

    private void OnMouseEnter()
    {
        if (DuckManager.IsPlacingDuck)
            meshRenderer.material = isOccupied ? OccupiedPlacementMaterial : VacantPlacementMaterial;
    }

    private void OnMouseExit()
    {
        if (DuckManager.IsPlacingDuck)
            meshRenderer.material = DefaultMaterial;
    }

}
