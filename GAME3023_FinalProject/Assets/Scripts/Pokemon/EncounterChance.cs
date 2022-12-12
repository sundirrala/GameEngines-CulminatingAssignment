using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EncounterChance : MonoBehaviour
{
    [SerializeField]
    List<Encounters> Encounters;

    private void Start()
    {
        int totalChance = 0;
        foreach (var encounter in Encounters)
        {
            encounter.lowerChance = totalChance;
            encounter.higherChance = totalChance + encounter.chance;

            totalChance = totalChance + encounter.chance;
        }
    }

    public Pokemon GetRandomEncounter()
    {
        int rand = Random.Range(1, 101);
        var pokemonEncounter = Encounters.First(p => rand >= p.lowerChance && rand <= p.higherChance);

        var levelRange = pokemonEncounter.levelRange;
        int level = levelRange.y == 0 ? levelRange.x : Random.Range(levelRange.x, levelRange.y + 1);

        var WildPokemon = new Pokemon(pokemonEncounter.pokemon, level);

        //WildPokemon.Init();
        return WildPokemon;
    }

}

[System.Serializable]
public class Encounters
{
    public PokemonSO pokemon;
    public Vector2Int levelRange;
    public int chance;

    public int lowerChance { get; set; }
    public int higherChance { get; set; }
}
