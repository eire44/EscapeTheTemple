using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class stagesController : MonoBehaviour
{
    public textBoard[] rounds;
    //string[] firstRow = { "“I am valuable if I am recognized.”", "“I want to be admired.”", "“I need that approval.”", "“Jealousy and insecurity are consuming me.”" };
    //string[] secondRow = { "“Being ‘spiritual’ makes me superior.”", "“I want to appear enlightened.”", "“I need to hold onto this image.”", "" };
    //string[] thirdRow = { "“Having money gives security.”", "“I want to earn more.”", "“I need to maintain and increase what I have.”", "“I am dissatisfied with what I have.”" };

    string[] playerConfiguration = {"", "", "", ""};

    int currentRowIndex = 0;
    public TextMeshPro[] boards;
    public GameObject[] pieces;
    public GameObject fourthBoard;
    public GameObject fourthText;
    public GameObject keyPiece;
    [HideInInspector] public bool puzzleAlreadySolved = false;
    public int puzzleIndex = 2;

    public interiorLanternsController[] interiorLantern1;
    public interiorLanternsController[] interiorLantern2;
    public interiorLanternsController[] interiorLantern3;

    void Start()
    {
        nextRow(currentRowIndex);
    }


    bool checkConfiguration(int bowlIndex, string symbol)
    {
        playerConfiguration[bowlIndex] = symbol;
        for (int i = 0; i < rounds[currentRowIndex].pairs.Length; i++)
        {
            if(rounds[currentRowIndex].pairs[i].correctSymbol != playerConfiguration[i])
            {
                return false;
            }
        }

        return true;
    }

    public void callForCheckConfiguration(int bowlIndex, string symbol)
    {
        if(checkConfiguration(bowlIndex, symbol))
        {
            if (currentRowIndex == 0)
            {
                FindObjectOfType<GameManager>().turnOn_InteriorLanterns(interiorLantern1);
            } else if (currentRowIndex == 1)
            {
                FindObjectOfType<GameManager>().turnOn_InteriorLanterns(interiorLantern2);
            } else if (currentRowIndex == 2)
            {
                puzzleAlreadySolved = true;
                FindObjectOfType<GameManager>().turnOn_InteriorLanterns(interiorLantern3);
            }
            currentRowIndex++;
            
            nextRow(currentRowIndex);
        }
    }


    void nextRow(int roundIndex)
    {
        if (roundIndex >= rounds.Length)
        {
            foreach (var item in pieces)
            {
                item.gameObject.layer = LayerMask.NameToLayer("Default");
            }

            //foreach (Transform child in fourthBoard.transform)
            //{
            //    child.GetComponent<fadeRoomDoor>().StartFade();
            //}
            fourthText.SetActive(false);
            keyPiece.SetActive(true);
            keyPiece.GetComponent<fadeIn_PuzzlePieces>().StartFade();
            GameManager.instance.checkIfRoom2Completed();
            GameManager.instance.callForSunMovement(puzzleIndex);
            return;
        }

        for (int i = 0; i < boards.Length; i++)
        {
            boards[i].text = rounds[roundIndex].pairs[i].phrase;
            playerConfiguration[i] = "";
        }

    }
}
