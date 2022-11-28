using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour
{
    public MovesSO Base { get; set; }

    public Moves(MovesSO move)
    {
        Base = move;
    }
}
