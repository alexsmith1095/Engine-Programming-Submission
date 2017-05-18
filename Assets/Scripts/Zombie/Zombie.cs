using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomFunctions;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Zombie : Character {

    public StateMachine stateMachine = new StateMachine();

    // Controls
    public float attackRange = 2f;
    public float attackStrength = 40;
    public float scoreWorth = 50;
    public float moneyWorth = 100;

    // Components
    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent agent;
    //[HideInInspector]
    public Transform target;
    [HideInInspector]
    public FieldOfView fov;
    public SpriteRenderer stateIndicator;
    public ParticleSystem deathParticle;
    public GameObject healthPickup;
    public AudioClip deathSound;
    private PlayerCharacter player;

    void Start()
    {
        stateMachine.ChangeState(new WanderState(this)); // Enter the default state
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        stateMachine.Update();
    }

	public void SetNewPosition (Vector3 newPosition)
	{
		if (agent.enabled == true)
			agent.SetDestination(newPosition);
	}

	public void SetNewTarget (Transform newTarget)
    {
        StopCoroutine(UpdateTarget());
        target = newTarget;
        StartCoroutine(UpdateTarget());
    }

    public void NullifyTarget ()
    {
        StopCoroutine(UpdateTarget());
        target = null;
    }

    // Update the agent destination once a second for performance
    IEnumerator UpdateTarget ()
    {
        float rate = .25f; // The rate in seconds to update the target

        while (target != null && agent.enabled == true) {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            Vector3 targetPosition = target.position - targetDirection * 1.5f; // Dont go to the target exact position
            agent.SetDestination(targetPosition);
            yield return new WaitForSeconds(rate);
        }
        yield break;
    }

    public bool CheckForTargets ()
    {
        if (fov.visibleTargets.Count > 0)
            return true;
        else
            return false;
    }


    // Funtions inherited from Character
    public override void Damage(float amount, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (amount >= health) {
            // Play the death particle effect and destroy when finished
            Destroy(Instantiate(deathParticle.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathParticle.startLifetime);
        }
        base.Damage(amount, hitPoint, hitDirection);
	}

    public override void Die()
    {
        SoundManager.Main.Play(deathSound);
        // If this is a fast zombie, give it a 25 prercent chance of dropping a health pickup
        if (gameObject.name == "ZombieFast(Clone)") {
            int dropHealth = Random.Range(1, 4);
            if (dropHealth == 1) {
                Instantiate(healthPickup, transform.position, Quaternion.Euler(Vector3.right * 90));
                this.scoreWorth *= 2; // Double points for killing a fast zombie
            }
        }
        player.AddScore(scoreWorth);
        player.AddMoney(moneyWorth);
		base.Die();
	}
}
