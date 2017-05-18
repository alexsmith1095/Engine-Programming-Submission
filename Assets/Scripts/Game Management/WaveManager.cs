using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	public enum SpawnerState { Counting, Waiting, Spawning }

    // Controls
    public float waveInterval; // Seconds between waves
	public float spawnDelay; // Seconds between spawning new enemies
	private int count; // Current wave number
    private float countdown; // Countdown before the wave starts

    // Components
    public Transform zombie; // Zombie prefab to spawn
	public Transform zombieFast; // Fast zombie prefab
    public AudioClip newWaveSound;
    public AudioClip spawnSound;
    public Transform[] spawnPoints; // Points at which to spawn enemies
    private SpawnerState state = SpawnerState.Counting; // Current state of the spawner

	void Start ()
    {
		countdown = waveInterval; // Set the wave countdown
		StartCoroutine(NewWave()); // Begin the first wave
    }

	void Update ()
    {
		// Find all enemies in the scene, if there are none start a new wave
		if (state == SpawnerState.Waiting) {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			if (enemies.Length <= 0) {
				SoundManager.Main.Play(newWaveSound);
				StartCoroutine(NewWave()); // Start a new wave
			}
        }
    }

    IEnumerator NewWave()
    {
		state = SpawnerState.Counting; // We are counting down to the next wave

        count++; // Increment the current wave number
		Events.NewWave(count, countdown); // Trigger new wave event to display the subscribed UI
        yield return new WaitForSeconds(countdown);

		state = SpawnerState.Spawning; // We are spawning enemies into the scene

		// Loop through wave count and spawn enemies e.g. If wave number is 5, spawn 13 enemies
		for (int i = 0; i < count * 2.5f; i++) {
			yield return StartCoroutine(Spawn(zombie, zombieFast)); // Spawn an enemy
			if (i < count -1)
            	yield return new WaitForSeconds(spawnDelay); // Wait before spawning next enemy
		}

        state = SpawnerState.Waiting; // We are waiting for the player to kill the enemies
		countdown = waveInterval; // Reset the wave countdown

		yield break;
    }

    IEnumerator Spawn(Transform _zombie, Transform _zombieFast)
	{
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Get a random spawn point from the array

        Color originalColour = point.FindChild("Indicator").GetComponent<SpriteRenderer>().color; // Get the default colour of the current spawner
        Color flashColor = new Color(0.91f, 0.30f, 0.24f); // Create a red colour to flash before spawning

		// Make the spawnpoint flash a couple of times before spawning an enemy on it
        point.FindChild("Indicator").GetComponent<SpriteRenderer>().color = flashColor;
        yield return new WaitForSeconds(.1f);
        point.FindChild("Indicator").GetComponent<SpriteRenderer>().color = originalColour;
        yield return new WaitForSeconds(.2f);
		point.FindChild("Indicator").GetComponent<SpriteRenderer>().color = flashColor;
		yield return new WaitForSeconds(.1f);
        point.FindChild("Indicator").GetComponent<SpriteRenderer>().color = originalColour;
		yield return new WaitForSeconds(.2f);

		// Create a 1 in 6 chance of spawning a faster zombie
		if (Random.Range(1,6) == 1) {
			Instantiate(_zombieFast, point.position, point.rotation); // Instantiate the fast zombie at the spawn point

		} else {
			Instantiate(_zombie, point.position, point.rotation); // Instantiate the normal enemy at the spawn point
		}
        SoundManager.Main.Play(spawnSound);
        yield return new WaitForSeconds(1f);
	}
}
