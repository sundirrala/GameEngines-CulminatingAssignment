using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    int TurnNumber;
    //List<Combatant> combatants;
    //Queue<Combatant> actionOrder;

    //event OnEffectActivated<IEffects>;
    public delegate void OnTurnBegin(int TurnNumber);
    public static event OnTurnBegin onTurnBegin;
}
