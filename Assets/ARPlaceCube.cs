using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class ARPlaceCube : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject cubePrefab;

    private GameObject spawnedCube;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        if (raycastManager.Raycast(touch.position, hits,
            UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            if (spawnedCube == null)
                spawnedCube = Instantiate(cubePrefab, hitPose.position, hitPose.rotation);
            else
                spawnedCube.transform.position = hitPose.position;
        }
    }
}