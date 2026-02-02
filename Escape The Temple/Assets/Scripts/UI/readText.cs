using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Numerics;

public class readText : MonoBehaviour
{
    public Camera playerCamera;
    public float distance = 4f;
    public TMP_Text textoTranscripcion;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        //Debug.DrawRay(playerCamera.transform.position,
        //      playerCamera.transform.forward * distance,
        //      Color.red);

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.name.Contains("PapelFraseIluminados"))
            {
                mostrarTexto("Those who attain enlightenment continue to shine beyond time.");
            }
            else
            {
                textoTranscripcion.gameObject.SetActive(false);
            }
        }
        else
        {
            textoTranscripcion.gameObject.SetActive(false);
        }
    }

    void mostrarTexto(string mensaje)
    {
        textoTranscripcion.text = mensaje;
        textoTranscripcion.gameObject.SetActive(true);
    }
}
