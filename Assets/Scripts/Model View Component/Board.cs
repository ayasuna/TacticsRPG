using System;
using System.Collections.Generic;
using UnityEngine;

//Script that can load LevelData and create game board at runtime

public class Board : MonoBehaviour {

	[SerializeField] GameObject tilePrefab;
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

    //Private array of points to denote cardinal directions
    Point[] dirs = new Point[4]
    {
        new Point(0, 1),
        new Point(0, -1),
        new Point(1, 0),
        new Point(-1, 0)
    };

    //Define colors for tiles player can move to, and currently sielected tile.
    public Color selectedTileColor = new Color(0, 1, 1, 1);
    public Color defaultTileColor = new Color(1, 1, 1, 1);

    public void Load(LevelData data)
    {
        //Use reference of a Tile prefab to instantiate all tiles
        for (int i=0; i<data.tiles.Count; i++)
        {
            GameObject instance = Instantiate(tilePrefab) as GameObject;
            Tile t = instance.GetComponent<Tile>();
            t.Load(data.tiles[i]);  //save tiles to dictionary based on location
            tiles.Add(t.pos, t);
        }
    }

    /* This method will return a list of Tiles, starting from a specific tile, which meet a certain criteria. 
     * The criteria to be met is passed along as a delegate via the generic Func delegate which takes as 
     * parameters the segment of a potential path (where you would move from and where you would move to) and 
     * returns a bool indicating whether or not to allow the movement. 
     */

	public List<Tile> Search(Tile start, Func<Tile, Tile, bool> addTile)
    {
        List<Tile> retValue = new List<Tile>();
        retValue.Add(start);

        //Begin search

        ClearSearch();

        //Define two queues of tiles. One for tile to be checked now, one for tile being checked later
        Queue<Tile> checkNext = new Queue<Tile>();
        Queue<Tile> checkNow = new Queue<Tile>();

        //Set values for initial tile

        start.distance = 0;
        checkNow.Enqueue(start);

        //Main loop: Dequeue tile and apply logic operations
        while (checkNow.Count > 0)
        {
            Tile t = checkNow.Dequeue();
            //For loop to get tiles in each direction adjacent to the tile being checked
            for (int i = 0; i < 4; i++)
            {
                Tile next = GetTile(t.pos + dirs[i]);
                //If there's a tile at that pos, and tile offers a shorter path
                if (next==null||next.distance <= t.distance + 1)
                {
                    continue;
                }

                if (addTile(t, next))
                {
                    next.distance = t.distance + 1;
                    next.prev = t;
                    checkNext.Enqueue(next);
                    retValue.Add(next);
                }
            }   
            
            //Check if we have cleared the queue, if we have we switch references so checkNow points to checkNext and checkNext points to an empty queue
            if (checkNow.Count==0)
            {
                SwapReference(ref checkNow, ref checkNext);
            } 
        }

        return retValue;
    }

    //Highlight or unhighlight tiles
    public void SelectTiles(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            tiles[i].GetComponent<Renderer>().material.SetColor("_Color", selectedTileColor);
    }

    public void DeSelectTiles(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            tiles[i].GetComponent<Renderer>().material.SetColor("_Color", defaultTileColor);
    }

    //Method to return a tile at specified point if it exists, null if otherwise
    public Tile GetTile(Point p)
    {
        return tiles.ContainsKey(p) ? tiles[p] : null;
    }

    void SwapReference(ref Queue<Tile> a, ref Queue<Tile> b)
    {
        Queue<Tile> temp = a;
        a = b;
        b = temp;
    }

    //Clear results of previous search
    public void ClearSearch()
    {
        foreach (Tile t in tiles.Values)
        {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }
}
