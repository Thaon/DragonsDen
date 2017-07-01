using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuzzGeneratingButton : MonoBehaviour {

    #region member variables

    private string[] m_words;
    private string[] m_conjunctions = { "and", "with", "for", "in" };

    #endregion

    void Start ()
    {
        TextAsset text = Resources.Load("Words") as TextAsset;
        m_words = text.text.Split('\n');
        GetComponentInChildren<Text>().text = m_words[Random.Range(0, m_words.Length)];
	}
	
	public void SelectWord()
    {
        GameObject.Find("Document").GetComponent<Text>().text += GetComponentInChildren<Text>().text + " " + m_conjunctions[Random.Range(0, m_conjunctions.Length)] + " ";
        GetComponentInChildren<Text>().text = m_words[Random.Range(0, m_words.Length)];
    }
}
