using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockExtensions {

    //Blocks are a container class for an int
    //Each bit is partitioned for data that is shared between all blocks

    //Partition:
    //  Shader Type - 2 bits
    //  Block Type  - 16 bits
    //  Metadata    - 4 bits    1:Is haftblock and if so shape Type - 2 bit    3-4:Shader Type - 2 bits
    //  Rotation    - 4 bits
    // -- test when it the best times for it --  NOTE: Utility blocks use the 16 bits for Multi-Block Structure data

    //Summary: Get the block ID if block is NOT utility
    public static int GetBlockID(this Block block) {

        return (block.GetValue() & 0xFF00) >> 8;
    }

    //Summary: Get the block metadata if block is NOT utility
    public static int GetMetadataType(this Block block) {
        
        return (block.GetValue() & 0xF0) >> 4;
    }

    //Summary: Get the block rotation if block is NOT utility
    public static int GetRotationType(this Block block) {

        return (block.GetValue() & 0xF);
    }

    public static IShape GetShape(this Block block) {

        if (IsCubeShape(block)) {

            return new SlantMesh();
        }


        return new CubeMesh();
    }

    public static void SetMetadataType(this Block block, int Value) {

        block.SetValue(Value);
    }

    //Summary: Check if block is utility (used for Multi-Block Structure data)
    //public static bool IsUtility(this Block block) {

    //    int metadata = GetMetadataType(block);

    //    return (metadata & 0x2) != 0;
    //}

    public static bool IsCubeShape(this Block block) {

        int metadata = GetMetadataType(block);

        if ((metadata & 0x1) > 0) {
            return true;
        }

        return false;
    }

    public static bool IsPartOfBlock(this Block block) { //idk what this is for

        int metadata = GetMetadataType(block);

        return (metadata & 0x10) != 0x10;
    }

    public static bool IsOpaque (this Block block) {

        return block.GetValue() == (int)0x00;
    }

    public static Direction[] Directionslist(this Block block) {

        int rotation = GetRotationType(block);

        return LookDirection[rotation];
    }

    public static bool IfLookDirection(this Block block, Direction dir) {

        Direction[] Directionslist = block.Directionslist();

        Direction oppdir = DirectionExtensions.GetOppositeDir(dir);

        if (oppdir == Directionslist[0] || oppdir == Directionslist[1] || oppdir == Directionslist[2] || oppdir == Directionslist[3]) {

            return true;
        }

        return false;
    }

    public static Direction[][] LookDirection = new Direction[][] { // haftbloacks

        new Direction []{Direction.North, Direction.East, Direction.South, Direction.Up},
        new Direction []{Direction.East, Direction.South, Direction.West, Direction.Up},
        new Direction []{Direction.South, Direction.West, Direction.North, Direction.Up},
        new Direction []{Direction.West, Direction.North, Direction.East, Direction.Up},

        new Direction []{Direction.North, Direction.East, Direction.Down, Direction.Up},
        new Direction []{Direction.East, Direction.South, Direction.Down, Direction.Up},
        new Direction []{Direction.South, Direction.West, Direction.Down, Direction.Up},
        new Direction []{Direction.West, Direction.North, Direction.Down, Direction.Up},

        new Direction []{Direction.North, Direction.East, Direction.South, Direction.Down},
        new Direction []{Direction.East, Direction.South, Direction.West, Direction.Down},
        new Direction []{Direction.South, Direction.West, Direction.North, Direction.Down},
        new Direction []{Direction.West, Direction.North, Direction.East, Direction.Down}

    };
}
