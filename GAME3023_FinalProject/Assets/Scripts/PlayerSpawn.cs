using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    /// <summary>
    ///  This is the prefab of the player character we will use to spawn the original
    /// </summary>
    [SerializeField]
    private GameObject playerPrefab;

    private static PlayerController player = null;
    /// <summary>
    /// Access to the Original Player
    /// </summary>
    public static PlayerController Player
    {
        get { return player; }
        private set { }
    }

    private void Awake()
    {
        if(player == null)
        {
            GameObject newObject = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            player = newObject.GetComponent<PlayerController>();
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(GetComponent<PlayerController>());
    }

    public void LoadPlayer()
    {
        SavePlayerData data = SaveSystem.LoadPlayer();

        Vector3 pos;
        pos.x = data.pos[0];
        pos.y = data.pos[1];
        pos.z = data.pos[2];
        transform.position = pos;

        Debug.Log("The player's position is " + pos);
    }
}
