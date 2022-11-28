using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public PokemonSO Base { get; set; }
    public int PokemonLevel { get; set; }
    public int CurrentHP { get; set; }

    List<Status> currentStatusEffects;

    [SerializeField]
    public List<Moves> currentMoves { get; set; }


    public Pokemon(PokemonSO SO, int level)
    {
        Base = SO;   //Setting the pokemon to the scriptable object
        PokemonLevel = level; //Same with level
        CurrentHP = MaxHealth; //Setting current hp to the pokemon's max
        //Possible ability to have list of learnable moves, time permitting

        currentMoves = new List<Moves>();
        foreach (var move in Base.moves)
        {
            currentMoves.Add(new Moves(move));
        }
    }

    /// This is the calculation GameFreak uses to calculate stats for pokemon at a certain level
    /// ((pokemon.Attack * PokemonLevel) / 100f) + 5
    public int MaxHealth
    {
        get { return Mathf.FloorToInt((Base.maxHealth * PokemonLevel) / 100f) + 10; }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * PokemonLevel) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * PokemonLevel) / 100f) + 5; }
    }
    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * PokemonLevel) / 100f) + 5; }
    }

}
