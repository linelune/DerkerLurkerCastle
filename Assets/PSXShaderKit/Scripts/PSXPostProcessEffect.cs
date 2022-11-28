using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSXShaderKit
{
    public class PSXPostProcessEffect : MonoBehaviour
    {
        public enum DitheringMatrixSize
        {
            Dither2x2,
            Dither4x4
        };

        public Vector3 colorDepth = new Vector3(255, 255, 255);
        public Vector3 ditherDepth = new Vector3(31, 31, 31);
        public DitheringMatrixSize ditheringMatrixSize = DitheringMatrixSize.Dither4x4;

        [SerializeField]
        private Shader _Shader;
        private Material _Material;

        void Start()
        {
            if (_Shader != null && _Shader.isSupported)
            {
                _Material = new Material(_Shader);
            }
            else
            {
                enabled = false;
                return;
            }
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            _Material.SetVector("_ColorResolution", colorDepth);
            _Material.SetVector("_DitherResolution", ditherDepth);
            _Material.SetFloat("_HighResDitherMatrix", ditheringMatrixSize == DitheringMatrixSize.Dither2x2 ? 0.0f : 1.0f);
            Graphics.Blit(source, destination, _Material);
        }
    }
}
