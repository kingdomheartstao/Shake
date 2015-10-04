using UnityEngine;
using System.Collections;

public class Check : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
            transform.GetComponent<Rigidbody2D>().isKinematic = true;
            Debug.Log("ops");
        }
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
