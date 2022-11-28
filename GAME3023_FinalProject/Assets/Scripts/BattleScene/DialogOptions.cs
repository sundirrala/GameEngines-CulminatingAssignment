using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogOptions : MonoBehaviour
{
    [SerializeField]
    TMP_Text dialog, DamageText,UseText, TypeText;
    [SerializeField]
    Color higlightedColour;
    public string name, damage, uses, type;
    [SerializeField]
    GameObject MoveSelection, Options, MoveDetails;
    [SerializeField, Tooltip("Always keep this list at 4!")]
    List<TMP_Text> MoveText;
    [SerializeField]
    int LettersPerSecond;

    List<Moves> pokemonMoves = new List<Moves>();

    bool isInFight;

    public bool IsInFight { get { return isInFight; } }


    private void Start()
    {
        //Different from ResetText bc at start player needs to know what they have encountered!
        dialog.enabled = true;
        Options.SetActive(true);

        isInFight = false;
        MoveSelection.SetActive(false);
        MoveDetails.SetActive(false);
    }

    private void Update()
    {
        if (isInFight)
        {
            if (Input.GetKeyDown(KeyCode.B))    //If the user wants to back out of fight, they can press "B"
            {
                ResetText();
                Options.SetActive(true);
                dialog.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dialog Text movement and setting it up
    /// </summary>

    public void SetDialog(string text)
    {
        dialog.text = text;
    }

    public IEnumerator TypeDialog(string text)
    {
        dialog.text = "";
        foreach (var letter in text.ToCharArray())
        {
            dialog.text += letter;
            yield return new WaitForSeconds(1f/LettersPerSecond);
        }
    }

    public void SetMoveName(List<Moves> moves)
    {
        for (int i = 0; i < MoveText.Count; ++i)
        {
            if (i < moves.Count)
            {
                MoveText[i].GetComponentInChildren<TMP_Text>().text = (moves[i].Base.Name);


            }
            else
            {
                MoveText[i].GetComponentInChildren<TMP_Text>().text = ("--"); //If there is no move for that slot (ie the pokemon has less than 4 moves) make the name --
            }
        }
    }

    public void UpdateMoveSelection(int selectedMove, Moves move)
    {
        for (int i = 0; i < MoveText.Count; ++i)
        {
            if (i == selectedMove)
            {
                MoveText[i].color = higlightedColour;
            }
            else
            {
                MoveText[i].color = Color.black;
            }
        }
        DamageText.SetText("Damage: " + move.Base.Damage.ToString());
        TypeText.SetText("Type: " + move.Base.Type.ToString());
        UseText.SetText("Uses: " + move.Base.Usage.ToString());
    }


    public void SetMoveList(List<Moves> moves)
    {
        for (int i = 0; i < moves.Count; i++)
        {
            moves[i] = pokemonMoves[i];
        }
    }

    /// <summary>
    /// Basic Functions for buttons and setting active GO / text
    /// </summary>

    public void ResetText()
    {
        //OPTIONS ON
        //MOVE DETAILS OFF
        //MOVES OFF
        //DIALOG TEXT OFF

        MoveSelection.SetActive(false);
        MoveDetails.SetActive(false);
        dialog.enabled = false;
        isInFight = false;
    }

    public void Fight()
    {
        Debug.Log("Fight Button Bressed");

        MoveSelection.SetActive(true);
        MoveDetails.SetActive(true);
        isInFight = true;

        Options.SetActive(false);
        dialog.enabled = false;
    }

    public void Run()
    {
        Debug.Log("Run Button Pressed");
        //Load Overworld Scene
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        for (int i = 0; i < MoveText.Count; i++)
        {
            if (MoveText[i] == this)
            {
                Debug.Log("Exit: " + MoveText[i].GetComponentInChildren<TMP_Text>().text);
            }
        }
    }
}
