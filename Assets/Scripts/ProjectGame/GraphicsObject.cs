using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectGame
{
    public class GraphicsObject: MonoBehaviour
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        [FormerlySerializedAs("meshRenderer")] [SerializeField] private MeshRenderer _meshRenderer;
        
        private Color _defaultObjectColor;
        
        private bool _lighted = false;

        private void Awake()
        {
            _defaultObjectColor = _meshRenderer.material.color;
        }

        public void LightObject()
        {
            if (_lighted) return;
            
            _meshRenderer.material.SetColor(BaseColor, Color.green);
            _lighted = true;
        }

        public void DelightObject()
        {
            if (!_lighted) return;
            
            _meshRenderer.material.SetColor(BaseColor, _defaultObjectColor);
            _lighted = false;
        }
    }
}
