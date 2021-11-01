using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScJoinInput : MonoBehaviour
{
    public void ButtonSubmit()
    {
        // TODO
        InputManager.instance.DisplayRequiredUI();
    }

    public void ButtonBack()
    {
        SessionManager.session.target = SessionTarget.unspecified;
        InputManager.instance.DisplayRequiredUI();
    }
}
