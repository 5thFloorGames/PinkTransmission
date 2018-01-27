using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShroomSlot : MonoBehaviour {

    public Shroom shroom;
    private Image iconImage;

	// Use this for initialization
	void Start () {
        iconImage = transform.Find("Image").GetComponent<Image>();
	}
	
	public void SetShroom(Shroom shroom)
    {
        this.shroom = shroom;
        iconImage.sprite = shroom.icon;
    }
}
