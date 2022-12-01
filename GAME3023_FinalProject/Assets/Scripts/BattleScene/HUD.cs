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

    Pokemon Pokemon;

    public void SetupHUD(Pokemon pokemon)
    {
        Pokemon = pokemon;

        NameText.text = pokemon.Base.name;
        LevelText.text = "Lvl " + pokemon.PokemonLevel;
        HP.SetHP((float) pokemon.CurrentHP / pokemon.MaxHealth);
    }

    public void UpdateHP()
    {
        HP.SetHP((float)Pokemon.CurrentHP / Pokemon.MaxHealth);
    }
}
