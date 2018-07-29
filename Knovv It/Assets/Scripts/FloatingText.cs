using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
    public GameObject textObject;
    public Animation anim;
    private Text popText;
    private Vector3 originalPos;
    private float animLength;
   //float clipLength = animator.GetCurrentAnimatorStateInfo(0).length;

    void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        popText = this.GetComponent<Text>();
        animLength = anim.clip.length;
        SetText(textObject.GetComponent<Text>().text);
    }
    public void OnEnable ()
    {
        anim.Play();
        //textObject.active = false;
        //Destroy(gameObject, anim.clip.length);
        
	}

    void Update()
    {
        if(anim.clip.length >= animLength)
        {
            //gameObject.active = false;
            anim.Stop();
            //textObject.active = true;
            //gameObject.transform.position = originalPos;
        }
    }
	
	public void SetText(string text)
    {
        popText.text = text;
    }
}
