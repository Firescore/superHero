using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{

    private float Horizontal;
    public GameObject laser;
    public float turnStrength = 90;
    private float laserValue;

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        laserValue += (Horizontal * turnStrength * Time.deltaTime);
        laser.transform.rotation = Quaternion.Euler(0, laserValue, 0);

        leftCheck();
    }

    void leftCheck()
    {
        /*if (laserValue > -1)
        {
            laserValue = -1;
        }*/

    }
}
