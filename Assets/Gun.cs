using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    GameObject hero;
    public float smoothing = 12f; 
	// Use this for initialization
	void Start () {
        hero = GameObject.Find("Hero");
        offset = transform.position - hero.transform.position;
        face = new Vector3(0.5f, 0.2f, 0);
	}

    public int drt = 1;
    Vector3 offset;
    Vector3 targetPos;
    Vector3 face;
    public bool isFire = false;
    float fireSpeed = 0.1f;
    float fireSpeedTimer = 0;
    float backTimer = 0;
    float fireTimer = 0;
    float fireTimer2 = 0;
    public void ChangDrt(int drt)
    {
        this.drt = drt;
        face.x = drt * 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
	    targetPos = hero.transform.position + face;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
        if (fireSpeedTimer >= fireSpeed)
        {
            fireSpeedTimer = 0;
            if (isFire)
            {
                Shoot();
                if (backTimer >= 0.101f)
                {
                    backTimer = 0;
                    transform.position -= new Vector3(drt * 0.3f * fireTimer, 0.2f * fireTimer, 0);
                }
                fireTimer += 0.2f;
                if (fireTimer > 1)
                    fireTimer = 1;
            }
        }
        if (fireTimer2 > 0.2f)
        {
            fireTimer2 = 0;
            fireTimer -= 0.2f;
            if (fireTimer < 0)
                fireTimer = 0;
        }
        fireTimer2 += Time.deltaTime;
        backTimer += Time.deltaTime;
        fireSpeedTimer += Time.deltaTime;
        //Debug.Log(fireTimer);
	}

    IEnumerator EjectShell(GameObject shell)
    {
        Rigidbody2D rig = shell.GetComponent<Rigidbody2D>();
        for (float timer = 0; timer < 0.5; timer += Time.deltaTime) {
            rig.AddTorque(Random.Range(-1,2)*20);
            rig.velocity = new Vector3(5 * -drt, 5, 0);
        }
        yield return 0;
    }



    void Shoot()
    {
        GameObject bullet = Instantiate(Resources.Load("Bullet") as GameObject);
        GameObject shell = Instantiate(Resources.Load("Shell") as GameObject);
        shell.transform.position = transform.position - new Vector3(drt * 0.2f, 0, 0);
        
        StartCoroutine(EjectShell(shell));
        bullet.transform.Rotate(Vector3.forward,Random.Range(-4*fireTimer,5*fireTimer));
        isFire = false;
    }
}
