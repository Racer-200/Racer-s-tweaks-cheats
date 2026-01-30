using HutongGames.PlayMaker;
using MSCLoader;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Racer_s_cheats
{
	public class ScrapSeller : MonoBehaviour
	{
		public bool SelectBool;

		private float _scrapPrice;

		private AudioClip _audioCash;

		private PlayMakerFSM _pm;

		private FsmGameObject _hitObj;

		private List<Scrap> _selectedParts = [];

		void Start()
		{
			_scrapPrice = Load.Variable.ScrapPrice();

			_pm = Racer_s_tweaks_cheats.PlayerTrns
				.FindChild("Pivot/AnimPivot/Camera/FPSCamera/1Hand_Assemble/Hand")
				.GetComponent<PlayMakerFSM>();

			_hitObj = _pm.GetVariable<FsmGameObject>("RaycastHitObject");

			_audioCash = Racer_s_tweaks_cheats.AllSounds
				.Where(v => v.name == "cash_register_2")
				.FirstOrDefault();
		}

		void Update()
		{
            CheckSell();
            TestBool();
            CheckHit();
        }

		private void TestBool()
		{
			if (!Racer_s_tweaks_cheats.Keybind_SelectParts.GetKeybindDown())
				return;

			SelectBool = !SelectBool;
			ResetSelection();

			ModConsole.Log($"Select mode {(SelectBool ? "enabled" : "disabled")}");
		}

		private void ResetSelection()
		{
			_selectedParts.ForEach(v => v?.OldColor());
			_selectedParts.Clear();
		}

		private void CheckHit()
		{
            var scrap = _hitObj.Value.GetComponent<Scrap>();

            if (
				!SelectBool
				|| scrap == null
				|| _selectedParts.Contains(scrap)
			) return;

            _selectedParts.Add(scrap);
            scrap.LightUp();
        }

		private void CheckSell()
		{
			if (
				!Racer_s_tweaks_cheats.keybind_SellParts.GetKeybindDown()
				|| !SelectBool
				|| _selectedParts.Count == 0
			) return;

			var mass = GetAllMass(_selectedParts);
			var earnedMoney = mass * _scrapPrice;

            Garbage();

            Racer_s_tweaks_cheats.PlayerMoney.Value += earnedMoney;

            AudioSource.PlayClipAtPoint(_audioCash, Racer_s_tweaks_cheats.PlayerTrns.position);

            ModConsole.Log($"You sold scrap of mass is {mass}kg." +
                $"\nThe scrap price per kg is {_scrapPrice}MK." +
                $"\nYou earned {earnedMoney}MK.\n"
            );
        }

		private void Garbage()
		{
			SelectBool = false;

			_selectedParts?.ForEach(v => v.gameObject
				.GetPlayMaker("Data")
				.SendEvent("GARBAGE")
			);

            _selectedParts?.Clear();
        }

		private float GetAllMass(List<Scrap> scraps)
		{
			float allMass = 0;

			foreach(var scrap in scraps)
			{
				allMass += scrap.GetComponent<Rigidbody>().mass;
			}

			return allMass;
		}
    }
}