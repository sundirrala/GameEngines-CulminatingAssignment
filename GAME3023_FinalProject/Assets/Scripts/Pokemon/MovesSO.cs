using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Moves/New Move")]
public class MovesSO : ScriptableObject
{
    [SerializeField]
    public string Name, Description;
    [SerializeField]
    public float Damage;
    [SerializeField]
    public int Usage, Accuracy;
    [SerializeField]
    public bool IsPriorityMove;

    [SerializeField]
    PokemonType Type;

    public IEffects Effects;
    public Target Target;

    public void Activate(Combatant caster, Combatant target)
    {

    }
  
}
