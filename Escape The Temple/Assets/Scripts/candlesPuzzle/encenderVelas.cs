using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class encenderVelas : MonoBehaviour
{
    bool jugadorCerca = false;
    public ParticleSystem llama;
    public string nombreTabla;
    TMP_Text txtNombres;
    // Start is called before the first frame update
    void Start()
    {
        txtNombres = FindObjectOfType<identificarObjeto>().txtIdentificacionTablas;
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindObjectOfType<candlesPuzzleSolution>().candlesPuzzleSolved)
        {
            if (Input.GetKeyDown(KeyCode.E) && jugadorCerca)
            {
                if (llama.gameObject.activeInHierarchy)
                {
                    llama.gameObject.SetActive(false);
                }
                else
                {
                    llama.gameObject.SetActive(true);
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            jugadorCerca = true;
            mostrarNombreTabla(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugadorCerca = false;
            mostrarNombreTabla(false);
        }
    }

    void mostrarNombreTabla(bool mostrar)
    {
        txtNombres.text = nombreTabla;
        txtNombres.gameObject.SetActive(mostrar);
    }
}
