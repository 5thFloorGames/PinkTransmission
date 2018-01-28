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
    
    public bool SpawnShroom(Vector2 pos)
    {
        return SpawnShroom((int)pos.x, (int)pos.y);
    }

    public bool SpawnShroom(int x, int y)
    {
        for (int i = spawnedShrooms.Count - 1; i >= 0; i--)
        {
            var ss = spawnedShrooms[i];

            if(!ss)
            {
                spawnedShrooms.RemoveAt(i);
                continue;
            }

            if (ss.x == x && ss.y == y)
            {
                Debug.Log("Shroom exists at location.");
                return false;
            }
        }
        var shroom = Instantiate(PopShroom());
        shroom.SetPos(x, y);
        spawnedShrooms.Add(shroom);
        return true;
    }

    public ShroomType CheckShroomDestroy(int x, int y)
    {
        for (int i = spawnedShrooms.Count - 1; i >= 0; i--)
        {
            var ss = spawnedShrooms[i];

            if (!ss)
            {
                spawnedShrooms.RemoveAt(i);
                continue;
            }

            if (ss.x == x && ss.y == y)
            {
                var type = ss.GetShroomType();
                Destroy(ss.gameObject);
                spawnedShrooms.RemoveAt(i);
                return type;
            }
        }
        return ShroomType.None;
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
