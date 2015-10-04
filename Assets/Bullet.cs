using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
    public float speed = 1f;
    bool isMove = true;
	float timer = 0;
    int drt = 0;
    GameObject gun;
	void Start () {
        gun = GameObject.Find("Gun");
        drt = gun.GetComponent<Gun>().drt;
        transform.position = gun.transform.position + new Vector3(0.2f*drt,0,1);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Gun" || collider.tag == "Player" || collider.tag == "BackGround")
            return;
        if (collider.tag == "Enemy")
            if (collider.GetComponent<Enemy>().isDeath)
            {
                return;
            }
        Debug.Log("boom!");
        MainCamera.isShake = true;
        Destroy(gameObject);
        Destroy(this);
    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log("boom!");
    }
	
	// Update is called once per frame
	void Update () {
        if (timer > 0.025 && isMove)
        {
            timer = 0;
            if(drt > 0)
                transform.position += transform.rotation * new Vector3(speed *0.05f, 0, 0) ;
            else if(drt < 0)
                transform.position -= transform.rotation * new Vector3(speed *0.05f, 0, 0);
        }
        timer += Time.deltaTime;
        if (transform.position.x > 50 || transform.position.x < -50)
        {
            DestroyImmediate(gameObject);
            Destroy(this);
        }
	}
}
