using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class CombineMesh : MonoBehaviour
{
    public GameObject[] go;
    public Vector3 offset = Vector3.zero;

    private void Start()
    {
        if (go == null || go.Length == 0)
        {
            Debug.LogWarning("CombineMesh: GameObject array 'go' is null or empty.");
            return;
        }

        List<CombineInstance> combineList = new List<CombineInstance>();

        for (int i = 0; i < go.Length; i++)
        {
            if (go[i] == null)
            {
                Debug.LogWarning($"CombineMesh: GameObject at index {i} is null. Skipping.");
                continue;
            }

            MeshFilter mf = go[i].GetComponent<MeshFilter>();
            if (mf == null || mf.sharedMesh == null)
            {
                Debug.LogWarning($"CombineMesh: MeshFilter or sharedMesh missing on GameObject '{go[i].name}'. Skipping.");
                continue;
            }

            Matrix4x4 worldToLocal = transform.worldToLocalMatrix;
            Matrix4x4 meshMatrix = mf.transform.localToWorldMatrix;
            Matrix4x4 finalMatrix = worldToLocal * meshMatrix * Matrix4x4.Translate(offset);

            CombineInstance ci = new CombineInstance
            {
                mesh = mf.sharedMesh,
                transform = finalMatrix
            };

            combineList.Add(ci);
        }

        if (combineList.Count == 0)
        {
            Debug.LogWarning("CombineMesh: No valid meshes found to combine.");
            return;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.name = "CombinedMesh";
        combinedMesh.CombineMeshes(combineList.ToArray(), true, true);

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = combinedMesh;

        MeshCollider meshCollider = GetComponent<MeshCollider>() ?? gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = combinedMesh;
        meshCollider.convex = false;

#if UNITY_EDITOR
        string meshInfo = combinedMesh.vertexCount.ToString() + combinedMesh.bounds.ToString() + offset.ToString();
        string hash = GetMD5Hash(meshInfo);
        string path = $"Assets/A.Work/Mesh/CombinedMesh_{hash}.asset";

        Mesh existingMesh = AssetDatabase.LoadAssetAtPath<Mesh>(path);

        if (existingMesh == null)
        {
            AssetDatabase.CreateAsset(combinedMesh, path);
            AssetDatabase.SaveAssets();
            Debug.Log("New mesh saved to: " + path);
        }
        else
        {
            meshFilter.sharedMesh = existingMesh;
            meshCollider.sharedMesh = existingMesh;
            Debug.Log("Using existing mesh asset at: " + path);
        }
#endif
    }

#if UNITY_EDITOR
    private string GetMD5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
#endif
}
