using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SVector2
{
    public float x;
    public float y;
}


[System.Serializable]
public class SavePlayerData
{
    public SVector2 pos;
    public Scene activeScene;
    public string activeSceneName;

    public SavePlayerData (PlayerController player)
    {
 
        activeScene = SceneManager.GetActiveScene(); 
        activeSceneName = activeScene.name;

        pos = new SVector2();
        pos.x = player.transform.position.x;
        pos.y = player.transform.position.y;
    }

}
