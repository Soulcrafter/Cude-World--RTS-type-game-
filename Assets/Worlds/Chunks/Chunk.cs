using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk {

    public enum ChunkFlags {
        Awake = 0x01,
        StartedBlocks = 0x02,
        HasBlocks = 0x04,
        StartedMesh = 0x08,
        HasMesh = 0x10,
        chunkLoaded = 0x14
    };

    public static WorldManager world;
    public readonly static int ChuchMaxWidth = 16, ChuchMaxHight = 16;

    public Block[,,] blockData = new Block[Chunk.ChuchMaxWidth, Chunk.ChuchMaxHight, Chunk.ChuchMaxWidth];
    public Vector3Int position;

    //Ai
    public Node[,,] NodeArray;

    public ChunkFlags flags;
    private Mesh mesh;

    public Chunk (int x, int y, int z) {

        position.x = x;
        position.y = y;
        position.z = z;

        flags |= ChunkFlags.Awake;
    }

    public bool Tick () {

        if ((flags & ChunkFlags.Awake) == 0x00) return false;

        if ((flags & ChunkFlags.StartedBlocks) == 0x00) {

            ThreadManager.CreateBlockBuilderThread(this);
            flags |= ChunkFlags.StartedBlocks;
        }

        if ((flags & ChunkFlags.HasBlocks) != 0x00 && (flags & ChunkFlags.StartedMesh) == 0x00) {
            ThreadManager.CreateMeshBuilderThread(this);
            flags |= ChunkFlags.StartedMesh;
        }

        if ((flags & ChunkFlags.chunkLoaded) != 0x00) {

            //Debug.Log("loaded" + position.ToString());
        }

        return true;
    }

    public void Draw () {

        if ((flags & ChunkFlags.HasMesh) != 0x00) {
            Graphics.DrawMesh(mesh, Matrix4x4.identity, Manager.gameManager.material, 0);
        }

        
    }

    public void OnBlockDataFinished (Block[,,] blocks, bool isAir = false) { //public void OnBlockDataFinished (ChunkData blocks, List<int> lightSourceIndices, bool isAir = false) {

        this.blockData = blocks;

        if (isAir == true) { flags &= ~ChunkFlags.Awake; }
        flags |= ChunkFlags.HasBlocks;
    }

    public void OnMeshDataFinished (Mesh graphics, Mesh physics) {

        if (graphics.vertexCount == 0)
            flags &= ~ChunkFlags.Awake;

        mesh = graphics;
        flags |= ChunkFlags.HasMesh;

        if (physics == null) return;

        world.CreateChunkGameObject(this, graphics, physics);

        //Debug.Log( Vector3Int.DecimalToBinary(((int) flags).ToString()));
    }

    public bool GetBlockAt (int x, int y, int z, out Block block) {

        x = negToPos(x);
        y = negToPos(y);
        z = negToPos(z);

        if ((flags & ChunkFlags.HasBlocks) == 0) {

            block = ItemSetManager.GetBlockType("Air");
            return false;
        }

        if (IsWithInBounds(x, y, z)) {

            block = blockData[x, y, z];
            return true;
        }

        block = ItemSetManager.GetBlockType("Air");
        return false;
    }

    public void SetBlockAt (int x, int y, int z, Block block) {

        if ((flags & ChunkFlags.HasBlocks) == 0) return;

         x = negToPos((Mathf.FloorToInt(x)) - (position.x * ChuchMaxWidth));
         y = negToPos((Mathf.FloorToInt(y)) - (position.y * ChuchMaxHight));
         z = negToPos((Mathf.FloorToInt(z)) - (position.z * ChuchMaxWidth));

        //
        //if is out of Bounds look for other chunk to brake it in.................9
        //

        if (IsWithInBounds(x, y, z)) {

            blockData[x,y,z] = block;
            flags &= ~ChunkFlags.StartedMesh;
        }
    }

    public bool GetNeighborChuck(int x, int y, int z, out Chunk chunk) {

        if(world.GetChunkAt(x, y, z, out chunk)){
            return true;
        }
        return false;
    }

    public Block GetNeighborByBlock(int x, int y, int z, Direction dir) {
        
        Vector3Int neighborCoord = Vector3Int.GetNeighborByVector3Int(x, y, z, dir);

        if (neighborCoord.x < 0 || neighborCoord.x >= ChuchMaxWidth || neighborCoord.y < 0 || neighborCoord.y >= ChuchMaxHight || neighborCoord.z < 0 || neighborCoord.z >= ChuchMaxWidth) {

            return ItemSetManager.GetBlockType("Air");
        } else {

            return blockData[neighborCoord.x, neighborCoord.y, neighborCoord.z];
        }
    }

    public static Vector3Int WorldCoordinatestoChunkCoordinates(float fx, float fy, float fz) {

        int x = Mathf.FloorToInt(fx / Chunk.ChuchMaxWidth);
        int y = Mathf.FloorToInt(fy / Chunk.ChuchMaxHight);
        int z = Mathf.FloorToInt(fz / Chunk.ChuchMaxWidth);

        return new Vector3Int(x, y, z);
    }

    public static bool IsWithInBounds(int x, int y, int z) {

        return x >= 0 && y >= 0 && z >= 0 && x < ChuchMaxWidth && y < ChuchMaxHight && z < ChuchMaxWidth;
    }

    public static int negToPos(int n) {

        if (n < 0) {
            n *= -1;
        }

        return n;
    }
}
