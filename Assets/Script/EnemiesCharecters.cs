using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCharecters : MonoBehaviour
{
    public static EnemiesCharecters enemiesCharecters;
    public List<GameObject> allChild = new List<GameObject>();
    private void Start()
    {
        enemiesCharecters = this;

        foreach(Transform child in transform)
        {
            allChild.Add(child.gameObject);
        }
    }
}
