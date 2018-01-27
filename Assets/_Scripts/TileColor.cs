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
		tileState = TileState.Neutral;
		EnableState();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void EnableState(){
		tiles[tileState].SetActive(true);
	}

	void DisableState(){
		tiles[tileState].SetActive(false);
	}

	public void ChangeOwner(TileState state){
		DisableState();
		tileState = state;
		EnableState();
	}
}
