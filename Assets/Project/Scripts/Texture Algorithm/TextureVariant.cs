using UnityEngine;
using Mirror;

public class TextureVariant : NetworkBehaviour
{
    public string placementName;
    public Location placementLocation; 
    public Collider _collider;
    public Renderer _renderer;

    [SyncVar (hook = nameof (StateChanged))]
    private bool currentState = false;

    /// <summary>
    /// Called on every NetworkBehaviour when it is activated on a client.
    /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
    /// </summary>
    public override void OnStartClient()
    {
        Debug.LogWarning(currentState);
        SetVisabillity(currentState);
    }

    public void SetVisabillity(bool value)
    {
        if (_collider != null) _collider.enabled = value;
        if (_renderer != null) _renderer.enabled = value;
    }

    public void StateChanged(bool _, bool value)
    {
        // show / hide elements
        SetVisabillity(value);
    }

    [Command(requiresAuthority = false)]
    public void CmdToggle()
    {
        currentState = !currentState;
    }

    [Command(requiresAuthority = false)]
    public void CmdSetState(bool value)
    {
        currentState = value;
    }
}

public enum Location {
    Flur,
    Kinderzimmer,
    Schlafzimmer,
    Wohnzimmer,
    Badezimmer,
    Kueche,
    Balkon
}
