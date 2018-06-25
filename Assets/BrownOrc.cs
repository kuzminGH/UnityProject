using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrc : Ork
{

    public GameObject prefabCarrot;
    public int timeOfThrowing;
    float timeCur;


    // Use this for initialization
    new void Start()
    {
        base.Start();
        timeCur = timeOfThrowing;

    }
    //stay when attack
    protected override float attackDirection()
    {
        return 0;
    }

    protected override void checkAttack()
    {
        Vector3 position = this.transform.position;
        Vector3 rabit_pos = HeroRabit.current.transform.position;
        if (GetComponent<BoxCollider2D>().IsTouching(HeroRabit.current.GetComponent<BoxCollider2D>()))
        {
            if (HeroRabit.current.transform.position.y > transform.position.y)
            {
                animator.SetBool("death", true);
                return;
            }
        }
        if (mode == Mode.Attack)//rabit_pos.x > pointA.x && rabit_pos.x < pointB.x
        {


            if (rabit_pos.x > position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                if (timeCur > 0)
                {
                    timeCur -= Time.deltaTime;
                    return;
                }
                animator.SetTrigger("isAttack");
                this.launchCarrot(1);
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                if (timeCur > 0)
                {
                    timeCur -= Time.deltaTime;
                    return;
                }
                animator.SetTrigger("isAttack");
                this.launchCarrot(-1);
            }
            timeCur = timeOfThrowing;
        }
    }

    void launchCarrot(float direction)
    {
        //Створюємо копію Prefab
        GameObject obj = GameObject.Instantiate(this.prefabCarrot);
        //Розміщуємо в просторі
        obj.transform.position = this.transform.position + new Vector3(0, this.GetComponent<BoxCollider2D>().size.y / 2 - 0.2f, 0);
        //Запускаємо в рух
        Carrot carrot = obj.GetComponent<Carrot>();

        carrot.launch(direction);
    }

    protected override bool isDieAnimation()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("brownOrcDie");
    }


}
