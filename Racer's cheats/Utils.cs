using MSCLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Racer_s_cheats
{
    internal class Utils
    {
        public static void DeleteActionsOfIndex(PlayMakerFSM playMakerFSM, string nameOfState, int index, int count)
        {
            var state = playMakerFSM.GetState(nameOfState);
            var listOfActions = state.Actions.ToList();

            listOfActions.RemoveRange(index, count);

            state.Actions = listOfActions.ToArray();
            state.SaveActions();
        }

        public static void DeleteAllAction(PlayMakerFSM playMakerFSM, string nameOfState)
        {
            var state = playMakerFSM.GetState(nameOfState);
            var listOfActions = state.Actions.ToList();

            listOfActions.RemoveRange(0, listOfActions.Count);

            state.Actions = listOfActions.ToArray();
            state.SaveActions();
        }
    }
}
