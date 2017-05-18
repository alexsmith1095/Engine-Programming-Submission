using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomFunctions;

public class ChaseState : IState {

    Zombie zombie;

    public ChaseState(Zombie zombie) {
		this.zombie = zombie;
    }

	private Transform target;

    public void Enter()
    {
        zombie.stateIndicator.color = new Color(0.83f, 0.33f, 0.0f);
        target = zombie.fov.visibleTargets[0];
        zombie.SetNewTarget(target); // Set the new target
    }

    public void Execute()
    {
        // Check for targets
        if (!zombie.CheckForTargets()) { // Cannot see a target
            zombie.stateMachine.ChangeState(new InvestigateState(zombie));
        } else { // Can see a target, check if they are within attacking distance
            float sqrTargetDist = Maths.SqrMag(zombie.transform.position - zombie.target.transform.position);
            if (sqrTargetDist < Maths.Squared(zombie.attackRange)) {
                zombie.stateMachine.ChangeState(new AttackState(zombie));
            }
        }
    }

    public void Exit()
    {

    }
}
