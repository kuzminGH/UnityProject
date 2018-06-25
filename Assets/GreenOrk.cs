using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrk : Ork
{

    public float runSpeed = 3;


    protected override void checkAttack()
    {
        if (GetComponent<BoxCollider2D>().IsTouching(HeroRabit.current.GetComponent<BoxCollider2D>()))
        {
            if (!animator.GetBool("isAttack")) animator.SetTrigger("isAttack");
            if (HeroRabit.current.transform.position.y > transform.position.y+1)
            {
                animator.SetBool("death", true);
                return;
            }
            else HeroRabit.current.GetComponent<Animator>().SetBool("death", true);
        }

    }

    protected override float attackDirection()
    {
        Vector3 myPosition = this.transform.position;
        Vector3 rabit_pos = HeroRabit.current.transform.position;
        //Move towards rabit
        if (speed != runSpeed) speed = runSpeed;

        if (GetComponent<BoxCollider2D>().IsTouching(HeroRabit.current.GetComponent<BoxCollider2D>()))
        {
            return 0;
        }
        if (myPosition.x < rabit_pos.x)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }


    protected override bool isDieAnimation()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("orcGreenDie");
    }

}