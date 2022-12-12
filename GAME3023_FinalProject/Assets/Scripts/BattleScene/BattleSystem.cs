using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    int TurnNumber;
    /// All UI Elements that are needed
    /// </Setting up all the ui and grabbing references to whatever I need>
    
    //List<Combatant> combatants = new List<Combatant>();
    [SerializeField]
    Combatant PlayerUnit, EnemyUnit;
    [SerializeField]
    HUD PlayerHUD, EnemyHUD;
    [SerializeField]
    DialogOptions DialogOptions;

    Moves PlayerMove, EnemyMove;
    bool PlayerMadeChoice = false;
    int TurnOrder;
    int CurrentMove;
    [SerializeField]
    List<Combatant> CalculateActionOrder;
    Queue<Moves> actionOrder;
    //Dictionary<Combatant, Moves> CurrentTurnMoves = new Dictionary<Combatant, Moves>();
    //List<Moves> CurrentTurnMoves = new List<Moves>();

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
            if (PlayerMadeChoice)
            {
                LoopThroughTurn();
            }
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
            //ui for move
            DialogOptions.MoveUsedUI();

            //Player uses a move
            PlayerMadeChoice = true;
            //Current move for later use
            //StartCoroutine(DialogOptions.TypeDialog($"{PlayerUnit.pokemon.Base.Name} used {PlayerUnit.pokemon.currentMoves[CurrentMove].Base.Name}!"));
            //PlayerUnit.UseMove(PlayerUnit, EnemyUnit, PlayerUnit.pokemon.currentMoves[CurrentMove], PlayerUnit.pokemon.currentMoves[CurrentMove].Base.Target);
            //OrderAttacks();
        }
    }

    void ActionTurnDialog(Combatant unit)
    {
        StartCoroutine(DialogOptions.TypeDialog($"{unit.pokemon.Base.Name} used {unit.pokemon.currentMoves[CurrentMove].Base.Name}!"));
        Debug.Log("Unit Used: " + unit.pokemon.currentMoves[CurrentMove].Base.Name);
    }

    public void OrderAttacks()
    {
        ///Checking Unit's Speed
        //Order the turn correctly
        //Player is SLOWER than enemy
        if (PlayerUnit.pokemon.Base.Speed < EnemyUnit.pokemon.Base.Speed)
        {
            StartCoroutine(EnemyUseMove(true)); //Will automatically go into the next units move
        }
        //Player is FASTER than enemy
        else if (PlayerUnit.pokemon.Base.Speed > EnemyUnit.pokemon.Base.Speed)
        {
            StartCoroutine(PlayerUseMove(true)); //Will automatically go into the next units move
        }
        //Player is TIED in speed w enemy
        else if (PlayerUnit.pokemon.Base.Speed == EnemyUnit.pokemon.Base.Speed)
        {
            //Player will go first bc why not
            StartCoroutine(PlayerUseMove(true)); //Will automatically go into the next units move
        }
        //LoopThroughTurn();


        //Now that we have the order of the moves...
        //I need to put into a queue which move will be used first, and so on
        //Once player has made their choice and the bool is true, get a random move the the enemy to use, and put it in the queue *DONE*
    }

    //Meant to go through each iteration of the action order queue and have said unit to use a move
    public void LoopThroughTurn()
    {
        OrderAttacks();
        PlayerMadeChoice = false;
        DialogOptions.ResetText();
        //StartCoroutine(DialogOptions.TypeDialog("Choose an action"));
    }

    IEnumerator PlayerUseMove(bool didGoFirst)
    {
        var Move = PlayerUnit.pokemon.currentMoves[CurrentMove];

        yield return DialogOptions.TypeDialog($"{PlayerUnit.pokemon.Base.Name} used {Move.Base.Name}!");

        yield return new WaitForSeconds(1f);

        bool isFainted = EnemyUnit.pokemon.TakeDamage(Move, PlayerUnit.pokemon);
        if (Move.Base.Damage > 0)
        {

            EnemyHUD.UpdateHP();
        }
        Debug.Log("Enemy Hp is " + EnemyUnit.pokemon.CurrentHP);

        if (isFainted)
        {
            yield return DialogOptions.TypeDialog($"{EnemyUnit.pokemon.Base.Name} Fainted!");
            PlayerWin();
        }
        if (didGoFirst)
        {
            StartCoroutine(EnemyUseMove(false));
        }
        else
        {
            yield return DialogOptions.TypeDialog($"Choose an action: ");
            DialogOptions.SetOptions(true);
        }
    }

    IEnumerator EnemyUseMove(bool DidGoFirst)
    {
        var Move = EnemyUnit.pokemon.GetRandomMove();

        yield return DialogOptions.TypeDialog($"{EnemyUnit.pokemon.Base.Name} used {Move.Base.Name}!");

        yield return new WaitForSeconds(1f);

        bool isFainted = PlayerUnit.pokemon.TakeDamage(Move, EnemyUnit.pokemon);
        if (Move.Base.Damage > 0)
        {
            PlayerHUD.UpdateHP();
        }
        Debug.Log("Player Hp is " + PlayerUnit.pokemon.CurrentHP);
        if (isFainted)
        {
            yield return DialogOptions.TypeDialog($"{PlayerUnit.pokemon.Base.Name} Fainted!");
            PlayerLose();

        }
        if (DidGoFirst)
        {
            StartCoroutine(PlayerUseMove(false));
        }
        else
        {
            yield return DialogOptions.TypeDialog($"Choose an action: ");
            DialogOptions.SetOptions(true);
        }
    }

    void PlayerWin()
    {
        Debug.Log("Player wins");
        if (PlayerUnit.pokemon.currentMoves.Count < 4)
        {
            PlayerUnit.pokemon.Base.moves.Add(EnemyUnit.pokemon.GetRandomMove().Base);
            Debug.Log("Player Added move: " + EnemyUnit.pokemon.GetRandomMove().Base.Name);
        }
        else if (PlayerUnit.pokemon.currentMoves.Count >= 4)
        {
            int rand = Random.Range(0, PlayerUnit.pokemon.Base.moves.Count);
            Debug.Log("Player removed the move: " + PlayerUnit.pokemon.Base.moves[rand].Name);
            PlayerUnit.pokemon.Base.moves.RemoveAt(rand);
            PlayerUnit.pokemon.Base.moves.Add(EnemyUnit.pokemon.GetRandomMove().Base);
            Debug.Log("Player Added the move: " + EnemyUnit.pokemon.GetRandomMove().Base.Name);
        }

        SceneManager.LoadScene("FinalForest", LoadSceneMode.Single);
    }   

    void PlayerLose()
    {
        Debug.Log("Player has lost");
        SceneManager.LoadScene("FinalForest", LoadSceneMode.Single);
    }
}


//TODO
//Figure out if there are any status effects
//Order all of the combatants speeds
//Based of the units speeds, use the moves chosen from fastest to slowest

//Once the queue is full from the moves, what do