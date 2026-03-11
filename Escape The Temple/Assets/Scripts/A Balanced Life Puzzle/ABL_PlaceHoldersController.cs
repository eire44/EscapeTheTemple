using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABL_PlaceHoldersController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ABLP_Pieces"))
        {
            gameObject.tag = "Untagged";
            //other.transform.localScale = other.gameObject.GetComponent<placeableObjectController>().objectSize;
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ABLP_Pieces"))
        {
            gameObject.tag = "PlaceHolder";

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }
}
