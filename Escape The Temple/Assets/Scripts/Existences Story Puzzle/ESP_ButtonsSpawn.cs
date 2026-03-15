using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESP_ButtonsSpawn : MonoBehaviour
{
    public Vector3 newPosition;
    Vector3 startPos;
    [HideInInspector] public Vector3 moveToPosition;

    void Start()
    {
        startPos = transform.localPosition;
        moveToPosition = startPos + newPosition;
    }

    public void spawnButtons()
    {
        StartCoroutine(MoveButtonsCoroutine());
    }

    IEnumerator MoveButtonsCoroutine()
    {
        float durationMove = 2f;
        float timeMove = 0f;

        while (timeMove < durationMove)
        {
            transform.localPosition = Vector3.Lerp(startPos, moveToPosition, timeMove / durationMove);
            timeMove += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = moveToPosition;
    }
}
