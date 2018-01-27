using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour {

    public static GenerateBoard Instance { get; private set; }

	public readonly int YSize = 9;
	public readonly int XSize = 9;
	GameObject tile;
	TileColor [,] tiles;
	Vector2 FawnPosition;
	public GameObject fawn;

    // Shroom
    Vector2 ShroomPosition;
    public GameObject shroom;
    

	// Use this for initialization
	void Start () {
        Instance = this;
		FawnPosition = new Vector2(0,0);
		ShroomPosition = new Vector2(YSize,XSize);
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
        ShroomPosition = new Vector2(XSize, YSize);
        shroom.transform.position = tiles[XSize, YSize].transform.position;
	}
    
    public Vector3 GetPos(int x, int y)
    {
        return new Vector3(x - XSize / 2f, 0, y - YSize / 2f);
    }
	
	public void MoveFawn(MoveDirection direction)
    {
        Vector2 newPos = FawnPosition;
        Vector3 delta = Vector3.zero;
        if (direction == MoveDirection.Down){
			if(newPos.y > 0){
				delta = new Vector3(0,0,-1);
                newPos += new Vector2(0,-1);
			}
		} else if (direction == MoveDirection.Up){
			if(newPos.y < YSize){
                delta = new Vector3(0,0,1);
                newPos += new Vector2(0,1);
			}
		} else if(direction == MoveDirection.Right){
			if(newPos.x > 0){
                delta = new Vector3(-1,0,0);
                newPos += new Vector2(-1,0);
			}
		} else if (direction == MoveDirection.Left){
			if(newPos.x < XSize){
                delta = new Vector3(1,0,0);
                newPos += new Vector2(1,0);
			}
        }

        if (newPos != ShroomPosition && newPos != FawnPosition)
        {
            fawn.transform.position += delta;
            FawnPosition = newPos;
            UpdateTileOnFawn();
        }
	}
    
	void UpdateTileOnFawn(){
		int x = (int)FawnPosition.x;
		int y = (int)FawnPosition.y;
		tiles[x,y].ChangeOwner(TileState.Animal);
	}
    
    public void MoveShroom(MoveDirection direction)
    {
        Vector2 newPos = ShroomPosition;
        Vector3 delta = Vector3.zero;
        if (direction == MoveDirection.Down)
        {
            if (newPos.y > 0)
            {
                delta = new Vector3(0, 0, -1);
                newPos += new Vector2(0, -1);
            }
        }
        else if (direction == MoveDirection.Up)
        {
            if (newPos.y < YSize)
            {
                delta = new Vector3(0, 0, 1);
                newPos += new Vector2(0, 1);
            }
        }
        else if (direction == MoveDirection.Right)
        {
            if (newPos.x > 0)
            {
                delta = new Vector3(-1, 0, 0);
                newPos += new Vector2(-1, 0);
            }
        }
        else if (direction == MoveDirection.Left)
        {
            if (newPos.x < XSize)
            {
                delta = new Vector3(1, 0, 0);
                newPos += new Vector2(1, 0);
            }
        }

        if (newPos != ShroomPosition && newPos != FawnPosition)
        {
            shroom.transform.position += delta;
            ShroomPosition = newPos;
            UpdateTileOnShroom();
        }
    }

    void UpdateTileOnShroom()
    {
        int x = (int)ShroomPosition.x;
        int y = (int)ShroomPosition.y;
        tiles[x, y].ChangeOwner(TileState.Shroom);
    }

    public void SpawnShroom()
    {
        ShroomSlotManager.Instance.SpawnShroom((int)ShroomPosition.x, (int)ShroomPosition.y);
    }
}
