using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{

    public GameObject Body;
    public GameObject slash;
    public GameObject particleEffect;
    public float health = 10;
    public float a = 0;
    public bool added = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        obstracleManager();
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
}
