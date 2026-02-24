using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LTP_DrumsController : MonoBehaviour
{
    bool jugadorCerca = false;
    AudioSource drumSound;
    // Start is called before the first frame update
    void Start()
    {
        drumSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && jugadorCerca)
        {
            drumSound.Play();
            FindObjectOfType<LTP_Controller>().savePlayedSound(drumSound.clip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }

}
