﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance { get; private set; }

	public delegate void BeatAction();
	public static event BeatAction OnBeat;
	private float lastBeat = 0;
	private int beatCounter = 0;

	void Awake()
	{
		Instance = this;
        lastBeat = 0;
        beatCounter = 0;
	}
    
	// Use this for initialization
	void Start () {
		AkSoundEngine.PostEvent("PlayPlaceholderLoop", gameObject, 0x0100, Beat, null);		
	}

	public void FirstMove(){
		AkSoundEngine.PostEvent("ActionFirstMove", gameObject);		
	}

	public bool CloseToBeat(){
		if(Time.time - lastBeat < 0.15 || 0.43 - (Time.time - lastBeat) < 0.15){
			// print("OnBeat");
			// print((Time.time - lastBeat < 0.2) + ": " + (Time.time - lastBeat));
			// print((0.43 - (Time.time - lastBeat) < 0.2) + ": " + (0.43 - (Time.time - lastBeat)));
			return true;
		} else {
			//print("OffBeat!");
		}
		return false;
	}

	public int TimeToBarAfterNext(){
		return ((beatCounter % 4) - 4) + 8;
	}

	public bool CloseToHalfBar(){

		if(beatCounter % 2 == 1 && Time.time - lastBeat < 0.2){
			return true;
		}
		if(beatCounter % 2 == 0 && 0.43 - (Time.time - lastBeat) < 0.2){
			return true;
		}
		return false;
	}

	public float TimeToNextBeat(){
		return 0.43f - (Time.time - lastBeat);
	}

	void Beat(object in_cookie, AkCallbackType in_type, object in_info){
		if(Time.time - lastBeat > 0.1){
			//print(Time.time - lastBeat);
			lastBeat = Time.time;
			beatCounter++;

            if(OnBeat != null)
			    OnBeat();
		}
	}

	public void EndGameMusic(){
		AkSoundEngine.PostEvent("End",gameObject);
		StartCoroutine(StopAllMusic());
	}

	IEnumerator StopAllMusic(){
		yield return new WaitForSeconds(7);
		AkSoundEngine.PostEvent("StopAll",gameObject);
	}

	public void EffectSwitch(string effectName){
		AkSoundEngine.SetState("Effect", effectName);
	}
}
