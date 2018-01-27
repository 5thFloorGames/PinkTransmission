using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	int i = 0;
	GenerateBoard board;

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<GenerateBoard>();
		AkSoundEngine.PostEvent("PlayPlaceholderLoop", gameObject, 0x0100, Move, null);		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void Move(object in_cookie, AkCallbackType in_type, object in_info){
		if(i++ % 2 == 0){
			if(Input.GetAxis("Horizontal") > 0){
				board.MoveFawn(MoveDirection.Left);
			} else if (Input.GetAxis("Horizontal") < 0){
				board.MoveFawn(MoveDirection.Right);
			} else if(Input.GetAxis("Vertical") > 0){
				board.MoveFawn(MoveDirection.Up);
			} else if (Input.GetAxis("Vertical") < 0){
				board.MoveFawn(MoveDirection.Down);
			}
		}
	}
}
