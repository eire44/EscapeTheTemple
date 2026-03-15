using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ESP_LeverMovement : MonoBehaviour
{
    public bool leverIsMoving = false;
    public Transform center;
    Vector3 startPos;
    private void Start()
    {
        startPos = center.position;
    }

    public void moveLever(Transform direction)
    {
        if (leverIsMoving) return;
        StartCoroutine(MoveLeverCoroutine(direction));
    }

    IEnumerator MoveLeverCoroutine(Transform targetPos)
    {
        leverIsMoving = true;

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

        transform.position = startPos;

        leverIsMoving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == center.gameObject && !FindObjectOfType<ESP_Controller>().enablePuzzle)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            center.gameObject.layer = LayerMask.NameToLayer("Default");
            center.gameObject.tag = "Untagged";
            FindObjectOfType<ESP_Controller>().enablePuzzle = true;

            foreach (ESP_ButtonsSpawn button in FindObjectsOfType<ESP_ButtonsSpawn>())
            {
                button.gameObject.layer = LayerMask.NameToLayer("ESP_Buttons");
                button.spawnButtons();
            }
            //FindObjectOfType<ESP_AudioClueController>().playAudioClue();
        }
    }
}
