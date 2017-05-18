using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesUI : MonoBehaviour {

    private Text t;
    private bool waveStarting;
    private int waveNumber;
    private float waveCountdown;

    void OnEnable ()
    {
        Events.newWave += StartWaveUI; // Subscribe to event
    }

    void OnDisable()
    {
        Events.newWave -= StartWaveUI; // Unsubscribe from event
    }

	void Start ()
	{
        t = GetComponent<Text>();
		t.enabled = false;
    }

    void Update()
    {
		if (waveStarting) {
            waveCountdown -= Time.deltaTime;
			t.text = string.Format("Wave {0} \n Starting in... {1}", waveNumber, Mathf.Round(waveCountdown));
		}
	}

	void StartWaveUI (int number, float countdown)
	{
		StartCoroutine(WaveUI(number, countdown));
	}

	IEnumerator WaveUI (int number, float countdown) {

		waveNumber = number;
		waveCountdown = countdown;

        waveStarting = true;
		t.enabled = true;

        yield return new WaitForSeconds(4.5f);
        t.enabled = false;
		waveStarting = false;

		yield break;
	}
}
