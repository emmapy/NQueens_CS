using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chesspiece : MonoBehaviour
{
    protected string token = "X";
    protected int row = -1;
    protected int col = -1;

    protected boardManager tempBoard;

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

    public void setLoc(int r, int c) {
        row = r;
        col = c;
    }

    public string getPrint() {
        string prefix = token + " at " + row + ", " + col;
        string suffix = row == -1 || col == -1 ? " (UNINITIALIZED)" : "";
        return prefix + suffix;
    }
}
