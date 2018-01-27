using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomMovement : MonoBehaviour {

    int i = 0;
    // Use this for initialization
    void Start()
    {
        MusicManager.OnBeat += Move;
    }
    
    private void Move()
    {
        if (i++ % 2 == 0)
        {
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
            }
        }
    }
}
