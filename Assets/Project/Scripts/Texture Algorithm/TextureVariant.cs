using UnityEngine;
using Mirror;

public class TextureVariant : NetworkBehaviour
{
    public string placementName;
    public TextureDefinition textureDefinition;
    public Location placementLocation;
    public Collider _collider;
    public Renderer _renderer;

    // some textures need to be available 2 times (textures at open door / closed door)
    // clones will be activated along if this texture is going to be shown
    public Collider cloneCollider;
    public Renderer cloneRenderer;

    private GameObject maengellisteInstance;

    [SyncVar (hook = nameof (StateChanged))]
    private bool currentState = false;

    /// <summary>
    /// Called on every NetworkBehaviour when it is activated on a client.
    /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
    /// </summary>
    public override void OnStartClient()
    {
        SetVisabillity(currentState);
    }

    public void SetVisabillity(bool value)
    {
        if (_collider != null) _collider.enabled = value;
        if (_renderer != null) _renderer.enabled = value;
        if (cloneCollider != null) cloneCollider.enabled = value;
        if (cloneRenderer != null) cloneRenderer.enabled = value;

        if (value && maengellisteInstance == null)
        {
            string listString = $"<b>{placementLocation.ToString()}:</b> {textureDefinition.description}";
            // Debug.LogWarning("Would add this TextureVariant to Mängelliste: " + listString);
            maengellisteInstance = Instantiate(LobbyUIManager.instance.maengellistePrefab, LobbyUIManager.instance.maengellisteParent.transform);
            MaengellisteEntryHelper helper = maengellisteInstance.GetComponent<MaengellisteEntryHelper>();
            helper.text.text = listString;
        }
        else if (!value && maengellisteInstance != null)
        {
            Destroy(maengellisteInstance.gameObject);
            maengellisteInstance = null;
        }
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
