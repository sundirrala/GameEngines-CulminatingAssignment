using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    [SerializeField]
    PokemonSO pokemonBase;
    [SerializeField]
    int pokemonLevel;
    
    public PokemonSO Base { get; set; }
    public int PokemonLevel { get; set; }
    public int CurrentHP { get; set; }

    List<Status> currentStatusEffects;

    [SerializeField]
    public List<Moves> currentMoves { get; set; }


    

    public Pokemon(PokemonSO so, int level)
    {
        Base = so;
        PokemonLevel = level;
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

    //Function that tells if the pokemon has fainted or not
    public bool TakeDamage(Moves move, Pokemon attacker)
    {
        float modifiers = Random.Range(0.85f, 1f);  //Pokemon has a damage range for moves
        float attack = (2 * attacker.PokemonLevel + 10) / 250f;
        float defense = attack * move.Base.Damage * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(defense * modifiers);

        CurrentHP -= damage;
        Debug.Log(attacker + " has done " + damage + "to the other pokemon. Current HP is " + CurrentHP);
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            return true;
        }

        return false;
    }

    //For AI move choice
    public Moves GetRandomMove()
    {
        int rand = Random.Range(0, currentMoves.Count);
        return currentMoves[rand];
    }
}
