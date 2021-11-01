using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : Singleton<SessionManager>
{
    public static Session session;
    
    void Awake()
    {
        session = new Session();
    }
}
