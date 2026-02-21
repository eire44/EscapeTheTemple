using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABL_PuzzleController : MonoBehaviour
{
    public GameObject room3_Door;
    public Transform balance;
    public float maxTiltAngle = 20f;
    public float tiltSpeed = 5f;
    float currentTilt = 0f;

    float side0_Weight = 0f;
    float side1_Weight = 0f;

    bool puzzleSolved = false;

    List<placeableObjectController> objects = new List<placeableObjectController>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var placeableObject in FindObjectsOfType<placeableObjectController>())
        {
            objects.Add(placeableObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!puzzleSolved)
        {
            float weightDifference = side1_Weight - side0_Weight;

            float targetTilt = Mathf.Clamp(weightDifference * 5f, -maxTiltAngle, maxTiltAngle);
            currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

            balance.localRotation = Quaternion.Euler(0f, 0f, -currentTilt);
        }
    }

    public void saveWeightPlaced(int sideIndex, float newWeight)
    {
        if(sideIndex == 0)
        {
            side0_Weight = newWeight;
        }
        else
        {
            side1_Weight = newWeight;
        }

        Debug.Log(side0_Weight + " vs " + side1_Weight);

        if(compareWeights())
        {
            puzzleSolved = true;
            balance.localRotation = Quaternion.Euler(0f, 0f, 0f);
            foreach (var item in objects)
            {
                item.GetComponent<grabItem>().enabled = false;
            }
            unlockRoom3();
            Debug.Log("BALANZA EQUILIBRADA");
        }
        
    }

    bool compareWeights()
    {
        if (side1_Weight == side0_Weight)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].placed == false)
                {
                    return false;
                }
            }
            return true;
        } else
        {
            return false;
        }
    }

    void unlockRoom3()
    {
        room3_Door.GetComponent<fadeRoomDoor>().StartFade();
    }
}
