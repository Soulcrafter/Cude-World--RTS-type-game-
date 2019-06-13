using Game_Properties.ItemSets.IPhysical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ThreadManager {

    public class BlockBuilder : ThreadedProcess {



        Chunk chunk;
        Block[,,] cons;
        //List<int> lightSourceIndices;
        bool isAir;

        public BlockBuilder(Chunk chunk) {

            this.chunk = chunk;
            this.cons = new Block[Chunk.ChuchMaxWidth, Chunk.ChuchMaxHight, Chunk.ChuchMaxWidth];
        }

        protected override void ThreadFunction() {

            isAir = true;

            int heightSacale = 6;

            int seed = GameManager._seed;

            float detailScale = 15.0f;

            int fixX = chunk.position.x * Chunk.ChuchMaxWidth + seed;
            int fixY = chunk.position.y * Chunk.ChuchMaxHight;
            int fixZ = chunk.position.z * Chunk.ChuchMaxWidth + seed;

            for (int x = 0; x < Chunk.ChuchMaxWidth; x++) {
                for (int y = 0; y < Chunk.ChuchMaxHight; y++) {
                    for (int z = 0; z < Chunk.ChuchMaxWidth; z++) {

                        int value = (int) (Mathf.PerlinNoise((x + fixX) / detailScale, (z + fixZ) / detailScale) * heightSacale) + 4;

                        int comV = (int) (y + fixY);

                        if ((y + fixY) < value && isAir) {
                                isAir = false;
                        }

                        if (comV > value) {

                            cons[x, y, z] = ItemSetManager.GetBlockType("Air");

                            //float randX = UnityEngine.Random.Range(1, 100);

                            //if (comV == value + 1 && randX == 1) {

                            //StructureGenerator.GenRockSpike(position, x, y, z, cons);
                            //}
                            continue;
                        }

                        if (comV <= value) {

                            cons[x, y, z] = ItemSetManager.GetBlockType("Stone");
                        }
                        
                        if (comV > value - 2) {

                            cons[x, y, z] = ItemSetManager.GetBlockType("Dirt");
                        }

                        if (comV == value) {

                            cons[x, y, z] = ItemSetManager.GetBlockType("Grass");
                        }

                    }

                }
            }

            //StructureGenerator.GetWaitingBlocks(position, cons);

            //isAir = true;
            //lightSourceIndices = new List<int>();
            //blocks = new Block[Chunk.size * Chunk.size * Chunk.size];

            //float caveTuning = (Mathf.Sin((chunk.position.x / 256f)) + 3.5f);

            //int idx = 0;
            //for (int x = 0; x < Chunk.size; x++) {
            //    for (int y = 0; y < Chunk.size; y++) {
            //        for (int z = 0; z < Chunk.size; z++) {

            //            int _X = x + chunk.position.x + GameManager._seed;
            //            int _Z = z + chunk.position.z + GameManager._seed;

            //            float height = Mathf.PerlinNoise((_X) / 64f + 756, (_Z) / 64f) * 16f
            //                + Mathf.PerlinNoise((_X) / 16f - 5778, (_Z) / 16f +78) * 4f
            //                + Mathf.PerlinNoise((_X) / 256f, (_Z) / 256f + 567) * 96f
            //                + Mathf.PerlinNoise((_X) / 128f - 7798, (_Z) / 64f +120) * 48f
            //                + Mathf.PerlinNoise((_X) / 64f, (_Z) / 96f + 4800) * 32f
            //                + Mathf.PerlinNoise((_X) / 256f + 846, (_Z) / 128f + 12) * 72f
            //                + 160f;

            //            if (y + chunk.position.y < height) {

            //                if (GetNoise3D(_X, y + chunk.position.y, _Z) < caveTuning) {


            //                    blocks[idx].value = 0x20;
            //                    isAir = false;
            //                }

            //            }


            //            idx++;
            //        }
            //    }
            //}
        }

        //float GetNoise3D (int x, int y, int z) {

        //    return Mathf.PerlinNoise(x / 32f, y / 32f) + 
        //        Mathf.PerlinNoise(x / 32f, z / 32f) + 
        //        Mathf.PerlinNoise(y / 32f, z / 32f) + 
        //        Mathf.PerlinNoise(y / 32f, x / 32f) + 
        //        Mathf.PerlinNoise(z / 32f, x / 32f) + 
        //        Mathf.PerlinNoise(z / 32f, y  / 32f);

        //}

        protected override void OnFinished() {

            chunk.OnBlockDataFinished(cons, isAir);
        }
    }
}

