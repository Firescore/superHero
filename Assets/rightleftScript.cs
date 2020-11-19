using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightleftScript : MonoBehaviour
{
    public Vector3 start, end;
    public bool pStart,pEnd;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        check();
    }
    void check()
    {
        if (transform.position.z <= start.z)
        {
            pStart = true;
            //pEnd = true;
        }
        else if(transform.position == end)
        {
            pStart = false;
            //pEnd = false;
        }
    }
    void move()
    {
        if(pStart)
        {
            gameObject.GetComponent<Animator>().SetBool("walk", true);
            transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (!pStart)
        {
            gameObject.GetComponent<Animator>().SetBool("walk", true);
            transform.position = Vector3.MoveTowards(transform.position, start, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
