using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    [SerializeField]
    public Material light_off;
    [SerializeField]
    public Material light_on;

    MeshRenderer mesh;

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void SetMaterial(int num)
    {
        if (num == 1)
            mesh.material = light_off;
        else
            mesh.material = light_on;
    }

}
