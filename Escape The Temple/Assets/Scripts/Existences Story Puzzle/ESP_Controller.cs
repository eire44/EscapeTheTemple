using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static ESP_Controller;

public class ESP_Controller : MonoBehaviour
{
    public Transform screenAreasContainer;
    public Transform[] screenPieces;
    public Transform[] leverPositions;
    int screenPieces_Index = 0;
    int leverPositions_Index = 0;

    public AudioSource audiosource;
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

    [HideInInspector] public bool enablePuzzle = false;

    [HideInInspector] public bool puzzleAlreadySolved = false;
    public int puzzleIndex = 9;
    public interiorLanternsController[] interiorLantern;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (enablePuzzle)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.CompareTag("Character1_Button"))
                    {
                        currentCharacter = characters.Character1;
                        hit.transform.gameObject.GetComponent<ESP_ButtonsPressed>().Press();
                    }
                    else if (hit.collider.CompareTag("Character2_Button"))
                    {
                        currentCharacter = characters.Character2;
                        hit.transform.gameObject.GetComponent<ESP_ButtonsPressed>().Press();
                    }
                    else if (hit.collider.CompareTag("Character3_Button"))
                    {
                        currentCharacter = characters.Character3;
                        hit.transform.gameObject.GetComponent<ESP_ButtonsPressed>().Press();
                    }
                    else if (hit.collider.CompareTag("Character4_Button"))
                    {
                        currentCharacter = characters.Character4;
                        hit.transform.gameObject.GetComponent<ESP_ButtonsPressed>().Press();
                    }
                    else if (hit.transform.gameObject.TryGetComponent<ESP_DirectionButtonsController>(out ESP_DirectionButtonsController dirButton))
                    {
                        hit.transform.gameObject.GetComponent<ESP_ButtonsPressed>().Press();
                        if (dirButton.index == 0)
                        {
                            newDirection = directions.Up;
                            FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection], false);
                            callForMovePieces();
                        } 
                        else if(dirButton.index == 1)
                        {
                            newDirection = directions.Down;
                            FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection], false);
                            callForMovePieces();
                        } 
                        else if(dirButton.index == 2)
                        {
                            newDirection = directions.Right;
                            FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection], true);
                            callForMovePieces();
                        } 
                        else if(dirButton.index == 3)
                        {
                            newDirection = directions.Left;
                            FindObjectOfType<ESP_LeverMovement>().moveLever(directionsCorrelation[newDirection], true);
                            callForMovePieces();
                        }

                    }
                    else if (hit.collider.CompareTag("ESP_ListenAgain"))
                    {
                        hit.transform.gameObject.GetComponent<ESP_ButtonsPressed>().Press();
                        FindObjectOfType<ESP_AudioClueController>().playAudioClue();
                    }
                }
            }
        }
    }


    void callForMovePieces()
    {
        audiosource.Play();
        Vector3 newPosition_CurrentCharacter = charactersCorrelation[currentCharacter].position;
        Transform affectedCharacter = null;

        if (currentCharacter == characters.Character1)
        {
            affectedCharacter = charactersCorrelation[characters.Character3];
        }
        else if (currentCharacter == characters.Character2)
        {
            affectedCharacter = charactersCorrelation[characters.Character4];
        }
        else if (currentCharacter == characters.Character3)
        {
            affectedCharacter = charactersCorrelation[characters.Character2];
        }
        else if (currentCharacter == characters.Character4)
        {
            affectedCharacter = charactersCorrelation[characters.Character3];
        }
        Vector3 newPosition_AffectedCharacter = affectedCharacter.position;

        if (newDirection == directions.Up)
        {
            newPosition_CurrentCharacter.y += 0.3f;
            newPosition_AffectedCharacter.y -= 0.3f;
        }
        else if (newDirection == directions.Down)
        {
            newPosition_CurrentCharacter.y -= 0.3f;
            newPosition_AffectedCharacter.y += 0.3f;
        }
        else if (newDirection == directions.Left)
        {
            newPosition_CurrentCharacter.x -= 0.3f;
            newPosition_AffectedCharacter.x += 0.3f;
        }
        else if (newDirection == directions.Right)
        {
            newPosition_CurrentCharacter.x += 0.3f;
            newPosition_AffectedCharacter.x -= 0.3f;
        }

        FindObjectOfType<ESP_PiecesMovement>().movePieces(newPosition_CurrentCharacter, charactersCorrelation[currentCharacter], affectedCharacter, newPosition_AffectedCharacter);
    }


    public void checkCharactersPositions()
    {
        Debug.Log("CHECK");
        foreach (Transform item in screenAreasContainer)
        {
            if (item.GetComponent<ESP_AreasController>().collidedCharacters.Count == 0)
            {
                Debug.Log(item.name + "FALTA");
                return;
            }
            foreach (var obj in item.GetComponent<ESP_AreasController>().collidedCharacters)
            {
                ESP_ScreenPiecesController sp = obj.GetComponent<ESP_ScreenPiecesController>();
                if (sp != null)
                {
                    if (sp.index != item.GetComponent<ESP_AreasController>().correctCharacterIndex)
                    {
                        Debug.Log("necesita: " + item.GetComponent<ESP_AreasController>().correctCharacterIndex + " y tiene: " + sp.index);
                        return;
                    }
                }
            }

        }
        puzzleAlreadySolved = true;

        enablePuzzle = false;
        GameManager.instance.turnOn_ExteriorLanterns(FindObjectOfType<SP_Controller>().lanternsExitGame, true);
        GameManager.instance.turnOn_InteriorLanterns(interiorLantern);
        GameManager.instance.callForSunMovement(puzzleIndex);
        FindObjectOfType<ESP_AudioClueController>().audioClue.Stop();
        FindObjectOfType<endGame>().enablePortal();
    }
}
