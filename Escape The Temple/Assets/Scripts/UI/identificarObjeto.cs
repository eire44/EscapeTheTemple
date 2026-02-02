using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class identificarObjeto : MonoBehaviour
{
    public Camera playerCamera;
    public float distance = 4f;
    public TMP_Text nombreObjeto;
    public TMP_Text txtIdentificacionTablas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if(!hit.collider.gameObject.CompareTag("Untagged") && !hit.collider.gameObject.CompareTag("Player"))
            {
                nombreObjeto.gameObject.SetActive(true);
                nombreObjeto.text = hit.collider.gameObject.tag;
            }
            else
            {
                nombreObjeto.gameObject.SetActive(false);
            }
        }
        else
        {
            nombreObjeto.gameObject.SetActive(false);
        }
    }
}
