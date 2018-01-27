using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSlotManager : MonoBehaviour {

    public static ShroomSlotManager Instance { get; private set; }

    [SerializeField]
    private Shroom[] shroomPrefabs;
    private ShroomSlot[] slots;

	// Use this for initialization
	void Start ()
    {
        Instance = this;
        slots = GetComponentsInChildren<ShroomSlot>();
        
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].SetShroom(Static.GetRandom(shroomPrefabs));
        }
    }

    private void Update()
    {
        // Test
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PopShroom();
        }
    }

    public Shroom PopShroom()
    {
        var shroom = slots[0].shroom;
        for(int i = 0; i < slots.Length - 1; i++)
        {
            slots[i].SetShroom(slots[i + 1].shroom);
        }
        slots[slots.Length - 1].SetShroom(Static.GetRandom(shroomPrefabs));
        return shroom;
    }
}
