using HutongGames.PlayMaker;
using MSCLoader;
using System.Linq;
using UnityEngine;

namespace Racer_s_cheats
{
    public class MoreDurabilitySuspension : MonoBehaviour
    {
        void Start()
        {
            Racer_s_tweaks_cheats.Corris.transform
                .FindChild("Simulation/Systems/Suspension/Calculations").gameObject
                .GetComponents<PlayMakerFSM>()
                .Where(v =>
                {
                    var force = v.Fsm.GetFsmFloat("ForceBend");

                    if (force == null)
                        return false;

                    return true;
                })
                .ToList()
                .ForEach(v => v.Fsm.GetFsmFloat("ForceBend").Value = 95000);
        }
    }
}