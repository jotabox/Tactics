using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseActionState : State
{


    public override void Enter()
    {
        base.Enter();
        InputController.instance.OnMove += OnMove;
    }
    public override void Exit()
    {
        base.Enter();
        InputController.instance.OnMove -= OnMove;
    }

    void OnMove(object sender, object args)
    {

    }

}
