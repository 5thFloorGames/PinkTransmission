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
        i = 0;
        music = FindObjectOfType<MusicManager>();
        MusicManager.OnBeat += Jump;
        animator = GetComponentInChildren<Animator>();
    }

    void OnDestroy()
    {
        MusicManager.OnBeat -= Jump;
    }

    private void CheckDirection(MoveDirection d){
        if(/*music.CloseToHalfBar()*/ScoreManager.Instance.shroomBeatAnimator.CloseToBeat()){
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
                if(/*music.CloseToHalfBar()*/ScoreManager.Instance.shroomBeatAnimator.CloseToBeat())
                {
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

    private void Jump()
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
