using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Override input for Monoscript data types
/// </summary>
/// <typeparam name="T">Field data type</typeparam>
public class OverrideMonoscriptField<T> : OverrideField<T>
    where T : UnityEngine.Object
{
    string inputMessage;

    public OverrideMonoscriptField(string checkboxMessage = "Override", string inputMessage = "Monoscript")
        :base(checkboxMessage)
    {
        this.inputMessage = inputMessage;
    }

    public override T CreateField(T currentValue)
    {
        return EditorGUILayout.ObjectField(inputMessage, currentValue, typeof(T), true) as T;
    }

    public override T DefaultValue()
    {
        return null;
    }

    public override bool IsDefault(T sample)
    {
        return sample == null;
    }

    /// <summary>
    /// Handles error for null override
    /// </summary>
    /// <param name="currentValue">Current value</param>
    /// <param name="messageBox">Message box (where to add the error message)</param>
    /// <param name="errorMessage">Error message text</param>
    /// <param name="style">Error message style</param>
    public void CheckForNullOverride(T currentValue, EditorMessageBox messageBox, string errorMessage = "Overrided value is not set", GUIStyle style = null)
    {
        if (IsDefault(currentValue) && OverrideChecked)
            messageBox.AddMessage(errorMessage, style);
    }

    /// <summary>
    /// Handles error for null override
    /// </summary>
    /// <param name="currentValue">Current value</param>
    /// <param name="messageBox">Message box (where to add the error message)</param>
    /// <param name="errorMessage">Error message text</param>
    public void CheckForNullOverride(T currentValue, EditorMessageBox messageBox, string errorMessage = "Overrided value is not set")
    {
        CheckForNullOverride(currentValue, messageBox, errorMessage, DefaultEditor<MonoBehaviour>.ErrorStyle);
    }
}
