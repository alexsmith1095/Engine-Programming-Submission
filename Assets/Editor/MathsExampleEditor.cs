using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MathsExample))]
public class MathsExampleEditor : Editor {

	public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MathsExample mathsExampleScript = (MathsExample)target;
        if(GUILayout.Button("Calculate"))
        {
            mathsExampleScript.CalculateNewValues();
        }
    }
}
