using System.Linq;
using UnityEngine;

namespace Racer_s_cheats
{
    public class GlassWithoutFrost : MonoBehaviour
    {
        void Start()
        {
            Racer_s_tweaks_cheats.AllObjects
                .Where(v =>
                {
                    if (v == null || !v.GetComponent<MeshRenderer>())
                        return false;

                    var name = v.name.ToLower();
                    return name.Contains("frost") || name.Contains("frozen");
                })
                .ToList()
                .ForEach(v => v.SetActive(false));
        }
    }
}