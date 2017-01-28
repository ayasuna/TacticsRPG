using UnityEngine;
using System.Collections;
using System;

public class InputController : MonoBehaviour
{

    Repeater _hor = new Repeater("Horizontal");
    Repeater _ver = new Repeater("Vertical");

    //String array to hold names of buttons we want to check for.
    string[] _buttons = new string[] { "Fire1", "Fire2", "Fire3" };

    /*Share event whenever Repeaters report input. 
     *Static so other scripts need only know about the class, not its instances.
     *Use generic InfoEventArgs so we can specify event type as point; indicates direction
     */
    public static event EventHandler<InfoEventArgs<Point>> moveEvent;

    /*Share event whenever Fire buttons are pressed
     *Event type int, just need to know which Fire button was pressed
     */ 
    public static event EventHandler<InfoEventArgs<int>> fireEvent;  

    //Tie repeaters to Unity Update() loop and fire events declared at appropriate time.
    void Update ()
    {

        //Movement

        int x = _hor.Update();
        int y = _ver.Update();

        if (x != 0 || y != 0)
        {
            if (moveEvent != null)
            {
                moveEvent(this, new InfoEventArgs<Point>(new Point(x, y)));
            }
        }

        //Fire buttons

        for (int i=0; i <3; i++)
        {
            if (Input.GetButtonUp(_buttons[i]))
            {
                if (fireEvent != null)
                {
                    fireEvent(this, new InfoEventArgs<int>(i));
                }
            }
        }
	}
}

//Reusable class to handle "repeat" functionality i.e. Holding down a button.
//Private, only used by input controller.

class Repeater
{
    const float threshold = 0.5f; //Delay between initial press and point at which input begind to repeat
    const float rate = 0.25f;   //Rate at which input will repeat
    float _next;                //marks target point in time which must be passed before new events will be registered
    bool _hold;                 //Indicate whether user has  continued pressing the same button since last time an event was fired.
    string _axis;

    public Repeater(string axisName)
    {
        _axis = axisName;
    }

    //Update method retunr -1, 0, or 1. 0 means user not pressing a button or we're waiting for repeat event.
    public int Update()
    {
        int retValue = 0;   //Value which will be returned
        int value = Mathf.RoundToInt(Input.GetAxisRaw(_axis));  //Use Unity's Input Manager to get value this object is tracking, round the result, cast to int.

        if (value != 0)     //If there's user input
        {
            if (Time.time > _next)  //Check if enough time has past since last event
            {
                retValue = value;   //The return value is the new calculated value.
                _next = Time.time + (_hold ? rate : threshold);     //Set next time check
                _hold = true;  
            }
        }
        else    //If no user input
        {
            _hold = false;   
            _next = 0;      //Default and reset to 0 so first press always registered immediately.
        }

        return retValue;
    }
    // Use this for initialization
    void Start()
    {

    }
}
