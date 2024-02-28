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
        if (row == y) return true; // matches on y
        if (col == x) return true; // matches on x
        if (Math.Abs(row-y) == Math.Abs(col-x)) return true; // matches on diagonal

        return false;
    }

    public override Boolean canReach(Chesspiece p) {
        int[] loc = p.getLoc();
        return canReach(loc[0], loc[1]);
    }
}
