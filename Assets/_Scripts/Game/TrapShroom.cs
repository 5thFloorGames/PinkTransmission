using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShroom : Shroom
{
    public override ShroomType GetShroomType()
    {
        return ShroomType.Trap;
    }
}
