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

    public int MAX_STEPS = 100;
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
        solveNQueens();
        myBoard.printBoardState();
        myBoard.printPieceList();
    }

    void initializeNQueens() {
        int[] startingCoords = new int[myBoard.boardSize];
        for (int i = 0; i < myBoard.boardSize; i++) {
            startingCoords[i] = Random.Range(0, myBoard.boardSize);
        }
        populateBoard(startingCoords);
    }

    void populateBoard(int[] coords) {
        for (int i = 0; i < coords.Length; i++) {
            Chesspiece piece = myBoard.allPieces[i];
            myBoard.placeItem(piece, coords[i], i);
        }
    }

    void solveNQueens() {

        for (int i = 0; i < MAX_STEPS; i++) {
            List<int> boardConflicts = getBoardConflicts();

            if (boardConflicts.Count == 0) {
                Debug.Log($"NQueens complete in {i} steps");
                return;
            } else {
                int rndConflictIndex = Random.Range(0, boardConflicts.Count);
                int rndConflictRow = boardConflicts[rndConflictIndex];

                List<int> bestMoves = findBestMoves(rndConflictRow);
                int rndBestMove = bestMoves[Random.Range(0, bestMoves.Count)];

                // move the rndConflictRow piece to row, rndBestMove
                Chesspiece piece = myBoard.allPieces[rndConflictRow];
                myBoard.moveItem(piece, rndBestMove, rndConflictRow);
            }
        }

        Debug.LogError($"did not finish nQueens in {MAX_STEPS} steps, fail");
    }

    // return a List<int> of indeces, each representing a row that is in conflict
    List<int> getBoardConflicts() {
        List<int> conflicts = new List<int>();
        for (int i = 0; i < myBoard.boardSize; i++) {
            Chesspiece p = myBoard.allPieces[i];
            for (int j = 0; j < myBoard.boardSize; j++) {
                if (i == j) continue;
                if ((p as Queen).canReach(myBoard.allPieces[j])) {
                    conflicts.Add(i);
                    break;
                }
            }
        }
        return conflicts;   
    }

    // return a List<int> of best possible moves along the row that have the fewest possible conflicts
     List<int> findBestMoves(int row) {
        List<int> bestConflictCells = new List<int>();

        int bestConflictsThreshold = -1;

        // traverse this row (loop over col)
        for (int col = 0; col < myBoard.boardSize; col++) {

            // compare this cell (row, col) to all queens (except for self)
            int conflictsFound = 0;
            for (int q = 0; q < myBoard.boardSize; q++) {
                Chesspiece queen = myBoard.allPieces[q];
                if (q == row) {
                    // this queen is self, skip
                    continue;
                } else if ((queen as Queen).canReach(col, row)) {
                    conflictsFound += 1;
                } 
                // else does not conflict
            }

            // update bestConflicts accordingly
            if (conflictsFound == bestConflictsThreshold) {
                bestConflictCells.Add(col);
            } else if (bestConflictsThreshold == -1 || conflictsFound < bestConflictsThreshold) {
                bestConflictsThreshold = conflictsFound;
                bestConflictCells.Clear();
                bestConflictCells.Add(col);
            }
        }
        return bestConflictCells;
    }
}
