using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESP_ButtonsSpawn : MonoBehaviour
{
    public Vector3 newPosition;
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }


    public void spawnButtons()
    {
        StartCoroutine(MoveButtonsCoroutine());
    }

    IEnumerator MoveButtonsCoroutine()
    {
        Vector3 moveToPosition = new Vector3(transform.position.x + newPosition.x, transform.position.y + newPosition.y, transform.position.z + newPosition.z);

        float durationMove = 2f;
        float timeMove = 0f;

        while (timeMove < durationMove)
        {
            transform.position = Vector3.Lerp(startPos, moveToPosition, timeMove / durationMove);
            timeMove += Time.deltaTime;
            yield return null;
        }

        transform.position = moveToPosition;
    }
}
