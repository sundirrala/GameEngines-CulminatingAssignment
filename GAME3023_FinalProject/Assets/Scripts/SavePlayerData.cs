using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public SavePlayerData (PlayerController player)
    {
        pos = new SVector2();
        pos.x = player.transform.position.x;
        pos.y = player.transform.position.y;
    }
}
