
using Game_Properties.ItemSets.interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetManager : Manager {

    public static Dictionary<string, ItemTile> ItemSetList;
    private static List<int> TakenIndex;

    public static int ItemloadIndex = 0;
    
    public ItemSetManager () {
        
    }

    public override void Start () {

        ItemSetList = new Dictionary<string, ItemTile>();
        TakenIndex = new List<int>();
        LoadItemSet();
    }

    void LoadItemSet() {

        List<Block> LoadingBlock = new List<Block>();

        Type[] cmds_classes = GameManager.GetTypesInNamespace("Game_Properties.ItemSets.IPhysical");

        foreach (Type type in cmds_classes) {

            if (type.IsSubclassOf(typeof(Block))) {
                try {

                    Block block = (Block) Activator.CreateInstance(type);
                    
                    if (block.HasValue()) {

                        TakenIndex.Add(block.GetBlockID());
                    }
                    
                    LoadingBlock.Add(block);

                } catch (Exception e) {
                    Debug.Log("" + e.Message);
                }
            } else {
                continue;
            }
        }

        Block[] newTakenIndex = LoadingBlock.ToArray();
        
        if (newTakenIndex != null && newTakenIndex.Length > 0) {

            for (int i = 0; i < newTakenIndex.Length; i++) {

                bool okfornext = false;

                Block check = newTakenIndex[i];
                
                if (ItemSetList.ContainsKey(check.GetName())) {
                    continue;
                }

                int j = 0;

                if (!check.HasValue()) {
                    
                    while (!okfornext) {

                        if (!TakenIndex.Contains(j)) {

                            check.InitializeID(j);
                            okfornext = true;
                        }

                        j++;
                    }
                }
                ItemSetList.Add(check.GetName(), check);

            }

        }

    }

    void AddbeforeLoad() {
        
    }

    void ParseBlockFile (string[] data) {
        
    }

    public static ItemTile GetItemSet(string name) {

        ItemTile tryGet;

        if (ItemSetList.ContainsKey(name)) {
            if (ItemSetList.TryGetValue(name, out tryGet)) {
                return tryGet;
            }
        }

        return null;
    }

    public static Block GetBlockType(string name) {

        ItemTile block = GetItemSet(name);

        if (block != null) {

            if (block is Block) {
                return (Block) block;
            }
        }
        return (Block)GetItemSet("Air");
        
    }


}
