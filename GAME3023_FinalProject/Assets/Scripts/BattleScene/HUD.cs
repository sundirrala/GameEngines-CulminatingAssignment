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

    public void SetupHUD(Pokemon pokemon)
    {
        NameText.text = pokemon.Base.name;
        LevelText.text = "Lvl " + pokemon.PokemonLevel;
        HP.SetHP((float) pokemon.CurrentHP / pokemon.MaxHealth);
    }
}
