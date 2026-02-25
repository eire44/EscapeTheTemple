using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ESP_LeverMovement : MonoBehaviour
{
    public bool isMoving = false;
    public Transform center;
    Vector3 startPos;
    private void Start()
    {
        startPos = center.position;
    }
    public void moveLever(Transform direction)
    {
        if (isMoving) return;
        StartCoroutine(MoveLeverCoroutine(direction));
    }

    IEnumerator MoveLeverCoroutine(Transform targetPos)
    {
        isMoving = true;

        Vector3 newPosition = new Vector3(targetPos.position.x, transform.position.y, targetPos.position.z);

        float durationMove = 0.5f;
        float timeMove = 0f;

        while (timeMove < durationMove)
        {
            transform.position = Vector3.Lerp(startPos, newPosition, timeMove / durationMove);
            timeMove += Time.deltaTime;
            yield return null;
        }

        transform.position = newPosition;

        float durationReturn = 0.5f;
        float timeReturn = 0f;

        while (timeReturn < durationReturn)
        {
            transform.position = Vector3.Lerp(newPosition, startPos, timeReturn / durationReturn);
            timeReturn += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }
}
