using Unity.AI.Navigation;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class NavMeshBuilderMenu : MonoBehaviour
{
    [MenuItem("Navigation/Rebuild NavMesh")]
    private static void RebuildNavMesh()
    {
        var surfaces = FindObjectsOfType<NavMeshSurface>();
        foreach (var surface in surfaces)
        {
            surface.BuildNavMesh();
        }

        Debug.Log("NavMesh rebuilt for all surfaces.");
    }
}
