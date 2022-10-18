using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    private static PlayerController player;

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
}
