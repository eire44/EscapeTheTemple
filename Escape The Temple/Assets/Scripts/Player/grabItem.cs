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
                    Debug.Log("SOLTAR");
                    playerIn = false;
                    transform.SetParent(null, true);
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    sC.enabled = true;
                }

                FindObjectOfType<incensePuzzleSolution>().grabbedItem(pickUpItem, gameObject);
            }
        }
        //if (!playerIn) return;

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    if (currentGrabbedItem != null && currentGrabbedItem != gameObject)
        //    {
        //        currentGrabbedItem.GetComponent<grabItem>().Drop();
        //        currentGrabbedItem = null;
        //    }

        //    pickUpItem = !pickUpItem;

        //    if (pickUpItem)
        //    {
        //        currentGrabbedItem = gameObject;

        //        transform.SetParent(posHand.transform, false);
        //        transform.position = posHand.transform.position;
        //        rb.isKinematic = true;
        //        rb.useGravity = false;
        //        sC.enabled = false;

        //        FindObjectOfType<collectItems>().collectItemText.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        currentGrabbedItem = null;
        //        Drop();
        //    }

        //    FindObjectOfType<incensePuzzleSolution>().grabbedItem(pickUpItem, gameObject);
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerIn = true;
            Debug.Log(gameObject.name + " " + playerIn);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIn = false;
            Debug.Log(gameObject.name + " " + playerIn);
        }
    }

    void Drop()
    {
        pickUpItem = false;

        transform.SetParent(null, true);
        rb.isKinematic = false;
        rb.useGravity = true;
        sC.enabled = true;
    }
}
