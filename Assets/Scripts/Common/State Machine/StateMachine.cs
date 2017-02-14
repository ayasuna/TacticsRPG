using System.Collections;
using UnityEngine;

/*  StateMachine class to maintain reference to current state and
 *  handle switching between states*/

public class StateMachine : MonoBehaviour
{
    public virtual State CurrentState   //public property CurrentState to expose _currentState  
    {
        get { return _currentState; }   //getter and setter. Set done through Transition() which supplies some additional logic
        set { Transition(value); }
    }
    protected State _currentState;      //protected field holding a single instance of State
    protected bool _inTransition;

    /* Convenience method to tell the StateMachine to change state based on type of state in a generic method call
     * Avoids having to hard code references to instances of state we want to change to. Achieved using:
     * ChangeState method using a constraint that the generic parameter must be a type of State.
     * GetState which passes along the generic type. The GetState method attempts to get a state for you 
     * using Unity’s GetComponent call, and when that fails, performs an AddComponent.
     * */

    public virtual T GetState<T>() where T : State
    {
        T target = GetComponent<T>();
        if (target == null)
            target = gameObject.AddComponent<T>();
        return target;
    }

    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GetState<T>();
    }

    protected virtual void Transition(State value)
    {
        //Exits early if trying to transition to current state, and cannot set current state when in transition
        if (_currentState == value || _inTransition)    
            return;

        _inTransition = true;   //Mark the beginning of transition

        if (_currentState != null) //If previous state not null, it is sent a message to exit
            _currentState.Exit();

        _currentState = value;  //set the current state to the new

        if (_currentState != null)
            _currentState.Enter();  //If new state not null, send message to enter

        _inTransition = false;  //mark end of transition
    }
}
 
 