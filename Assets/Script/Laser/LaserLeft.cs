using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLeft : MonoBehaviour
{
    private LineRenderer lr1;
    [SerializeField]private GameObject laserParticle;
    public float speedOfRotation = 200;
    public LayerMask layer;

    public bool hideSparkles = false;

    private void Start()
    {
        lr1 = GetComponent<LineRenderer>();
    }

    void Update()
    {
        healthDeduction();
        RAYCASTING();

        lr1.SetPosition(0, transform.position);

        if(!hideSparkles)
            laserParticle.transform.position = lr1.GetPosition(1);
        if (hideSparkles)
            laserParticle.SetActive(false);

        if(GameManager.Manager.Enemies >= EnemiesCharecters.enemiesCharecters.allChild.Count)
        {
            lr1.enabled = false;
        }
    }

    void RAYCASTING()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,10))
        {
            if (hit.collider)
            {
                lr1.gameObject.SetActive(true);
                lr1.SetPosition(1, hit.point);
                laserParticle.SetActive(true);
                GameManager.Manager.Ditection = hit.transform.gameObject;
                if (hit.transform.CompareTag("girl"))
                {
                    lr1.enabled = false;
                }
                if (!hit.transform.CompareTag("girl"))
                {
                    lr1.enabled = true;
                }
            }
            else
            {
                lr1.gameObject.SetActive(false);
            }

        }
        else
        {
            lr1.SetPosition(1, transform.position);
            laserParticle.SetActive(false);
        }
    }

    void healthDeduction()
    {
        if (GameManager.Manager.Ditection != null)
        {
            if (GameManager.Manager.Ditection.CompareTag("enemy"))
            {
                GameManager.Manager.Ditection.GetComponent<Obstracle>().health -= GameManager.Manager.obstracleHealthDeduction;

                if (GameManager.Manager.Ditection.GetComponent<Obstracle>().a <= 1)
                {
                    GameManager.Manager.Ditection.GetComponent<Obstracle>().a += GameManager.Manager.lerpSpeed;
                }
            }
        }
    }
}
