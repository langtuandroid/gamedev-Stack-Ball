using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyInstances : MonoBehaviour
{
    public List<string> sceneName = new List<string>();   
    public int currentSceneId;

    public void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        for (int i = 0; i < sceneName.Count; i++)
        {
            if (scene.name == sceneName[i])
                currentSceneId = i;           
        }
       
        if (scene.name != sceneName[currentSceneId])
        {          
                Destroy(this.gameObject);                 
        }
    }
}
