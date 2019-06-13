using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FilePathLibrary {
    
    public static string shapeDataPath = Application.streamingAssetsPath + "/Shapes/";
    public static string structureDataPath = Application.streamingAssetsPath + "/Structures/";
    public static string shaderDataPath = Application.streamingAssetsPath + "/Shaders/";
    public static string textureDataPath = Application.streamingAssetsPath + "/Textures/";

    public static List<string[]> GetAllFileDataInDirectory (string path) {

        List<string[]> files = new List<string[]>();
        SearchDirectory(path, files);
        return files;
    }

    static void SearchDirectory (string path, List<string[]> fileData) {

        string[] subDirs = Directory.GetDirectories(path);
        for (int i = 0; i < subDirs.Length; i++) {

            SearchDirectory(subDirs[i], fileData);
        }

        string[] files = Directory.GetFiles(path);
        for (int i = 0; i < files.Length; i++) {

            if (Path.GetExtension(files[i]) == "") {
                fileData.Add(File.ReadAllLines( files[i] ));
            }
        }
    }
    
}
