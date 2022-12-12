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
    bool isFainted = false;


    
    public bool IsFainted { get { return isFainted; } }
<<<<<<< HEAD

    public bool SetisPlayerUnit(bool playerunit) 
    { 
        isPlayerUnit = playerunit;
        return isPlayerUnit;
    }
    public Pokemon Pokemon { get; set; }
    
    public PokemonSO PokemonBase { get; set; }
    public int PokemonLevel { get; set; }

    public void Setup(Pokemon pokemon)
    {
        pokemon = Pokemon;
=======
    public Pokemon pokemon { get; set; }

    public void Setup()
    {
        pokemon = new Pokemon(pokemonBase, level);
>>>>>>> parent of e8d20663 (Merge branch 'main' of https://github.com/sundirrala/GameEngines-CulminatingAssignment)
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
       isFainted = targetEnemy.Pokemon.TakeDamage(move, attacker.Pokemon);

    }
}
