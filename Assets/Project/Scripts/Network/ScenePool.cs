using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePool
{
    private List<ScenePoolItem> subScenes;

    public ScenePool()
    {
        subScenes = new List<ScenePoolItem>();
    }

    public void Add(Scene scene)
    {
        subScenes.Add(new ScenePoolItem(scene));
    }

    public ScenePoolItem Get(int index)
    {
        return subScenes[index];
    }

    public ScenePoolItem Get(string id)
    {
        for (int i = 0; i < subScenes.Count; i++)
        {
            if (subScenes[i].id == id) return subScenes[i];
        }
        return null;
    }

    public ScenePoolItem GetUnused()
    {
        for (int i = 0; i < subScenes.Count; i++)
        {
            if (!subScenes[i].used)
            {
                subScenes[i].used = true;
                return subScenes[i];
            }
        }
        return null;
    }

    public void Clear()
    {
        subScenes.Clear();
    }

    public int Count => subScenes.Count;
}

public class ScenePoolItem
{
    public Scene scene;
    public string id;
    public bool used = false;
    public NetworkedScenario scenario;

    public ScenePoolItem(Scene scene)
    {
        this.scene = scene;
        this.id = System.Guid.NewGuid().ToString("n").Substring(0, 4);
        this.used = false;
    }
}
