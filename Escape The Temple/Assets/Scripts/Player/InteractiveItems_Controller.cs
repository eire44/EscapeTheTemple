using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItems_Controller : MonoBehaviour
{
    public Camera playerCamera;
    public float distance = 4f;
    public LayerMask interactiveItemsLayer;
    int playerLayerMask;
    int finalMask;

    private void Start()
    {
        playerLayerMask = LayerMask.GetMask("Player");
        finalMask = interactiveItemsLayer & ~playerLayerMask;
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, distance, finalMask))
            {
                if(hit.collider.gameObject.CompareTag("Candle"))
                {
                    encenderVelas vela = hit.collider.gameObject.GetComponent<encenderVelas>();
                    if (vela != null)
                    {
                        vela.encenderVela();
                    }
                }
            }
        }
    }
}
