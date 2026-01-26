using HutongGames.PlayMaker;
using MSCLoader;
using System.Linq;
using UnityEngine;

namespace Racer_s_cheats
{
    public class Racer_s_tweaks_cheats : Mod
    {
        public override string ID => "Racer_s_cheats"; // Your (unique) mod ID 
        public override string Name => "Racer's tweaks & cheats"; // Your mod name
        public override string Author => "@racer"; // Name of the Author (your name)
        public override string Version => "0.2.1"; // Version
        public override string Description => "Cheats that make life easier in this damned game."; // Short description of your mod 
        public override Game SupportedGames => Game.MyWinterCar;

        public static GameObject[]
            AllObjects,
            AllPrefabs,
            AllParts;

        public static Material[] AllMat;

        public static AudioClip[] AllSounds;

        public static Deformable[] AllDeformable;

        public static GameObject 
            ModMainObject,
            CheatsMainObject,
            TweaksMainObject;

        public static Transform PlayerTrns;

        public static Material BoltActiveMat;

        public static Material SelectedPartMat;

        public static Color ColorSelectPart;

        public static SettingsKeybind Keybind_SelectParts, keybind_SellParts;

        public static SettingsCheckBox
            ScrapSellerCheat,
            WithoutFrostedGlass,
            NicerColorSelectedBolts,
            DisableDeformLogic;

        public static FsmFloat PlayerMoney;

        public override void ModSetup()
        {
            SetupFunction(Setup.OnMenuLoad, Mod_OnMenuLoad);
            SetupFunction(Setup.OnLoad, Mod_OnLoad);
            SetupFunction(Setup.Update, Mod_Update);
            SetupFunction(Setup.ModSettings, Mod_Settings);
            SetupFunction(Setup.PreLoad, Mod_PreLoad);
            SetupFunction(Setup.PostLoad, Mod_PostLoad);
        }

        private void Mod_Settings()
        {
            Settings.AddHeader("Cheats");

            ScrapSellerCheat = Settings
                .AddCheckBox("Cheat_ScrapSeller",
                    "Scrap Seller",
                    value: true
                );

            Settings.AddHeader("Tweaks");

            WithoutFrostedGlass = Settings
                .AddCheckBox("Tweak_WithoutFrozenOnGlass",
                    "Without Frosted Glass",
                    value: true
                );

            NicerColorSelectedBolts = Settings
                .AddCheckBox("Tweak_NicerColorSelectedBolts",
                    "A Nicer Color For The Selected Bolts",
                    value: false
                );

            DisableDeformLogic = Settings
                .AddCheckBox("Tweak_DisableDeformLogic",
                    "Disable Car Deformation",
                    value: false
                );


            Keybind.AddHeader("Hotkeys For ScrapSeller");

            Keybind_SelectParts = Keybind
                .Add("Cheat_ScrapSeller_SelectPartsID", "Select Parts", KeyCode.RightAlt);

            keybind_SellParts = Keybind
                .Add("CheatScrap_ScrapSeller_SellId", "Sell Selected Parts", KeyCode.Return);

        }

        private void Mod_OnMenuLoad()
        {
            // Called once, when the mod is loaded in the main menu
        }

        private void Mod_PreLoad()
        {
            ModMainObject = new GameObject("Racer's tweaks & cheats");

            CheatsMainObject = new GameObject("Cheats");
            TweaksMainObject = new GameObject("Tweaks");

            CheatsMainObject.transform.SetParent(ModMainObject.transform);
            TweaksMainObject.transform.SetParent(ModMainObject.transform);

            AllObjects = Load.Variable.GetAllObjects<GameObject>();

            AllMat = Load.Variable.GetAllObjects<Material>();

            AllSounds = Load.Variable.GetAllObjects<AudioClip>();

            AllDeformable = Load.Variable.GetAllObjects<Deformable>();

            BoltActiveMat = AllMat
                .Where(v => v.name == "activebolt")
                .FirstOrDefault();

            AllParts = AllObjects
                .Where(v =>
                    v != null
                    && v.CompareTag("PART")
                    && v.GetPlayMaker("Data")?.GetVariable<FsmInt>("AssemblyID") != null
                ).ToArray();

            ColorSelectPart = new Color(1, 1, 1, 0.35f);

            SelectedPartMat = new Material(BoltActiveMat);
            SelectedPartMat.color = ColorSelectPart;
        }

        private void Mod_OnLoad()
        {
            PlayerTrns = GameObject.Find("PLAYER").transform;

            PlayerMoney = FsmVariables.GlobalVariables.FindFsmFloat("PlayerMoney");

            Cheats();
            Tweaks();
        }

        private void Mod_PostLoad()
        {

        }

        private void Mod_Update()
        {
            // Update is called once per frame
        }

        private void Cheats()
        {
            if (ScrapSellerCheat.GetValue())
            {
                Load.Component.Cheat.Add_ScrapSeller();

                foreach (var part in AllParts)
                {
                    if (part != null && !part.GetComponent<Scrap>())
                    {
                        part.AddComponent<Scrap>();
                    }
                }

                LoadLog("ScrapSeller");
            }
        }

        private void Tweaks()
        {
            if (WithoutFrostedGlass.GetValue())
            {
                Load.Component.Tweak.Add_GlassWithoutFrost();

                LoadLog("WithoutFtostedGlass");
            }

            if (NicerColorSelectedBolts.GetValue())
            {
                Load.Component.Tweak.Add_NicerColorSelectedBolts();

                LoadLog("NicerColorSelectedBolts");
            }

            if (DisableDeformLogic.GetValue())
            {
                Load.Component.Tweak.Add_DisableCarDeformation();

                LoadLog("DisableCarDeformation");
            }

            ModConsole.Log("\n\n");
        }

        private void LoadLog(string nameComponent)
        {
            ModConsole.Log($"The \"{nameComponent}\" is loaded\n");
        }
    }
}
