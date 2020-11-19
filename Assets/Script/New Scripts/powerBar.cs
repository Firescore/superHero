using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerBar : MonoBehaviour
{
    public static powerBar power;
    float x = 0.1f;
    float localScale;
    float currentSize = 1f;
    float StartSize = 1f;
    float maxSize = 4.5f;

    bool start = false;

    public float countOfEnemy = 0;
    public GameObject shild;

    public Collider col;
    // Start is called before the first frame update
    void Start()
    {
        power = this;
        currentSize = StartSize;
        countOfEnemy = 0;
        shild.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerMove.player.transform.position.x, playerMove.player.powerBarYPos, playerMove.player.transform.position.z);
        ring();
        colliderChheck();
    }
    float test = 1;
    public void ring()
    {
        
        if (!start)
        {
            countOfEnemy = GameManager.Manager.CurrentEnemy;

            currentSize = (countOfEnemy);

            if (currentSize >= maxSize)
            {
                currentSize = maxSize;
            }
            if(currentSize < maxSize)
            {
                test += x;

                if (test >= currentSize)
                {
                    test = currentSize;
                }
            }

            transform.localScale = new Vector3(test, test, test);
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
            shild.SetActive(true);
            localScale += x;
            if (localScale >= currentSize)
            {
                localScale = currentSize;
            }
            transform.localScale = new Vector3(localScale, localScale, localScale);
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
