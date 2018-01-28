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


    LightBeamController lbc;
    int colorCounter = 0;
    

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
		tiles[0,0].setState(TileState.Animal);
		tiles[XSize,YSize].setState(TileState.Shroom);
        lbc = GameObject.Find("Environment").transform.Find("Lights").gameObject.GetComponent<LightBeamController>();
	}
    
    public Vector3 GetPos(int x, int y)
    {
        return new Vector3(x - XSize / 2f, 0, y - YSize / 2f);
    }
	
	public void MoveFawn(MoveDirection direction, int factor)
    {
        if (ScoreManager.end)
            return;

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
        if (direction == MoveDirection.Down){
			if(newPos.y > 0){
                newPos += new Vector2(0,-1 * factor);
			}
		} else if (direction == MoveDirection.Up){
			if(newPos.y < YSize){
                newPos += new Vector2(0,1 * factor);
			}
		} else if(direction == MoveDirection.Right){
			if(newPos.x > 0){
                newPos += new Vector2(-1 * factor,0);
			}
		} else if (direction == MoveDirection.Left){
			if(newPos.x < XSize){
                newPos += new Vector2(1 * factor,0);
			}
        }

		newPos = ClampPosition(newPos);

        if (newPos != shroomPosition && newPos != FawnPosition)
        {
            LeanTween.delayedCall(fawn, 0.1f, ()=>{ 
                LeanTween.move( fawn, tiles[(int)newPos.x,(int)newPos.y].TilePosition(), 0.2f);
                if (colorCounter == 0) {
                    colorCounter++;
                    lbc.ChangeColorTo(1);
                } else if (colorCounter == 1) {
                    colorCounter--;
                    lbc.ChangeColorTo(0);
                }
                
                });
            FawnPosition = newPos;
            StartCoroutine(PlayDelayed(0.3f, true));
        } 
	}

	Vector2 ClampPosition(Vector2 pos){
		return new Vector2(Mathf.Clamp(pos.x,0,XSize), Mathf.Clamp(pos.y,0,YSize));
	}
    
	void UpdateTileOnFawn(){
		int x = (int)FawnPosition.x;
		int y = (int)FawnPosition.y;

        var previousState = tiles[x, y].tileState;
        var scoreChange = previousState == TileState.Animal ? 1 : 2;
        ScoreManager.Instance.GenerateFloatText(scoreChange, true);

		tiles[x,y].ChangeOwner(TileState.Animal);

        var shroomType = ShroomSlotManager.Instance.CheckShroomDestroy(x, y);
        if (shroomType != ShroomType.None) {
            ScoreManager.Instance.GenerateFloatText(3, true);
			AkSoundEngine.PostEvent("ActionBiteStinger",fawn);
			lbc.SharpFlash();
			if(shroomType == ShroomType.Normal){
				fawn.SendMessage("Confuse");
			} else if(shroomType == ShroomType.Jump){
				fawn.SendMessage("Double");
			}
        }
        
	}
    
    public void MoveShroom(MoveDirection direction)
    {
        if (ScoreManager.end)
            return;

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
                LeanTween.move( shroomPlayer, tiles[(int)newPos.x,(int)newPos.y].TilePosition(), 0.2f);
                lbc.ChangeColorTo(2);
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

        var previousState = tiles[x, y].tileState;
        var scoreChange = previousState == TileState.Shroom ? 2 : 4;
        ScoreManager.Instance.GenerateFloatText(scoreChange, false);

        tiles[x, y].ChangeOwner(TileState.Shroom);
    }

    public bool SpawnShroom()
    {
        return ShroomSlotManager.Instance.SpawnShroom((int)shroomPosition.x, (int)shroomPosition.y);
    }

    public int ChangeTileOwner(int x, int y, TileState tileState)
    {
        if (x >= 0 && x <= XSize && y >= 0 && y <= YSize)
        {
            var prev = tiles[x, y].tileState;

            if (prev != tileState)
            {
                tiles[x, y].ChangeOwner(tileState);
                return 3;
            }
            return 2;
        }
        return 0;
    }
}
