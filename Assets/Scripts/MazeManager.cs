using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeManager : MonoBehaviour {

    public GameObject start;
    public GameObject end;

    private EnemyController ec;

	// Use this for initialization
	void Start () {
        //Instantiate(Resources.Load("Prefabs/PreBuiltMaze", typeof(GameObject)), new Vector3(0f,1.5f,0f), new Quaternion(0f,0f,0f,0f));
        Instantiate(Resources.Load("Prefabs/PreBuiltMaze", typeof(GameObject)));
        GameObject enemy = Instantiate(Resources.Load("Models/Zombunny/Enemy", typeof(GameObject)), end.transform.position, new Quaternion(0f, 0f, 0f, 0f)) as GameObject;

        ec = enemy.GetComponent<EnemyController>();
        ec.agent = enemy.GetComponent<NavMeshAgent>();

        ec.points = new Transform[2];
        ec.points[0] = start.transform;
        ec.points[1] = end.transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
