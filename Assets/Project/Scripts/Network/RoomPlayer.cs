using UnityEngine;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-room-player
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkRoomPlayer.html
*/

/// <summary>
/// This component works in conjunction with the NetworkRoomManager to make up the multiplayer room system.
/// The RoomPrefab object of the NetworkRoomManager must have this component on it.
/// This component holds basic room player data required for the room to function.
/// Game specific data for room players can be put in other components on the RoomPrefab or in scripts derived from NetworkRoomPlayer.
/// </summary>
public class RoomPlayer : NetworkRoomPlayer
{
    [SyncVar(hook = nameof(TestChanged))]
    public int test;

    public void TestChanged(int oldValue, int newValue)
    {
        Debug.Log("HOOK TEST CHANGED");
    }

    // [Command]
    // private void CmdSetTest(int value)
    // {
    //     Debug.Log("COMMAND SET TEST");
    //     test = value;
    // }

    // public void SetTest(int value)
    // {
    //     Debug.Log("SET TEST NO COMMAND");
    //     CmdSetTest(value);
    // }

    // /// <summary>
    // /// Called when the local player object has been set up.
    // /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
    // /// </summary>
    // public override void OnStartLocalPlayer()
    // {
    //     CameraManager.instance.InitCamera();
    // }

    // public override void OnGUI()
    // {
    //     base.OnGUI();

    //     if (isLocalPlayer)
    //     {
    //         if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
    //         {
    //             Debug.Log("Clicked the button with text");
    //             SetTest(Random.Range(0, 100));
    //         }
    //     }
    // }
}
