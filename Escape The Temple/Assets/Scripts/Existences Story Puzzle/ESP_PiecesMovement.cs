using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ESP_PiecesMovement : MonoBehaviour
{
    public void movePieces(Vector3 newPosition, Transform character)
    {
        StartCoroutine(MoveScreenPiece(newPosition, character));
    }

    IEnumerator MoveScreenPiece(Vector3 newPosition, Transform character)
    {
        //isMoving = true;

        Vector3 startPos = character.position;

        float durationMove = 0.5f;
        float timeMove = 0f;

        while (timeMove < durationMove)
        {
            character.transform.position = Vector3.Lerp(startPos, newPosition, timeMove / durationMove);
            timeMove += Time.deltaTime;
            yield return null;
        }

        character.transform.position = newPosition;

        //isMoving = false;
    }
}
