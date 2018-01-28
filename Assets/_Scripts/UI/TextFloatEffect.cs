using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFloatEffect : MonoBehaviour {

    public float time = 0.5f, distance = 100;
    private float currentTime, speed;

	// Use this for initialization
	void Start ()
    {
        speed = distance / time;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentTime += Time.deltaTime;
        transform.localPosition += speed * Time.deltaTime * Vector3.up;

        if(currentTime >= time)
        {
            Destroy(gameObject);
        }
	}
}
