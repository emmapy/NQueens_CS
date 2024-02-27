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
        if (row == r1) return true;
        if (col == c1) return true;
        if (Math.Abs(row-r1) == Math.Abs(col-c1)) return true;
        return false;
    }

    public Boolean canReach(Chesspiece p) {
        int[] loc = p.getLoc();
        int r1 = loc[0];
        int c1 = loc[1];
        if (row == r1) return true;
        if (col == c1) return true;
        if (Math.Abs(row-r1) == Math.Abs(col-c1)) return true;
        return false;
    }
}
