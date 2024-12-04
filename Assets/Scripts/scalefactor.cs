using UnityEngine;

public class ResizeMesh : MonoBehaviour
{
    public float scaleFactor = 1.0f; // Scale factor for resizing the mesh

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.mesh != null)
        {
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;

            // Scale each vertex
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] *= scaleFactor;
            }

            // Apply the modified vertices back to the mesh
            mesh.vertices = vertices;
            mesh.RecalculateBounds(); // Ensure bounds are recalculated
        }
        else
        {
            Debug.LogError("No MeshFilter or Mesh found on this GameObject.");
        }
    }
}