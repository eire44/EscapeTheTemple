using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheatAllDoors : MonoBehaviour
{
    public GameObject[] doors;
    bool flag = true;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) && flag)
        {
            flag = false;
            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
        }
    }
}
