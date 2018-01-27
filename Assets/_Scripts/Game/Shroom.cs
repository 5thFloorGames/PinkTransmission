using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : MonoBehaviour {

    public Sprite icon;

    public virtual void DestroyShroom()
    {
        Destroy(gameObject);
    }

	public virtual void Explode()
    {
        // TODO: Shroom animation
        DestroyShroom();
    }
}
