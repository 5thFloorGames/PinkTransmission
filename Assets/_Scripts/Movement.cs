using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	int i = 0;
	GenerateBoard board;

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<GenerateBoard>();
		MusicManager.OnBeat += Move;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void Move(){
		if(i++ % 2 == 0){
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
