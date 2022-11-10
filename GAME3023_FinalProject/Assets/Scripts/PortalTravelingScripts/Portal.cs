// this portal script will allow any Traveler to touch itm then it 3will send them to specified location in a specified scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This Portal leads to the scene with the same name as its Tag, to a portal with the same Tag as this one
/// </summary>
public class Portal : MonoBehaviour
{

    // target scene
    // target location within scene
    // who can travel?

    [SerializeField]
    string targetSpawn = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Portal triggered with: " + collision.gameObject.name);
        Traveller traveller = collision.GetComponent<Traveller>();

        if(traveller != null)
        {
            Debug.Log("Portal warping " + traveller.gameObject.name);
            traveller.SetSpawn(targetSpawn);
            SceneManager.LoadScene(tag, LoadSceneMode.Single);
        }
    }
}
