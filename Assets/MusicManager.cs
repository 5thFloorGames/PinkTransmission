using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public delegate void BeatAction();
	public static event BeatAction OnBeat;
	// Use this for initialization
	void Start () {
		AkSoundEngine.PostEvent("PlayPlaceholderLoop", gameObject, 0x0100, Beat, null);		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Beat(object in_cookie, AkCallbackType in_type, object in_info){
		OnBeat();
	}
}
