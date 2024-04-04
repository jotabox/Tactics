using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{

    public override void Enter()
    {
        base.Enter();
        InputController.instance.OnMove += OnMove;
        CheckNullPosition();
    }
    public override void Exit()
    {
        base.Enter();
        InputController.instance.OnMove -= OnMove;
    }

    void OnMove(object sender , object args)
    {
        //Vector3 input = (Vector3)args;
        //Vector3Int inputInt = new Vector3Int((int)input.x, (int)input.y, (int)input.z);

        Vector3Int input = (Vector3Int)args;
        //TileLogic tile = Board.GetTile(SelectorSprite.instance.position + inputInt);
        TileLogic tile = Board.GetTile(SelectorSprite.instance.position + input);

        if (tile != null)
        {
            SelectorSprite.instance.tileLogic = tile;
            SelectorSprite.instance.spriteRenderer.sortingOrder = tile.contentOrder;
            SelectorSprite.instance.transform.position = tile.worldPos;
        }
    }


    void CheckNullPosition()
    {
        if(SelectorSprite.instance.tileLogic == null)
        {
            TileLogic tile = Board.GetTile(new Vector3Int(0, 0, 0));

            SelectorSprite.instance.tileLogic = tile;
            SelectorSprite.instance.spriteRenderer.sortingOrder = tile.contentOrder;
            SelectorSprite.instance.transform.position = tile.worldPos;

        }
    }
}
