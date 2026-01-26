using HutongGames.PlayMaker;
using MSCLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Racer_s_cheats
{
    internal class Load
    {
        public class Variable
        {
            public static  float ScrapPrice()
            {
                return GameObject.Find("Systems/ScrapPrice")
                    .GetComponent<PlayMakerFSM>()
                    .GetVariable<FsmFloat>("ScrapPriceMKkg")
                    .Value;
            }

            public static T[] GetAllObjects<T>() where T : UnityEngine.Object
            {
                return Resources.FindObjectsOfTypeAll<T>();
            }
        }

        public class Component
        {
            public class Cheat
            {
               public static void Add_ScrapSeller()
                {
                    Racer_s_tweaks_cheats.CheatsMainObject
                        .AddComponent<ScrapSeller>();
                }
            }

            public class Tweak
            {
                public static void Add_GlassWithoutFrost()
                {
                    Racer_s_tweaks_cheats.TweaksMainObject
                        .AddComponent<GlassWithoutFrost>();
                }

                public static void Add_NicerColorSelectedBolts()
                {
                    Racer_s_tweaks_cheats.TweaksMainObject
                        .AddComponent<NicerColorSelectedBolts>();
                }

                public static void Add_DisableCarDeformation()
                {
                    Racer_s_tweaks_cheats.TweaksMainObject
                        .AddComponent<DisableCarDeformation>();
                }
            }
        }
    }
}
