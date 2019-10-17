using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class EditorMessageBox
{
    /// <summary>
    /// Message list
    /// </summary>
    List<MessageRecord> messageList = new List<MessageRecord>();

    /// <summary>
    /// Adds a message to the list
    /// </summary>
    /// <param name="message">Message text</param>
    /// <param name="style">Text style</param>
    public void AddMessage(string message, GUIStyle style)
    {
        messageList.Add(new MessageRecord(message, style));
    }

    /// <summary>
    /// Clears saved messages
    /// </summary>
    public void ClearMessages()
    {
        messageList.Clear();
    }

    /// <summary>
    /// Writes messages and clears current messages
    /// </summary>
    public void WriteMessages()
    {
        foreach(MessageRecord message in messageList)
            EditorGUILayout.LabelField(message.Message, message.Style);
        ClearMessages();
    }

    /// <summary>
    /// Returns number of saved messages
    /// </summary>
    /// <returns>Count of saved messages</returns>
    public int Count()
    {
        return messageList.Count;
    }
    
    /// <summary>
    /// Tells if messages list is empty
    /// </summary>
    /// <returns>True if message list is empty, else false</returns>
    public bool IsEmpty()
    {
        return Count() == 0;
    }
}
