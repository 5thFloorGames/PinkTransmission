using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerID
{
    A, B
}

public class Player : MonoBehaviour {

    [SerializeField]
    private PlayerID id;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckInput();
	}

    private void CheckInput()
    {
        var dx = Input.GetAxis("Horizontal" + id);
        var dy = Input.GetAxis("Vertical" + id);
    }
}
