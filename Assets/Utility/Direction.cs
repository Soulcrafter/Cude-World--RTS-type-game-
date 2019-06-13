using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    North,
    East,
    South,
    West,
    Up,
    Down
}

public static class DirectionExtensions {
        
    static Direction[] OppositeDir = new Direction[] { Direction.South, Direction.West, Direction.North, Direction.East, Direction.Down, Direction.Up };
    
    public static Direction GetOppositeDir(Direction dir) {

        return OppositeDir[(int) dir];
    }
}