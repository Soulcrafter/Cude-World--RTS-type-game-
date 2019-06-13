using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Manager {

    public Transform parent;
    
	// Use this for initialization
	public override void Start () {

        parent = new GameObject("Entities").transform;

	}
	
	// Update is called once per frame
	public override void Update (float dt) {
		
	}

    public void SpawnEntityAtPosition (Vector3Int position) {

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.name = "CUBE_" + parent.childCount;
        cube.transform.position = new Vector3() {
            x = position.x,
            y = position.y,
            z = position.z
        };

        cube.AddComponent<Rigidbody>();
        cube.transform.parent = parent;
    }

}
