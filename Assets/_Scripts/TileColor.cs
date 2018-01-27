using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColor : MonoBehaviour {

	public TileState tileState = TileState.Neutral;

	// Use this for initialization
	void Start () {
		tileState = TileState.Neutral;
	}
	
	// Update is called once per frame
	void Update () {
		if(tileState == TileState.Animal){
			transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.red;
		} else if(tileState == TileState.Shroom){
			transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.green;
		}
	}

	public void ChangeOwner(TileState state){
		tileState = state;
	}
}
