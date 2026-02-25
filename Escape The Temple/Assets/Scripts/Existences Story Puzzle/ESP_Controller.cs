using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESP_Controller : MonoBehaviour
{
    public Transform[] screenPieces;
    public Transform[] leverPositions;
    int screenPieces_Index = 0;
    int leverPositions_Index = 0;
    [HideInInspector] public enum characters
    {
        Character1, Character2, Character3, Character4
    }
    [HideInInspector] public characters currentCharacter;

    [HideInInspector] public enum directions
    {
        Up, Down, Left, Right
    }
    [HideInInspector] public directions newDirection;

    Dictionary<directions, Transform> directionsCorrelation = new Dictionary<directions, Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (directions dir in System.Enum.GetValues(typeof(directions)))
        {
            directionsCorrelation[dir] = leverPositions[leverPositions_Index];
            leverPositions_Index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.CompareTag("Character1"))
                {
                    currentCharacter = characters.Character1;
                }
                else if (hit.collider.CompareTag("Character2"))
                {
                    currentCharacter = characters.Character2;
                }
                else if (hit.collider.CompareTag("Character3"))
                {
                    currentCharacter = characters.Character3;
                }
                else if (hit.collider.CompareTag("Character4"))
                {
                    currentCharacter = characters.Character4;
                }
                else if (hit.collider.CompareTag("ESP_Up"))
                {
                    newDirection = directions.Up;
                    FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection]);
                }
                else if (hit.collider.CompareTag("ESP_Down"))
                {
                    newDirection = directions.Down;
                    FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection]);
                }
                else if (hit.collider.CompareTag("ESP_Left"))
                {
                    newDirection = directions.Left;
                    FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection]);
                }
                else if (hit.collider.CompareTag("ESP_Right"))
                {
                    newDirection = directions.Right;
                    FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection]);
                }
            }
        }
    }
}
