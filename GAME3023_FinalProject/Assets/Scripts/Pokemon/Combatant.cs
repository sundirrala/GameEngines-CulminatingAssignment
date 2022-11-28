using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combatant : MonoBehaviour
{
    [SerializeField]
    PokemonSO pokemonBase;
    [SerializeField]
    int level;
    [SerializeField]
    bool isPlayerUnit;

    

    public Pokemon pokemon { get; set; }

    public void Setup()
    {
        pokemon = new Pokemon(pokemonBase, level);
        if (isPlayerUnit)
        {
            GetComponent<Image>().sprite = pokemon.Base.BackSide;
        }
        else
        {
            GetComponent<Image>().sprite = pokemon.Base.FrontSide;
        }
        

    }
}
