using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	// This will be looped through to check if are spawning on the position of a spawnpoint
	public GameObject[] spawnPoints; 

	void Start () {
		spawnPoints = GameObject.FindGameObjectsWithTag ("Spawn");
		foreach(GameObject _point in spawnPoints) {
			if(transform.position == _point.transform.position) {
				Destroy (gameObject);
				Debug.Log (gameObject.name + " was covering a spawnpoint and was destroyed"); 
			}
		}
	}
}
