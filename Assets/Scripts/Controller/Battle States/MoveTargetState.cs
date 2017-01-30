using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to allow interaction with board with Input Controller

public class MoveTargetState : BattleState
{

	protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
    }
	
}
