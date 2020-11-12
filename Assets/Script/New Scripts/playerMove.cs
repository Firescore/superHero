using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float moveSpeed;
    public JoyStick jS;
    private Animator anime;
    private Vector3 moveVector;
    [Header("Enegry Effects")]
    [SerializeField] private GameObject energyExplode;
    [SerializeField] private GameObject energy;

/*    [Header("Enegry Effects Controller")]
    [SerializeField] private float increamentSpeed = 0;
    [SerializeField] private float maxSize = 0.5f;
    [SerializeField] private float minSize = 0;
    [SerializeField] private float size = 1;*/

    private bool start, end, spwanEnegry;
    [Header("Cool Down")]
    public float coolDownT;
    public float spwanRate;

    void Start()
    {
        anime = transform.GetChild(0).GetComponent<Animator>();
        start = end = spwanEnegry =false;
        //size = minSize;
    }


    void Update()
    {
        moveVector.x = jS.Horizontal();
        moveVector.z = jS.Vertical();
        move();
        isMoving();
        rotation();
        powerBar();
        spwanEnery();
        //enegryRuntime();
        cooldownTime();
    }
    private void move()
    {
        transform.Translate(moveVector * moveSpeed * Time.deltaTime,Space.World);
    }
    private void isMoving()
    {
        if(jS.Horizontal() == 0f || jS.Vertical() == 0f)
        {
            anime.SetBool("run", false);
        }
        else
        {
            anime.SetBool("run", true);
        }
    }
    private void rotation()
    {
        Vector3 dir,lookDir;
        dir = new Vector3(moveVector.x, 0, moveVector.z).normalized;
        lookDir = transform.position + dir;
        transform.LookAt(lookDir);
    }
    public void powerBar()
    {
        if (Input.GetMouseButton(0) && coolDownT <=0)
        {
            //size += increamentSpeed;
            end = false;
            start = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            //size = minSize;
            coolDownT = spwanRate;
            end = true;
            start = false;
        }
        /*if(size >= maxSize)
        {
            size = maxSize;
        }*/
    }
    public void spwanEnery()
    {
        if (start && !spwanEnegry && energy == null)
        {
            energy = Instantiate(energyExplode, transform.position + new Vector3(0,0.05f,0), Quaternion.Euler(90, 0, 0));
            energy.transform.parent = transform;
            spwanEnegry = true;
        }
        if (end && spwanEnegry)
        {
            Destroy(energy,spwanRate);
            spwanEnegry = false;
        }
    }
    /*public void enegryRuntime()
    {
        if (energy !=null)
        {
            if (start)
            {
                energy.transform.localScale = new Vector3(size, size, 1);
            }
        }
    }*/
    void cooldownTime()
    {
        coolDownT -= Time.deltaTime;
    }
}
