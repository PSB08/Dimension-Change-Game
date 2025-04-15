using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class CombineMesh : MonoBehaviour
{
    public GameObject[] go;
    public Vector3 offset = Vector3.zero;

    private void Start()
    {
        MeshFilter[] meshFilters = new MeshFilter[go.Length];
        CombineInstance[] combine = new CombineInstance[go.Length];

        for (int i = 0; i < go.Length; i++)
        {
            meshFilters[i] = go[i].GetComponent<MeshFilter>();

            Matrix4x4 worldToLocal = transform.worldToLocalMatrix;
            Matrix4x4 meshMatrix = meshFilters[i].transform.localToWorldMatrix;
            
            Matrix4x4 finalMatrix = worldToLocal * meshMatrix * Matrix4x4.Translate(offset);

            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = finalMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.name = "CombinedMesh";
        combinedMesh.CombineMeshes(combine, true, true);

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = combinedMesh;

        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }
        meshCollider.sharedMesh = combinedMesh;
        meshCollider.convex = false;

#if UNITY_EDITOR
        {
            string path = "Assets/A.Work/Mesh/MyMesh.asset";
            string uniquePath = AssetDatabase.GenerateUniqueAssetPath(path);

            Mesh existingMesh = AssetDatabase.LoadAssetAtPath<Mesh>(path);

            bool isSameMesh = false;

            if (existingMesh != null)
            {
                // 간단 비교: vertex count와 bounds 비교
                isSameMesh = existingMesh.vertexCount == combinedMesh.vertexCount &&
                             existingMesh.bounds.size == combinedMesh.bounds.size;
            }

            if (!isSameMesh)
            {
                AssetDatabase.CreateAsset(combinedMesh, uniquePath);
                AssetDatabase.SaveAssets();
                Debug.Log("New mesh saved to: " + uniquePath);
            }
            else
            {
                meshFilter.sharedMesh = existingMesh;
                meshCollider.sharedMesh = existingMesh;
                Debug.Log("Using existing mesh asset at: " + path);
            }
        }
#endif
    }
    
    
    
    
}
