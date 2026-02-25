using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static ESP_Controller;

public class ESP_Controller : MonoBehaviour
{
    public Transform[] screenPieces;
    public Transform[] leverPositions;
    public Material[] materials;
    Material newLeverColor;
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
    Dictionary<characters, Transform> charactersCorrelation = new Dictionary<characters, Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (directions dir in System.Enum.GetValues(typeof(directions)))
        {
            directionsCorrelation[dir] = leverPositions[leverPositions_Index];
            leverPositions_Index++;
        }

        foreach (characters character in System.Enum.GetValues(typeof(characters)))
        {
            charactersCorrelation[character] = screenPieces[screenPieces_Index];
            screenPieces_Index++;
        }

        currentCharacter = characters.Character3;
        newLeverColor = materials[2];
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
                    FindObjectOfType<ESP_LeverMovement>().changeLeverColor(materials[0]);
                }
                else if (hit.collider.CompareTag("Character2"))
                {
                    currentCharacter = characters.Character2;
                    FindObjectOfType<ESP_LeverMovement>().changeLeverColor(materials[1]);
                }
                else if (hit.collider.CompareTag("Character3"))
                {
                    currentCharacter = characters.Character3;
                    FindObjectOfType<ESP_LeverMovement>().changeLeverColor(materials[2]);
                }
                else if (hit.collider.CompareTag("Character4"))
                {
                    currentCharacter = characters.Character4;
                    FindObjectOfType<ESP_LeverMovement>().changeLeverColor(materials[3]);
                }
                else if (hit.collider.CompareTag("ESP_Up"))
                {
                    newDirection = directions.Up;
                    FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection]);
                    callForMovePieces();
                }
                else if (hit.collider.CompareTag("ESP_Down"))
                {
                    newDirection = directions.Down;
                    FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection]);
                    callForMovePieces();
                }
                else if (hit.collider.CompareTag("ESP_Left"))
                {
                    newDirection = directions.Left;
                    FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection]);
                    callForMovePieces();
                }
                else if (hit.collider.CompareTag("ESP_Right"))
                {
                    newDirection = directions.Right;
                    FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection]);
                    callForMovePieces();
                }
            }
        }
    }


    void callForMovePieces()
    {
        Vector3 newPosition = charactersCorrelation[currentCharacter].position;
        if (newDirection == directions.Up)
        {
            newPosition.y += 0.5f;
        } else if (newDirection == directions.Down)
        {
            newPosition.y -= 0.5f;
        } else if(newDirection == directions.Left)
        {
            newPosition.z -= 0.5f;
        } else if(newDirection == directions.Right)
        {
            newPosition.z += 0.5f;
        }
        FindObjectOfType<ESP_PiecesMovement>().movePieces(newPosition, charactersCorrelation[currentCharacter]);
    }
}
