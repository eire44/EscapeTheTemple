using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockPuzzle5 : MonoBehaviour
{
    public GameObject[] keyPieces;
    public GameObject[] placesForKeyPieces;
    public GameObject puzzle5_Architecture;

    public void checkPlaces()
    {
        foreach (var item in placesForKeyPieces)
        {
            if(!item.GetComponent<keyPieceHolder_Controller>().keyPlaced)
            {
                Debug.Log("PIEZA: " + item.name + "SIN COLOCAR");
                return;
            }
        }

        foreach (var item in keyPieces)
        {
            item.gameObject.layer = LayerMask.NameToLayer("Default");
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().isKinematic = true;
        }
        spawnPuzzle5();
    }

    void spawnPuzzle5()
    {
        StartCoroutine(raisePlatformPuzzle5());
    }

    IEnumerator raisePlatformPuzzle5()
    {
        Vector3 startPos = puzzle5_Architecture.transform.position;
        Vector3 newPosition = Vector3.zero;

        float duration = 1f;
        float time = 0f;

        while (time < duration)
        {
            puzzle5_Architecture.transform.position = Vector3.Lerp(startPos, newPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        puzzle5_Architecture.transform.position = newPosition;
    }
}
