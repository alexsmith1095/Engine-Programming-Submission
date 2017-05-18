using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

	// Controls
    private float healthValue = 10;

    // Components
    public AudioClip pickupSound;

	void Update () {
        transform.Rotate(Vector3.up, Time.deltaTime * 100);
        transform.Rotate(Vector3.right, Time.deltaTime * 100);
    }

    void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
            col.transform.GetComponent<PlayerCharacter>().AddHealth(healthValue); // Add health to the player
            SoundManager.Main.Play(pickupSound);
			Destroy(transform.parent.gameObject); // The cube is a child of the map icon holder, we need to destroy this as well
        }
    }
}
