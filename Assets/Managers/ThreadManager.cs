using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ThreadManager : Manager {

    public int maxThreads;
    public int updatesPerTick;
    
    public Queue<ThreadedProcess> activeProcesses;
    public Queue<ThreadedProcess> waitingProcesses;

    public static Action<Chunk> CreateBlockBuilderThread;
    public static Action<Chunk> CreateMeshBuilderThread;

    static float timer;

    public ThreadManager (int maxThreads = 4, int updatesPerTick = 256) {

        this.maxThreads = maxThreads;
        this.updatesPerTick = updatesPerTick;
    }

	// Use this for initialization
	public override void Start () {

        activeProcesses = new Queue<ThreadedProcess>();
        waitingProcesses = new Queue<ThreadedProcess>();

        CreateBlockBuilderThread = Internal_BlockBuilderThread;
        CreateMeshBuilderThread = Internal_MeshGenerationThread;
	}
	

	// Update is called once per frame
	public override void Update (float dt) {
        
        for (int j = 0; j < updatesPerTick; j++) {

            for (int i = 0; i < activeProcesses.Count; i++) {
                ThreadedProcess next = activeProcesses.Dequeue();

                if (next.Update() == false) activeProcesses.Enqueue(next);

            }

            for (int i = activeProcesses.Count; i < maxThreads && waitingProcesses.Count > 0; i++) {

                ThreadedProcess next = waitingProcesses.Dequeue();
                next.Start();
                activeProcesses.Enqueue(next);
            }
        }
	}

    public void Internal_BlockBuilderThread (Chunk chunk) {

        BlockBuilder builder = new BlockBuilder(chunk);
        waitingProcesses.Enqueue(builder);
    }

    public void Internal_MeshGenerationThread (Chunk chunk) {

        MeshBuilder builder = new MeshBuilder(chunk);
        waitingProcesses.Enqueue(builder);
    }
}
