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
        Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
        SelectTile(p);
        SpawnTestUnits(); // This is new
        yield return null;
        owner.ChangeState<SelectUnitState>(); // This is changed
    }


    //Temp code to test. REMOVE LATER   
    void SpawnTestUnits()
    {
        string[] jobs = new string[] { "Rogue", "Warrior", "Wizard" };
        for (int i = 0; i < jobs.Length; ++i)
        {
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;

            Stats s = instance.AddComponent<Stats>();
            s[StatTypes.LVL] = 1;

            GameObject jobPrefab = Resources.Load<GameObject>("Jobs/" + jobs[i]);
            GameObject jobInstance = Instantiate(jobPrefab) as GameObject;
            jobInstance.transform.SetParent(instance.transform);

            Job job = jobInstance.GetComponent<Job>();
            job.Employ();
            job.LoadDefaultStats();

            Point p = new Point((int)levelData.tiles[i].x, (int)levelData.tiles[i].z);

            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();

            instance.AddComponent<WalkMovement>();

            units.Add(unit);

                  Rank rank = instance.AddComponent<Rank>();
                  rank.Init (10);
        }
    }
}
