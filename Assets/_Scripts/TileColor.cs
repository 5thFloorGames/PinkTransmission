using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColor : MonoBehaviour {

	public TileState tileState = TileState.Neutral;
	public GameObject neutral;
	public GameObject animal;
	public GameObject shroom;
	private Dictionary<TileState,GameObject> tiles;

	// Use this for initialization
	void Start () {
		tiles = new Dictionary<TileState, GameObject>();
		tiles.Add(TileState.Neutral,neutral);
		tiles.Add(TileState.Animal,animal);
		tiles.Add(TileState.Shroom,shroom);
		EnableState();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void EnableState(){
		if(tiles != null){
			tiles[tileState].SetActive(true);
		}
	}

	void DisableState(){
		if(tiles != null){
			tiles[tileState].SetActive(false);
		}
	}

	public void ChangeOwner(TileState state){
		if(state != tileState){
			DisableState();
			tileState = state;
			EnableState();
		}
	}

	public Vector3 TilePosition(){
		return transform.position;
	}

	public void setState(TileState state){
		tileState = state;
	}
}
