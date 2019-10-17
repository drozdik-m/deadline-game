using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles cursor changing.
/// </summary>
public class CursorManager : MonoBehaviour
{
    /// <summary>
    /// Texture for regular cursor
    /// </summary>
    public Texture2D NormalCursorTexture;

    /// <summary>
    /// Texture for pointing cursor
    /// </summary>
    public Texture2D PointerCursorTexture;

    /// <summary>
    /// Cursor mode
    /// </summary>
    public CursorMode CursorMode = CursorMode.Auto;

    /// <summary>
    /// Current texture for smarter swapping
    /// </summary>
    private Texture2D currentTexture = null;

    private void Start()
    {
        ChangeCursorToNormal();
    }

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            //Did it hit interactable object?
            if (hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                ChangeCursorToPoint();
            }
            else
            {
                ChangeCursorToNormal();
            }
        }
        else
            ChangeCursorToNormal();
    }

   
    /// <summary>
    /// Changes cursor to normal
    /// </summary>
    public void ChangeCursorToNormal()
    {
        //Cursor.SetCursor(NormalCursorTexture, new Vector2(75, 0), CursorMode);
        if (currentTexture != NormalCursorTexture)
        {
            Cursor.SetCursor(NormalCursorTexture, Vector2.zero, CursorMode);
            currentTexture = NormalCursorTexture;
        }
    }

    /// <summary>
    /// Changes cursor to the pointy one
    /// </summary>
    public void ChangeCursorToPoint()
    {
        if (currentTexture != PointerCursorTexture)
        {
            Cursor.SetCursor(PointerCursorTexture, Vector2.zero, CursorMode);
            currentTexture = PointerCursorTexture;
        }
        
    }
}
