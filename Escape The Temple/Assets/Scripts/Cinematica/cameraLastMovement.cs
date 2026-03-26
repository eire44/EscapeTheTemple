using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cameraLastMovement : MonoBehaviour
{
    public Transform camera;

    public float forwardDistance = 2f;
    public float upwardDistance = 7f;
    public float finalBackDistance = 0.5f;

    public float durationForward = 2f;
    public float durationUp = 7f;
    public float durationBack = 1f;

    public Vector3 rotationOffset;

    Vector3 startPos;
    Quaternion startRot;

    float time = 0f;
    int phase = 0;

    bool playing = false;

    public TMP_Text gameTitle;
    public TMP_Text madeBy;

    public void StartMotion()
    {
        startPos = camera.position;
        startRot = camera.rotation;

        time = 0f;
        phase = 0;
        playing = true;
    }

    void Update()
    {
        if (!playing) return;

        time += Time.deltaTime;

        if (phase == 0)
        {
            float t = time / durationForward;

            if (t >= 1f)
            {
                t = 1f;
                NextPhase();
            }

            camera.position = Vector3.Lerp(startPos, startPos + camera.forward * forwardDistance, t);
        }
        else if (phase == 1)
        {
            float t = time / durationUp;

            if (t >= 1f)
            {
                t = 1f;
                NextPhase(); 
                
                gameTitle.GetComponent<fadeTitle>().FadeIn(gameTitle);
                madeBy.GetComponent<fadeTitle>().FadeIn(madeBy);
            }

            Vector3 move = camera.forward * forwardDistance + Vector3.up * upwardDistance;
            camera.position = Vector3.Lerp(startPos + camera.forward * forwardDistance, startPos + move, t);

            Quaternion targetRot = startRot * Quaternion.Euler(rotationOffset);
            camera.rotation = Quaternion.Slerp(startRot, targetRot, t);
        }
        else if (phase == 2)
        {
            float t = time / durationBack;

            if (t >= 1f)
            {
                t = 1f;
                playing = false;
            }

            Vector3 currentPos = camera.position;
            Vector3 backTarget = currentPos - camera.forward * finalBackDistance;
            camera.position = Vector3.Lerp(currentPos, backTarget, t);
        }
    }

    void NextPhase()
    {
        time = 0f;
        phase++;
    }
}
