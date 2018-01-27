using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamController : MonoBehaviour {

	List<GameObject> rightLights;
	List<GameObject> leftLights;

	bool lightsMoving = true;

	void Start () {

		rightLights = new List<GameObject>();
		foreach (Transform obj in transform.Find("Right")) {
			rightLights.Add(obj.gameObject);
		}

		leftLights = new List<GameObject>();
		foreach (Transform obj in transform.Find("Left")) {
			leftLights.Add(obj.gameObject);
		}

		lightsMoving = false;
		
	}
	void Update () {
		if (!lightsMoving) {
			if (Input.GetKeyDown(KeyCode.P)) {
				StartCoroutine(WaveFromFront());
			}
		}
	}

	void ReturnToDefault() {
		for (int i = 0; i < 5; i++) {
			rightLights[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
			leftLights[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
		}
	}

	IEnumerator WaveFromFront() {
		
		for (int i = 0; i < 5; i++) {
			LeanTween.rotateZ(rightLights[i], 90f, 1.0f);
			LeanTween.rotateZ(leftLights[i], -90f, 1.0f);
			yield return new WaitForSeconds(0.5f);
		}
		yield return new WaitForSeconds(0.5f);
		ReturnToDefault();
	}

}
