using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceCube : MonoBehaviour
{
    public GameObject cubePrefab;

    private ARRaycastManager raycastManager;

    static List<ARRaycastHit> hits = new();

    bool placed = false;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (placed)
            return;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        if (raycastManager.Raycast(
            touch.position,
            hits,
            TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            Instantiate(
                cubePrefab,
                pose.position,
                pose.rotation);

            placed = true;
        }
    }
}