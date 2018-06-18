using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 pointA;
    Vector3 pointB;

    bool isMoveToPointA;

    public Vector3 Vector;
    Vector3 moveVector;
    Vector3 pauseVector;

    public float timeOfMovingDistance = 5;
    public float waitng_time = 1;
    public float currentWaiting;

    bool isArrived(Vector3 firstPos, Vector3 secondPos)
    {
        firstPos.z = 0;
        secondPos.z = 0;
        return Vector3.Distance(firstPos, secondPos) < 0.05f;
    }

    // Use this for initialization
    void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + Vector;
        

        moveVector = Vector/timeOfMovingDistance;
        pauseVector = new Vector3(0,0,0);
        isMoveToPointA = false;
        currentWaiting = 0;
    }

    void Update()
    {
        if (currentWaiting >= waitng_time)
        {
            Vector3 my_pos = this.transform.position;
            if (isMoveToPointA)
            {
                if (isArrived(my_pos, pointA))
                {
                    isMoveToPointA = false;
                    currentWaiting = 0;
                    moveVector.x *= -1;
                    moveVector.y *= -1;
                }
            }
            else
                if (isArrived(my_pos, pointB))
            {
                isMoveToPointA = true;
                currentWaiting = 0;
                moveVector.x = -moveVector.x;
                moveVector.y = -moveVector.y;
            }
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = moveVector;

        }
        else
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            currentWaiting += Time.deltaTime;
            rb.velocity = pauseVector;
        }
    }

}
