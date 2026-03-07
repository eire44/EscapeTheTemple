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

    void Start()
    {
        nextRow(currentRowIndex);
    }


    bool checkConfiguration(int bowlIndex, string symbol)
    {
        playerConfiguration[bowlIndex] = symbol;
        Debug.Log("ahora en " + bowlIndex + " esta " + symbol);
        for (int i = 0; i < rounds[currentRowIndex].pairs.Length; i++)
        {
            //Debug.Log("en " + i + " esta " + playerConfiguration[i]);
            if(rounds[currentRowIndex].pairs[i].correctSymbol != playerConfiguration[i])
            {
                return false;
            }
        }

        return true;
    }

    public void callForCheckConfiguration(int bowlIndex, string symbol)
    {
        Debug.Log("entro " + symbol + " en " + bowlIndex);
        if(checkConfiguration(bowlIndex, symbol))
        {
            Debug.Log("CORRECT");
            currentRowIndex++;
            nextRow(currentRowIndex);
        }
    }


    void nextRow(int roundIndex)
    {
        if (roundIndex >= rounds.Length)
        {
            Debug.Log("Puzzle completo");
            foreach (var item in pieces)
            {
                item.gameObject.layer = LayerMask.NameToLayer("Default");
            }

            foreach (Transform child in fourthBoard.transform)
            {
                child.GetComponent<fadeRoomDoor>().StartFade();
            }
            fourthText.SetActive(false);
            keyPiece.SetActive(true);
            keyPiece.GetComponent<fadeIn_PuzzlePieces>().StartFade();
            return;
        }

        for (int i = 0; i < boards.Length; i++)
        {
            boards[i].text = rounds[roundIndex].pairs[i].phrase;
            playerConfiguration[i] = "";
        }

    }
}
