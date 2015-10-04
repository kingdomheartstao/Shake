using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    int drt = -1;
    public bool isDeath = false;
    float speed = 6f;
    float timer = 0;
    float blockTimer = 0;
    Vector3 start;
    Vector3 end;




    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Death();
        }
        if (other.tag == "Ground" && isDeath)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    IEnumerator DeathAnima()
    {
        float x = 0;
        float y = 0;
        Debug.Log("Fly");
        for (float ltimer = 0; ltimer <= 0.8; ltimer += Time.deltaTime)
        {
            x = ltimer - 0.4f;
            y = -x*x + 0.16f;
            transform.position += new Vector3(ltimer * 0.1f, 0, 0);
            if (ltimer <= 0.4f)
            {
                transform.position += new Vector3(0, y * 0.2f, 0);
                transform.localScale -= new Vector3(0, ltimer * 0.1f, 0);
            }
            else
                transform.position -= new Vector3(0, y * 0.2f, 0);
            yield return 0;
        }
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    void Death()
    {
        if (isDeath)
            return;
        GetComponent<SpriteRenderer>().color = Color.gray;
        MainCamera.isShake = true;
        LayerMask.NameToLayer("BackGround");
        speed = 0;
        GetComponent<Collider2D>().isTrigger = true;
        StartCoroutine(DeathAnima());
        isDeath = true;
    }

	// Update is called once per frame
	void Update () {
        if (timer > 0.025)
        {
            timer = 0;
            transform.position += transform.rotation * new Vector3(speed * 0.025f * drt, speed * 0.0125f, 0);
        }
        timer += Time.deltaTime;
        
        if (blockTimer > 0.5)
        {
            blockTimer = 0;
            end = transform.position;
            if (start.x - end.x < 0.4f && start.x - end.x > -0.4f)
            {
                drt = -drt;
            }
        }
        if(blockTimer == 0){
            start = transform.position;
        }
        blockTimer += Time.deltaTime;
	}
}
