using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{

    public override void Enter()
    {
        base.Enter();
        InputController.instance.OnMove += OnMove;
    }

    void OnMove(object sender , object args)
    {
        Vector2 input = (Vector2)args;
        Debug.Log("Moveu: " + input);
    }
}
