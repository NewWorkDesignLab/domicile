using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }
    public bool dontDestroyOnLoad = true;

    protected virtual void Awake ()
    {
        if (instance != null)
        {
            Debug.Log ("[Singleton Awake] A instance of type \"" + instance.GetType () + "\" already exists.");
            Destroy (this);
            return;
        }
        instance = this as T;
        if (dontDestroyOnLoad) DontDestroyOnLoad (this.gameObject);
    }
}