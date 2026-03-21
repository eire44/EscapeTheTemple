using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlsController : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audiosource;
    public int bowlIndex;
    stagesController stages;
    candlesPuzzleSolution candlesPuzzleController;
    HashSet<GameObject> objetosDentro = new HashSet<GameObject>();
    private void Start()
    {
        stages = FindObjectOfType<stagesController>();
        candlesPuzzleController = FindObjectOfType<candlesPuzzleSolution>();
        audiosource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //stages.callForCheckConfiguration(bowlIndex, collision.gameObject.tag);

        if (collision.gameObject.layer == LayerMask.NameToLayer("SOSP_Pieces"))
        {
            if (candlesPuzzleController.candlesPuzzleSolved)
            {
                objetosDentro.Add(collision.gameObject);
                ActualizarEstado();
                audiosource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("SOSP_Pieces"))
        {
            objetosDentro.Remove(collision.gameObject);
            ActualizarEstado();
        }
        //stages.callForCheckConfiguration(bowlIndex, "");
    }

    void ActualizarEstado()
    {
        string simbolo = "";

        if (objetosDentro.Count > 0)
        {
            GameObject obj = null;
            foreach (var o in objetosDentro)
            {
                obj = o;
                break;
            }

            simbolo = obj.tag;
        }

        stages.callForCheckConfiguration(bowlIndex, simbolo);
    }
}
