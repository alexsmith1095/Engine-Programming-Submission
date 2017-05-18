using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomFunctions;

public class AttackState : IState {

    Zombie zombie;

    public AttackState(Zombie zombie) {
		this.zombie = zombie;
    }

    // Controls
    private float attackInterval = 1f;
    private float nextAttackTime;

    // Components
    private PlayerCharacter player;

    public void Enter()
    {
		zombie.stateIndicator.color = new Color(0.91f, 0.3f, 0.24f);
        player = zombie.fov.visibleTargets[zombie.fov.visibleTargets.Count - 1].GetComponent<PlayerCharacter>();
		player.Damage(zombie.attackStrength, player.transform.position, zombie.transform.forward);

    }

    public void Execute()
    {
 		attackInterval -= Time.deltaTime;
        if (attackInterval <= 0) {
			zombie.stateMachine.ChangeState(new ChaseState(zombie));
		}
    }

    public void Exit()
    {
    }
}
