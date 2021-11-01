using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainLandingInput : MonoBehaviour
{
    public TMP_InputField name;
    public Button button;

    private bool nameReady = false;
    private bool consentReady = false;

    public void OnChangedName (string input)
    {
        SessionManager.session.name = input;
        nameReady = SessionManager.session.IsValidName();
        CheckButtonInteraction();
    }

    public void OnChangedConsent (bool status)
    {
        SessionManager.session.consent = status;
        consentReady = status;
        CheckButtonInteraction();
    }

    public void Submit ()
    {
        InputManager.instance.DisplayRequiredUI();
    }

    private void CheckButtonInteraction() 
    {
        button.interactable = nameReady && consentReady;
    }
}
