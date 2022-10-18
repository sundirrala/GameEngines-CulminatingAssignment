using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPortalTravel : MonoBehaviour
{
    public string targetPortal = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerTravel trav = collision.GetComponent<PlayerTravel>();
        if(trav != null)
        {
            Debug.Log("Portal Warping: " + trav.gameObject.name);
            trav.SetSpawn(targetPortal);
            SceneManager.LoadScene(tag, LoadSceneMode.Single);
        }
    }
}
