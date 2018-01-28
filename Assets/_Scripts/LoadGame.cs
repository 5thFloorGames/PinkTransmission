﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Load(2.0f));
	}
	
    private IEnumerator Load(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("Game");
    }
}
