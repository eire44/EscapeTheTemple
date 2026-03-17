using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilesController : MonoBehaviour
{
    [HideInInspector] public Transform pattern;
    public int tileNumber;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        pattern = transform.Find("Pattern");
        audiosource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!FindObjectOfType<tilesPuzzleController>().tilesPuzzleSolved)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!pattern.gameObject.activeInHierarchy)
                {
                    audiosource.Play();
                    pattern.gameObject.SetActive(true);
                    FindObjectOfType<tilesPuzzleController>().addPattern(tileNumber);
                }
            }
        }
    }
}
