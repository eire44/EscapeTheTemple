using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ABL_PuzzleController : MonoBehaviour
{
    public GameObject room3_Door;
    public Transform balance_bar;
    public Transform plate1;
    public Transform plate0;
    public float maxTiltAngle = 20f;
    public float tiltSpeed = 5f;
    float currentTilt = 0f;

    float side0_Weight = 0f;
    float side1_Weight = 0f;

    bool puzzleSolved = false;

    List<placeableObjectController> objects = new List<placeableObjectController>();
    Quaternion initialRotation;

    [HideInInspector] public GameObject object1;
    [HideInInspector] public GameObject object2;
    [HideInInspector] public GameObject object3;
    [HideInInspector] public GameObject object4;

    void Start()
    {
        initialRotation = balance_bar.localRotation;

        foreach (var placeableObject in FindObjectsOfType<placeableObjectController>())
        {
            objects.Add(placeableObject);
        }
    }

    void Update()
    {
        if (!puzzleSolved)
        {
            float weightDifference = side1_Weight - side0_Weight;

            float targetTilt = Mathf.Clamp(weightDifference * 5f, -maxTiltAngle, maxTiltAngle);
            currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

            balance_bar.localRotation = initialRotation * Quaternion.Euler(0f, currentTilt, 0f);
        }
    }

    public void saveObject(placeableObjectController objectSaved)
    {
        if(objectSaved.index == 0)
        {
            object1 = objectSaved.gameObject;
        } 
        else if(objectSaved.index == 1)
        {
            object2 = objectSaved.gameObject;
        } 
        else if (objectSaved.index == 2)
        {
            object3 = objectSaved.gameObject;
        }
        else
        {
            object4 = objectSaved.gameObject;
        }
    }

    public GameObject getObject(int objectIndex)
    {
        if (objectIndex == 0)
        {
            object1.SetActive(true);
            return object1;
        }
        else if (objectIndex == 1)
        {
            object2.SetActive(true);
            return object2;
        }
        else if (objectIndex == 2)
        {
            object3.SetActive(true);
            return object3;
        }
        else
        {
            object4.SetActive(true);
            return object4;
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
            balance_bar.localRotation = initialRotation;
            foreach (var item in objects)
            {
                item.gameObject.layer = 0;
            }
            unlockRoom3();
            FindObjectOfType<SunMovement>().sunProgression();
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
