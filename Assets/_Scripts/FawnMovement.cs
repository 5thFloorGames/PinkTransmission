﻿using System.Collections;
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
			if(!shuffleOn){
	            GenerateBoard.Instance.MoveFawn(d);
			} else {
				GenerateBoard.Instance.MoveFawn(shuffled[d]);
			}
            StartCoroutine(ResetMove());
        } else {
            ScoreManager.Instance.GenerateFloatText(0, true);
        }
    }

	void ShuffleMoves(){
		Static.Shuffle(moves);
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
