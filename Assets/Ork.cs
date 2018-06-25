using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ork : MonoBehaviour
{
    public Vector3 MoveBy;
    public float normalSpeed = 2;
    public float time_to_wait = 1;
    public Mode mode;

    protected Vector3 pointA;
    protected Vector3 pointB;
    protected float speed;
    protected Rigidbody2D myBody = null;
    protected Animator animator;

    float currentTimeOfWait = 0;


    protected abstract float attackDirection();
    protected abstract void checkAttack();
    protected abstract bool isDieAnimation();

    // Use this for initialization
    protected void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        if (pointA.x > pointB.x)
        {
            Vector3 copy = pointA;
            pointA = pointB;
            pointB = copy;
        }
        speed = normalSpeed;
        mode = Mode.GoToB;
        currentTimeOfWait = time_to_wait;
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (animator.GetBool("death"))
        {
            if (!isDieAnimation() || animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
                return;
            animator.SetBool("death", false);

            Destroy(this.gameObject);
            return;
        }

        checkAttack();
        float value = this.getDirection();

        if (mode == Mode.Stay)
        {
            if (currentTimeOfWait > 0)
            {
                currentTimeOfWait -= Time.deltaTime;
                return;
            }
            else
            {
                if (distX(transform.position, pointA) < distX(transform.position, pointB)) mode = Mode.GoToB;
                else mode = Mode.GoToA;
                animator.SetBool("stay", false);
            }
        }

        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", mode == Mode.Attack);
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;

        }
        animator.SetBool("run", mode == Mode.Attack);
        /* else
             if (mode == Mode.Attack) animator.SetTrigger("isAttack");
             */

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = false;
        }
        else if (value > 0)
        {
            sr.flipX = true;
        }


    }


    public enum Mode
    {
        GoToA,
        GoToB,
        Attack,
        Stay
    }

    float getDirection()
    {
        Vector3 myPosition = this.transform.position;
        Vector3 rabit_pos = HeroRabit.current.transform.position;

        if (rabit_pos.x > pointA.x && rabit_pos.x < pointB.x)
        {
            mode = Mode.Attack;
        }
        else if (mode == Mode.Attack)
        {
            mode = Mode.GoToA;
            speed = normalSpeed;
        }

        if (mode == Mode.Attack)
        {
            return attackDirection();
        }

        if (mode == Mode.GoToB)
        {
            if (isArrivedX(myPosition, pointB))
            {
                mode = Mode.Stay;
                currentTimeOfWait = time_to_wait;
                animator.SetBool("stay", true);
            }
            else return 1;
        }
        else if (mode == Mode.GoToA)
        {
            if (isArrivedX(myPosition, pointA))
            {
                mode = Mode.Stay;
                currentTimeOfWait = time_to_wait;
                animator.SetBool("stay", true);
            }
            else return -1;
        }

        return 0;
    }

    bool isArrivedX(Vector3 pos, Vector3 target)
    {
        return distX(pos, target) < 0.2f;
    }

    float distX(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        pos.y = 0;
        target.y = 0;
        return Vector3.Distance(pos, target);
    }

    public void hide()
    {
        Destroy(this.gameObject);
    }
}
