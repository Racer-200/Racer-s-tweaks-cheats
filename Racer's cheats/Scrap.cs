using HutongGames.PlayMaker.Actions;
using MSCLoader;
using UnityEngine;

namespace Racer_s_cheats
{
    public class Scrap : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;

        private Material _oldMat;

        void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _oldMat = _meshRenderer.material;
        }

        public void LightUp()
        {
            _meshRenderer.material = Racer_s_tweaks_cheats.SelectedPartMat;
        }

        public void OldColor()
        {
            _meshRenderer.material = _oldMat;
        }
    }
}