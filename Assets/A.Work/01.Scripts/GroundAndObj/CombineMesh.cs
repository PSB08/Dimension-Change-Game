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
    public Vector3 offset = Vector3.zero; // 내가 원하는 위치로 mesh만 이동

    private void Start()
    {
        MeshFilter[] meshFilters = new MeshFilter[go.Length];
        CombineInstance[] combine = new CombineInstance[go.Length];

        for (int i = 0; i < go.Length; i++)
        {
            meshFilters[i] = go[i].GetComponent<MeshFilter>();

            Matrix4x4 worldToLocal = transform.worldToLocalMatrix;
            Matrix4x4 meshMatrix = meshFilters[i].transform.localToWorldMatrix;

            // Mesh를 월드 기준으로 가져오고 offset만큼 이동한 후 내 로컬 공간으로 맞춰줌
            Matrix4x4 finalMatrix = worldToLocal * meshMatrix * Matrix4x4.Translate(offset);

            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = finalMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.name = "CombinedMesh";
        combinedMesh.CombineMeshes(combine, true, true);

        // MeshFilter에 넣기
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = combinedMesh;

        // MeshCollider에도 넣기
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }
        meshCollider.sharedMesh = combinedMesh;
        meshCollider.convex = false;

#if UNITY_EDITOR
        {
            string path = "Assets/MyMesh.asset";
            string uniquePath = AssetDatabase.GenerateUniqueAssetPath(path);

            Mesh existingMesh = AssetDatabase.LoadAssetAtPath<Mesh>(path);
            if (existingMesh == null)
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
