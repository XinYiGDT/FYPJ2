using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
    public Animation anim;
    private Text popText;
   //float clipLength = animator.GetCurrentAnimatorStateInfo(0).length;

    public void OnEnable ()
    {
        anim.Play();
        Destroy(gameObject, anim.clip.length);
        SetText(GetComponent<Text>().text);
        //popText = anim.GetComponent<Text>();
	}
	
	public void SetText(string text)
    {
        popText.text = text;
    }
}
