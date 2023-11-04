using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonLight : MonoBehaviour
{
    [SerializeField] Light _light;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] Material onMaterial;
    [SerializeField] Material offMaterial;

    public void Set(bool isOn)
    {
        _light.enabled = isOn;
        if (isOn )
        {
            _meshRenderer.material = onMaterial;
        }
        else
        {
            _meshRenderer.material = offMaterial;
        }
    }
}
