using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headRotation : MonoBehaviour
{
    public float speedOfRotation = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //directiion();
    }
    void directiion()
    {
        if (GameManager.Manager.Ditection != null)
        {
            if (GameManager.Manager.Ditection.CompareTag("enemy"))
            {
                Vector3 directions = GameManager.Manager.Ditection.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(directions);
                Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * speedOfRotation);
                transform.rotation = lookAt;
            }
            
        }
    }
}
