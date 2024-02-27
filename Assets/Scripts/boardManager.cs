using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using Image = UnityEngine.UI.Image;
using Vector3 = UnityEngine.Vector3;

public class boardManager : MonoBehaviour {

    public int boardSize;
    public Chesspiece testPiece;
    Chesspiece[] allPieces;
    RectTransform rectT;
    string[,] boardState;

    void Awake() {
        if (boardSize == 0) {
            Debug.LogError("Initial board size is invalid.  Exit");
            return;
        }
        
        // create chess board
        createBoard(boardSize);
    
    }
    // Start is called before the first frame update
    void Start() {
        placeItem(testPiece, 4, 4);
        printBoardState();
        printPieceList();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void createBoard(int size) {
        rectT = GetComponent<RectTransform>();
        Graphic G = GetComponent<Graphic>();
        Material mat = G.material;
        mat.SetFloat("_boardDim", boardSize);

        initBoardState();
    }

    void initBoardState() {
        boardState = new string[boardSize, boardSize];
        for (int i = 0; i < boardState.GetLength(0); i++) {
            for (int j = 0; j < boardState.GetLength(1); j++) {
                boardState[i, j] = "-";
            }
        }
        Debug.LogWarning("DEBUG: allPieces[] contains testPiece only");
        allPieces = new Chesspiece[] {testPiece};
    }

    void printBoardState() {
        string toPrint = "PRINTED BOARD =>";

        string col = "";
        
        for (int j = 0; j<boardState.GetLength(1); j++) {
            col += "\tc" + j;
        }
        toPrint += "\n" + col;

        for (int i = 0; i < boardState.GetLength(0); i++) {
            string row = "r"+i;

            for (int j = 0; j < boardState.GetLength(1); j++) {
                row += "\t" + boardState[i, j] + " ";
            }
            toPrint += "\n" + row;
        }

        Debug.Log(toPrint);
    }

    void printPieceList() {
        string log = "AllPieces: =>\n";
        foreach (Chesspiece p in allPieces) {
            log += p.getPrint() + "\n";
        }
        Debug.Log(log);
    }

    void placeItem(Chesspiece p, int x, int y) {
        // check for user errors...
        Debug.Assert(
            (x > 0) && (x < boardSize) && (y > 0) && (y < boardSize), 
            "The input coordinate lies outside the board's dimensions."
        );

        Debug.Assert(rectT.pivot == new Vector2(0, 1), "The board's pivot is not 0,1. Fix in inspector");

        // handle Transform....
        Vector3[] corners = new Vector3[4];
        rectT.GetWorldCorners(corners);

        // world width and height
        float wWidth = Math.Abs(corners[0].x - corners[2].x);
        float wHeight = Math.Abs(corners[0].y - corners[1].y);

        Vector2 cellSz = new Vector2(wWidth / boardSize, wHeight / boardSize);
        Vector3 boardLoc = new Vector3(x * cellSz.x + cellSz.x/2, -1 * (y * cellSz.y + cellSz.y/2), 0);
        
        p.transform.position = rectT.position + boardLoc;

        // handle boardState and Piece data
        boardState[x, y] = p.getToken();
        p.setLoc(x, y);

    }
}
