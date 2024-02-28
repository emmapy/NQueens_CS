using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chesspiece : MonoBehaviour
{
    protected string token = "X";
    protected int row = -1;
    protected int col = -1;

    void Awake() {
        token = "X";
    }
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public string getToken() {
        return token;
    }

    // sets a location coordinate
    public void setLoc(int x, int y) {
        row = y;
        col = x;
    }

    public int[] getLoc() {
        return new int[]{col, row};
    }

    public string getPrint() {
        string prefix = token + " at coordinate " + col + ", " + row;
        string suffix = row == -1 || col == -1 ? " (UNINITIALIZED)" : "";
        return prefix + suffix;
    }

    public virtual Boolean canReach(int r1, int c1) {
        Debug.LogError("calling canReach(row, col) on generic parent Chesspiece");
        return false;
    }

    public virtual Boolean canReach(Chesspiece p) {
        Debug.LogError("calling canReach(piece) on generic parent Chesspiece");
        return false;
    }
}
