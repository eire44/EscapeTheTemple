using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ESP_LeverMovement : MonoBehaviour
{
    public bool leverIsMoving = false;
    public Transform center;
    Vector3 startPos;
    public AudioSource audiosource;
    Quaternion startRot;
    private void Start()
    {
        startPos = center.position;
        startRot = Quaternion.Euler(-90f, 0f, 0f);
    }

    public void moveLever(Transform direction, bool movingSideways)
    {
        if (leverIsMoving) return;
        if (movingSideways)
        {
            transform.rotation *= Quaternion.Euler(0, 0, 90f);
        }
        StartCoroutine(MoveLeverCoroutine(direction, movingSideways));
    }

    IEnumerator MoveLeverCoroutine(Transform targetPos, bool movingSideways)
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
        transform.rotation = startRot;

        leverIsMoving = false;
    }

    public void placeLever()
    {
        if (!FindObjectOfType<ESP_Controller>().enablePuzzle)
        {
            audiosource.Play();
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
