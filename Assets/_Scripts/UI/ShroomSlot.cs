using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShroomSlot : MonoBehaviour {

    public Shroom shroom;
    private Image iconImage;
    
	public void SetShroom(Shroom shroom)
    {
        if(!iconImage)
            iconImage = transform.Find("Image").GetComponent<Image>();

        this.shroom = shroom;
        iconImage.sprite = shroom.icon;
    }
}
