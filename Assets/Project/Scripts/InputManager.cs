using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public MainLandingInput mainLandingInput;
    public MainMenuInput mainMenuInput;
    public ScCreateInput scCreateInput;
    public ScJoinInput scJoinInput;

    void Start() {
        DisplayRequiredUI();
    }

    public void DisplayRequiredUI()
    {
        Debug.Log(SessionManager.session.ToString());
        HideAll();
        if (SessionManager.session.IsValidName() && SessionManager.session.consent)
        {
            if (SessionManager.session.target == SessionTarget.create)
            {
                if (SessionManager.session.setup)
                {
                    Debug.Log("TODO next Screen");
                }
                else
                {
                    scCreateInput.gameObject.SetActive(true);
                }
            }
            else if (SessionManager.session.target == SessionTarget.join)
            {
                if (SessionManager.session.scenario != 0)
                {
                    Debug.Log("TODO next Screen");
                }
                else
                {
                    scJoinInput.gameObject.SetActive(true);
                }
            }
            else
            {
                mainMenuInput.gameObject.SetActive(true);
            }
        }
        else 
        {
            mainLandingInput.gameObject.SetActive(true);
        }
    }

    private void HideAll() {
        mainLandingInput.gameObject.SetActive(false);
        mainMenuInput.gameObject.SetActive(false);
        scCreateInput.gameObject.SetActive(false);
        scJoinInput.gameObject.SetActive(false);
    }
}
