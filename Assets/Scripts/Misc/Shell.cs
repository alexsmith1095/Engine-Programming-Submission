using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    // Controls
    private float duration = 3;
    private float timeSinceSpawn;

    // Components
	private Rigidbody rb;

	// Using OnEnable here because we are pooling and need to call every time we reuse a shell
    void OnEnable()
    {
		rb = GetComponent<Rigidbody>();
		timeSinceSpawn = Time.time + duration;
		StartCoroutine("Fade");
		rb.AddForce(transform.forward * Random.Range(150, 200) + transform.position * Random.Range(-25, 25));
	}

    IEnumerator Fade ()
	{
		while(true) {
            yield return new WaitForSeconds(.2f);

			if (Time.time >= timeSinceSpawn) {
				this.Recycle();
            }
		}
    }

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Ground") {
			rb.Sleep(); // Sleep the rigidbody once it hits the ground for performance reasons
		}
	}
}
