using System;
using UnityEngine;

namespace ProjectGame
{
    public class GraphicsObject: MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        private Color _defaultObjectColor;
        
        private bool _lighted;

        private void Start()
        {
            _defaultObjectColor = meshRenderer.material.color;
        }

        public void LightObject()
        {
            if (_lighted) return;
            
            meshRenderer.material.color = Color.green;
            _lighted = true;
        }

        public void DelightObject()
        {
            if (!_lighted) return;
            
            meshRenderer.material.color = _defaultObjectColor;
            _lighted = false;
        }
    }
}
