using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextController : MonoBehaviour {
    private static FloatingText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!popupText)
            popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");
    }
	
    public static void CreateFloatingText(string text, Transform location)
    {
        FloatingText instance = Instantiate(popupText);
        instance.transform.SetParent(canvas.transform, false);
        instance.SetText(text);
    }
}
