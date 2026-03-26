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
    public AudioClip[] audioClips;
    AudioSource audiosource;
    private void Start()
    {
        ignorePlayerLayer = ~LayerMask.GetMask("Player"); 
        audiosource = GetComponent<AudioSource>(); 
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
                if (text != null && !FindObjectOfType<ESP_CaptionsController>().audioSource.isPlaying && !FindObjectOfType<captionsController>().audioSource.isPlaying)
                {
                    mostrarTexto(text.textClue);
                }
            }
            else
            {
                if(!FindObjectOfType<ESP_AudioClueController>().audioClue.isPlaying && !FindObjectOfType<captionsController>().audioSource.isPlaying)
                {
                    textoTranscripcion.gameObject.SetActive(false);
                }
                

                if (hit.collider.gameObject.CompareTag("LongClue"))
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        paperClue paper = hit.collider.gameObject.GetComponent<paperClue>();
                        if (paper != null)
                        {
                            audiosource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
                            paper.openPaper();
                        }
                    }
                }
            }
        }
        else
        {
            if (!FindObjectOfType<ESP_AudioClueController>().audioClue.isPlaying && !FindObjectOfType<captionsController>().audioSource.isPlaying)
            {
                textoTranscripcion.gameObject.SetActive(false);
            }
        }
    }

    void mostrarTexto(string mensaje)
    {
        textoTranscripcion.text = mensaje;
        textoTranscripcion.gameObject.SetActive(true);
    }
}
