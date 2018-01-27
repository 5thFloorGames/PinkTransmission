using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour {

    public static GenerateBoard Instance { get; private set; }

	int YSize = 9;
	int XSize = 9;
	GameObject tile;
	TileColor [,] tiles;
	Vector2 FawnPosition;
	public GameObject fawn;

	public delegate void BeatAction();
	public static event BeatAction OnBeat;

	// Use this for initialization
	void Start () {
        Instance = this;
		FawnPosition = new Vector2(0,0);
		tiles = new TileColor[XSize * 2, YSize *2];
		tile = Resources.Load<GameObject>("Tile");
		for(int i = 0;i<=XSize;i++){
			for(int j = 0;j<=YSize;j++){
				GameObject g = Instantiate(tile,transform);
				g.transform.localPosition = GetPos(i, j);
				tiles[i,j] = g.GetComponent<TileColor>();
			}
		}
		fawn.transform.position = tiles[0,0].GetComponent<Transform>().position;
	}
    
    public Vector3 GetPos(int x, int y)
    {
        return new Vector3(x - XSize / 2f, 0, y - YSize / 2f);
    }
	
	public void MoveFawn(MoveDirection direction){
		if(direction == MoveDirection.Down){
			if(FawnPosition.x > 0){
				fawn.transform.position += new Vector3(1,0,0);
				FawnPosition += new Vector2(-1,0);
			}
		} else if (direction == MoveDirection.Up){
			if(FawnPosition.x < XSize){
				fawn.transform.position += new Vector3(-1,0,0);
				FawnPosition += new Vector2(1,0);
			}
		} else if(direction == MoveDirection.Right){
			if(FawnPosition.y < YSize){
				fawn.transform.position += new Vector3(0,0,1);
				FawnPosition += new Vector2(0,-1);
			}
		} else if (direction == MoveDirection.Left){
			if(FawnPosition.y > 0){
				fawn.transform.position += new Vector3(0,0,-1);
				FawnPosition += new Vector2(0,1);
			}
		}
		print(FawnPosition);
		UpdateTileOnFawn();
	}

	void UpdateTileOnFawn(){
		int x = (int)FawnPosition.x;
		int y = (int)FawnPosition.y;
		tiles[x,y].ChangeOwner(TileState.Animal);
	}
}
