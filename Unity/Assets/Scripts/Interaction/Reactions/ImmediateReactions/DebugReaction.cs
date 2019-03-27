using Assets.Scripts.Interaction.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interaction.Reactions.ImmediateReactions
{
    public class DebugReaction : Reaction
    {
        public string debugMessage;

        protected override void SpecificInit()
        {
            base.SpecificInit();
        }

        protected override void ImmediateReaction()
        {
            Debug.Log(debugMessage);
        }
    }
}
