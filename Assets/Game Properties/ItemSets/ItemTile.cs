using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_Properties.ItemSets.interfaces;
using Game_Properties.ItemSets;
using System;

public abstract class ItemTile : ItemObject, INItem {
    
    protected byte bitdata;
    protected ItemHandler handler;

    public abstract ItemSetType GetItemSetType();

    public string GetName() {
        return Name;
    }
    
    public int GetValue() {

        return this.Value;
    }

    public void SetValue(int Value) {

        this.Value = Value;
    }

    public bool HasValue() {
        if (string.IsNullOrEmpty(Value.ToString())) {

            return false;
        } else {
            return true;
        }
    }

    public bool InitializeID(int id) {

        if (HasValue()) {

            return false;
        }

        if (id <= 0xFE00) {
            this.Value = (this.Value | id);
            return true;
        } else {

            return false;
        }
    }
}
