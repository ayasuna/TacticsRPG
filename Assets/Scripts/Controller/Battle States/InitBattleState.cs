using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BattleState
{
    //Override base class's Enter method to add additional functionality
    //Important to also call base class' Enter method so important functionaly won't be missed
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init()); //Wait one frame by using coroutine in order to get Init to change to next state
    }

    IEnumerator Init()
    {
        board.Load(levelData);
        Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].y);
        SelectTile(p);
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }
}
