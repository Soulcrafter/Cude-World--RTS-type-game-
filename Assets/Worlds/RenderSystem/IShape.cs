using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShape {
    
    void GenMesh(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs, List<Color32> colors, float adjScale, float scale, Direction dir, Block conblock, Block block, int x, int y, int z);

}
