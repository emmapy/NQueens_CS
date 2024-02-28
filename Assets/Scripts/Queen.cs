using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chesspiece
{
    void Awake() {
        token = "Q";
    }

    public Boolean canReach(int r1, int c1) {
        Debug.Log($"from row {row}, col {col} (={col}, {row}), attempting to reach row {r1}, col {c1} = ({c1}, {r1})");
        if (row == r1) return true; // matches on y
        if (col == c1) return true; // matches on x
        if (Math.Abs(row-r1) == Math.Abs(col-c1)) return true;
        return false;
    }
}
