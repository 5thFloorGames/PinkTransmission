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
    
    private void CheckDirection(MoveDirection d){
        if(music.CloseToHalfBar()){
            GenerateBoard.Instance.MoveShroom(d);
            StartCoroutine(ResetMove());
        } else {
            ScoreManager.Instance.GenerateFloatText(0, false);
        }
    }

    private void Update(){
        if(!jumpedAlready){
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                CheckDirection(MoveDirection.Up);
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                CheckDirection(MoveDirection.Down);
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                CheckDirection(MoveDirection.Right);
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                CheckDirection(MoveDirection.Left);
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                if(music.CloseToHalfBar()){
                    GenerateBoard.Instance.SpawnShroom();
                    ShroomStinger();
                    StartCoroutine(ResetMove());
                } else {
                    ScoreManager.Instance.GenerateFloatText(0, false);
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
