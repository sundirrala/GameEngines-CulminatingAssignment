using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    int TurnNumber;
    /// All UI Elements that are needed
    /// </Setting up all the ui and grabbing references to whatever I need>
    [SerializeField]
    Combatant PlayerUnit, EnemyUnit;
    [SerializeField]
    HUD PlayerHUD, EnemyHUD;
    [SerializeField]
    DialogOptions DialogOptions;

    /// 
    int TurnOrder;
    int CurrentMove;
    //List<Combatant> combatants;

    //Queue<Combatant> actionOrder;

    //event OnEffectActivated<IEffects>;
    public delegate void OnTurnBegin(int TurnNumber);
    public static event OnTurnBegin onTurnBegin;

    private void Start()
    {
        SetupUI();
    }

    private void Update()
    {
        if (DialogOptions.IsInFight)
        {
            HandleMoveSelection();
        }
    }

    public void SetupUI()
    {
        PlayerUnit.Setup();
        EnemyUnit.Setup();

        PlayerHUD.SetupHUD(PlayerUnit.pokemon);
        EnemyHUD.SetupHUD(EnemyUnit.pokemon);

        //DialogOptions.SetMoveList(PlayerUnit.pokemon.currentMoves);
        DialogOptions.SetMoveName(PlayerUnit.pokemon.currentMoves);

        StartCoroutine(DialogOptions.TypeDialog($"A wild {EnemyUnit.pokemon.Base.name} appeared!"));

    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CurrentMove < PlayerUnit.pokemon.currentMoves.Count - 1)
            {
                ++CurrentMove;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CurrentMove > 0)
            {
                --CurrentMove;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentMove > 1)
            {
                CurrentMove -= 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentMove < PlayerUnit.pokemon.currentMoves.Count - 2)
            {
                CurrentMove += 2;
            }
        }

        DialogOptions.UpdateMoveSelection(CurrentMove, PlayerUnit.pokemon.currentMoves[CurrentMove]);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Add to the queue for moves happening this turn
            DialogOptions.ResetText();
            PerformMove(PlayerUnit, PlayerUnit.pokemon.currentMoves[CurrentMove].Base.Target); //Takes the players move to put into queue
        }

        void TargetChoice(Target target)
        {
            if (target == Target.Self)
            {

            }
            if (target == Target.Both)
            {

            }
            if (target == Target.Other)
            {

            }
            if (target == Target.All)
            {

            }
        }

        //Going to be called multiple times in a turn in queue
        void PerformMove(Combatant combatant, Target target)
        {
            var move = combatant.pokemon.currentMoves[CurrentMove]; //To Change later, find a way for enemy to choose move
            //Possibly switch to a corutine or have one to display which move was used
        }
    }

}
