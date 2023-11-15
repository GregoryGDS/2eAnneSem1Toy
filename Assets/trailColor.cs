using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailColor : MonoBehaviour
{
    public Color _color = Color.red; // Couleur du trail
    public TrailRenderer trailRenderer;
    void Start()
    {
        trailRenderer.material.color = _color;
    }
}
