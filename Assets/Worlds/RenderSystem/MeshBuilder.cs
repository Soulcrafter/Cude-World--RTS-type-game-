using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ThreadManager {

    class MeshBuilder : ThreadedProcess {
        
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Color32> colors = new List<Color32>();
        List<Vector2> uvs = new List<Vector2>();

        Chunk[] ch = new Chunk[6];
        bool[] exists = new bool[6];

        Chunk chunk;
        Block[,,] blockData;

        public readonly float scale = 1f;

        public float adjScale;

        public MeshBuilder(Chunk chunk) {

            this.chunk = chunk;
            this.blockData = chunk.blockData;
            adjScale = scale * 0.5f;
        }

        public override void Start() {

            //This MUST be done in the main thread. Really bad things happen if it doesn't.
            base.Start();

        }

        protected override void ThreadFunction() {

            Vector3Int vec = chunk.position;

            exists[0] = chunk.GetNeighborChuck(vec.x, vec.y, vec.z + 1, out ch[0]);//north   // z
            exists[1] = chunk.GetNeighborChuck(vec.x + 1, vec.y, vec.z, out ch[1]);//east    // x
            exists[2] = chunk.GetNeighborChuck(vec.x, vec.y, vec.z - 1, out ch[2]);//south   // -z
            exists[3] = chunk.GetNeighborChuck(vec.x - 1, vec.y, vec.z, out ch[3]);//west    // -x

            exists[4] = chunk.GetNeighborChuck(vec.x, vec.y + 1, vec.z, out ch[4]);//Top     // y
            exists[5] = chunk.GetNeighborChuck(vec.x, vec.y - 1, vec.z, out ch[5]);//Bottom  // -y

            //for (int i = 0; i < exists.Length; i++) {
            //    if (exists[i]) {
            //        Debug.Log(chunk.position.ToString() + " nch " + ch[i].position.ToString());
            //    }
            //}

            vec = new Vector3Int(chunk.position.x * Chunk.ChuchMaxWidth, chunk.position.y * Chunk.ChuchMaxHight, chunk.position.z * Chunk.ChuchMaxWidth);
            
            for (int z = 0; z < Chunk.ChuchMaxWidth; z++) {
                for (int y = 0; y < Chunk.ChuchMaxHight; y++) {
                    for (int x = 0; x < Chunk.ChuchMaxWidth; x++) {

                        Block block = blockData[x, y, z];

                        if (block.IsOpaque()) {
                            continue;
                        }

                        for (int g = 0; g < 6; g++) {

                            Direction dir = (Direction) g;
                            
                            Block conblock = chunk.GetNeighborByBlock(x, y, z, dir);

                            if (exists[g]) {

                                if (x == 0 || y == 0 || z == 0 || x == Chunk.ChuchMaxWidth - 1 || y == Chunk.ChuchMaxHight - 1 || z == Chunk.ChuchMaxWidth - 1) {

                                    Vector3Int v = Vector3Int.GetNeighborByVector3Int(x + vec.x, y + vec.y, z + vec.z, dir);
                                    Vector3Int vs = new Vector3Int(v.x - (ch[g].position.x * Chunk.ChuchMaxWidth), v.y - (ch[g].position.y * Chunk.ChuchMaxHight), v.z - (ch[g].position.z * Chunk.ChuchMaxWidth));
                                    Block b;

                                    if (z == Chunk.ChuchMaxWidth - 1 && g == (int) Direction.North && ch[g].GetBlockAt(vs.x, vs.y, vs.z, out b)) {
                                        
                                        if (!b.IsOpaque()) {
                                            continue;
                                        }
                                    }

                                    if (z == 0 && (g == (int) Direction.South && ch[g].GetBlockAt(vs.x, vs.y, vs.z, out b))) {
                                        if (!b.IsOpaque()) {
                                            continue;
                                        }
                                    }

                                    if (x == 0 && (g == (int) Direction.West && ch[g].GetBlockAt(vs.x, vs.y, vs.z, out b))) {
                                        if (!b.IsOpaque()) {
                                            continue;
                                        }
                                    }

                                    if (x == Chunk.ChuchMaxWidth - 1 && (g == (int) Direction.East && ch[g].GetBlockAt(vs.x, vs.y, vs.z, out b))) {
                                        if (!b.IsOpaque()) {
                                            continue;
                                        }
                                    }

                                    if (y == 0 && (g == (int) Direction.Down && ch[g].GetBlockAt(vs.x, vs.y, vs.z, out b))) {
                                        if (!b.IsOpaque()) {
                                            continue;
                                        }
                                    }

                                    if (y == Chunk.ChuchMaxHight - 1 && (g == (int) Direction.Up && ch[g].GetBlockAt(vs.x, vs.y, vs.z, out b))) {
                                        if (!b.IsOpaque()) {
                                            continue;
                                        }
                                    }

                                }

                            }

                            //Debug.Log(block.IsHaftBlock());

                            if (!block.IsCubeShape() && conblock.IsCubeShape()) {

                                if (!conblock.IfLookDirection(dir)) {
                                    continue;
                                }
                            }

                            if (block.IsCubeShape() && (!conblock.IsCubeShape() && !conblock.IsOpaque())) {

                                if (block.IfLookDirection(dir)) {
                                    continue;
                                }
                            }
                            
                            if (block.IsCubeShape() && conblock.IsCubeShape()) {
                                Direction[] dirlist = block.Directionslist();
                                Direction[] condirlist = conblock.Directionslist();

                                if (conblock.GetRotationType() != block.GetRotationType() && !conblock.IfLookDirection(dir) && !(dir == dirlist[1] || dir == DirectionExtensions.GetOppositeDir(dirlist[1]))) {
                                    continue;
                                }

                                if (conblock.GetRotationType() == block.GetRotationType() && !block.IfLookDirection(dir) && dir == DirectionExtensions.GetOppositeDir(dirlist[1])) {
                                    continue;
                                } else if (conblock.GetRotationType() == block.GetRotationType() && block.IfLookDirection(dir) && dir == dirlist[1]) {
                                    continue;
                                } else if (conblock.GetRotationType() == block.GetRotationType() && block.IfLookDirection(dir) && !(dir == dirlist[1] || dir == DirectionExtensions.GetOppositeDir(dirlist[1]))) {
                                    continue;
                                }

                                if (DirectionExtensions.GetOppositeDir(dirlist[1]) == condirlist[1] && dir == DirectionExtensions.GetOppositeDir(dirlist[1])) {
                                    continue;
                                }
                            }
                            
                            block.GetShape().GenMesh(vertices, triangles, uvs, colors, adjScale, scale, dir, conblock, block, x + vec.x, y + vec.y, z + vec.z);
                                
                        }

                    }

                }

            }

        }
        
        protected override void OnFinished() {

            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.colors32 = colors.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.RecalculateNormals();
            
            chunk.OnMeshDataFinished(mesh, mesh);
        }
    }

    static void spawnball(Vector3Int vec, string s) {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        go.name = s;
        go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        go.transform.localPosition = new Vector3(vec.x + .5f, vec.y + .5f, vec.z + .5f);
    }

}
