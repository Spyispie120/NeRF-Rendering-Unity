using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BuildTexture3DFromSlices))]
public class BuildTexture3DFromSlicesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BuildTexture3DFromSlices buildTexture3D = (BuildTexture3DFromSlices)target;

        if(GUILayout.Button("Build Texture3D"))
        {
            
            buildTexture3D.BuildTexture3D();
        }
    }
}
