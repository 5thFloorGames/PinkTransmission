using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSlotManager : MonoBehaviour {

    public static ShroomSlotManager Instance { get; private set; }

    [SerializeField]
    private Shroom[] shroomPrefabs;
    private ShroomSlot[] slots;

    private List<Shroom> spawnedShrooms;

    // Use this for initialization
    void Start ()
    {
        Instance = this;
        slots = GetComponentsInChildren<ShroomSlot>();
        spawnedShrooms = new List<Shroom>();

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].SetShroom(Static.GetRandom(shroomPrefabs));
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnShroom(Random.Range(0,GenerateBoard.Instance.XSize), Random.Range(0, GenerateBoard.Instance.YSize));
        }
    }

    public void SpawnShroom(Vector3 pos)
    {
        SpawnShroom((int)pos.x, (int)pos.y);
    }

    public void SpawnShroom(int x, int y)
    {
        var shroom = Instantiate(PopShroom());
        shroom.SetPos(x, y);
        spawnedShrooms.Add(shroom);
    }

    /// <summary>
    /// Takes and returns the first shroom from the 'tetris' slot
    /// </summary>
    /// <returns></returns>
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
