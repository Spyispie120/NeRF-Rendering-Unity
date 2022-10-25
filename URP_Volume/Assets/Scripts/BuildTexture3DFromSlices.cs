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

    // Start is called before the first frame update
    void Start()
    {
        slicesPath = $"{Application.streamingAssetsPath}/{folderName}";
        ReadFile();
    }

    private void ReadFile()
    {
        Color[] texture3dColor = new Color[(int)(textureVolume.x * textureVolume.y * textureVolume.z)];
        int sliceZ = 0;
        foreach(string filePath in Directory.EnumerateFiles(slicesPath))
        {
            string extension = Path.GetExtension(filePath).ToLower().Trim();
            //Debug.Log($"File ext: {extension}");
            if (!extension.Equals(".png") && !extension.Equals(".jpg")) continue;
            Debug.Log($"File {filePath}");

            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D slice = new Texture2D(2, 2);
            slice.LoadImage(bytes);
            int z = 0;
            for(int x = 0; x < textureVolume.x; x++)
            {
                for(int y = 0; y < textureVolume.y; y++)
                {
                    Color color = slice.GetPixel(x, y);
                    
                    texture3dColor[sliceZ * (int)textureVolume.x * (int)textureVolume.y + z] = color;
                    z++;
                }
            }
            sliceZ++;
        }

        texture3d = new Texture3D((int)textureVolume.x, (int)textureVolume.y, (int)textureVolume.z, TextureFormat.RGBA32, false);
        texture3d.SetPixels(texture3dColor);
        texture3d.Apply();

        AssetDatabase.CreateAsset(texture3d, $"Assets/Volume/3DTexture_{outputName}.asset");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
