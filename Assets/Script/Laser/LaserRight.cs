using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRight : MonoBehaviour
{
    /*private LineRenderer lr;

    [SerializeField]
    private GameObject laserParticle;
    [SerializeField] private string enemyTag = "enemy";


    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
       
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,100))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
                GameManager.Manager.RightDitaction = hit.transform.gameObject;
                laserParticle.transform.position = lr.GetPosition(1);
                laserParticle.SetActive(true);
            }
        }
        else
        {
            lr.SetPosition(1, transform.forward * 500);
            laserParticle.SetActive(false);
        }


        if (GameManager.Manager.RightDitaction.CompareTag(enemyTag))
        {

            GameManager.Manager.RightDitaction.GetComponent<Obstracle>().health -= GameManager.Manager.obstracleHealthDeduction;

            if (GameManager.Manager.RightDitaction.GetComponent<Obstracle>().a <= 1)
            {
                GameManager.Manager.RightDitaction.GetComponent<Obstracle>().a += GameManager.Manager.lerpSpeed;
            }
        }
    }*/
}
