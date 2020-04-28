using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckManager : MonoBehaviour
{
    public static DuckManager Instance { get; private set; }

    public GameObject ConfigPanel;
    public Dropdown TargetModeDropdown;

    public Transform ToppingPrefab;

    public Transform RangeDiscPrefab;
    private Transform RangeDisc;

    public Transform DuckToPlacePrefab { get; private set; }
    public bool IsPlacingDuck { get => DuckToPlacePrefab != null; }

    public bool IsDuckSelected { get => selectedDuck != null; }


    private Duck selectedDuck;

    public float CameraTransitionSpeed = 10f;
    private Vector3 previousCameraPosition;
    private Quaternion previousCameraRotation;

    private float cameraFOV;
    private Quaternion cameraRotation;
    private Vector3 cameraPosition;

    private void Awake()
    {
        Instance = this;
        ConfigPanel.SetActive(false);
    }

    private void Start()
    {
        cameraFOV = Camera.main.fieldOfView;
        cameraPosition = Camera.main.transform.position;
        cameraRotation = Camera.main.transform.rotation;
    }

    public void ToppingButtonPressed()
    {
        DuckToPlacePrefab = ToppingPrefab;
    }

    public void DuckBuilt()
    {
        DuckToPlacePrefab = null;
    }

    public void Select(Duck duck)
    {
        if (!IsDuckSelected)
        {
            previousCameraPosition = Camera.main.transform.position;
            previousCameraRotation = Camera.main.transform.rotation;
            cameraPosition = Camera.main.transform.position - new Vector3(0, 100, 0);
            cameraFOV -= 16;
        }
        cameraRotation = Quaternion.LookRotation(duck.transform.position - cameraPosition);

        selectedDuck = duck;
        if (RangeDisc == null)
            RangeDisc = Instantiate(RangeDiscPrefab, duck.transform.position, Quaternion.identity);
        else
            RangeDisc.position = duck.transform.position;
        RangeDisc.localScale = new Vector3(duck.Range, 1f, duck.Range);
        TargetModeDropdown.SetValueWithoutNotify((int)duck.TargetMode);
        ConfigPanel.SetActive(true);

    }

    public void Deselect()
    {
        if (IsDuckSelected)
        {
            selectedDuck = null;
            Destroy(RangeDisc.gameObject);
            ConfigPanel.SetActive(false);
            cameraPosition = previousCameraPosition;
            cameraRotation = previousCameraRotation;
            cameraFOV += 16;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsPlacingDuck)
        {
            DuckToPlacePrefab = null;
            GameObject node = GameObject.FindWithTag("PlacementHoverNode");
            if (node != null)
            {
                node.GetComponent<Node>().SetMaterial(false);
                node.tag = "Untagged";
            }
        }

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPosition, CameraTransitionSpeed * Time.deltaTime);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cameraRotation, CameraTransitionSpeed * Time.deltaTime);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, cameraFOV, CameraTransitionSpeed * Time.deltaTime);

    }

    public void PlaceDuckOnNode(Node node)
    {
        node.Duck = Instantiate(DuckToPlacePrefab, node.transform.position + new Vector3(0, 1.02f, 0), Quaternion.identity);
        node.SetMaterial(false);
        node.tag = "Untagged";
        DuckBuilt();
    }

    public void TargetModeSelected()
    {
        if (IsDuckSelected)
        {
            TargetMode targetMode = (TargetMode)TargetModeDropdown.value;
            selectedDuck.TargetMode = targetMode;
        }
    }

}
