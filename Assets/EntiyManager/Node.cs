using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    /* Variables */

    private Vector3Int worldPosition;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(Vector3Int _worldPos) {
        worldPosition = _worldPos;
    }

    public int getX {
        get { return this.worldPosition.x; }
    }

    public int getY {
        get { return this.worldPosition.y; }
    }

    public int getZ {
        get { return this.worldPosition.z; }
    }
}

