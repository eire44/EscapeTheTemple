using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cinematicaController : MonoBehaviour
{
    public bool cinematicaPlaying = false;
    public Camera mainCamera;
    public cameraSpot[] cameraSpots;
    [HideInInspector] public int spotIndex = 0;
    public Mov_Controller controller;
    public grabItem_wRaycast grabItem_WRaycast;
    public InteractiveItems_Controller interactiveItems;
    captionsController captions;
    blinkController blinkController;
    cameraLastMovement cameraLastMovement;

    private void Start()
    {
        captions = gameObject.GetComponent<captionsController>();
        blinkController = gameObject.GetComponent<blinkController>();
        cameraLastMovement = gameObject.GetComponent<cameraLastMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            startKinematics();
        }

        if (cinematicaPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1.0f;
                SceneManager.LoadScene("Inicio");
            }
        }
    }
    public void startKinematics()
    {
        StartCoroutine(StartKinematicsCoroutine());
    }

    IEnumerator StartKinematicsCoroutine()
    {
        cinematicaPlaying = true;
        controller.enabled = false;
        grabItem_WRaycast.enabled = false;
        interactiveItems.enabled = false;
        FindObjectOfType<burningLiesController>().EncenderFuegoInstantaneo();

        yield return blinkController.PlayBlink(false);

        StartCoroutine(FindObjectOfType<ESP_AudioClueController>().DuckAudio());
        FindObjectOfType<txtControls>().showTabInstructions(3);
        captions.audioSource.Play();

        nextSpot();
    }

    public void nextSpot()
    {
        mainCamera.transform.position = cameraSpots[spotIndex].transform.position;
        mainCamera.transform.rotation = cameraSpots[spotIndex].transform.rotation;
        StartCoroutine(slowMovement(cameraSpots[spotIndex]));
    }

    IEnumerator slowMovement(cameraSpot spot)
    {
        Vector3 startPos = mainCamera.transform.position;

        Vector3 newPos = startPos + spot.transform.TransformDirection(spot.pressingDirection) * spot.movLength;
        //Vector3 newPos = startPos + spot.pressingDirection * spot.movLength;
        float t = 0;

        while (t < spot.movDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPos, newPos, t / spot.movDuration);
            t += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = newPos;
        spotIndex++;

        if (spotIndex < cameraSpots.Length)
        {
            nextSpot();
        }
        else
        {
            cameraLastMovement.StartMotion();
        }
    }

    public void goBackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Inicio");
    }
}
