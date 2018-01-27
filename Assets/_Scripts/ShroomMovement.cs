using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomMovement : MonoBehaviour {

	int i = 1;
	GenerateBoard board;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
		board = FindObjectOfType<GenerateBoard>();
		MusicManager.OnBeat += Move;
	}

	void Move(){
		if(i++ % 2 == 0){
			if(Input.GetAxis("HorizontalB") > 0){
				board.MoveShroom(MoveDirection.Left);
			} else if (Input.GetAxis("HorizontalB") < 0){
				board.MoveShroom(MoveDirection.Right);
			} else if(Input.GetAxis("VerticalB") > 0){
				board.MoveShroom(MoveDirection.Up);
			} else if (Input.GetAxis("VerticalB") < 0){
				board.MoveShroom(MoveDirection.Down);
			}
		}
	}
}
