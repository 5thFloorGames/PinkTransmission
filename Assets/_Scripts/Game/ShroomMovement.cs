using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomMovement : MonoBehaviour {

    int i = 0;
    Animator animator;

    MusicManager music;

    bool jumpedAlready = false;
    // Use this for initialization
    void Start()
    {
        music = FindObjectOfType<MusicManager>();
        MusicManager.OnBeat += Move;
        animator = GetComponentInChildren<Animator>();
    }
    
    private void Update(){
        if(!jumpedAlready){
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                if(music.CloseToHalfBar()){
                    GenerateBoard.Instance.MoveShroom(MoveDirection.Up);
                    StartCoroutine(ResetMove());
                }
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                if(music.CloseToHalfBar()){
                    GenerateBoard.Instance.MoveShroom(MoveDirection.Down);
                    StartCoroutine(ResetMove());
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                if(music.CloseToHalfBar()){
                    GenerateBoard.Instance.MoveShroom(MoveDirection.Right);
                    StartCoroutine(ResetMove());
                }
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                if(music.CloseToHalfBar()){
                    GenerateBoard.Instance.MoveShroom(MoveDirection.Left);
                    StartCoroutine(ResetMove());
                }
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                if(music.CloseToHalfBar()){
                    GenerateBoard.Instance.SpawnShroom();
                    ShroomStinger();
                    StartCoroutine(ResetMove());
                }
            }
        }
    }

    IEnumerator ResetMove(){
		jumpedAlready = true;
		yield return new WaitForSeconds(music.TimeToNextBeat() + 0.1f);
		jumpedAlready = false;
	}

    private void Move()
    {
        if (i++ % 2 == 0)
        {
            animator.SetTrigger("Jump");
        }
    }

    public void ShroomStinger(){
		AkSoundEngine.PostEvent("ActionShroomStinger", gameObject);
	}
}
