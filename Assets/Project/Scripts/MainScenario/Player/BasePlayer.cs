using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;

public class BasePlayer : MonoBehaviour
{
    public static BasePlayer localPlayer;

    [Header ("Visibillity")]
    public MeshRenderer playerHead;
    public MeshRenderer playerBody;
    public MeshRenderer playerVector;
    public Light viewportInner;
    public Light viewportOuter;

    [Header ("Local Player")]
    public BasePlayerMovement playerMovement;
    public PlayerRotation playerRotation;
    public GameObject movementTrigger;
    public Rigidbody rb;
    public CapsuleCollider capsuleCollider;
    public TrackedPoseDriver trackedPoseDriver;

    [Header ("Settings")]
    public float headNormalHeight = 2.75f;
    public float headCrawlHeight = 0.5f;

    public void SetupInactivePlayer ()
    {
        RenamePlayer ("InactivePlayer");
        SetVisabillity (false);
        SetInteractabillity (false);
    }

    public void SetupLocalPlayer ()
    {
        localPlayer = this;
        RenamePlayer ("LocalPlayer");
        SetVisabillity (false);
        SetInteractabillity (true);
        Camera.main.gameObject.transform.SetParent(playerHead.transform, false);
    }

    public void SetupVisablePlayer ()
    {
        RenamePlayer ("VisablePlayer");
        SetVisabillity (true);
        SetInteractabillity (false);
    }

    private void RenamePlayer (string type)
    {
        transform.gameObject.name = $"{type}";
        transform.gameObject.tag = type;
    }

    private void SetVisabillity (bool value)
    {
        playerHead.enabled = value;
        playerBody.enabled = value;
        playerVector.enabled = value;
        playerVector.gameObject.SetActive (value);
        viewportInner.enabled = value;
        viewportInner.gameObject.SetActive (value);
        viewportOuter.enabled = value;
        viewportOuter.gameObject.SetActive (value);
    }

    private void SetInteractabillity (bool value)
    {
        playerMovement.enabled = value;
        playerRotation.enabled = value;

        capsuleCollider.enabled = value;
        rb.isKinematic = !value;
        rb.detectCollisions = value;

        SetMovementTriggerIfAndroid (value);
#if UNITY_ANDROID
        trackedPoseDriver.enabled = value;
#else
        trackedPoseDriver.enabled = false;
#endif
    }

    private void SetMovementTriggerIfAndroid (bool value)
    {
#if UNITY_ANDROID
        movementTrigger.SetActive (value);
#else
        movementTrigger.SetActive (false);
#endif
    }

    public void Stand ()
    {
        SetMovementTriggerIfAndroid (true);
        SetHeadPosition (headNormalHeight);
    }

    public void Crawl ()
    {
        SetMovementTriggerIfAndroid (false);
        SetHeadPosition (headCrawlHeight);
    }

    private void SetHeadPosition (float value)
    {
        Vector3 newHeadPos = playerHead.gameObject.transform.localPosition;
        newHeadPos.y = value;

        float valueForBody = (value - 0.25f) / 2f;
        Vector3 newBodyPos = playerBody.gameObject.transform.localPosition;
        Vector3 newBodyScale = playerBody.gameObject.transform.localScale;
        newBodyPos.y = valueForBody;
        newBodyScale.y = valueForBody;

        playerHead.gameObject.transform.localPosition = newHeadPos;
        playerBody.gameObject.transform.localPosition = newBodyPos;
        playerBody.gameObject.transform.localScale = newBodyScale;
    }

    public void TeleportPlayer ()
    {
        if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward * 50f, out RaycastHit hit, TeleportManager.instance.raycastMaxDistance, TeleportManager.instance.layerToRaycast)) {
            Vector3 targetPos = TeleportManager.instance.GetTeleportPosition (hit.point);
            transform.position = targetPos;
        }
    }
}
