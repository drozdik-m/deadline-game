using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interaction.Abstracts
{
    public abstract class Reaction : ScriptableObject
    {
        public void Init()
        {
            SpecificInit();
        }

        protected virtual void SpecificInit()
        {

        }

        public void React(MonoBehaviour monoBehaviour)
        {
            ImmediateReaction();
        }

        protected abstract void ImmediateReaction();
    }
}
