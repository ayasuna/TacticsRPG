
using UnityEngine;

/*Class definition for a tile on the board*/

public class Tile : MonoBehaviour
{
    //Tile height divisor. Each tile can have height to .25 precision
    public const float stepHeight = 0.25f;

    //Tile position and height on map
    public Point pos;
    public int height;

    //Convenience property to place objects centre of top of tile surface
    public Vector3 center { get { return new Vector3(pos.x, height * stepHeight, pos.y); } } 

    //Visually reflect new values of position or height when modified
    void Match()
    {
        transform.localPosition = new Vector3(pos.x, height * stepHeight/2f, pos.y);
        transform.localScale = new Vector3(1, height * stepHeight, 1);
    }

    //Allow data to be changed and view to update at same time
    public void Grow()
    {
        height++;
        Match();
    }

    public void Shrink()
    {
        height--;
        Match();
    }

    //Load method to load a tile with given position and height
    //Overloaded to accept Vector3 as parameter

    public void Load(Point p, int h)
    {
        pos = p;
        height = h;
        Match();
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.y), (int)v.z );
    }
}
