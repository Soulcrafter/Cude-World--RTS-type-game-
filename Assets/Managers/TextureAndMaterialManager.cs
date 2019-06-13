using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAndMaterialManager : Manager {

    public static Material[] materials;
    public static Dictionary<string, Vector2[]> textureUVMap;

    Texture2D[] atlases; // albedo, normal, cutout, metallic

	// Use this for initialization
	public override void Start () {

        LoadTextures();
        LoadShader();
	}
	
    void LoadShader () {

        Material[] chunkRenderingMaterials = Resources.LoadAll<Material>("Materials");

        if (chunkRenderingMaterials.Length > 0) {

            Debug.Log(chunkRenderingMaterials[0].name);

            chunkRenderingMaterials[0].SetTexture("_Albedo", atlases[0]);

            materials = chunkRenderingMaterials;
        }
    }

    void LoadTextures () {

        textureUVMap = new Dictionary<string, Vector2[]>(); 
        atlases = new Texture2D[1];

        Texture2D[] albedoTextures = Resources.LoadAll<Texture2D>("Textures/Albedo");
        
        atlases[0] = new Texture2D(64, 64, TextureFormat.RGBA32, false);

        //Step 1: Iterate through all textures
        Vector2Int tileOffset = new Vector2Int(0, 0);

        for (int i = 0; i < albedoTextures.Length; i++) {

            Color[] pixels = albedoTextures[i].GetPixels();
            atlases[0].SetPixels(tileOffset.x, tileOffset.y, 32, 32, pixels, 0);

            textureUVMap.Add(
                albedoTextures[i].name,
                new Vector2[] {
                    new Vector2(tileOffset.x / 64f, tileOffset.y / 64f),
                    new Vector2((tileOffset.x + 32f) / 64f, tileOffset.y / 64f),
                    new Vector2(tileOffset.x / 64f, (tileOffset.y + 32f) / 64f),
                    new Vector2((tileOffset.x + 32f) / 64f, (tileOffset.y + 32f) / 64f)
                }
            );

            tileOffset.x += 32;

            if (tileOffset.x >= atlases[0].width) {
                tileOffset.x = 0;
                tileOffset.y += 32;
            }
        }

        atlases[0].filterMode = FilterMode.Point;
        atlases[0].Apply();
    }
}
