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

	IEnumerator WaveFromFront() {
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < 5; i++) {
			
		}
	}

}
