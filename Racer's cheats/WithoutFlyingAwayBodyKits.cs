using Harmony;
using MSCLoader;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Racer_s_cheats
{
    public class WithoutFlyingAwayBodyKits : MonoBehaviour
    {
        private PlayMakerFSM _pm;

        private GameObject _activePart;

        void Start()
        {
            _pm = Racer_s_tweaks_cheats.Corris.transform
                .FindChild("AssembliesTuning/VINP_FrontSpoiler").gameObject
                .GetPlayMaker("Data");

            Utils.DeleteActionsOfIndex(_pm,
                nameOfState: "Set joint 2",
                index: 2,
                count: 3
            );

            Utils.DeleteActionsOfIndex(_pm,
                nameOfState: "Assemble 2",
                index: 0,
                count: 2
            );

            Utils.DeleteAllAction(_pm,
                nameOfState: "Check joint"
            );


            _pm.Fsm.GetState("Assemble 2")
                .AddAction(new BodyKitInstallOrRemove
                {setBool = true});

            _pm.Fsm.GetState("Remove part")
                .AddAction(new BodyKitInstallOrRemove 
                {setBool = false});

            _activePart = _pm.Fsm.GetFsmGameObject("ActivePart").Value;

            CheckActivePart();
        }

        private void CheckActivePart()
        {
            if (_activePart == null)
                return;

            _pm.SendEvent("REMOVE");
            _pm.SendEvent("INSTALL");

            _activePart
            .GetPlayMaker("Data")
            .SendEvent("INSTALL");
        }
    }
    
}