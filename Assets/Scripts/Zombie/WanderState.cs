using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomFunctions;

public class WanderState : IState {

    Zombie zombie;

    public WanderState(Zombie zombie) {
		this.zombie = zombie;
	}

    // Components
	private Vector3 target;

    public void Enter()
    {
        // Set the state indicator colour above the zombies head to green
        zombie.stateIndicator.color = new Color(0.29f, 0.65f, 0.28f);
        zombie.NullifyTarget();
    }

    public void Execute()
    {
        // Set a new random target position if we have reached the last one
		if (zombie.transform.position.x == target.x || target == Vector3.zero) {
			if(zombie.transform.position.z == target.z || target == Vector3.zero) {
                target = AI.GetRandomTargetPosition(zombie.transform.position, Random.Range(20f, 40f));
                zombie.agent.SetDestination(target);
			}
        }

        // If zombie can see a target
		if (zombie.CheckForTargets()) {
            // Go to the chase state
            zombie.stateMachine.ChangeState(new ChaseState(zombie));
		}
    }

    public void Exit()
    {

    }
}
