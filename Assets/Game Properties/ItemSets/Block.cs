using Game_Properties.ItemSets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : ItemTile {

    /* -Block Data Partitioning-  help save ram when having lots of blocks
    Block ID: 8 bits         [0000 0000 . 0000 0000 . 1111 1111 . 0000 0000] Hex: 0x0000FF00; DEC: 65,280 - 100 & 0
    Rotation: 4 bits         [0000 0000 . 0000 0000 . 0000 0000 . 0000 1111] Hex: 0x0000000F; DEC: 15-0
    MetadataMask: 4 bits     [0000 0000 . 0000 0000 . 0000 0000 . 1111 0000] Hex: 0x0000000F; DEC: 240-16 & 0
    Multi-Block data: 8 bit  [0000 0000 . 1111 1111 . 0000 0000 . 0000 0000] Hex: 0x0000FF00; DEC: 16,711,680 - 65,281 & 0 if MetadataMask had 32/ bit 6 is true;
    */

    //    0000 0000 . 0000 0000 . 0000 0000 . 1111 1111
    
    public static readonly int MetadataMask = 0xF0;
    public static readonly int RotationMask = 0x0F;
    public static readonly int BlockIDMask = 0xFF << 8;

    private static readonly ItemSetType type = ItemSetType.physical;
    
    public override ItemSetType GetItemSetType() {

        return type;
    }

    public override string ToString() {

        return "[Name: " + this.GetName() + "] [IsNotCube: " + this.IsCubeShape() + "] [Rotation: " + this.GetRotationType() +"]";
    }
}
