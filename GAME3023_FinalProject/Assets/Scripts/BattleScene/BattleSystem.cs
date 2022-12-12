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
    Combatant  EnemyUnit;
    Combatant PlayerUnit;
    [SerializeField]
    HUD PlayerHUD, EnemyHUD;
    [SerializeField]
    DialogOptions DialogOptions;


    //[SerializeField]
    //PokemonSO testbase;
    //[SerializeField]
    //int leveltest;
    Pokemon Player, Enemy;

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

    public void SetupUnits(Pokemon player, Pokemon enemy)
    {
        Player = player;
        Enemy = enemy;
        PlayerUnit.SetisPlayerUnit(true);
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        
        PlayerUnit.Setup(Player);
        EnemyUnit.Setup(Enemy);

        PlayerHUD.SetupHUD(PlayerUnit.Pokemon);
        EnemyHUD.SetupHUD(EnemyUnit.Pokemon);

        //DialogOptions.SetMoveList(PlayerUnit.pokemon.currentMoves);
        DialogOptions.SetMoveName(PlayerUnit.Pokemon.currentMoves);

        yield return DialogOptions.TypeDialog($"A wild {EnemyUnit.Pokemon.Base.name} appeared!");

    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CurrentMove < PlayerUnit.Pokemon.currentMoves.Count - 1)
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
            if (CurrentMove < PlayerUnit.Pokemon.currentMoves.Count - 2)
            {
                CurrentMove += 2;
            }
        }

        DialogOptions.UpdateMoveSelection(CurrentMove, PlayerUnit.Pokemon.currentMoves[CurrentMove]);

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
        StartCoroutine(DialogOptions.TypeDialog($"{unit.Pokemon.Base.Name} used {unit.Pokemon.currentMoves[CurrentMove].Base.Name}!"));
        Debug.Log("Unit Used: " + unit.Pokemon.currentMoves[CurrentMove].Base.Name);
    }

    public void OrderAttacks()
    {
        ///Checking Unit's Speed
        //Order the turn correctly
        //Player is SLOWER than enemy
        if (PlayerUnit.Pokemon.Base.Speed < EnemyUnit.Pokemon.Base.Speed)
        {
            StartCoroutine(EnemyUseMove(true)); //Will automatically go into the next units move
        }
        //Player is FASTER than enemy
        else if (PlayerUnit.Pokemon.Base.Speed > EnemyUnit.Pokemon.Base.Speed)
        {
            StartCoroutine(PlayerUseMove(true)); //Will automatically go into the next units move
        }
        //Player is TIED in speed w enemy
        else if (PlayerUnit.Pokemon.Base.Speed == EnemyUnit.Pokemon.Base.Speed)
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
        var Move = PlayerUnit.Pokemon.currentMoves[CurrentMove];

        yield return DialogOptions.TypeDialog($"{PlayerUnit.Pokemon.Base.Name} used {Move.Base.Name}!");

        yield return new WaitForSeconds(1f);

        bool isFainted = EnemyUnit.Pokemon.TakeDamage(Move, PlayerUnit.Pokemon);
        if (Move.Base.Damage > 0)
        {
            EnemyHUD.UpdateHP();
        }
        Debug.Log("Enemy Hp is " + EnemyUnit.Pokemon.CurrentHP);

        if (isFainted)
        {
            yield return DialogOptions.TypeDialog($"{EnemyUnit.Pokemon.Base.Name} Fainted!");
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
        var Move = EnemyUnit.Pokemon.GetRandomMove();

        yield return DialogOptions.TypeDialog($"{EnemyUnit.Pokemon.Base.Name} used {Move.Base.Name}!");

        yield return new WaitForSeconds(1f);

        bool isFainted = PlayerUnit.Pokemon.TakeDamage(Move, EnemyUnit.Pokemon);
        Debug.Log("Player Hp is " + PlayerUnit.Pokemon.CurrentHP);
        if (Move.Base.Damage > 0)
        {
            PlayerHUD.UpdateHP();
        }
        if (isFainted)
        {
            yield return DialogOptions.TypeDialog($"{PlayerUnit.Pokemon.Base.Name} Fainted!");
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

    public void PlayerWin()
    {
        Debug.Log("Player wins");
        if (PlayerUnit.Pokemon.currentMoves.Count < 3)
        {
            
            PlayerUnit.Pokemon.Base.moves.Add(EnemyUnit.Pokemon.GetRandomMove().Base);

            Debug.Log("Player Added the move: " + EnemyUnit.Pokemon.GetRandomMove().Base.Name);
        }
        else if (PlayerUnit.Pokemon.currentMoves.Count == 3)
        {
            int rand = Random.Range(0, PlayerUnit.Pokemon.Base.moves.Count);
            Debug.Log("Player removed the move: " + PlayerUnit.Pokemon.Base.moves[rand].Name);
            PlayerUnit.Pokemon.Base.moves.RemoveAt(rand);
            PlayerUnit.Pokemon.Base.moves.Add(EnemyUnit.Pokemon.GetRandomMove().Base);
            Debug.Log("Player Added the move: " + EnemyUnit.Pokemon.GetRandomMove().Base.Name);

        }

        //To change to whereever we want the player to go after they win. If going to main menu may want to clear saved data or somethin
        SceneManager.LoadScene("FinalWorldMap", LoadSceneMode.Single);
    }

    public void PlayerLose()
    {
        Debug.Log("Player lost");
        //To change to whereever we want the player to go after they lose. If going to main menu may want to clear saved data or somethin
        SceneManager.LoadScene("FinalWorldMap", LoadSceneMode.Single);
    }

}




//TODO
//Figure out if there are any status effects
//Order all of the combatants speeds
//Based of the units speeds, use the moves chosen from fastest to slowest

//Once the queue is full from the moves, what do