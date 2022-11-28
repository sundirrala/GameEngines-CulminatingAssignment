using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleSceneButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    DialogOptions DialogOptions;
    [SerializeField]
    TMP_Text  NameText, DamageText, UseText, TypeText;
    Moves move;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(UpdateMove);
    }

    public void SetMove(Moves moves)
    {
        move = moves;
    }

    void UpdateMove()
    {
        Debug.Log("Move " + this.GetComponentInChildren<TMP_Text>() +" was used.");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DamageText.SetText("Damage: " + DialogOptions.damage);
        TypeText.SetText("Type: " + DialogOptions.type);
        UseText.SetText("Uses: " + DialogOptions.uses);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DamageText.SetText("--");
        TypeText.SetText("--");
        UseText.SetText("--");
    }
}
