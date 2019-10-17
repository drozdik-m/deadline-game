using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Type of the dialog
/// </summary>
public enum DialogType
{
    Self,
    Twin
}

/// <summary>
/// Sentence wrapper of position, sentence and type.
/// </summary>
public struct SentenceWrapper
{
    /// <summary>
    /// The position of the bubble
    /// </summary>
    public Transform Position;
    /// <summary>
    /// The sentence to display
    /// </summary>
    public string Sentence;
    /// <summary>
    /// The type of the dialog
    /// </summary>
    public DialogType Type;

    public SentenceWrapper(Transform position, string sentence, DialogType type)
    {
        Position = position;
        Sentence = sentence;
        Type = type;
    }
}
