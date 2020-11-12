using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;
    public GameObject Ditection;
    public GameObject laserParticle;
    public float obstracleHealthDeduction = 1;
    public float Enemies = 0;
    public float lerpSpeed = 0.2f;

    public float CurrentEnemy = 0;

    // Start is called before the first frame update
    void Start()
    {
        Manager = this;
        Application.targetFrameRate = 60;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(setCurrentEnemyCountZero(1));
        }
    }
    IEnumerator setCurrentEnemyCountZero(float t)
    {
        yield return new WaitForSeconds(t);
        CurrentEnemy = 1;
    }


}
