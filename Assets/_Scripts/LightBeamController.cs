using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamController : MonoBehaviour {

	public Color[] colors;
	public int currentColorNumber;

	List<GameObject> rightLights;
	List<GameObject> leftLights;

	bool lightsMoving = true;
	bool lightsFlashing = true;

	void Start () {

		rightLights = new List<GameObject>();
		foreach (Transform obj in transform.Find("Right")) {
			rightLights.Add(obj.gameObject);
		}

		leftLights = new List<GameObject>();
		foreach (Transform obj in transform.Find("Left")) {
			leftLights.Add(obj.gameObject);
		}

		ChangeColorTo(0);
		lightsMoving = false;
		lightsFlashing = false;
		
	}
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)) {
			SlowLights();
		}

		if (Input.GetKeyDown(KeyCode.L)) {
			VerySlowLights();
		}

		if (Input.GetKeyDown(KeyCode.O)) {
			SharpFlash();
		}

		if (Input.GetKeyDown(KeyCode.I)) {
			Flash();
		}

		if (Input.GetKeyDown(KeyCode.H)) {
			ChangeColorTo(1);
		}

		if (Input.GetKeyDown(KeyCode.G)) {
			ChangeColorTo(2);
		}

		if (Input.GetKeyDown(KeyCode.F)) {
			ChangeColorTo(0);
		}
	}

	void ReturnToDefault() {
		for (int i = 0; i < 5; i++) {
			rightLights[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
			leftLights[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
		}
	}

	public void SlowLights() {
		if (lightsMoving) {
			return;
		}
		lightsMoving = true;
		StartCoroutine(WaveFromFront(1f, 1f, 0.3f, 1.0f));
	}

	public void VerySlowLights() {
		if (lightsMoving) {
			return;
		}
		lightsMoving = true;
		StartCoroutine(WaveFromFront(5f, 2f, 1f, 7.0f));
	}

	public void Flash() {
		if (lightsFlashing) {
			return;
		}
		lightsFlashing = true;
		StartCoroutine(FlashTheLights(0.05f, 10));
	}

	public void SharpFlash() {
		if (lightsFlashing) {
			return;
		}
		lightsFlashing = true;
		StartCoroutine(FlashTheLights(0.01f, 20));
	}

	public void ChangeColorTo(int j) {
		if (j > colors.Length) {
			j = 0;
		}

		for (int i = 0; i < 5; i++) {
			rightLights[i].transform.Find("LightBeam").GetComponent<SpriteRenderer>().color = colors[j];
			leftLights[i].transform.Find("LightBeam").GetComponent<SpriteRenderer>().color = colors[j];
		}

		currentColorNumber = j;
	}

	void TweenZ(GameObject obj, float rotatingTo, float time, float back) {
		LeanTween.rotateZ(obj, rotatingTo, time);
		LeanTween.delayedCall(obj, time, ()=>{ 
			LeanTween.rotateZ(obj, 0f, back);
		});
	}

	IEnumerator WaveFromFront(float moveTimeTo, float moveTimeBack, float waitTimeBetween, float waitTimeAfter) {
		
		for (int i = 0; i < 5; i++) {
			TweenZ(rightLights[i], 90f, moveTimeTo, moveTimeBack);
			TweenZ(leftLights[i], -90f, moveTimeTo, moveTimeBack);
			yield return new WaitForSeconds(waitTimeBetween);
		}
		yield return new WaitForSeconds(waitTimeAfter);
		lightsMoving = false;
	}

	IEnumerator FlashTheLights(float flashTime, int howMany) {

		Color original = rightLights[0].transform.Find("LightBeam").GetComponent<SpriteRenderer>().color;

		for (int j = 0; j < howMany; j++) {
		
			for (int i = 0; i < 5; i++) {
				rightLights[i].transform.Find("LightBeam").GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
				leftLights[i].transform.Find("LightBeam").GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			}
			yield return new WaitForSeconds(flashTime);

			for (int i = 0; i < 5; i++) {
				rightLights[i].transform.Find("LightBeam").GetComponent<SpriteRenderer>().color = colors[currentColorNumber];
				leftLights[i].transform.Find("LightBeam").GetComponent<SpriteRenderer>().color = colors[currentColorNumber];
			}
			yield return new WaitForSeconds(flashTime);

		}

		lightsFlashing = false;
	}

}
