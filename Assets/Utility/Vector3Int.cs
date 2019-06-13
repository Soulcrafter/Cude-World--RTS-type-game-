using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Vector3Int {

    public int x, y, z;

    public Vector3Int(int x, int y, int z) {

        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override string ToString() {
        return "(" + x + ", " + y + ", " + z + ")";
    }

    public bool Equals(Vector3Int Vec) {

        if (this.x == Vec.x) {
            if (this.y == Vec.y) {
                if (this.z == Vec.z) {
                    return true;
                }
            }

        }
        return false;
    }

    public Vector3 IntToFloatVec() {
        return new Vector3(this.x, this.y, this.z);
    }

    public static Vector3Int FloatToIntVec(Vector3 Vec) {
        return new Vector3Int(Mathf.FloorToInt(Vec.x), Mathf.FloorToInt(Vec.y), Mathf.FloorToInt(Vec.z));
    }

    public static Vector3Int GetNeighborByVector3Int(int x, int y, int z, Direction dir) {

        DataCoordinata[] offsets = {
            new DataCoordinata ( 0 ,0, 1),//north
            new DataCoordinata ( 1, 0, 0),//east
            new DataCoordinata ( 0, 0,-1),//south
            new DataCoordinata (-1, 0, 0),//west
            new DataCoordinata ( 0, 1, 0),//up
            new DataCoordinata ( 0,-1, 0)//down
        };

        DataCoordinata offsetToCheck = offsets[(int) dir];

        x += offsetToCheck.x;
        y += offsetToCheck.y;
        z += offsetToCheck.z;

        return new Vector3Int(x, y, z);
    }

    public static string DecimalToBinary(string data) {
        string result = string.Empty;
        int rem = 0;
        try {
            int num = int.Parse(data);
            while (num > 0) {
                rem = num % 2;
                num = num / 2;
                result = rem.ToString() + result;
            }

        } catch (Exception e) {
            Debug.Log(e.Message);
        }
        return result;
    }
}
