using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMesh : IShape {

    private static Vector3[] vertices = {
        new Vector3( 1, 1, 1), //0
        new Vector3(-1, 1, 1), //1
        new Vector3(-1,-1, 1), //2
        new Vector3( 1,-1, 1), //3
        new Vector3(-1, 1,-1), //4
        new Vector3( 1, 1,-1), //5
        new Vector3( 1,-1,-1), //6
        new Vector3(-1,-1,-1)  //7
    };

    private static int[][] faceTriangles = {
        new int[]{0, 1, 2, 3}, //north
        new int[]{5, 0, 3, 6}, //east
        new int[]{4, 5, 6, 7}, //south
        new int[]{1, 4, 7, 2}, //west
        new int[]{5, 4, 1, 0}, //up
        new int[]{3, 2, 7, 6}  //down
    };

    public static Vector3[] faceVertices(int dir, float scale, Vector3 pos) {
        Vector3[] fv = new Vector3[4];
        for (int i = 0; i < fv.Length; i++) {
            fv[i] = (vertices[faceTriangles[dir][i]] * scale) + pos;
        }
        return fv;
    }

    private static Vector3[] faceVertices(Direction dir, float scale, Vector3 pos) {
        return faceVertices((int) dir, scale, pos);
    }

    public void GenMesh(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs, List<Color32> colors, float adjScale, float scale, Direction dir, Block conblock, Block block, int x, int y, int z) {

        if (!conblock.IsOpaque() && !conblock.IsCubeShape()) {
            return;
        }

        vertices.AddRange(faceVertices(dir, adjScale, new Vector3((float) x * scale + .5f, (float) y * scale + .5f, (float) z * scale + .5f)));

        int vCount = vertices.Count;

        //uvs.AddRange(TextureController.AddTextures(data.GetBlock(x, y, z), dir));

        colors.Add(block.Color);
        colors.Add(block.Color);
        colors.Add(block.Color);
        colors.Add(block.Color);

        //set two triangle for quad
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 1);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4 + 3);
    }
}
