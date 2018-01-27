using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AkSoundEngine.PostEvent("PlayPlaceholderLoop", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
