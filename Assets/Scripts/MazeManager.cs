using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


[Serializable]
class GameData
{
    public int score;
    Vector3 playerPos;
    Vector3 playerRot;
    Vector3 cameraLocalRot;
    Vector3 enemyPos;
    Vector3 enemyRot;
};

public class MazeManager : MonoBehaviour {

    public static MazeManager mzMngr;

    public GameObject start;
    public GameObject end;

    public Transform test;
    public Text HUDText;

    private EnemyController ec;
    public int score = 0;

    private void Awake()
    {
        if (mzMngr == null)
        {
            mzMngr = this;
        }
        else if (mzMngr != this)
        {
            //mzMngr.LoadScore();
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        SetupEnemy();
        //LoadScore();
    }

    // Update is called once per frame
    void Update()
    {
        HUDText.text = "Score: " + score;
    }

    void SetupEnemy()
    {
        GameObject enemy = Instantiate(Resources.Load("Models/Zombunny/Enemy", typeof(GameObject)), end.transform.position, new Quaternion(0f, 0f, 0f, 0f)) as GameObject;
        GameObject enemy2 = Instantiate(Resources.Load("Models/Zombunny/Enemy", typeof(GameObject)), test.position, new Quaternion(0f, 0f, 0f, 0f)) as GameObject;

        enemy2.GetComponent<NavMeshAgent>().enabled = false;

        ec = enemy.GetComponent<EnemyController>();
        ec.agent = enemy.GetComponent<NavMeshAgent>();

        ec.points = new Transform[2];
        ec.points[0] = start.transform;
        ec.points[1] = end.transform;
    }
	
    public void LoadScore()
    {
        string scoreStr = PlayerPrefs.GetString("CurrentScore", "0");
        score = System.Convert.ToInt32(scoreStr);

        HUDText.text = "Score: " + score;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetString("CurrentScore", score.ToString());
    }
}
