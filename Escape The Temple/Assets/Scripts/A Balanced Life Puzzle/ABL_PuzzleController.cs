using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABL_PuzzleController : MonoBehaviour
{
    public Transform balance;
    public float maxTiltAngle = 20f;
    public float tiltSpeed = 5f;
    float currentTilt = 0f;

    float side0_Weight = 0f;
    float side1_Weight = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float weightDifference = side1_Weight - side0_Weight;

        float targetTilt = Mathf.Clamp(weightDifference * 5f, -maxTiltAngle, maxTiltAngle);
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

        balance.localRotation = Quaternion.Euler(0f, 0f, -currentTilt);
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
    }
}
