using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    float maxSpeed = 6;
    float maxAccele = 2;
    float accele = 0;
    float speed = 0;
    float g = 4;
    float Horizontal = 0;
    float verticalSpeed = 0;
    bool inAir = true;
    Gun gun;

    void InputHandle(){

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            inAir = false;
            accele = 0;
            speed = 0;
        }
        
    }

	// Use this for initialization
	void Start () {
        gun = GameObject.Find("Gun").GetComponent<Gun>();
	}
    float timer = 0;
	// Update is called once per frame
	void Update () {
        
        if (inAir)
        {
            maxSpeed = 14;
            maxAccele = 1.5f;
            transform.localScale = new Vector3(0.7f,1.2f,0);
        }
        else
        {
            maxSpeed = 6;
            maxAccele = 2;
            transform.localScale = new Vector3(0.8f, 1f, 0);
        }
        if (gun.isFire)
        {
            accele = -0.5f * gun.drt;
            maxSpeed = 1;
            speed += accele;
            if (speed < -maxSpeed)
                speed = -maxSpeed;
            if (speed > maxSpeed)
                speed = maxSpeed;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

        }
        else if (Input.GetKeyDown(KeyCode.Space) && !inAir &&
            Input.GetKey(KeyCode.RightArrow))
        {
            inAir = true;
            gun.ChangDrt(1);
            accele += 50f;
            speed += accele;
            if (speed > maxSpeed + 10)
                speed = maxSpeed + 10;
            if (accele > maxAccele + 5)
                accele = maxAccele + 5;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            verticalSpeed = 45;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !inAir &&
           Input.GetKey(KeyCode.LeftArrow))
        {
            inAir = true;
            gun.ChangDrt(-1);
            accele -= 50f;
            speed += accele;
            if (speed < -maxSpeed - 10)
                speed = -maxSpeed - 10;
            if (accele < -maxAccele - 5)
                accele = -maxAccele - 5;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            verticalSpeed = 45;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            gun.ChangDrt(-1);
            accele -= 2f;
            speed += accele;
            if (speed < -maxSpeed)
                speed = -maxSpeed;
            if (accele < -maxAccele)
                accele = -maxAccele;
            transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            gun.ChangDrt(1);
            accele += 2f;
            speed += accele;
            if (speed > maxSpeed)
                speed = maxSpeed;
            if (accele > maxAccele)
                accele = maxAccele;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            inAir = true;
            verticalSpeed = 50;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            gun.isFire = true;
        }
        

        if (verticalSpeed > 0)
        {
            transform.position += new Vector3(0, verticalSpeed * Time.deltaTime, 0);
            verticalSpeed -= g;
        }
	}


}
