using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mirror.Examples.NetworkRoom
{
    [AddComponentMenu("")]
    public class NetworkRoomPlayerExt : NetworkRoomPlayer
    {
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

        public override void OnStartClient()
        {
            //Debug.Log($"OnStartClient {gameObject}");
        }

        public override void OnClientEnterRoom()
        {
            //Debug.Log($"OnClientEnterRoom {SceneManager.GetActiveScene().path}");
        }

        public override void OnClientExitRoom()
        {
            //Debug.Log($"OnClientExitRoom {SceneManager.GetActiveScene().path}");
        }

        public override void IndexChanged(int oldIndex, int newIndex)
        {
            //Debug.Log($"IndexChanged {newIndex}");
        }

        public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
        {
            //Debug.Log($"ReadyStateChanged {newReadyState}");
        }

        public override void OnGUI()
        {
            base.OnGUI();
            
            if (isLocalPlayer)
            {
                if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
                {
                    Debug.Log("Clicked the button with text");
                    CmdSetTest(Random.Range(0, 100));
                }
            }
        }

        public void TestReadyState()
        {
            CmdChangeReadyState(true);
        }
    }
}
