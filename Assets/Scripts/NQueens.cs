using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// N QUEENS:
/*
    1. initialize: randomly place pieces (1 per col)
    2. solve:
        for i->MAX_STEPS
            if the board has no conflicts, success
            else
                1. select a random piece (ie, a row index) that is in conflict
                2. traverse the piece's row to find the cells with the fewest conflicts (there may be more than one)
                3. re locate the piece to one of the cells with fewest conflicts.  if there are more than 1, it is randomly selected

            if i = MAX_STEPS, n-queens has failed


*/
public class NQueens : MonoBehaviour
{

    public Board myBoard;
    public GameObject queenPrefab;

    void Awake() {
        // instantiate n queens
        for (int i = 0; i < myBoard.boardSize; i++) {
            GameObject go = Instantiate(queenPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            myBoard.allPieces.Add(go.GetComponent<Queen>());
        }

    }

    // Start is called before the first frame update
    void Start() {
        initializeNQueens();
        Debug.Log("initialized");
        myBoard.printBoardState();
        myBoard.printPieceList();

        List<int>[] attacks = new List<int>[myBoard.allPieces.Count];


        for (int i = 0; i < myBoard.allPieces.Count; i++) {
            Chesspiece qI = myBoard.allPieces[i];
            attacks[i] = new List<int>();
            for (int j = 0; j < myBoard.allPieces.Count; j++) {
                if (j == i) continue;
                Chesspiece qJ = myBoard.allPieces[j];
                int[] loc = qJ.getLoc();
                if ((qI as Queen).canReach(loc[1], loc[0])) {
                    attacks[i].Add(j);
                }
            }
        }

        string log = "";
        int k = 0;
        foreach(List<int> L in attacks) {
            log += k + " can attack " + string.Join(", ", L) + "\n";
            k += 1;
        }
        Debug.Log(log);

    }

    void initializeNQueens() {
        int[] startingCoords = new int[myBoard.boardSize];
        for (int i = 0; i < myBoard.boardSize; i++) {
            startingCoords[i] = Random.Range(0, myBoard.boardSize-1);
        }
        populateBoard(startingCoords);
    }

    void populateBoard(int[] coords) {
        for (int i = 0; i < coords.Length; i++) {
            Chesspiece piece = myBoard.allPieces[i];
            myBoard.placeItem(piece, coords[i], i);
        }
    }
}
