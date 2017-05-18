using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour
{
	// Controls
    public float duration;

    // Components
    public AnimationCurve animationCurve;

	void OnEnable ()
	{
        StartCoroutine(Fade());
	}

    public void SetColour(Color colour)
    {
		Component[] renderers = transform.GetComponentsInChildren(typeof(Renderer));
		foreach(Renderer childRenderer in renderers) {
			if (childRenderer.material.color != colour) {
                childRenderer.material.color = colour;
            }
		}
	}

	IEnumerator Fade ()
	{
		transform.localScale = Vector3.one;
		float elapsed = 0;
		while (elapsed < duration) {
			float scale = 1 - animationCurve.Evaluate(elapsed / duration);
			transform.localScale = new Vector3(scale, scale, scale);
			elapsed += Time.deltaTime;
			yield return 0;
		}
		this.Recycle();
	}
}
