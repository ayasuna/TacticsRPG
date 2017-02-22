﻿using System.Collections;
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
        Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
        SelectTile(p);
        SpawnTestUnits(); // This is new
        yield return null;
        owner.ChangeState<SelectUnitState>(); // This is changed
    }


    //Temp code to test. REMOVE LATER   
    void SpawnTestUnits()
    {
        System.Type[] components = new System.Type[] { typeof(WalkMovement), typeof(FlyMovement), typeof(TeleportMovement) };
        for (int i = 0; i < 3; ++i)
        {
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;

            Point p = new Point((int)levelData.tiles[i].x, (int)levelData.tiles[i].z);

            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();

            Movement m = instance.AddComponent(components[i]) as Movement;
            m.range = 5;
            m.jumpHeight = 1;

            units.Add(unit);
        }
    }
}
