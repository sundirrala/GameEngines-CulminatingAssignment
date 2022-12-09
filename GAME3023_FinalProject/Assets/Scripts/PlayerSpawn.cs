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
    public static PlayerController Player => player;

    private void Awake()
    {
        if (player == null)
        {
            GameObject newObject = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            player = newObject.GetComponent<PlayerController>();
        }
    }

}
