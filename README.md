MinConflicts NQueens

A Unity project to solve NQueens using the Minimum Conflicts Algorithm (see more at: https://en.wikipedia.org/wiki/Min-conflicts_algorithm)

NQueens.cs functions as the main file and instantiates a N number of Queens based on the given Board of boardSize (adjustable from the Board Component in the inspector). 
 The results are also printed in the console, both as a board map and as a list of pieces and their coordinates from 0, 0 (top left) to boardSize-1, boardSize-1 (bottom right)

The board and pieces are also structured in such a way to support the future addition of other types of chesspieces.  At the moment, the Board contains a list of Chesspieces, all of which are of child class Queen.  For the sake of NQueens, this structure is not necessary but demonstrates future-oriented design.



