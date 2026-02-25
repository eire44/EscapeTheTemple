using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ESP_LeverMovement : MonoBehaviour
{
    bool puzzleActive = false;
    bool isDragging = false;
    bool isMoving = false;

    Vector2 startMousePos;
    float dragThreshold = 50f;

    public Transform[] screenPieces;
    int screenPieces_Index = 0;
    enum characters
    {
        Character1, Character2, Character3, Character4
    }

    characters currentCharacter;
    Dictionary<characters, Transform> piecesCorrelation = new Dictionary<characters, Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (characters pair in System.Enum.GetValues(typeof(characters)))
        {
            piecesCorrelation[pair] = screenPieces[screenPieces_Index];
            screenPieces_Index++;
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
                if (hit.collider.CompareTag("Lever"))
                {
                    moveLever();
                }
                else if (hit.collider.CompareTag("Character1"))
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
            }
        }
    }

    void moveLever()
    {

    }
}
