using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : Manager {

    Transform world;
    Dictionary<Chunk, GameObject> chunkGameObjectMap;
    
    static float tickUpdateTimer;
    static float initialGenerationTimer;

    public Dictionary<Vector3Int, Chunk> chunkMap;
    public Dictionary<Vector3Int, MeshCollider> meshColliderMap;
    public List<Chunk> activeChunks;

    public static int hight;
    public static int radius;

    public WorldManager(int hight = 1, int radius = 1) {

        //tickUpdateTimer = 0f;

        chunkMap = new Dictionary<Vector3Int, Chunk>();
        chunkGameObjectMap = new Dictionary<Chunk, GameObject>();
        activeChunks = new List<Chunk>();

        WorldManager.hight = hight;
        WorldManager.radius = radius;

        world = new GameObject("World").transform;
    }

    // Use this for initialization
    public override void Start() {

        initialGenerationTimer = Time.realtimeSinceStartup;

        //Note: Do not set this in the constructor.
        Chunk.world = this;

        for (int x = -radius; x <= radius; x++) {
            for (int y = 0; y <= hight; y++) {
                for (int z = -radius; z <= radius; z++) {

                    CreateChunkAt(x, y, z);
                }
            }
        }
        
        CreateChunkAt(2, 0, 2);

        
    }


    public void CreateChunkAt(int x, int y, int z) {

        Vector3Int key = new Vector3Int(x, y, z);

        if (chunkMap.ContainsKey(key) == false) {

            Chunk chunk = new Chunk(x, y, z);

            activeChunks.Add(chunk);

            chunkMap.Add(key, chunk);
        }
        
    }

    public void UnloadChunkAt(int x, int y, int z) {

        Chunk chunk;
        GameObject gochunk;

        Vector3Int key = new Vector3Int(x, y, z);
        
        if (chunkMap.TryGetValue(key, out chunk)) {
            if (chunkGameObjectMap.TryGetValue(chunk, out gochunk)) {

                chunk.flags = ~Chunk.ChunkFlags.Awake;
                chunkMap.Remove(key);
                GameManager.RemoveGO(gochunk);
                chunkGameObjectMap.Remove(chunk);
            }
        }
    }


    // Update is called once per frame
    public override void Update(float dt) {

        if (Input.anyKeyDown) {
            
            //Debug.Log(GetBlockAt(0,5,0).GetBlockID());
        }

        tickUpdateTimer += dt;
        bool doTick = tickUpdateTimer >= (1 / 20f);

        for (int i = 0; i < activeChunks.Count; i++) {

            //Is it time to tick?
            if (doTick) {
                //Should this chunk continue ticking
                if (activeChunks[i].Tick() == false) {

                    //if not, sleep
                    activeChunks.RemoveAt(i);
                    i--;
                }
            }
        }

        if (doTick) tickUpdateTimer = 0f;
    }

    public void UpdateChunkWorld() {

        Chunk.world = this;
    }

    public Block GetBlockAt(float x, float y, float z) {

        Chunk ch;
        Block block;

        if (GetChunkAt(x, y, z, out ch)) {

            //Debug.Log("chunk " + ch.position.ToString());


            if (ch.GetBlockAt((int) x, (int) y, (int) z, out block)) {
                return block;
            }
        }

        return ItemSetManager.GetBlockType("Air");
    }

    public void SetBlockAt(float x, float y, float z, Block block) {
        
        Chunk ch;
        if (GetChunkAt(x, y, z, out ch)) {

            Debug.Log("chunk " + ch.position.ToString());


            ch.SetBlockAt((int) x, (int) y, (int) z, block);
        }
    }

    public bool GetChunkAt(int x, int y, int z, out Chunk ch) {

        Vector3Int key = new Vector3Int(x, y, z);

        Chunk chunk;
        if (chunkMap.TryGetValue(key, out chunk)) {
            ch = chunk;
            return true;
        }

        ch = null;
        return false;
    }

    public bool GetChunkAt(float x, float y, float z, out Chunk ch) {

        Vector3Int key = Chunk.WorldCoordinatestoChunkCoordinates(x, y, z);

        Chunk chunk;
        if (chunkMap.TryGetValue(key, out chunk)) {
            ch = chunk;
            return true;
        }

        ch = null;
        return false;
    }

    public void CreateChunkGameObject (Chunk chunk, Mesh graphics, Mesh physics) {


        //Does this chunk already have a gameObject?
        GameObject chunkGO;
        if (chunkGameObjectMap.TryGetValue(chunk, out chunkGO)) {

            chunkGO.GetComponent<MeshFilter>().sharedMesh = graphics;
            chunkGO.GetComponent<MeshCollider>().sharedMesh = physics;
            return;
        }

        //No. Create a new one
        Vector3Int position = chunk.position;

        chunkGO = new GameObject("CHUNK_" + position.ToString());
        chunkGO.transform.parent = world;
        chunkGO.isStatic = true;

        MeshFilter mf = chunkGO.AddComponent<MeshFilter>();
        MeshRenderer mr = chunkGO.AddComponent<MeshRenderer>();

        mr.sharedMaterial = gameManager.material;
        mf.mesh = graphics;
        
        MeshCollider mc = chunkGO.AddComponent<MeshCollider>();
        mc.sharedMesh = physics;

        chunkGameObjectMap.Add(chunk, chunkGO);
        

        if (chunkGameObjectMap.Count == 1) {
            initialGenerationTimer = Time.realtimeSinceStartup - initialGenerationTimer;
            Debug.Log("World took " + initialGenerationTimer * 1000 + "ms to generate");
        }

        return;
    }

    public Vector3Int GetBlockPositionFromRaycast (Vector3 point, Vector3 normal, bool getAdjacent) {

        if (getAdjacent) {

            Vector3Int hitPoint = new Vector3Int() {
                x = Mathf.FloorToInt(point.x + 0.5f + normal.x * 0.2f),
                y = Mathf.FloorToInt(point.y + 0.5f + normal.y * 0.2f),
                z = Mathf.FloorToInt(point.z + 0.5f + normal.z * 0.2f)
            };

            return hitPoint;

        } else {

            Vector3Int hitPoint = new Vector3Int() {
                x = Mathf.FloorToInt(point.x + 0.5f - normal.x * 0.2f),
                y = Mathf.FloorToInt(point.y + 0.5f - normal.y * 0.2f),
                z = Mathf.FloorToInt(point.z + 0.5f - normal.z * 0.2f)
            };

            return hitPoint;
        }
    }
}
