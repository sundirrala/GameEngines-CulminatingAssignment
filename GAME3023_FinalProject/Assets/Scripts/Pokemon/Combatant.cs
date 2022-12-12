using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combatant : MonoBehaviour
{
    [SerializeField]
    PokemonSO pokemonBase;
    [SerializeField]
    int Level;
    [SerializeField]
    bool isPlayerUnit;
    bool isFainted = false;


    
    public bool IsFainted { get { return isFainted; } }

    public Pokemon pokemon { get; set; }
    
    public PokemonSO PokemonBase { get; set; }
    public int PokemonLevel { get; set; }

    public void Setup()
    {
        pokemon = new Pokemon(pokemonBase, Level);
        if (isPlayerUnit)
        {
            GetComponent<Image>().sprite = pokemon.Base.BackSide;
        }
        else
        {
            GetComponent<Image>().sprite = pokemon.Base.FrontSide;
        }
    }

    public void UseMove(Combatant attacker, Combatant targetEnemy, Moves move, Target target)
    {
        //All UI or text stuff thats being done when return is pressed
       isFainted = targetEnemy.pokemon.TakeDamage(move, attacker.pokemon);

    }
}
