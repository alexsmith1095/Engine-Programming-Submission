using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomFunctions;

public class MathsExample : MonoBehaviour {

    public float value1;
    public float value2;
	public float value3;

	private Text absText;
    private Text clampText;
    private Text maxText;
    private Text minText;
    private Text circleAreaText;
    private Text squaredText;

    void Start ()
	{
        absText = GameObject.Find("TextAbs").GetComponent<Text>();
        clampText = GameObject.Find("TextClamp").GetComponent<Text>();
        maxText = GameObject.Find("TextMax").GetComponent<Text>();
        minText = GameObject.Find("TextMin").GetComponent<Text>();
        circleAreaText = GameObject.Find("TextCircleArea").GetComponent<Text>();
        squaredText = GameObject.Find("TextSquared").GetComponent<Text>();
		CalculateNewValues();
	}

	/// <summary>
	///	With the user input values, calculates the equations and prints them in the text boxes
	/// </summary>
	public void CalculateNewValues ()
	{
        absText.text = string.Format("The absolute value of {0} is ", value1);
        absText.text += Maths.Absolute(value1).ToString();

		clampText.text = string.Format("{0} clamped between {1} and {2} is ", value1, value2, value3);
        clampText.text += Maths.Clamp(value1, value2, value3).ToString();

		maxText.text = string.Format("The greatest of {0} and {1} is ", value1, value2);
        maxText.text += Maths.Max(value1, value2).ToString();

		minText.text = string.Format("The lesser of {0} and {1} is ", value1, value2);
        minText.text += Maths.Min(value1, value2).ToString();

		circleAreaText.text = string.Format("The area of a circle with a radius of {0} is ", value1);
        circleAreaText.text += (Maths.Pi * Maths.Squared(value1)).ToString();

        squaredText.text = string.Format("{0} squared is ", value1, value2);
        squaredText.text += Maths.Squared(value1).ToString();
	}
}
