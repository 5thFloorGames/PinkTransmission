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
				lightsMoving = true;
			}
		}
	}

	void ReturnToDefault() {
		for (int i = 0; i < 5; i++) {
			rightLights[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
			leftLights[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
		}
	}

	void Hello(GameObject go) {
		Debug.Log(go.name);
	}

	void TweenZ(GameObject obj, float rotatingTo, float time) {
		LeanTween.rotateZ(obj, rotatingTo, time);
		LeanTween.delayedCall(obj, time, ()=>{ 
		LeanTween.rotateZ(obj, 0f, time);
		});
	}

	IEnumerator WaveFromFront() {
		
		for (int i = 0; i < 5; i++) {
			TweenZ(rightLights[i], 90f, 1.0f);
			TweenZ(leftLights[i], -90f, 1.0f);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(1f);
		lightsMoving = false;
		//ReturnToDefault();
	}

}
