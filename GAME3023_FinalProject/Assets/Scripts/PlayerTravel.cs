using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTravel : MonoBehaviour
{

    private string lastSpawn = "";

    public void SetSpawn(string spawn)
    {
        lastSpawn = spawn;
    }

    // Start is called before the first frame update
    private void Start()
    {
#if UNITY_EDITOR
        EditorKillClone();
#endif
        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnSceneLoadedAction;

        void OnSceneLoadedAction(Scene scene, LoadSceneMode loadMode)
        {
            if(lastSpawn != "")
            {
                bool transportSuccessful = false;
                PlayerSpawn[] spawnPoints = FindObjectsOfType<PlayerSpawn>();
                foreach(PlayerSpawn spawn in spawnPoints)
                {
                    if(spawn.name == lastSpawn)
                    {
                        transform.position = spawn.transform.position;
                        transportSuccessful = true;
                        break;
                    }

                }

                if (!transportSuccessful)
                {
                    throw new System.Exception("Could not find spawn point" + lastSpawn);
                }
            }

        }
    }

    private void EditorKillClone()
    {
        if(PlayerSpawn.Player != GetComponent<PlayerController>())
        {
            Destroy(this);
        }
    }

}
