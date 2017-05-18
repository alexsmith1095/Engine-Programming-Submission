using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// TODO: Update with pooling
///
public class Character : MonoBehaviour {

    public float health = 100;

    public virtual void Damage(float amount, Vector3 hitPoint, Vector3 hitDirection)
	{
        health -= amount;
        if (health <= 0)
			Die();
	}

    public virtual void Die ()
	{
		Destroy(gameObject); // Update with pooling
	}
}
