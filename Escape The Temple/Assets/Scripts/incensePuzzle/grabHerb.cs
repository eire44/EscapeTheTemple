using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabHerb : MonoBehaviour
{
    //public GameObject herb;
    //public GameObject posHand;
    //SphereCollider sC;
    //Rigidbody rb;
    //bool playerIn = false;
    //bool pickUpItem = false;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    sC = GetComponent<SphereCollider>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (playerIn)
    //    {
    //        if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            pickUpItem = !pickUpItem;

    //            if (pickUpItem)
    //            {
    //                GameObject hierba = Instantiate(herb, posHand.transform.position, posHand.transform.rotation);
    //                hierba.transform.SetParent(posHand.transform, false);
    //                rb.isKinematic = true;
    //                rb.useGravity = false;
    //                //sC.enabled = false;
    //                FindObjectOfType<collectItems>().collectItemText.gameObject.SetActive(false);
    //                FindObjectOfType<incensePuzzleSolution>().grabbedItem(pickUpItem, hierba);
    //            }

    //        }
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        playerIn = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        playerIn = false;
    //    }
    //}
}
