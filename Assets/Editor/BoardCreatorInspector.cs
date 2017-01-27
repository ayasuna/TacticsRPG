using UnityEditor;
using UnityEngine;

/*Inspector script for BoardCreator to allow editing through the inspector*/

//Declare what type of script this custom inspector is targeting
[CustomEditor(typeof(BoardCreator))]

//Inherits from Editor
public class BoardCreatorInspector : Editor {

    //Selected object is available through property "target". Cast to correct type and assign to property "current"
    public BoardCreator current
    {
        get
        {
            return (BoardCreator)target;
        }
    }

    //Method override to modify appearance of inspector
    public override void OnInspectorGUI()
    {
        //Include default implementation (allow inspector to display all serialized fields)
        DrawDefaultInspector();

        //Provide a button for each public method defined in BoardCreator

        if (GUILayout.Button("Clear"))
            current.Clear();
        if (GUILayout.Button("Grow"))
            current.Grow();
        if (GUILayout.Button("Shrink"))
            current.Shrink();
        if (GUILayout.Button("Grow Area"))
            current.GrowArea();
        if (GUILayout.Button("Shrink Area"))
            current.ShrinkArea();
        if (GUILayout.Button("Save"))
            current.Save();
        if (GUILayout.Button("Load"))
            current.Load();

        //If values changed update marker position
        if (GUI.changed)
            current.UpdateMarker();
    }
}
