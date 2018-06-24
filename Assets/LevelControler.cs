using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControler : MonoBehaviour
{
    public static LevelControler current;
    Vector3 startingPosition;

    void Awake()
    {
        current = this;
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }
    public void onRabitDeath(HeroRabit rabit)
    {
        if (rabit.isBig())
        {
            Vector3 size = rabit.transform.localScale;
            rabit.transform.localScale = new Vector3(size.x / 1.3f, size.y / 1.3f, 0);
            rabit.setBig(false);
        }
        //При смерті кролика повертаємо на початкову позицію
        rabit.transform.position = this.startingPosition;
        rabit.transform.rotation = new UnityEngine.Quaternion(0, 0, 0, 0);
    }
}
