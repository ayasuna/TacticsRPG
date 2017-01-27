using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Class for storing level data.
 *Stores pos and height of each board tile so just need a list of Vector3
 */

public class LevelData : ScriptableObject
{
    public List<Vector3> tiles;

}
