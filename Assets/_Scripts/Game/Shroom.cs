using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShroomType
{
    Normal, Trap
}

public class Shroom : MonoBehaviour {

    public ShroomType type;
    public Sprite icon;
    public int x, y;

    public virtual void DestroyShroom()
    {
        Destroy(gameObject);
    }

    public void SetPos(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.localPosition = GenerateBoard.Instance.GetPos(x, y);
    }

	public virtual void Explode()
    {
        // TODO: Shroom animation
        DestroyShroom();
    }
}
