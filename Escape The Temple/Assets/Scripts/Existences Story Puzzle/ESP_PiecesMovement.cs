using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static ESP_Controller;

public class ESP_PiecesMovement : MonoBehaviour
{
    public void movePieces(Vector3 newPosition_CurrentCharacter, Transform character, Transform affectedCharacter, Vector3 newPosition_AffectedCharacter)
    {
        StartCoroutine(MoveScreenPiece(newPosition_CurrentCharacter, character)); 
        if(affectedCharacter != null )
        {
            StartCoroutine(MoveScreenPiece(newPosition_AffectedCharacter, affectedCharacter));
        }
    }

    IEnumerator MoveScreenPiece(Vector3 newPosition, Transform character)
    {
        newPosition.x = Mathf.Clamp(newPosition.x, -1.1f, 1.5f);
        newPosition.y = Mathf.Clamp(newPosition.y, 2.6f, 4.2f);

        Vector3 startPos = character.position;

        float durationMove = 0.5f;
        float timeMove = 0f;

        while (timeMove < durationMove)
        {
            character.position = Vector3.Lerp(startPos, newPosition, timeMove / durationMove);
            timeMove += Time.deltaTime;
            yield return null;
        }

        character.position = newPosition;

        FindObjectOfType<ESP_Controller>().checkCharactersPositions();
    }
}
