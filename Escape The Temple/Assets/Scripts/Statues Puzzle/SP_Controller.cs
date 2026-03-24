using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Controller : MonoBehaviour
{
    public Camera camera;
    public LayerMask pieceLayer;
    int[,] board;
    public Transform[] cellPositions;
    bool isMoving = false;
    public Transform lever;
    public AudioSource audiosource;
    public AudioClip[] audioClips;

    [HideInInspector] public bool puzzleAlreadySolved = false;
    public int puzzleIndex = 8;

    public interiorLanternsController[] interiorLantern;
    public interiorLanternsController[] puzzle10Lantern;
    public exteriorLanternsController[] lanternsExitGame;
    private void Start()
    {
        //board = new int[3, 3];

        //int value = 1;

        //for (int x = 0; x < 3; x++)
        //{
        //    for (int y = 0; y < 3; y++)
        //    {
        //        if (value <= 8)
        //            board[x, y] = value++;
        //        else
        //            board[x, y] = 0;
        //    }
        //}
        //Swap(new Vector2Int(2, 2), new Vector2Int(2, 1));
        board = new int[3, 3]
        {
            {1,2,3},
            {4,5,6},
            {7,0,8}
        };

        UpdateVisuals();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            trySelectPiece();
        }
    }

    void trySelectPiece()
    {
        if (isMoving) return;

        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, pieceLayer))
        {
            SP_PiecesController pieceController = hit.collider.GetComponent<SP_PiecesController>();

            if (pieceController != null)
            {
                Vector2Int piecePos = FindPosition(pieceController.id);
                Vector2Int emptyPos = FindPosition(0);

                if (IsAdjacent(piecePos, emptyPos))
                {
                    audiosource.PlayOneShot(audioClips[0]);
                    int index = emptyPos.x * board.GetLength(1) + emptyPos.y; //fila * cantidadColumnas + columna
                    Vector3 targetPos = cellPositions[index].position;
                    StartCoroutine(MovePieceCoroutine(pieceController, piecePos, emptyPos, targetPos));
                    FindObjectOfType<SP_StatuesMovement>().moveStatue(pieceController.id, index);
                }
            }
        }
    }

    IEnumerator MovePieceCoroutine(SP_PiecesController piece, Vector2Int piecePos, Vector2Int emptyPos, Vector3 targetPos)
    {
        isMoving = true;
        Vector3 startPos = piece.transform.position;

        //int index = emptyPos.x * board.GetLength(1) + emptyPos.y; //fila * cantidadColumnas + columna
        //Vector3 targetPos = cellPositions[index].position;

        float duration = 0.2f;
        float time = 0f;

        while (time < duration)
        {
            piece.transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        piece.transform.position = targetPos;

        Swap(piecePos, emptyPos);
        isMoving = false;
    }

    Vector2Int FindPosition(int id)
    {
        for (int x = 0; x < board.GetLength(0); x++)
            for (int y = 0; y < board.GetLength(1); y++)
                if (board[x, y] == id)
                    return new Vector2Int(x, y);

        return Vector2Int.zero;
    }

    bool IsAdjacent(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) == 1;
    }

    void UpdateVisuals()
    {
        SP_PiecesController[] pieces = FindObjectsOfType<SP_PiecesController>();

        foreach (var piece in pieces)
        {
            Vector2Int pos = FindPosition(piece.id);
            int index = pos.x * 3 + pos.y;
            piece.transform.position = cellPositions[index].position;
        }
    }

    public void Swap(Vector2Int a, Vector2Int b)
    {
        int temp = board[a.x, a.y];
        board[a.x, a.y] = board[b.x, b.y];
        board[b.x, b.y] = temp;
    }

    public void checkStatuesPositions(GameObject[] statues)
    {
        foreach (var statue in statues)
        {
            if(statue.GetComponent<SP_StatuesController>().currentPos != statue.GetComponent<SP_StatuesController>().correctPos)
            {
                return;
            }
        }

        SP_PiecesController[] pieces = FindObjectsOfType<SP_PiecesController>();
        foreach (var piece in pieces)
        {
            piece.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        audiosource.PlayOneShot(audioClips[1]);
        lever.gameObject.SetActive(true);
        lever.GetComponent<fadeIn_PuzzlePieces>().StartFade();
        foreach (Transform item in lever)
        {
            item.gameObject.SetActive(true);
            item.GetComponent<fadeIn_PuzzlePieces>().StartFade();
        }
        puzzleAlreadySolved = true;
        GameManager.instance.turnOn_InteriorLanterns(interiorLantern);
        GameManager.instance.callForSunMovement(puzzleIndex);
    }
}
