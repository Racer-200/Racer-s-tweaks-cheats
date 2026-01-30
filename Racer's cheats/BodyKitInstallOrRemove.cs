using HutongGames.PlayMaker;
using UnityEngine;

namespace Racer_s_cheats
{
    internal class BodyKitInstallOrRemove : FsmStateAction
    {
        public bool setBool;

        private GameObject _activePart;

        public override void OnEnter()
        {
            _activePart = Fsm.GetFsmGameObject("ActivePart").Value;

            _activePart.GetComponent<Collider>().isTrigger = setBool;
            _activePart.GetComponent<Rigidbody>().isKinematic = setBool;

            Finish();
        }
    }
}
