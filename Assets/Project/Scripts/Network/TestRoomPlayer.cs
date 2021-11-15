using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TestRoomPlayer : NetworkRoomPlayer
{
    // public static TestRoomPlayer localRoomPlayer;
    
    private float currentTime = 0;

    [SyncVar(hook = nameof(TestChanged))]
    public int test;

    public void TestChanged(int oldValue, int newValue)
    {
        Debug.Log("HOOK TEST CHANGED");
    }

    [Command]
    public void CmdSetTest(int value)
    {
        Debug.Log("COMMAND SET TEST");
        test = value;
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            currentTime += Time.deltaTime;
            if (currentTime > 5f) {
                currentTime = 0;
                CmdSetTest(Random.Range(0, 100));
            }
        }
    }




    // private GameObject lobbyPlayerUI;

    // [SyncVar(hook = nameof(RoleChanged))]
    // int role;

    // private void RoleChanged(int oldValue, int newValue)
    // {
    //     Debug.Log($"ROLE CHANGED HOOK FROM {oldValue}(OLD) to {newValue}(NEW)");
    // }

    // [Command]
    // public void CmdSetRole(int newRole)
    // {
    //     Debug.Log("COMMAND SET ROLE");
    //     role = newRole;
    //     Debug.Log(role);
    // }

    /// <summary>
    /// Called when the local player object has been set up.
    /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
    /// </summary>
    // public override void OnStartLocalPlayer()
    // {
    //     localRoomPlayer = this;
        // CameraManager.instance.InitCamera();
    // }

    public override void OnGUI()
    {
        base.OnGUI();
    }
}
