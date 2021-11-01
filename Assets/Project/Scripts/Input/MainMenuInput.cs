using System;
using UnityEngine;
using TMPro;

public class MainMenuInput : MonoBehaviour
{
    public TMP_Text text;

    public void OnEnable()
    {
        text.text = String.Format("Hallo {0}. Was möchtest du tun?", SessionManager.session.name);
    }

    public void ButtonJoin() 
    {
        SessionManager.session.target = SessionTarget.join;
        InputManager.instance.DisplayRequiredUI();
    }

    public void ButtonCreate()
    {
        SessionManager.session.target = SessionTarget.create;
        InputManager.instance.DisplayRequiredUI();
    }

    public void ButtonSignOut()
    {
        SessionManager.session.Clear();
        InputManager.instance.DisplayRequiredUI();
    }
}
