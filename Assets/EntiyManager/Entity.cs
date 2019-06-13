using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public int DX = 0, DY = 0, DZ = 0;
    public Stack<Vector3Int> path;
    public float moveSpeed = 7f;

    private Vector3 lastmove;

    Vector3 goalPosition;

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            FindPath(new Vector3Int(DX, DY, DZ));
        }

        test();

        //moveEntity();
    }

    private void test() {

        if (path == null) return;

        Debug.Log("moving....");

        if (path.Count >= 0 && Mathf.Abs(transform.position.x - goalPosition.x) < Mathf.Epsilon && Mathf.Abs(transform.position.y - goalPosition.y) < Mathf.Epsilon) {

            if (path.Count != 0) {
                goalPosition = path.Pop().IntToFloatVec();
            }

        } else {

            transform.position = Vector3.MoveTowards(transform.position, goalPosition, Time.deltaTime * moveSpeed);
        }

        //if (path.Count == 0) {
        //    goalPosition = transform.position;
        //}
    }

    private void moveEntity() {

        if (path == null)
            return;

        if (path.Count <= 0) {

            goalPosition = transform.position;
        }

        if (path.Count > 0 && transform.position != lastmove) {

            Debug.Log("moving....");

            lastmove = path.Pop().IntToFloatVec();
            goalPosition = lastmove;
            spawnball(lastmove, "(" + lastmove.x + ", " + lastmove.y + ", " + lastmove.z + ")");
            transform.position = Vector3.MoveTowards(transform.position, goalPosition, Time.deltaTime * moveSpeed);

        }
    }

    public void FindPath (Vector3Int destination) {
        
        goalPosition = transform.position;

        Vector3Int start = new Vector3Int() {
            x = Mathf.FloorToInt(transform.position.x),
            y = Mathf.FloorToInt(transform.position.y),
            z = Mathf.FloorToInt(transform.position.z)
        };

        AStar.FindPath(start, destination, out path);
    }

    static void spawnball(Vector3 vec, string s) {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        go.name = s;
        go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        go.transform.localPosition = vec;
    }
}
