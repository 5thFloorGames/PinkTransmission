using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour {

    public static GenerateBoard Instance { get; private set; }

	int YSize = 9;
	int XSize = 9;
	GameObject tile;
	TileColor [,] tiles;

	// Use this for initialization
	void Start () {
        Instance = this;
		tiles = new TileColor[XSize * 2, YSize *2];
		tile = Resources.Load<GameObject>("Tile");
		for(int i = 0;i<=XSize;i++){
			for(int j = 0;j<=YSize;j++){
				GameObject g = Instantiate(tile,transform);
				g.transform.localPosition = GetPos(i, j);
				tiles[i,j] = g.GetComponent<TileColor>();
			}
		}
	}

    public Vector3 GetPos(int x, int y)
    {
        return new Vector3(x - XSize / 2f, 0, y - YSize / 2f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
