using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ESP_Controller;

public class ESP_AreasController : MonoBehaviour
{
    public int correctCharacterIndex;
    public string correctCharacterTag; //EN VEZ DE COMPARAR EL TAG, PONER UN INDEX A LOS PERSONAJES
    [HideInInspector] public HashSet<GameObject> collidedCharacters = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ESP_Characters"))
        {
            collidedCharacters.Add(other.gameObject);
            Debug.Log("ENTRÓ " + other.gameObject.tag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ESP_Characters"))
        {
            collidedCharacters.Remove(other.gameObject);
            Debug.Log("SALIÓ " + other.gameObject.tag);
        }
    }
}
