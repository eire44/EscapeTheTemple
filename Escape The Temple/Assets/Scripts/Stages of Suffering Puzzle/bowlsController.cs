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

    private void Start()
    {
        stages = FindObjectOfType<stagesController>();
        candlesPuzzleController = FindObjectOfType<candlesPuzzleSolution>();
        audiosource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        stages.callForCheckConfiguration(bowlIndex, collision.gameObject.tag);

        if (collision.gameObject.layer == LayerMask.NameToLayer("SOSP_Pieces"))
        {
            if (candlesPuzzleController.candlesPuzzleSolved)
            {
                audiosource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //stages.callForCheckConfiguration(bowlIndex, "");
    }
}
