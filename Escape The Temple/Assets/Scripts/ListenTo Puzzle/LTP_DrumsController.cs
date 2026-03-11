using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LTP_DrumsController : MonoBehaviour
{
    [HideInInspector] public AudioSource drumSound;
    // Start is called before the first frame update
    void Start()
    {
        drumSound = GetComponent<AudioSource>();
    }
}
