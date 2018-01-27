using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public delegate void BeatAction();
	public static event BeatAction OnBeat;
	private float lastBeat = 0;

	// Use this for initialization
	void Start () {
		AkSoundEngine.PostEvent("PlayPlaceholderLoop", gameObject, 0x0100, Beat, null);		
	}

	public void FirstMove(){
		AkSoundEngine.PostEvent("ActionFirstMove", gameObject);		
	}

	public void ShroomStinger(){
		AkSoundEngine.PostEvent("ActionShroomStinger", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.B)){
			CloseToBeat();
		}
	}

	bool CloseToBeat(){
		if(Time.time - lastBeat < 0.15 || 0.43 - (Time.time - lastBeat) < 0.15){
			print("OnBeat");
			print((Time.time - lastBeat < 0.2) + ": " + (Time.time - lastBeat));
			print((0.43 - (Time.time - lastBeat) < 0.2) + ": " + (0.43 - (Time.time - lastBeat)));
			return true;
		} else {
			print("OffBeat!");
		}
		return false;
	}

	void Beat(object in_cookie, AkCallbackType in_type, object in_info){
		if(Time.time - lastBeat > 0.1){
			//print(Time.time - lastBeat);
			lastBeat = Time.time;
			OnBeat();
		}
	}
}
