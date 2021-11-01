using UnityEngine;

public class ScCreateInput : MonoBehaviour
{
    public void OnChangeRoomCount(int value)
    {
        SessionManager.session.rooms = (RoomCount)value;
    }

    public void OnChangeTextureDifficulty(int value)
    {
        SessionManager.session.textures = (TextureDifficulty)value;
    }

    public void OnChangeCaseReport(int value)
    {
        SessionManager.session.report = (CaseReport)value;
    }

    public void OnChangeTenant(int value)
    {
        SessionManager.session.tenant = (Tenant)value;
    }

    public void OnChangeRentalContract(int value)
    {
        SessionManager.session.contract = (RentalContract)value;
    }

    public void OnChangeHandoverProtocol(int value)
    {
        SessionManager.session.protocol = (HandoverProtocol)value;
    }


    public void ButtonSubmit()
    {
        SessionManager.session.setup = true;
        InputManager.instance.DisplayRequiredUI();
    }

    public void ButtonBack()
    {
        SessionManager.session.target = SessionTarget.unspecified;
        InputManager.instance.DisplayRequiredUI();
    }
}
