using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColor : MonoBehaviour {

	public TileState tileState = TileState.Neutral;

	// Use this for initialization
	void Start () {
		if(Random.Range(0f,1f) < 0.5f){
			tileState = TileState.Animal;
		} else {
			if(Random.Range(0f,1f) < 0.5f){	
				tileState = TileState.Shroom;
			} else {
				tileState = TileState.Neutral;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(tileState == TileState.Animal){
			GetComponent<SpriteRenderer>().color = Color.red;
		} else if(tileState == TileState.Shroom){
			GetComponent<SpriteRenderer>().color = Color.green;
		}
	}
}
