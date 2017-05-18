using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomFunctions;

public class InvestigateState : IState {

    Zombie zombie;

    public InvestigateState(Zombie zombie) {
        this.zombie = zombie;
    }

    // Controls
    private float targetCheckDelay = 1;
    private float nextTargetCheck;
    private int checks;

    // Components


    public void Enter()
    {
        // Set the state indicator colour above the zombies head to yellow
        zombie.stateIndicator.color = new Color(0.95f, 0.77f, 0.06f);
    }

    public void Execute()
    {
        // Check if zombie can see a target once a second
        if (Time.time > nextTargetCheck) {
            checks ++;
            nextTargetCheck = Time.time + targetCheckDelay;
            if (zombie.CheckForTargets()) {
                zombie.stateMachine.ChangeState(new ChaseState(zombie)); // It can see a target, go to chase state
            } else {
                if (checks > 3)
                    zombie.stateMachine.ChangeState(new WanderState(zombie));
            }
        }
    }

    public void Exit()
    {

    }
}
