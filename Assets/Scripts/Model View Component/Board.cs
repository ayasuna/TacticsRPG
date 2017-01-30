using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that can load LevelData and create game board at runtime

public class Board : MonoBehaviour {

	[SerializeField] GameObject tilePrefab;
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

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
	
	// Update is called once per frame
	void Update () {
		
	}
}
