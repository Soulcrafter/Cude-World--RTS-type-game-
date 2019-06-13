using Game_Properties.ItemSets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObject {

    protected abstract string Name {
        get;
    }

    public abstract ItemHandler Handler {
        get;
    }

    public abstract int Value {
        get;
        set;
    }

    public abstract Color32 Color {
        get;
    }

    // need to add a model method
}
