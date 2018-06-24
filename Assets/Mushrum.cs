using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushrum : Collectable
{
    protected override void OnRabitHit(HeroRabit rabit)
    {
        if (!rabit.isBig())
        {
            rabit.setBig(true);
            Vector3 size = rabit.transform.localScale;
            rabit.transform.localScale = new Vector3(size.x * 1.3f, size.y * 1.3f, 0);
        }
        this.CollectedHide();
    }
}