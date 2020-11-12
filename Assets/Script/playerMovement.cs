using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Transform headRotation;
    [SerializeField] private Animator anime;
    [SerializeField] private GameObject Laser, cape1,cape2;
    private bool finish_1 = false, stop_1 = false, go = false;

    [SerializeField] private float rotation = 90;

    private void Start()
    {
        cape2.SetActive(false);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (finish_1)
        {
            speed = 5;
            Laser.SetActive(false);
            headRotation.localEulerAngles = new Vector3(0, 0, 0);
            //anime.SetBool("land", true);
        }
        if (stop_1)
        {
            //anime.SetBool("idle", true);
            cape1.SetActive(false);
            cape2.SetActive(true);
        }
        if(GameManager.Manager.Enemies >= 53 && !finish_1)
        {
            speed = 10;
            Laser.SetActive(false);
        }
        rotationD();
        StartCoroutine(rot(2.5f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish Line I") && !finish_1)
        {
            finish_1 = true;
        }
        if (other.CompareTag("Stop I") && !stop_1)
        {
            stop_1 = true;
        }
    }
    
    
    private void rotationD()
    {
        if(go)
        {
            rotation -= 5;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
            if (rotation <= 0)
            {
                rotation = 0;
            }
        }
        
    }
    IEnumerator rot(float t)
    {
        if (stop_1 && !go)
        {
            speed = 0;
            yield return new WaitForSeconds(t);
            speed = 10;
            go = true;
        }
        
    }
}
