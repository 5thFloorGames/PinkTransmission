using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FawnMovement : MonoBehaviour {

	int i = 0;
	GenerateBoard board;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
		board = FindObjectOfType<GenerateBoard>();
		MusicManager.OnBeat += Move;
	}

	void Move(){
		if(i++ % 2 == 0)
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
