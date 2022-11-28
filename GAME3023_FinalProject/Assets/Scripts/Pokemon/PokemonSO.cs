using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/New Pokemon")]
public class PokemonSO : ScriptableObject
{
    [SerializeField]
    public Sprite FrontSide, BackSide;
    [SerializeField]
    public string name = "";
    [SerializeField]
    PokemonType type1, type2;
    [SerializeField]
    public int maxHealth, attack, defense, speed;

    [SerializeField]
    public List<MovesSO> moves;

    //Using these to be able to get the properties of a pokemon easily
    public string Name
    {
        get { return name; }
    }
    public PokemonType Type1
    {
        get { return type1; }
    }
    public PokemonType Type2
    {
        get { return type2; }
    }
    public int MaxHealth
    {
        get { return maxHealth; }
    }
    public int Attack
    {
        get { return attack; }
    }
    public int Defense
    {
        get { return defense; }
    }
    public int Speed
    {
        get { return speed; }
    }


}

public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Grass,
    Electric,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dark,
    Dragon,
    Steel
}
