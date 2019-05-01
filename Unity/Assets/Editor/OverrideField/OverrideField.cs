using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Input field that indicates overriding an input
/// </summary>
/// <typeparam name="T">Input data type</typeparam>
abstract public class OverrideField<T>
{
    /// <summary>
    /// Checkbox label
    /// </summary>
    string checkboxMessage;

    /// <summary>
    /// Input label
    /// </summary>
    string inputMessage;

    /// <summary>
    /// Check if override checkbox is checked
    /// </summary>
    public bool OverrideChecked { get; set; } = false;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checkboxMessage">Checkbox label</param>
    /// <param name="inputMessage">Input label</param>
    public OverrideField(string checkboxMessage = "Override", string inputMessage = "Input")
    {
        this.checkboxMessage = checkboxMessage;
        this.inputMessage = inputMessage;
    }

    /// <summary>
    /// Render the GUI
    /// </summary>
    /// <param name="currentValue">Current value</param>
    /// <returns>New value</returns>
    public T Render(T currentValue)
    {
        //If the value is not default, check override checkbox
        if (!IsDefault(currentValue))
            OverrideChecked = true;

        //Update override checkbox
        OverrideChecked = GUILayout.Toggle(OverrideChecked, checkboxMessage);

        //Override is checked - create custom field and return res
        if (OverrideChecked)
        {
            EditorGUI.indentLevel++;
            T res = CreateField(currentValue);
            EditorGUI.indentLevel--;
            return res;
        }

        //Override is not checked, return the default value
        return DefaultValue();
    }

    /// <summary>
    /// Create the GUI (input only, not the checkbox)
    /// </summary>
    /// <param name="currentValue">Current value</param>
    /// <returns>New value</returns>
    public abstract T CreateField(T currentValue);

    /// <summary>
    /// Returns the default value (f.e. null)
    /// </summary>
    /// <returns></returns>
    public abstract T DefaultValue();

    /// <summary>
    /// Checks if the input is set to the default value
    /// </summary>
    /// <param name="sample">Input to check</param>
    /// <returns>True if input is default, else false</returns>
    public abstract bool IsDefault(T sample);
}
