using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInWords : MonoBehaviour {

    public Text m_text;
    float m_fade = 2.0f;
    float m_colorFloat = 0.1f;
    int m_colorInt;
    int m_counter = 0;
    string m_show;
    string[] m_wordArray;

    // Use this for initialization
    void Start()
    {
        string l_words = "Here is some text to be diplayed word by word.";
        m_wordArray = l_words.Split(' ');
    }

    // Update is called once per frame
    void Update()
    {
        if (m_counter < m_wordArray.Length)
        {
            if (m_colorFloat < 1.0f)
            {
                m_colorFloat += Time.deltaTime / m_fade;
                m_colorInt = (int)(Mathf.Lerp(0.0f, 1.0f, m_colorFloat) * 255.0f);
                m_text.text = m_show + "<color=\"#FF0000" + string.Format("{0:X}", m_colorInt) + "\">" + m_wordArray[m_counter] + "</color>";

            }
            else
            {
                m_colorFloat = 0.1f;
                m_show += m_wordArray[m_counter] + " ";
                m_counter++;
            }
        }
    }
}
