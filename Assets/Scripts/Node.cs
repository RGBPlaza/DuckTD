using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Material DefaultMaterial;
    public Material VacantPlacementMaterial;
    public Material OccupiedPlacementMaterial;

    public Transform Duck { get; set; }
    public bool IsOccupied { get => Duck != null; }

    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();   
    }

    private void OnMouseDown()
    {
        if (DuckManager.Instance.IsPlacingDuck)
            DuckManager.Instance.PlaceDuckOnNode(this);
        else if (DuckManager.Instance.IsDuckSelected && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Duck == null)
                DuckManager.Instance.Deselect();
            else
                DuckManager.Instance.Select(Duck.GetComponent<Duck>());
        }
    }

    private void OnMouseEnter()
    {
        if (DuckManager.Instance.IsPlacingDuck)
        {
            SetMaterial(true);
            tag = "PlacementHoverNode";
        }
    }

    private void OnMouseExit()
    {
        if (DuckManager.Instance.IsPlacingDuck)
        {
            SetMaterial(false);
            tag = "Untagged";
        }
    }

    public void SetMaterial(bool hover) => meshRenderer.material = hover ? (Duck != null ? OccupiedPlacementMaterial : VacantPlacementMaterial) : DefaultMaterial;

}
