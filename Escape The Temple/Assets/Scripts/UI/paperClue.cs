using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paperClue : MonoBehaviour
{
    bool playerIn = false;
    public GameObject imgPaper;
    bool showPaper = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIn)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                showPaper = !showPaper;
                if(showPaper)
                {
                    Time.timeScale = 0f;
                } else
                {
                    Time.timeScale = 1f;
                }
                imgPaper.SetActive(showPaper);
            }
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIn = false;
        }
    }
}
