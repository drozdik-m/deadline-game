using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MessageRecord
{
    public string Message;
    public GUIStyle Style;

    public MessageRecord(string message, GUIStyle style)
    {
        Message = message;
        Style = style;
    }
}
