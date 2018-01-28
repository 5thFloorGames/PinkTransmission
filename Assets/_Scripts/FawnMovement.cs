using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FawnMovement : MonoBehaviour {

	int i = 0;
	GenerateBoard board;
	Animator animator;
	MusicManager music;
	bool jumpedAlready = false;
	private MoveDirection[] moves;
	private Dictionary<MoveDirection, MoveDirection> shuffled;
	private bool shuffleOn = false;
	private bool doubleMove = false;
	private int confuseCountdown = 0;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
		board = FindObjectOfType<GenerateBoard>();
		music = FindObjectOfType<MusicManager>();
		MusicManager.OnBeat += Jump;
		moves = new MoveDirection[4];
		moves[0] = (MoveDirection.Up);
		moves[1] = (MoveDirection.Down);
		moves[2] = (MoveDirection.Left);
		moves[3] = (MoveDirection.Right);
		shuffled = new Dictionary<MoveDirection, MoveDirection>();
	}

    private void CheckDirection(MoveDirection d){
        if(music.CloseToBeat()){
			if(shuffleOn){
	            GenerateBoard.Instance.MoveFawn(shuffled[d], 1);
			} else if(doubleMove){
				GenerateBoard.Instance.MoveFawn(d, 2);
			} else {
				GenerateBoard.Instance.MoveFawn(d, 1);
			}
            StartCoroutine(ResetMove());
        } else {
            ScoreManager.Instance.GenerateFloatText(0, true);
        }
    }

	void ShuffleMoves(){
		Static.Shuffle(moves);
		shuffled.Clear();
		shuffled.Add(MoveDirection.Up, moves[0]);
		shuffled.Add(MoveDirection.Down, moves[1]);
		shuffled.Add(MoveDirection.Left, moves[2]);
		shuffled.Add(MoveDirection.Right, moves[3]);
	}

	void Update(){
		if(!jumpedAlready){
			if(Input.GetKeyDown(KeyCode.W)){
				CheckDirection(MoveDirection.Up);
			} else if(Input.GetKeyDown(KeyCode.S)){
				CheckDirection(MoveDirection.Down);
			} else if(Input.GetKeyDown(KeyCode.A)){
				CheckDirection(MoveDirection.Right);
			} else if(Input.GetKeyDown(KeyCode.D)){
				CheckDirection(MoveDirection.Left);
			}
		}
	}

	IEnumerator ResetMove(){
		jumpedAlready = true;
		yield return new WaitForSeconds(music.TimeToNextBeat() - 0.05f);
		jumpedAlready = false;
	}

	public void Double(){
		shuffleOn = false;
		doubleMove = true;
		confuseCountdown = 8;
		music.EffectSwitch("DoubleJump");		
	}

	public void Confuse(){
		shuffleOn = true;
		doubleMove = false;
		ShuffleMoves();
		confuseCountdown = 8;
		music.EffectSwitch("MixedControls");
	}

	void Jump(){
		if(confuseCountdown > 0){
			confuseCountdown--;
		} else {
			doubleMove = false;
			shuffleOn = false;
			music.EffectSwitch("Neutral");
		}
		animator.SetTrigger("Jump");
	}

}
