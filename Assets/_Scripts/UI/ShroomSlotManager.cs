using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSlotManager : MonoBehaviour {

    [SerializeField]
    private Shroom[] shroomPrefabs;
    private ShroomSlot[] slots;

	// Use this for initialization
	void Start ()
    {
        slots = GetComponentsInChildren<ShroomSlot>();
        
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].SetShroom(Static.GetRandom(shroomPrefabs));
        }
    }
}
