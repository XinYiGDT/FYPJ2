using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChange : MonoBehaviour {

    public Sprite DefaultColor;
    public Sprite BlackColor;

    private Image GoSR;

    // Use this for initialization
    void Start () {
        GoSR = this.gameObject.GetComponent<Image>();
        GoSR.sprite = DefaultColor;
    }
	
	public void ChangeColor () {
            if (GoSR.sprite == DefaultColor)
                GoSR.sprite = BlackColor;
            else if (GoSR.sprite == BlackColor)
                GoSR.sprite = DefaultColor;
	}
}
