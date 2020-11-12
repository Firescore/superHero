using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [SerializeField]private bool jump = false;
    public Animator anime;
    public GameObject player;
    [SerializeField]private float dist = 10;
    public float minDist = 1;
    // Start is called before the first frame update
    void Start()
    {
        jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        dist = Vector3.Distance(transform.position, player.transform.position);
    }
    void Jump()
    {
        if (dist < 2.5f)
        {
            jump = true;
        }
        if (jump)
        {
            anime.SetBool("Jump", true);
        }
    }
    public void setplace()
    {
        transform.parent = player.transform;
        Vector3 a = new Vector3(-0.3f, 2.4f, 3.6f);
        Vector3 b = new Vector3(-18, -61, 30);
        transform.localPosition = Vector3.Lerp(transform.localPosition, a, 5 * Time.deltaTime);
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, b, 5 * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, minDist);
    }
}
