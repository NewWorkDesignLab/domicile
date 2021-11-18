using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionInstance : Singleton<SessionInstance>
{
    public Session session;
    
    protected override void Awake()
    {
        base.Awake();
        ClearSession();
    }

    public void ClearSession()
    {
        session = new Session();
    }
}
