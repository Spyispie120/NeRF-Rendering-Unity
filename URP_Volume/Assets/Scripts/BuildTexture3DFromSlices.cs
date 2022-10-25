using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class BuildTexture3DFromSlices : MonoBehaviour
{
    [SerializeField] private Vector3 textureVolume;
    [SerializeField] private string folderName;
    [SerializeField] private string outputName;
    private string slicesPath= $"{Application.streamingAssetsPath}/default";
    private Texture3D texture3d;

    private void ReadFile()
    {
        slicesPath = $"{Application.streamingAssetsPath}/{folderName}";
        Color[] texture3dColor = new Color[(int)(textureVolume.x * textureVolume.y * textureVolume.z)];
        //int sliceZ = 0;
        //foreach(string filePath in Directory.EnumerateFiles(slicesPath))
        //{
        //    string extension = Path.GetExtension(filePath).ToLower().Trim();
        //    //Debug.Log($"File ext: {extension}");
        //    if (!extension.Equals(".png") && !extension.Equals(".jpg")) continue;
        //    Debug.Log($"File {filePath}");

        //    byte[] bytes = File.ReadAllBytes(filePath);
        //    Texture2D slice = new Texture2D(2, 2);
        //    slice.LoadImage(bytes);
        //    int z = 0;
        //    int zOffset = z * (int)textureVolume.z;
        //    for(int x = 0; x < textureVolume.x; x++)
        //    {
        //        int yOffset = x * (int)textureVolume.x;
        //        for(int y = 0; y < textureVolume.y; y++)
        //        {
        //            Color32 color = slice.GetPixel(x, y);
        //            texture3dColor[sliceZ * (int)textureVolume.x * (int)textureVolume.y + z] = color;
        //            z++;
        //        }
        //    }
        //    sliceZ++;
        //}
        Debug.Log($"color size : {texture3dColor.Length}");
        int z = 0;
        foreach (string filePath in Directory.EnumerateFiles(slicesPath))
        {
            string extension = Path.GetExtension(filePath).ToLower().Trim();
            //Debug.Log($"File ext: {extension}");
            if (!extension.Equals(".png") && !extension.Equals(".jpg")) continue;
            Debug.Log($"File {filePath}");

            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D slice = new Texture2D(2, 2);
            slice.LoadImage(bytes);
            int zOffset = z * (int)textureVolume.x * (int)textureVolume.y;
            for (int y = 0; y < textureVolume.y; y++)
            {
                int yOffset = y * (int)textureVolume.x;
                for (int x = 0; x < textureVolume.x; x++)
                { 
                    Color color = slice.GetPixel(x, y);
                    texture3dColor[x + yOffset + zOffset] = color;
                }
            }
            z++;
            
        }

        texture3d = new Texture3D((int)textureVolume.x, (int)textureVolume.y, (int)textureVolume.z, TextureFormat.RGBA32, false);
        texture3d.SetPixels(texture3dColor);
        texture3d.Apply();
    }

    public void BuildTexture3D()
    {
        ReadFile();
        AssetDatabase.CreateAsset(texture3d, $"Assets/Volume/3DTexture_{outputName}.asset");
    }
}
