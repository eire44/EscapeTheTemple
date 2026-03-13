using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotspot = new Vector2(16, 16);

    void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }
}
