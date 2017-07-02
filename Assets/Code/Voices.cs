using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Voices : MonoBehaviour {

    private StudioEventEmitter m_emitter;

    public bool m_canPlay = false;

	void Start ()
    {
        m_emitter = GetComponent<StudioEventEmitter>();
        StartCoroutine(SayVoice());
	}
	
	void Update () {
		
	}

    public void VoiceOn()
    {
        m_canPlay = true;
    }

    public void VoiceOff()
    {
        m_canPlay = false;
    }

    IEnumerator SayVoice()
    {
        yield return new WaitForSeconds(0.1f);
        if (m_canPlay)
            m_emitter.Play();
        StartCoroutine(SayVoice());
    }
}
