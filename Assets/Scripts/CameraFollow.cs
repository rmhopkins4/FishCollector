using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //simple follow stuff
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    //screen shake stuff
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.7f;

    private float dampingSpeed = 1.0f;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        if (shakeDuration > 0)
        {
            transform.localPosition = transform.position + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = smoothedPosition;
        }
    }
	
	public void CameraShake(float shakeDur, float shakeStr)
	{
		shakeMagnitude = shakeStr;
		shakeDuration = shakeDur;
	}
}
