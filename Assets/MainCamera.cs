using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public Transform target;            
    public float smoothing = 5f;
    private Vector3 deltaPos = Vector3.zero;
    public static bool isShake = false;
    public static bool isScale = false;
    public static bool isFireS = false;

    float cameraSize;
    int drt = 1;
    Vector3 offset;
    Gun gun;
    void Start()
    {
        cameraSize = Camera.main.orthographicSize;
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        offset = transform.position - target.position;
    }

    void Shake()
    {
        transform.localPosition -= deltaPos;
        deltaPos = Random.insideUnitSphere / 15f;
        transform.localPosition += deltaPos;
    }

    void FireShake()
    {
        deltaPos = Random.insideUnitSphere / 20f;
        transform.localPosition += deltaPos*gun.drt;
    }

    void Scale()
    {

    }

    float fireSTimer = 0;
    float shakeTimer = 0;
    float scaleTimer = 0;
    Vector2 camScale = new Vector2(0,0);

    void Update()
    {
        
        if (isShake)
        {
            Shake();
            shakeTimer += Time.deltaTime;
            if (shakeTimer > 0.5f)
            {
                shakeTimer = 0;
                isShake = false;
            }
        }
        if(isFireS){
            FireShake();
            fireSTimer += Time.deltaTime;
            if (fireSTimer > 0.2f)
            {
                fireSTimer = 0;
                isFireS = false;
            }
        }
        camScale = Vector2.Lerp(camScale, new Vector2(gun.fireTimer, 0), smoothing * 2 * Time.deltaTime);
        Camera.main.orthographicSize = camScale.x + cameraSize;
    }

    void FixedUpdate()
    {
        drt = gun.drt;

        Vector3 targetCamPos = target.position + offset + new Vector3(drt,0,0);

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
