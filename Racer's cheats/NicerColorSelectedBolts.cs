using UnityEngine;

namespace Racer_s_cheats
{   
    public class NicerColorSelectedBolts : MonoBehaviour
    {
        void Start()
        {
            Racer_s_tweaks_cheats.BoltActiveMat.color = Racer_s_tweaks_cheats.ColorSelectPart;
        }
    }
}