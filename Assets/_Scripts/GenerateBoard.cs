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
    public float fawnDrunkness;

    // Shroom
    Vector2 shroomPosition;
    public GameObject shroomPlayer;
	private bool firstMove = false;
    

	// Use this for initialization
	void Start () {
        Instance = this;
		FawnPosition = new Vector2(0,0);
		shroomPosition = new Vector2(YSize,XSize);
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
        shroomPosition = new Vector2(XSize, YSize);
        shroomPlayer.transform.position = tiles[XSize, YSize].transform.position;
	}
    
    public Vector3 GetPos(int x, int y)
    {
        return new Vector3(x - XSize / 2f, 0, y - YSize / 2f);
    }
	
	public void MoveFawn(MoveDirection direction)
    {
		if(direction == MoveDirection.Right){
			fawn.transform.rotation = Quaternion.Euler(0,0,0);
		} else if(direction == MoveDirection.Left){
			fawn.transform.rotation = Quaternion.Euler(0,180,0);
		} else if(direction == MoveDirection.Up){
			fawn.transform.rotation = Quaternion.Euler(0,90,0);
		} else if(direction == MoveDirection.Down){
			fawn.transform.rotation = Quaternion.Euler(0,-90,0);
		}
		if(!firstMove){
			firstMove = true;
			FindObjectOfType<MusicManager>().FirstMove();
		}
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

        if (newPos != shroomPosition && newPos != FawnPosition)
        {
            LeanTween.delayedCall(fawn, 0.1f, ()=>{ 
                LeanTween.move( fawn, fawn.transform.position + delta, 0.2f);
                });
            FawnPosition = newPos;
            StartCoroutine(PlayDelayed(0.3f, true));
        }
	}
    
	void UpdateTileOnFawn(){
		int x = (int)FawnPosition.x;
		int y = (int)FawnPosition.y;
		tiles[x,y].ChangeOwner(TileState.Animal);

        if(ShroomSlotManager.Instance.CheckShroomDestroy(x, y)) {
			AkSoundEngine.PostEvent("ActionBiteStinger",fawn);
        }
        
	}
    
    public void MoveShroom(MoveDirection direction)
    {
        Vector2 newPos = shroomPosition;
		if(!firstMove){
			firstMove = true;
			FindObjectOfType<MusicManager>().FirstMove();
		}
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

        if (newPos != shroomPosition && newPos != FawnPosition)
        {
            LeanTween.delayedCall(shroomPlayer, 0.2f, ()=>{ 
                LeanTween.move( shroomPlayer, shroomPlayer.transform.position + delta, 0.2f);
                });
            shroomPosition = newPos;

            StartCoroutine(PlayDelayed(0.4f, false));
        }
    }

    private IEnumerator PlayDelayed(float delay, bool isFawn)
    {
        yield return new WaitForSeconds(delay);

        if (isFawn)
            UpdateTileOnFawn();
        else
            UpdateTileOnShroom();
    }

    void UpdateTileOnShroom()
    {
        int x = (int)shroomPosition.x;
        int y = (int)shroomPosition.y;
        tiles[x, y].ChangeOwner(TileState.Shroom);
    }

    public void SpawnShroom()
    {
        ShroomSlotManager.Instance.SpawnShroom((int)shroomPosition.x, (int)shroomPosition.y);
    }
}
