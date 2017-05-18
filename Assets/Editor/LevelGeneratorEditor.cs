using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (LevelGenerator))]
public class LevelGeneratorEditor : Editor {

    public override void OnInspectorGUI()
	{
        base.OnInspectorGUI();

		if (GUI.changed)
	    {
			// This allows the level to be changed in real time without having to play the scene
	    	LevelGenerator level = target as LevelGenerator;
		    level.Generate();
	    }﻿
    }
}
