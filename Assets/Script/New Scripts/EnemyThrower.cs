using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrower : MonoBehaviour
{

    #region init
    public static EnemyThrower emT;

    public GameObject Body;
    public GameObject Player;
    public GameObject slash;
    public GameObject particleEffect;
    public GameObject grabbedObject;

    [Header("Transform")]
    public Transform centre;
    public Transform gObjectHolder;

    public Animator anime;

    [Header("Charecter Analytics")]
    public float health = 10;
    

    [Header("Distance Checking")]
    [SerializeField] private float distanceFromPlayer = 0;
    public float maximumDistance;
    public float minimumDistance;

    [Header("Shooting Details")]
    public float killTime = 3;
    public float coolDown = 1;
    public float fireRate = 2;
    public float fireDelay = 0.5f;

    [Header("Navigation for using things")]
    public bool useFireBall = false;
    public bool added = false;
    

    [Header("Color Changer")]
    public float a = 0;

    [Header("Raycast Details")]
    public LayerMask layer;
    public float throwForce;
    public float grabbingDitance = 2f;
    public bool grabbed;
    public bool isThrowable;


    #endregion


    void Start()
    {
        emT = this;
    }


    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, Player.transform.position);
        obstracleManager();
        if ((distanceFromPlayer <= maximumDistance && distanceFromPlayer > minimumDistance))
        {
            throwObjOnPlayer();
        }
    }

    void obstracleManager()
    {
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

    public void throwObjOnPlayer()
    {
        transform.LookAt(Player.transform.position);
        if (!grabbed && grabbedObject == null)
        {
            RaycastHit hit;
            Ray directionRay = new Ray(centre.position, centre.forward);
            if (Physics.Raycast(directionRay, out hit, grabbingDitance, layer))
            {
                if (hit.collider.tag == "enemy")
                {
                    grabbed = true;
                    if (grabbed)
                    {
                        grabbedObject = hit.collider.gameObject;
                        grabbedObject.transform.SetParent(gObjectHolder);
                        grabbedObject.gameObject.transform.position = gObjectHolder.position;
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.GetComponent<Animator>().SetBool("fall", true);
                        grabbedObject.GetComponent<Collider>().enabled = false;
                    }
                }
            }
        }
        if (grabbed)
        {
            StartCoroutine(shoot(fireDelay));
        }
        
    }

    bool isTrigger = false;
    IEnumerator shoot(float t)
    {
            if (!isTrigger)
            {
                anime.SetTrigger("throw");
                isTrigger = true;
            }
            yield return new WaitForSeconds(t);
            gObjectHolder.DetachChildren();
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Collider>().enabled = true;
            grabbedObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
            grabbedObject.layer = default;
            Destroy(grabbedObject, killTime);
            yield return new WaitForSeconds(1f);
            grabbed = false;
            isTrigger = false;

    }
}
