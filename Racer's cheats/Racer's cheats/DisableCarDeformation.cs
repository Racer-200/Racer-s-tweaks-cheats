using System.Linq;
using UnityEngine;

namespace Racer_s_cheats
{
    public class DisableCarDeformation : MonoBehaviour
    {
        void Start()
        {
            GameObject.Find("CORRIS/DeformLogic")
                .SetActive(false);

            Racer_s_tweaks_cheats.AllDeformable
                .ToList()
                .ForEach(v => v.enabled = false);
        }
    }
}