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
    public float speed = 0.5f;
    public float redius = 5;
    public float health = 10;
    public float a = 0;

    public bool useNavigation = true;
    //public float speed = 0.5f;
    //public float timeLim = 0.5f;
    [SerializeField]
    private float distanceFromPlayer = 0;

    private bool on = true, off = false, lerp=false, added = false, isShieldCollided = false;


    public void Start()
    {
        obstracle = this;
    }
    private void Update()
    {

        followObject();
        if (health <= 0)
        {
            Destroy(Instantiate(slash, transform.position, Quaternion.Euler(0,90,0)), 5f);
            Destroy(Instantiate(particleEffect, transform.position + new Vector3(0,1,0), Quaternion.identity), 1f);
            Destroy(gameObject);
        }
        
        Body.GetComponent<Renderer>().material.SetFloat("_Blend", a);

        if(health <= 0 && !added)
        {
            GameManager.Manager.Enemies = GameManager.Manager.Enemies + 1;
            GameManager.Manager.CurrentEnemy = GameManager.Manager.CurrentEnemy + 1;
            added = true;
        }
    }





    public float x, y;

    void followObject()
    {
        if (useNavigation)
        {
            distanceFromPlayer = Vector3.Distance(transform.position, Player.transform.position);
            if (!isShieldCollided)
                anime.SetBool("walk", true);
            transform.LookAt(Player.transform.position);
            agent.SetDestination(Player.transform.position);
        }
        if (!useNavigation)
        {
            agent.enabled = false;
            distanceFromPlayer = Vector3.Distance(transform.position, Player.transform.position);


            if(distanceFromPlayer <= x && distanceFromPlayer > y)
            {
                gameObject.GetComponent<rightleftScript>().enabled = false;
                transform.LookAt(Player.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                anime.SetBool("walk", true);
            }
            if(distanceFromPlayer <= y)
            {
                anime.SetBool("walk", false);
            }


        }
    }
    IEnumerator moveToward(float t)
    {
        
        if (on)
        {
            yield return new WaitForSeconds(t);
            off = true;
            on = false;
        }
        if (off)
        {
            yield return new WaitForSeconds(t);
            off = false;
            on = true;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, redius);
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
}
