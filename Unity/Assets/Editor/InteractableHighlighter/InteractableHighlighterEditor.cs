using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableHighlighter))]
class InteractableHighlighterEditor : DefaultEditor<InteractableHighlighter>
{
    OverrideMonoscriptField<Animator> animatorField =
        new OverrideMonoscriptField<Animator>("Override Animator", "Animator");

    OverrideMonoscriptField<GameObject> targetGOField =
        new OverrideMonoscriptField<GameObject>("Override target GameObject", "GameObject");

    public override void OnCustomInspectorGUI()
    {
        //ANIMATOR
        Target.Animator = animatorField.Render(Target.Animator);
        animatorField.CheckForNullOverride(Target.Animator, MessageBox, "Animator is overriden but not set", ErrorStyle);

        //Gameobject does not have interactable
        if (Target.GetComponent<Interactable>() == null)
            MessageBox.AddMessage("Must be placed with Interactable component", ErrorStyle);
        else if (!animatorField.OverrideChecked &&
            Target.GetComponent<Interactable>().connectedObject.GetComponent<Animator>() == null)
            MessageBox.AddMessage("Connected interactable object does not have an Animator", ErrorStyle);

        //TARGET GAME OBJECT
        Target.TargetGameObject = targetGOField.Render(Target.TargetGameObject);
        targetGOField.CheckForNullOverride(Target.TargetGameObject, MessageBox, "Target GameObject is overriden but not set", ErrorStyle);
    }
}
