using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomMovement : MonoBehaviour {

    int i = 0;
    Animator animator;
    // Use this for initialization
    void Start()
    {
        MusicManager.OnBeat += Move;
        animator = GetComponentInChildren<Animator>();
    }
    
    private void Move()
    {
        if (i++ % 2 == 0)
        {
            animator.SetTrigger("Jump");
            if (Input.GetAxis("HorizontalB") > 0)
            {
                GenerateBoard.Instance.MoveShroom(MoveDirection.Left);
            }
            else if (Input.GetAxis("HorizontalB") < 0)
            {
                GenerateBoard.Instance.MoveShroom(MoveDirection.Right);
            }
            else if (Input.GetAxis("VerticalB") > 0)
            {
                GenerateBoard.Instance.MoveShroom(MoveDirection.Up);
            }
            else if (Input.GetAxis("VerticalB") < 0)
            {
                GenerateBoard.Instance.MoveShroom(MoveDirection.Down);
            } else if(Input.GetAxis("SubmitB") > 0)
            {
                GenerateBoard.Instance.SpawnShroom();
            } else
            {
                ScoreManager.Instance.GenerateFloatText(0, false);
            }
        }
    }
}
