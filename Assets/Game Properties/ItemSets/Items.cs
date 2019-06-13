using System.Collections;
using System.Collections.Generic;
using Game_Properties.ItemSets;

public abstract class Items : ItemTile {

    private static readonly ItemSetType type = ItemSetType.vertical;

    public override ItemSetType GetItemSetType() {

        return type;
    }
    
}