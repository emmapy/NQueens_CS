using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chesspiece
{
    void Awake() {
        token = "Q";
    }

    public override Boolean canReach(int x, int y) {
        // Debug.Log($"from row {row}, col {col} (={col}, {row}), attempting to reach row {r1}, col {c1} = ({c1}, {r1})");
        if (row == y) return true; // matches on y
        if (col == x) return true; // matches on x
        if (Math.Abs(row-y) == Math.Abs(col-x)) return true;

        Debug.Log($"canReach returning false: this coord ({col},{row}) can't reach coord ({x},{y})");
        return false;
    }

    public override Boolean canReach(Chesspiece p) {
        int[] loc = p.getLoc();
        return canReach(loc[0], loc[1]);
    }
}
