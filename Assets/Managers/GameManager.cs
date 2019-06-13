using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    public static int _seed;
    
    public bool enableThreading;

    public int chunksTall = 1, radius = 1;
    public int maxThreads = 4, updatesPerTick = 128;

    public bool randomSeed;
    public string seed;

    public Material material;


    public ItemSetManager ItemMgr;
    //public StructureManager structureMgr;
    //public ShapeManager shapeMgr;
    //public TextureAndMaterialManager textureAndMaterialMgr;

    public ThreadManager threadMgr;
    public WorldManager worldMgr;
    public EntityManager entityMgr;

	// Use this for initialization
	void Start () {

        if (randomSeed) {
            _seed = Random.Range(-100000, 100000);
            seed = _seed.ToString();
        } else {
            _seed = (seed.GetHashCode() % 200000) - 100000;
        }
        
        ConstructManagers();

        ThreadedProcess.enableThreading = enableThreading;

        StartManagers();
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(Time.timeSinceLevelLoad);
        //Debug.Log(Time.deltaTime);

        UpdateManagers(Time.deltaTime);
	}

    void ConstructManagers () {

        AStar.RegisterWorld(worldMgr);

        ItemMgr = new ItemSetManager();
        //structureMgr = new StructureManager();
        //shapeMgr = new ShapeManager();
        //textureAndMaterialMgr = new TextureAndMaterialManager();

        threadMgr = new ThreadManager(maxThreads, updatesPerTick);
        worldMgr = new WorldManager(chunksTall, radius);
        entityMgr = new EntityManager();
    }

    void StartManagers () {

        ItemMgr.Start();
        //structureMgr.Start();
        //shapeMgr.Start();
        //textureAndMaterialMgr.Start();

        threadMgr.Start();
        worldMgr.Start();
        entityMgr.Start();

    }

    void UpdateManagers (float dt) {

        threadMgr.Update(dt);
        worldMgr.Update(dt);
        entityMgr.Update(dt);
    }

    public static void RemoveGO(GameObject gameobject) {
        Destroy(gameobject);
    }

    public static Type[] GetTypesInNamespace(string nameSpace, Assembly assembly = null) {
        if (assembly == null) {
            assembly = Assembly.GetExecutingAssembly();
        }
        return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
    }
}
