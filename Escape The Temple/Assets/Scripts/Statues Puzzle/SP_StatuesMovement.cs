using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_StatuesMovement : MonoBehaviour
{
    public GameObject[] statues;
    public List<SP_StatuePiecePairs> pieceToStatue_List;
    Dictionary<int, Transform> pieceToStatue_Dictionary = new Dictionary<int, Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var pair in pieceToStatue_List)
        {
            pieceToStatue_Dictionary[pair.index] = pair.statuePlacer;
        }
    }


    public void moveStatue(int pieceID, int cellIndex)
    {
        foreach (var statue in statues)
        {
            if(pieceID == statue.GetComponent<SP_StatuesController>().id)
            {
                Vector3 targetPos = pieceToStatue_Dictionary[cellIndex].position;
                StartCoroutine(MovePieceCoroutine(statue, targetPos));
            }
        }
    }

    IEnumerator MovePieceCoroutine(GameObject statue, Vector3 targetPos)
    {
        Vector3 startPos = statue.transform.position;
        Vector3 newPosition = new Vector3(targetPos.x, statue.transform.position.y, targetPos.z);

        float duration = 0.2f;
        float time = 0f;

        while (time < duration)
        {
            statue.transform.position = Vector3.Lerp(startPos, newPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        statue.transform.position = newPosition;
    }
}
