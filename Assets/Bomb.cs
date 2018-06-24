using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable
{
    protected override void OnRabitHit(HeroRabit rabit)
    {
        if (rabit.isBig())
        {
            Vector3 size = rabit.transform.localScale;
            rabit.transform.localScale = new Vector3(size.x / 1.3f, size.y / 1.3f, 0);
            rabit.setBig(false);
        }
        else
        {
            rabit.GetComponent<Animator>().SetBool("death", true);
        }
        this.CollectedHide();
    }
}
