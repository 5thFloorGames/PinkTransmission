using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FawnMovement : MonoBehaviour {

	int i = 0;
	GenerateBoard board;
	Animator animator;
	MusicManager music;
	bool jumpedAlready = false;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
		board = FindObjectOfType<GenerateBoard>();
		music = FindObjectOfType<MusicManager>();
		MusicManager.OnBeat += Jump;
	}

	void Update(){
		if(!jumpedAlready){
			if(Input.GetKeyDown(KeyCode.W)){
				if(music.CloseToBeat()){
					board.MoveFawn(MoveDirection.Up);
					StartCoroutine(ResetMove());
				}
			} else if(Input.GetKeyDown(KeyCode.S)){
				if(music.CloseToBeat()){
					board.MoveFawn(MoveDirection.Down);
					StartCoroutine(ResetMove());
				}
			} else if(Input.GetKeyDown(KeyCode.A)){
				if(music.CloseToBeat()){
					board.MoveFawn(MoveDirection.Right);
					StartCoroutine(ResetMove());
				}
			} else if(Input.GetKeyDown(KeyCode.D)){
				if(music.CloseToBeat()){
					board.MoveFawn(MoveDirection.Left);
					StartCoroutine(ResetMove());
				}
			}
		}
	}

	IEnumerator ResetMove(){
		jumpedAlready = true;
		yield return new WaitForSeconds(music.TimeToNextBeat() - 0.05f);
		jumpedAlready = false;
	}

	void Jump(){
		animator.SetTrigger("Jump");
	}
    
	void Move(){
		if(i++ % 1 == 0 && music.CloseToBeat())
        {
			animator.SetTrigger("Jump");
			if(Input.GetAxis("HorizontalA") > 0){
				board.MoveFawn(MoveDirection.Left);
			} else if (Input.GetAxis("HorizontalA") < 0){
				board.MoveFawn(MoveDirection.Right);
			} else if (Input.GetAxis("VerticalA") > 0){
				board.MoveFawn(MoveDirection.Up);
			} else if (Input.GetAxis("VerticalA") < 0){
				board.MoveFawn(MoveDirection.Down);
			}
		}
	}
}
