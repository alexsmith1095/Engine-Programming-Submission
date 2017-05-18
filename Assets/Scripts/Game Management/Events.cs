using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {

    public delegate void OnNewWave(int number, float countdown);

    public static event OnNewWave newWave;

    public static void NewWave(int number, float countdown)
	{
		if (newWave != null)
			newWave(number, countdown);
    }
}
