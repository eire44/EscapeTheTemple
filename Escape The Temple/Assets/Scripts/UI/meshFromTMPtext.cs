using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class meshFromTMPtext : MonoBehaviour
{
    public MeshFilter meshFilter;

    void Start()
    {
        TextMeshPro tmp = GetComponent<TextMeshPro>();

        tmp.ForceMeshUpdate();

        Mesh mesh = tmp.mesh;

        meshFilter.mesh = Instantiate(mesh);
    }
}
