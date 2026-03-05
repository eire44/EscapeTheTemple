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
    int layerMask;
    private void Start()
    {
        layerMask = ~LayerMask.GetMask("Player");
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            if (!hit.collider.gameObject.CompareTag("Untagged") && !hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.CompareTag("ShortClue") && !hit.collider.gameObject.CompareTag("LongClue"))
            {
                nombreObjeto.gameObject.SetActive(true);
                if(hit.collider.gameObject.CompareTag("Plank"))
                {
                    PlankNames plank_name = hit.collider.gameObject.GetComponent<PlankNames>();
                    if(plank_name != null)
                    {
                        nombreObjeto.text = plank_name.nameToShow;
                    }
                } else
                {
                    nombreObjeto.text = hit.collider.gameObject.tag;
                }
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
