using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All Battle States will inherit from this
public abstract class BattleState : State
{
    protected BattleController owner;       //Field for reference to owner, which is battlecontroller
    public CameraRig cameraRig { get { return owner.cameraRig; } }
    public Board board { get { return owner.board; } }
    public LevelData levelData { get { return owner.levelData; } }
    public Transform tileSelectionIndicator { get { return owner.tileSelectionIndicator; } }
    public Point pos { get { return owner.pos;  } set { owner.pos = value; } }

    public AbilityMenuPanelController abilityMenuPanelController { get { return owner.abilityMenuPanelController; } }
    public Turn turn { get { return owner.turn; } }
    public List<Unit> units { get { return owner.units; } }

    protected virtual void Awake()
    {
        owner = GetComponent<BattleController>();   //Connect reference to BattleController
    }

    protected override void AddListeners()
    {
        InputController.moveEvent += OnMove;
        InputController.fireEvent += OnFire;
    }

    protected override void RemoveListeners()
    {
        InputController.moveEvent -= OnMove;
        InputController.fireEvent -= OnFire;
    }

    //Selects specified tile on gameboard if it exists
    //Updates field as well as moving indicator
    protected virtual void SelectTile(Point p)
    {
        if (pos==p || !board.tiles.ContainsKey(p))
        {
            return;
        }
        pos = p;
        tileSelectionIndicator.localPosition = board.tiles[p].center;
        Debug.Log("Tile: " + board.tiles[p].center);
    }

    //Event handlers for input events. Virtual with empty bodo so concrete subclasses not required to override.
    protected virtual void OnMove(object sender, InfoEventArgs<Point> e)
    {

    }

    protected virtual void OnFire(object sender, InfoEventArgs<int> e)
    {
        Debug.Log(tileSelectionIndicator.localPosition.x + " " + tileSelectionIndicator.localPosition.y + " " + tileSelectionIndicator.localPosition.z);
    }
}
