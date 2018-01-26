using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public static class Static
{
    public static readonly string endSceneName = "End", creationSceneName = "CharacterCreation",
        gameSceneName = "Game", loadingSceneName = "Loading", menuSceneName = "Menu";
    
    public static bool TouchedOverUI
    {
        get { return EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null; }
    }

    public static void Shuffle<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int idx = Random.Range(i, array.Length);

            //swap elements
            T tmp = array[i];
            array[i] = array[idx];
            array[idx] = tmp;
        }
    }
    public static T GetRandom<T>(this T[] array)
    {
        if(array.Length == 0)
        {
            Debug.Log("Warning! Getting random value from empty array!");
            return default(T);
        }
        return array[Random.Range(0, array.Length)];
    }

    public static float AngleTo(this Vector2 this_, Vector2 to)
    {
        Vector2 direction = to - this_;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0f) angle += 360f;
        return angle;
    }

    public static float VectorSqrDistance(Vector3 a, Vector3 b)
    {
        return (b - a).sqrMagnitude;
    }
    public static T GetComponentInChildrenOfAsset<T>(this GameObject go) where T : Component
    {
        List<Transform> tfs = new List<Transform>();
        CollectChildren(tfs, go.transform);
        for (int i = 0; i < tfs.Count; i++)
        {
            T temp = tfs[i].GetComponent<T>();

            if (temp)
                return temp;
        }
        return null;
    }
    public static T[] GetComponentsInChildrenOfAsset<T>(this GameObject go) where T : Component
    {
        List<Transform> tfs = new List<Transform>();
        CollectChildren(tfs, go.transform);
        List<T> all = new List<T>();
        for (int i = 0; i < tfs.Count; i++)
            all.AddRange(tfs[i].gameObject.GetComponents<T>());
        return all.ToArray();
    }

    static void CollectChildren(List<Transform> transforms, Transform tf)
    {
        transforms.Add(tf);
        foreach (Transform child in tf)
        {
            CollectChildren(transforms, child);
        }
    }
}
