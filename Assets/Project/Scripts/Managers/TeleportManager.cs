using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : Singleton<TeleportManager> {
    public float raycastMaxDistance = 50f;
    public LayerMask layerToRaycast;

    void Start () {
        foreach (Transform child in transform) {
            child.gameObject.SetActive (false);
        }
    }

    public Vector3 GetTeleportPosition (Vector3 raycastHitPosition) {
        if (transform.childCount <= 0) return Vector3.zero;
        Vector3 closestPoint = Vector3.zero;
        float closestDistance = float.MaxValue;

        foreach (Transform child in transform) {
            float distance = Vector3.Distance (raycastHitPosition, child.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestPoint = child.position;
            }
        }

        return closestPoint;
    }
}
