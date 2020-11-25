using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwerContainerData : MonoBehaviour
{
    public EnemyThrower em;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
    }

    [Header("Rotation Value")]
    public float min;
    public float max;
    public float current;
    public float speed;

    public bool start, end;
    void rotate()
    {
        if (!em.grabbed)
        {
            if (current >= max)
            {
                start = true;
                end = false;
            }
            if (current <= min)
            {
                start = false;
                end = true;
            }

            if (start)
            {
                current -= speed;
            }
            if (!start)
            {
                current += speed;
            }
            transform.rotation = Quaternion.Euler(0, current, 0);
        }



    }
}
