using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    private float maxNum, minNum, currentNum, speed, time;
    private bool shieldActivated = false;
    private bool front, back;
    [SerializeField] Animator anime;
    void Start()
    {
        currentNum = 4;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            anime.SetTrigger("smallHit");
        }
    }
    void shieldMoveForward()
    {
        if (currentNum >= maxNum)
        {
            back = true;
            front = false;
        }

        if (currentNum <= minNum)
        {
            back = false;
            front = true;
        }

        if (back && !front)
        {
            currentNum = currentNum - speed;
        }
        if(!back && front)
        {
            currentNum = currentNum + speed;
        }

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentNum);
    }
    IEnumerator shieldActivate(float t)
    {
        shieldMoveForward();
        yield return new WaitForSeconds(t);
        shieldActivated = false;
    }
}
