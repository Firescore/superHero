using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class amy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anime;
    [SerializeField] private Transform heroObject;

    [SerializeField] private float radius = 0.5f, distanceFromHero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        masuering();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void masuering()
    {
        distanceFromHero = Vector3.Distance(transform.position, heroObject.position);
        if (distanceFromHero > radius)
        {
            agent.SetDestination(heroObject.position);
            anime.SetBool("run", true);
            transform.LookAt(heroObject);
        }
        if (distanceFromHero < radius)
        {
            anime.SetBool("run", false);
        }
    }
}
