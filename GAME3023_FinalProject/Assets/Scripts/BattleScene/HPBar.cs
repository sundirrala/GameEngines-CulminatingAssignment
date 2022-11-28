using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    GameObject Health;

    private void Awake()
    {
        Health = GameObject.Find("Health");
    }


    public void SetHP(float HP)
    {
        
        Health.transform.localScale = new Vector3(HP, 1.0f);
    }
}
