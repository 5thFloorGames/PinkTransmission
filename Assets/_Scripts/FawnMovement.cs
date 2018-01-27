using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FawnMovement : MonoBehaviour {

	int i = 0;
	GenerateBoard board;
	Animator animator;
	MusicManager music;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
		board = FindObjectOfType<GenerateBoard>();
		music = FindObjectOfType<MusicManager>();
		//MusicManager.OnBeat += Move;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.W)){
			if(music.CloseToBeat()){
				animator.SetTrigger("Jump");
				board.MoveFawn(MoveDirection.Up);
			}
		}
		if(Input.GetKeyDown(KeyCode.S)){
			if(music.CloseToBeat()){
				animator.SetTrigger("Jump");
				board.MoveFawn(MoveDirection.Down);
			}
		}
		if(Input.GetKeyDown(KeyCode.A)){
			if(music.CloseToBeat()){
				animator.SetTrigger("Jump");
				board.MoveFawn(MoveDirection.Right);
			}
		}
		if(Input.GetKeyDown(KeyCode.D)){
			if(music.CloseToBeat()){
				animator.SetTrigger("Jump");
				board.MoveFawn(MoveDirection.Left);
			}
		}
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
