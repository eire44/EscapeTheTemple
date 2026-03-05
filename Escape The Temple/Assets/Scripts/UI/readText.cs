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
    int ignorePlayerLayer;
    private void Start()
    {
        ignorePlayerLayer = ~LayerMask.GetMask("Player");
    }
    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        //Debug.DrawRay(playerCamera.transform.position,
        //      playerCamera.transform.forward * distance,
        //      Color.red);

        if (Physics.Raycast(ray, out hit, distance, ignorePlayerLayer))
        {
            if (hit.collider.gameObject.CompareTag("ShortClue"))
            {
                ShortTextClue text = hit.collider.gameObject.GetComponent<ShortTextClue>();
                if (text != null)
                {
                    mostrarTexto(text.textClue);
                }
            }
            else
            {
                textoTranscripcion.gameObject.SetActive(false);

                if (hit.collider.gameObject.CompareTag("LongClue"))
                {
                    if(Input.GetKeyDown(KeyCode.R))
                    {
                        paperClue paper = hit.collider.gameObject.GetComponent<paperClue>();
                        if (paper != null)
                        {
                            paper.openPaper();
                        }
                    }
                }
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
