using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Obstracle : MonoBehaviour
{
    public static Obstracle obstracle;
    public NavMeshAgent agent;
    public Animator anime;
    public GameObject Head, Body, Player, slash;
    public float speed = 0.5f;
    public float redius = 5;
    public float health = 10;
    public float a = 0;

    public bool useNavigation = true;
    //public float speed = 0.5f;
    //public float timeLim = 0.5f;
    
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
            Destroy(Instantiate(slash, transform.position, Quaternion.identity), 01f);
            Destroy(gameObject);
        }


        Head.GetComponent<Renderer>().material.SetFloat("_Blend", a);
        Body.GetComponent<Renderer>().material.SetFloat("_Blend", a);

        if(health <= 0 && !added)
        {
            GameManager.Manager.Enemies = GameManager.Manager.Enemies + 1;
            GameManager.Manager.CurrentEnemy = GameManager.Manager.CurrentEnemy + 1;
            added = true;
        }
    }

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
            transform.LookAt(Player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            if (!isShieldCollided)
                anime.SetBool("walk", true);
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
