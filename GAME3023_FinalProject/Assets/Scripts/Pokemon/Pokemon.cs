using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public PokemonSO pokemon;
    int PokemonLevel;

    List<Status> currentStatusEffects;
    List<Moves> currentMoves;

    public Pokemon(PokemonSO SO, int level)
    {
        pokemon = SO;   //Setting the pokemon to the scriptable object
        PokemonLevel = level; //Same with level
    }

    /// This is the calculation GameFreak uses to calculate stats for pokemon at a certain level
    /// ((pokemon.Attack * PokemonLevel) / 100f) + 5
    public int MaxHealth
    {
        get { return Mathf.FloorToInt((pokemon.maxHealth * PokemonLevel) / 100f) + 10; }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((pokemon.Attack * PokemonLevel) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((pokemon.Defense * PokemonLevel) / 100f) + 5; }
    }
    public int Speed
    {
        get { return Mathf.FloorToInt((pokemon.Speed * PokemonLevel) / 100f) + 5; }
    }

}
