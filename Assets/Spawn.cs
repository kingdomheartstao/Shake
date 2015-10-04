using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    float timer = 0;
	
	// Update is called once per frame
	void Update () {
	    if(timer < 1)
        {
            GameObject enemy = Instantiate(Resources.Load("Enemy") as GameObject);
            enemy.transform.position = transform.position;
        }
        timer += Time.deltaTime;
	}
}
