using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField]
    TMP_Text NameText, LevelText;
    [SerializeField]
    HPBar HP;

    Pokemon _Pokemon;

    public void SetupHUD(Pokemon pokemon)
    {
        _Pokemon = pokemon;

        NameText.text = pokemon.Base.name;
        LevelText.text = "Lvl " + pokemon.PokemonLevel;
        HP.SetHP((float) pokemon.CurrentHP / pokemon.MaxHealth);
    }

    public void UpdateHP()
    {
        HP.SetHP((float)_Pokemon.CurrentHP / _Pokemon.MaxHealth);
    }
}
