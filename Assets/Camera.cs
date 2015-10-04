using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public Transform target;            
    public float smoothing = 5f;
    private Vector3 deltaPos = Vector3.zero;
    public static bool isShake = false;

    int drt = 1;
    Vector3 offset;
    Gun gun;
    void Start()
    {
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        offset = transform.position - target.position;
    }

    void Shake()
    {
        transform.localPosition -= deltaPos;
        deltaPos = Random.insideUnitSphere / 8f;
        transform.localPosition += deltaPos;
    }

    float shakeTimer = 0;

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
    }

    void FixedUpdate()
    {
        drt = gun.drt;

        Vector3 targetCamPos = target.position + offset + new Vector3(drt,0,0);

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
