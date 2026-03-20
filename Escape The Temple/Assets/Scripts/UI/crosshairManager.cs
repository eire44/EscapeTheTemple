using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class crosshairManager : MonoBehaviour
{
    public Image crosshairImage;
    public Sprite normalCrosshair;
    public Sprite interactCrosshair;
    public LayerMask interactiveItems;
    grabItem_wRaycast grabItem_WRaycast;
    int ignorePlayerLayer;

    private void Awake()
    {
        grabItem_WRaycast = FindFirstObjectByType<grabItem_wRaycast>();
    }

    private void Start()
    {
        ignorePlayerLayer = ~LayerMask.GetMask("Player");
    }

    private void Update()
    {
        Ray ray = new Ray(grabItem_WRaycast.playerCamera.transform.position, grabItem_WRaycast.playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabItem_WRaycast.distance, ignorePlayerLayer))
        {
            checkIfInteractive(hit.collider.gameObject.layer, hit.collider.gameObject.tag);
        }
    }


    public void checkIfInteractive(int layer, string tag)
    {
        if ((interactiveItems & (1 << layer)) != 0)
        {
            SetInteract();
        } else if(tag == "LongClue")
        {
            SetInteract();
        }
        else
        {
            SetNormal();
        }
    }

    void SetNormal()
    {
        if (crosshairImage.sprite != normalCrosshair)
            crosshairImage.sprite = normalCrosshair;
    }

    void SetInteract()
    {
        if (crosshairImage.sprite != interactCrosshair)
            crosshairImage.sprite = interactCrosshair;
    }
}
