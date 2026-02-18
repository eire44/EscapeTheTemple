using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabItem : MonoBehaviour
{
    public static GameObject currentGrabbedItem;
    public GameObject posHand;
    SphereCollider sC;
    Rigidbody rb;
    bool playerIn = false;
    bool pickUpItem = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sC = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pickUpItem)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                pickUpItem = !pickUpItem;

                playerIn = false;
                transform.SetParent(null, true);
                rb.isKinematic = false;
                rb.useGravity = true;
                sC.enabled = true;

                FindObjectOfType<incensePuzzleSolution>().grabbedItem(pickUpItem, gameObject);
            }
        } else
        {
            if (playerIn)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    pickUpItem = !pickUpItem;

                    if (pickUpItem)
                    {
                        transform.SetParent(posHand.transform, false);
                        transform.position = posHand.transform.position;
                        rb.isKinematic = true;
                        rb.useGravity = false;
                        sC.enabled = false;
                        FindObjectOfType<collectItems>().collectItemText.gameObject.SetActive(false);
                    }
                    else
                    {
                        //playerIn = false;
                        //transform.SetParent(null, true);
                        //rb.isKinematic = false;
                        //rb.useGravity = true;
                        //sC.enabled = true;
                    }

                    FindObjectOfType<incensePuzzleSolution>().grabbedItem(pickUpItem, gameObject);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
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
