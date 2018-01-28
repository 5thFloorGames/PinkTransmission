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

    private void CheckDirection(MoveDirection d){
        if(music.CloseToBeat()){
            GenerateBoard.Instance.MoveFawn(d);
            StartCoroutine(ResetMove());
        } else {
            ScoreManager.Instance.GenerateFloatText(0, true);
        }
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
