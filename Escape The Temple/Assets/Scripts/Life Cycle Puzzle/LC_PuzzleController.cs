using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LC_PuzzleController : MonoBehaviour
{
    public GameObject[] places;
    int[] correctOrder = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    [HideInInspector] public int[] currentOrder = new int[9];

    private void Start()
    {
        for (int i = 0; i < currentOrder.Length; i++)
        {
            currentOrder[i] = -1;
        }
    }
    public bool checkOrder()
    {
        //Debug.Log(string.Join(", ", currentOrder));

        for (int i = 0; i < currentOrder.Length; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                return false;
            }
        }

        foreach (var item in FindObjectsOfType<LC_PiecesController>())
        {
            item.gameObject.layer = LayerMask.NameToLayer("Default");
            item.GetComponent<LC_PiecesController>().enabled = false;
        }

        return true;
    }
}
