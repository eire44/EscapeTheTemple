using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ESP_Controller;

public class ESP_AreasController : MonoBehaviour
{
    public string correctCharacterTag;
    [HideInInspector] public HashSet<GameObject> collidedCharacters = new HashSet<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("ESP_Characters"))
        {
            collidedCharacters.Add(collision.gameObject);
            Debug.Log("ENTRÓ " + collision.gameObject.tag);
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ESP_Characters"))
        {
            collidedCharacters.Remove(collision.gameObject);
            Debug.Log("SALIÓ " + collision.gameObject.tag);
        }
        
    }
}
