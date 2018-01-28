using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterLifetime : MonoBehaviour {

	public float time;

	void Start () {
		StartCoroutine(DestroyAfterWait());
	}

	IEnumerator DestroyAfterWait() {
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
