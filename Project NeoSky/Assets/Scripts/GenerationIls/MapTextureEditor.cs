using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (TextureApply))]
public class MapTextureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TextureApply textureApply = (TextureApply)target;
        if (DrawDefaultInspector())
        {
            textureApply.Show();
        }
        
        if (GUILayout.Button("Generate"))
        {
            textureApply.islandConstructor.ConstrucIsland();
        }
    }
}
