using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Player, BattleSystem;
    [SerializeField, Tooltip("Used for reference to the encounters on this route")]
    EncounterChance encounters;


    // Start is called before the first frame update
    void Start()
    {
        BattleSystem.SetActive(false);
        Player.GetComponent<PlayerController>().OnEncountered += StartBattle;
       
    }

    void StartBattle()
    {
        Debug.Log("Start Battle called");
        BattleSystem.SetActive(true);
        BattleSystem.GetComponent<BattleSystem>().SetupUnits(Player.GetComponent<Pokemon>(), encounters.GetRandomEncounter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
