using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlantMesh : IShape {

   private static Vector3Int loc = new Vector3Int( 0,-1, 0);
   private static int check = 0;
    
   public void GenMesh(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs, List<Color32> colors, float adjScale, float scale, Direction dir, Block conblock, Block block, int x, int y, int z) {

        if (!loc.Equals(new Vector3Int(x, y, z))) {
            loc = new Vector3Int(x, y, z);
            check = 0;
        }
        
        //Block block = blockData[x, y, z];
        Direction[] LookDirection = block.Directionslist();

        Vector3 pos = new Vector3((float) x * scale + .5f, (float) y * scale + .5f, (float) z * scale + .5f);

        if (block.GetRotationType() == 0) {

            if (dir == Direction.North) { // z

                Vector3[] vec = new Vector3[3];

                vec[0] = (new Vector3(1, -1, 1) * adjScale) + pos;
                vec[1] = (new Vector3(-1, 1, 1) * adjScale) + pos;
                vec[2] = (new Vector3(-1,-1, 1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir, 3));

                triangles.Add(vCount - 3);
                triangles.Add(vCount - 3 + 1);
                triangles.Add(vCount - 3 + 2);

                if (!(check == 2)) {
                    check = 1;
                }
            } else if (dir == Direction.South) { // -z

                Vector3[] vec = new Vector3[3];

                vec[0] = (new Vector3(-1,-1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(-1, 1, -1) * adjScale) + pos;
                vec[2] = (new Vector3( 1,-1, -1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir, 3));

                triangles.Add(vCount - 3);
                triangles.Add(vCount - 3 + 1);
                triangles.Add(vCount - 3 + 2);

                if (!(check == 2)) {
                    check = 1;
                }

            } else if (dir == Direction.West) { // -x

                //Debug.Log("yes");

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(-1, -1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(-1, -1, 1) * adjScale) + pos;
                vec[2] = (new Vector3(-1, 1, 1) * adjScale) + pos;
                vec[3] = (new Vector3(-1, 1, -1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);

            } else if (dir == Direction.Down) { // -y

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(-1, -1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(1, -1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(1, -1, 1) * adjScale) + pos;
                vec[3] = (new Vector3(-1, -1, 1) * adjScale) + pos;

               //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);

            }else if (dir == Direction.East) { // x // haft

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(1, -1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(-1, 1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(-1, 1, 1) * adjScale) + pos;
                vec[3] = (new Vector3(1, -1, 1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);
            }
            return;

        } else if (block.GetRotationType() == 1) {

            if (dir == Direction.North) { // z

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(-1, -1, 1) * adjScale) + pos;
                vec[1] = (new Vector3(1, -1, 1) * adjScale) + pos;
                vec[2] = (new Vector3(1, 1, 1) * adjScale) + pos;
                vec[3] = (new Vector3(-1, 1, 1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);

                return;

            } else if (dir == Direction.South) { // -z

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(-1, 1, 1) * adjScale) + pos;
                vec[1] = (new Vector3(1, 1, 1) * adjScale) + pos;
                vec[2] = (new Vector3(1, -1, -1) * adjScale) + pos;
                vec[3] = (new Vector3(-1, -1, -1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);

                return;

            } else if (dir == Direction.East) { // x

                Vector3[] vec = new Vector3[3];

                vec[0] = (new Vector3(1, 1, 1) * adjScale) + pos;
                vec[1] = (new Vector3(1, -1, 1) * adjScale) + pos;
                vec[2] = (new Vector3(1, -1, -1) * adjScale) + pos;
                //vec[3] = (new Vector3(0, 1, -1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir, 3));

                triangles.Add(vCount - 3);
                triangles.Add(vCount - 3 + 1);
                triangles.Add(vCount - 3 + 2);

                return;

            } else if (dir == Direction.West) { // -x

                //Debug.Log("yes");

                Vector3[] vec = new Vector3[3];

                vec[0] = (new Vector3(-1, -1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(-1, -1, 1) * adjScale) + pos;
                vec[2] = (new Vector3(-1, 1, 1) * adjScale) + pos;
                //vec[3] = (new Vector3(-1, 1, -1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir, 3));

                triangles.Add(vCount - 3);
                triangles.Add(vCount - 3 + 1);
                triangles.Add(vCount - 3 + 2);

                return;

            } else if (dir == Direction.Down) { // -y

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(-1, -1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(1, -1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(1, -1, 1) * adjScale) + pos;
                vec[3] = (new Vector3(-1, -1, 1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);

                return;
            }
        } else if (block.GetRotationType() == 2) {

            if (dir == Direction.North) { // z
                Vector3[] vec = new Vector3[3];

                vec[0] = (new Vector3(1, 1, 1) * adjScale) + pos;
                vec[1] = (new Vector3(-1, -1, 1) * adjScale) + pos;
                vec[2] = (new Vector3(1, -1, 1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir, 3));

                triangles.Add(vCount - 3);
                triangles.Add(vCount - 3 + 1);
                triangles.Add(vCount - 3 + 2);

                return;

            } else if (dir == Direction.South) { // -z

                Vector3[] vec = new Vector3[3];

                vec[0] = (new Vector3(1, -1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(-1, -1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(1, 1, -1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir, 3));

                triangles.Add(vCount - 3);
                triangles.Add(vCount - 3 + 1);
                triangles.Add(vCount - 3 + 2);

                return;

            } else if (dir == Direction.East) { // x

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(1, -1, 1) * adjScale) + pos;
                vec[1] = (new Vector3(1, -1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(1, 1, -1) * adjScale) + pos;
                vec[3] = (new Vector3(1, 1, 1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);

                return;

            } else if (dir == Direction.West) { // -x

                //Debug.Log("yes");

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(1, 1, 1) * adjScale) + pos;
                vec[1] = (new Vector3(1, 1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(-1, -1, -1) * adjScale) + pos;
                vec[3] = (new Vector3(-1, -1, 1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);

                return;

            } else if (dir == Direction.Down) { // -y

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(-1, -1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(1, -1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(1, -1, 1) * adjScale) + pos;
                vec[3] = (new Vector3(-1, -1, 1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);

                return;
            }

        } else if (block.GetRotationType() == 3) {

            if (dir == Direction.North) { // z

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(1, 1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(-1, 1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(-1, -1, 1) * adjScale) + pos;
                vec[3] = (new Vector3(1, -1, 1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);

                return;

            } else if (dir == Direction.South) { // -z

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(-1, 1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(1, 1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(1, -1, -1) * adjScale) + pos;
                vec[3] = (new Vector3(-1, -1, -1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);

                return;

            } else if (dir == Direction.East) { // x

                Vector3[] vec = new Vector3[3];

                vec[0] = (new Vector3(1, -1, 1) * adjScale) + pos;
                vec[1] = (new Vector3(1, -1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(1, 1, -1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir, 3));

                triangles.Add(vCount - 3);
                triangles.Add(vCount - 3 + 1);
                triangles.Add(vCount - 3 + 2);

                return;

            } else if (dir == Direction.West) { // -x

                //Debug.Log("yes");

                Vector3[] vec = new Vector3[3];

                vec[0] = (new Vector3(-1, 1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(-1, -1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(-1, -1, 1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir, 3));

                triangles.Add(vCount - 3);
                triangles.Add(vCount - 3 + 1);
                triangles.Add(vCount - 3 + 2);

                return;

            } else if (dir == Direction.Down) { // -y

                Vector3[] vec = new Vector3[4];

                vec[0] = (new Vector3(-1, -1, -1) * adjScale) + pos;
                vec[1] = (new Vector3(1, -1, -1) * adjScale) + pos;
                vec[2] = (new Vector3(1, -1, 1) * adjScale) + pos;
                vec[3] = (new Vector3(0, -1, 1) * adjScale) + pos;

                //vec = rotation(vec, block.rot, pos);

                vertices.AddRange(vec);

                int vCount = vertices.Count;

                //uvs.AddRange(TextureController.AddTextures(block, dir));

                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 1);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4);
                triangles.Add(vCount - 4 + 2);
                triangles.Add(vCount - 4 + 3);

                return;
            }
        }

    }

    //public static Vector3[] rotation(Vector3[] vertices, Rotation rot, Vector3 pos) {

    //    Quaternion q = BlockEx.GetQuaternion(rot);

    //    Vector3 pivot = new Vector3(0.5f + pos.x, 0.5f + pos.y, 0.5f + pos.z);

    //    Vector3[] newVertices = new Vector3[vertices.Length];

    //    for (int i = 0; i < vertices.Length; i++) {

    //        Vector3 vertex = vertices[i];
    //        newVertices[i] = (q * (vertex - pivot)) + pivot;
    //    }

    //    return newVertices;
    //}

}
