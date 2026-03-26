using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BLP_resetPhrasesPosition : MonoBehaviour
{
    Vector3 startPoint;
    Vector3 endPoint;

    Quaternion startRotation;
    Quaternion endRotation;

    public float duration = 2f;
    public float height = 2f;

    float time;
    bool reseting = false;

    public AudioClip audioClip;
    AudioSource audiosource;

    Rigidbody rb;
    private void Start()
    {
        endPoint = transform.position;
        endRotation = transform.rotation;
        audiosource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (reseting)
        {
            time += Time.deltaTime;
            float t = time / duration;

            if (t >= 1f)
            {
                t = 1f;
                reseting = false;
                rb.isKinematic = false;
            }

            Vector3 pos = Vector3.Lerp(startPoint, endPoint, t);
            pos.y += Mathf.Sin(t * Mathf.PI) * height;
            //transform.position = pos;

            //transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            rb.MovePosition(pos);
            rb.MoveRotation(Quaternion.Slerp(startRotation, endRotation, t));
        }
    }

    public void resetPosition()
    {
        rb.isKinematic = true;

        startPoint = transform.position;
        startRotation = transform.rotation;

        time = 0f;
        reseting = true;

        audiosource.PlayOneShot(audioClip);
    }
}
