using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable
{

    public float speed = 2.5f;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(destroyLater());
    }

    public void launch(float direction)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed, 0);
        if (direction < 0) GetComponent<SpriteRenderer>().flipX = true;
        else GetComponent<SpriteRenderer>().flipX = false;
    }

    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }

    protected override void OnRabitHit(HeroRabit rabit)
    {
        if (rabit.isBig())
        {
            rabit.setBig(false);
            Vector3 currSize = rabit.transform.localScale;
            rabit.transform.localScale = new Vector3(currSize.x / 1.5f, currSize.y / 1.5f, 0);
        }
        else
        {
            rabit.GetComponent<Animator>().SetBool("death", true);
        }
        this.CollectedHide();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if (rabit != null)
            this.OnRabitHit(rabit);
        else
        {
            BrownOrc orc = collider.GetComponent<BrownOrc>();
            if (orc== null) this.CollectedHide();
        }
    }

}
