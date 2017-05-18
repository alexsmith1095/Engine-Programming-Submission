using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	// Controls
	public bool paused;

	// Components
	public Image redBackground;
	public Text pausedText;
	public PlayerController playerScript;
	public Gun gunScript;

	void Start ()
	{
		redBackground.enabled = false;
		pausedText.enabled = false;
		playerScript.enabled = true;
		gunScript.enabled = true;
	}

	/// <summary>
	/// Check if the player hits pause button. If so show paused UI and set timescale to 0
	/// </summary>
	void Update ()
	{
		// Pause
		if (Input.GetButtonDown ("Pause") && !paused) {
			Time.timeScale = 0;
			redBackground.enabled = true;
			pausedText.enabled = true;
			playerScript.enabled = false;
			gunScript.enabled = false;
			paused = true;
		} 
		// Unpause
		else if (Input.GetButtonDown ("Pause") && paused) {
			Time.timeScale = 1;
			redBackground.enabled = false;
			pausedText.enabled = false;
			playerScript.enabled = true;
			gunScript.enabled = true;
			paused = false;
		}
	}
}
