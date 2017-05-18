using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {

	IState currentState;

	public void ChangeState(IState newState) {
	    if (currentState != null)
	        currentState.Exit(); // Exit previous state

	    currentState = newState; // Assign the next state
	    currentState.Enter(); // Enter the next state
	}

	public void Update() {
        if (currentState != null)
			currentState.Execute();
	}
}
