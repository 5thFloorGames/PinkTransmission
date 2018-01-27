using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour {

	int YSize = 9;
	int XSize = 9;
	GameObject tile;
	TileColor [,] tiles;

	// Use this for initialization
	void Start () {
		tiles = new TileColor[XSize * 2, YSize *2];
		tile = Resources.Load<GameObject>("Tile");
		for(int i = 0;i<=XSize;i++){
			for(int j = 0;j<=YSize;j++){
				GameObject g = Instantiate(tile,transform);
				g.transform.position = new Vector3(i - 5,0,j - 5);
				tiles[i,j] = g.GetComponent<TileColor>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
