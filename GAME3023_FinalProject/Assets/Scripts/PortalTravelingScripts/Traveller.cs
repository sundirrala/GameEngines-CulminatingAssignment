using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Traveller : MonoBehaviour
{
    private string lastSpawnPoint = "";

    public void SetSpawn(string spawn)
    {
        lastSpawnPoint = spawn;
    }

    private void Start()
    {
#if UNITY_EDITOR
        EditorKillClones();
#endif
        DontDestroyOnLoad(gameObject); // this tells unity that this gameObject should not be cleaned up with all the others when changing scenes
        SceneManager.sceneLoaded += OnSceneLoadedAction;
    }

    void OnSceneLoadedAction(Scene scene, LoadSceneMode loadMode)
    {
        if(lastSpawnPoint != "")
        {
            // Go through all the spawn locations to find the one given
            bool transportSuccessful = false;

            PlayerSpawn[] spawnPoints = FindObjectsOfType<PlayerSpawn>(); // find all possible spawn locations
            foreach(PlayerSpawn spawn in spawnPoints)
            {
                if(spawn.name == lastSpawnPoint)
                {
                    // go to that spawn point.
                    transform.position = spawn.transform.position;
                    transportSuccessful = true;
                    break;
                }
            }

            if(!transportSuccessful)
            {
                throw new System.Exception("Could not find spawn point: " + lastSpawnPoint);
            }
        }
    }

    /// <summary>
    /// this is a convenience function only to be used while we keep Player Characters in all the scenes.
    /// </summary>
    private void EditorKillClones()
    {
        if (PlayerSpawn.Player != GetComponent<PlayerController>())
        {
            Destroy(this); // if we aren't the original, d i e .
        }
    }
}
