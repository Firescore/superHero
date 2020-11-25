using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Obstracle : MonoBehaviour
{
    public static Obstracle obstracle;


    public NavMeshAgent agent;
    public Animator anime;
    public GameObject Body, Player, slash, particleEffect;
    public GameObject fireBall;

    public Transform shootPoint;

    [Header("Enemy universal values")]
    public float speed = 0.5f;
    public float redius = 5;
    public float health = 10;

    [Header("Shooting Details")]
    public float killTime = 3;
    public float coolDown = 1;
    public float fireRate = 2;
    public float fireDelay = 0.5f;

    [Header("Color Changer")]
    public float a = 0;

    [Header("Navigation for using things")]
    public bool useNavigation = true;
    public bool runTowardsPlayer = false;
    public bool useFireBall = false;

    [SerializeField]
    private float distanceFromPlayer = 0;

    private bool on = true, off = false, lerp=false, added = false, isShieldCollided = false;


    public void Start()
    {
        obstracle = this;
    }
    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, Player.transform.position);
        obstracleManager();

        if (!useFireBall)
        {
            followObject();
        }
        if (useFireBall)
        {
            throwBall();
        }
    }


    public float x, y;

    void followObject()
    {
        if (useNavigation)
        {
            if (!isShieldCollided)
                anime.SetBool("walk", true);
            transform.LookAt(Player.transform.position);
            agent.SetDestination(Player.transform.position);
        }
        if (!useNavigation)
        {
            agent.enabled = false;
            if((distanceFromPlayer <= x && distanceFromPlayer > y) || runTowardsPlayer)
            {
                gameObject.GetComponent<rightleftScript>().enabled = false;
                transform.LookAt(Player.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                anime.SetBool("walk", true);
            }


        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("shield"))
        {
            isShieldCollided = true;
            Debug.Log("Test");
            speed = 0;
            anime.SetBool("walk", false);
        }
    }

    void obstracleManager()
    {
        runTowardsPlayer = Player.GetComponent<ColliderCheck>().runTowardP;
        if (health <= 0)
        {
            Destroy(Instantiate(slash, transform.position, Quaternion.Euler(0, 90, 0)), 5f);
            Destroy(Instantiate(particleEffect, transform.position + new Vector3(0, 1, 0), Quaternion.identity), 1f);
            Destroy(gameObject);
        }

        Body.GetComponent<Renderer>().material.SetFloat("_Blend", a);

        if (health <= 0 && !added)
        {
            GameManager.Manager.Enemies = GameManager.Manager.Enemies + 1;
            GameManager.Manager.CurrentEnemy = GameManager.Manager.CurrentEnemy + 1;
            added = true;
        }
    }

    void throwBall()
    {
        if ((distanceFromPlayer <= x && distanceFromPlayer > y))
        {
            transform.LookAt(Player.transform.position);
            coolDown -= Time.deltaTime;
            if (coolDown <= 0)
            {
                StartCoroutine(shoot(fireDelay));
                coolDown = 2 / fireRate;
            }
        }
            
    }
    IEnumerator shoot(float t)
    {
        anime.SetTrigger("shoot");
        yield return new WaitForSeconds(t);
        Destroy(Instantiate(fireBall, shootPoint.position, shootPoint.rotation), killTime);
    }
}
