using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTP_BellSwing : MonoBehaviour
{
    public float swingAngle = 10f;
    public float swingSpeed = 5f;
    public float damping = 2f;

    float time;
    float amplitude = 1f;

    bool ringing = false;
    bool stopping = false;

    Quaternion startRotation;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    void Update()
    {
        if (!ringing && !stopping)
            return;

        time += Time.deltaTime;

        float angle = Mathf.Sin(time * swingSpeed) * swingAngle * amplitude;

        transform.localRotation = startRotation * Quaternion.Euler(0f, 0f, angle);

        if (stopping)
        {
            amplitude = Mathf.Lerp(amplitude, 0f, Time.deltaTime * damping);

            if (amplitude < 0.01f)
            {
                stopping = false;
                transform.localRotation = startRotation;
            }
        }
    }

    public void StartRinging()
    {
        ringing = true;
        stopping = false;
        amplitude = 1f;
    }

    public void StopRinging()
    {
        ringing = false;
        stopping = true;
    }
}
