using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerBar : MonoBehaviour
{
    public static powerBar power;
    float x = 0.1f;
    float localScale;
    float StartSize = 0.5f;
    float currentSize = 0.5f;
    float maxSize = 3;

    bool start = false;

    public float countOfEnemy = 0;

    public Collider col;
    // Start is called before the first frame update
    void Start()
    {
        power = this;
        currentSize = StartSize;
        countOfEnemy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ring();
        colliderChheck();
    }
    public void ring()
    {
        if (!start)
        {
            countOfEnemy = GameManager.Manager.CurrentEnemy;
            currentSize = (countOfEnemy / 10);

            if (currentSize >= maxSize)
            {
                currentSize = maxSize;
            }

            transform.localScale = new Vector3(currentSize, currentSize, 1);
        }

        if (Input.GetMouseButtonUp(0))
        {
            start = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            start = false;
        }
        if (start)
        {
            localScale += x;
            if (localScale >= currentSize)
            {
                localScale = currentSize;
            }
            transform.localScale = new Vector3(localScale, localScale, 1);
        }
    }
    void colliderChheck()
    {
        if (!start)
        {
            col.isTrigger = true;
        }
        if (start)
        {
            col.isTrigger = false;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            col.gameObject.GetComponent<Obstracle>().health = 0;
        }
    }
}
