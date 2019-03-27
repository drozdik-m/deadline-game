using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interaction.Abstracts
{
    public abstract class DelayedReaction : Reaction
    {
        public float delay;
        protected WaitForSeconds wait;
        

        public new void Init()
        {
            wait = new WaitForSeconds(delay);
        }

        public new void React(MonoBehaviour monoBehaviour)
        {
            monoBehaviour.StartCoroutine(ReactCoroutine());
        }

        protected IEnumerator ReactCoroutine()
        {
            yield return wait;
            ImmediateReaction();
        }
    
    }
}
