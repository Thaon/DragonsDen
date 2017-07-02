using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EZCameraShake;

public enum GameState { Playing, Paused };

public class PersistentData : MonoBehaviour {

    #region member variables

    public GameState m_state = GameState.Paused;
    public Vector3 m_speed;
    public List<KeyValuePair<string, int>> m_items;
    public Sprite[] m_itemsIcons;
    public int[] m_itemValuesByIndex;
    public List<GameObject> m_slotsArray;

    private Material m_transitionMat;
    private bool m_fadeIn = true;
    private float m_fadeInAmount = 0;

    #endregion

    void Awake()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start ()
    {

	}


    void Update ()
    {
		if (!m_fadeIn)
        {
            if (m_fadeInAmount > 0)
            {
                m_fadeInAmount -= Time.deltaTime;
            }
            Play();
        }
        else
        {
            if (m_fadeInAmount < 1)
            {
                m_fadeInAmount += Time.deltaTime;
            }
            else
                Pause();
        }

        m_fadeInAmount = Mathf.Clamp01(m_fadeInAmount);
        m_transitionMat.SetFloat("_Cutoff", m_fadeInAmount);

    }

    public void ModifyItemsValue(string name, int value, ItemType type)
    {
        if (type == ItemType.Increase)
        {
            m_items.Add(new KeyValuePair<string, int>(name, value));
            m_slotsArray[m_items.Count - 1].SetActive(true);
            m_slotsArray[m_items.Count - 1].GetComponentInChildren<Image>().sprite = m_itemsIcons[value];

            if (m_items.Count >= 10)
            {
                Pause();
                ChangeSceneTo("PlanInfo");
            }
        }
        else
        {
            float magn = 5, rough = 10, fadeIn = 0.1f, fadeOut = .2f;
            CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(magn, rough, fadeIn, fadeOut);

            if (m_items.Count != 0)
            {
                int itemID = m_items.FindIndex(nm => nm.Key == name);
                m_slotsArray[m_items.Count - 1].SetActive(false);
                m_items.RemoveAt(m_items.Count - 1);
            }
        }
    }

    public int GetItemNumber(string item)
    {
        int num = 0;
        foreach (KeyValuePair<string, int> itm in m_items)
        {
            if (itm.Key == item)
                num++;
        }
        return num;
    }

    public int GetTotalItamsValue()
    {
        int val = 0;
        foreach (KeyValuePair<string, int> kvp in m_items)
        {
            val += m_itemValuesByIndex[kvp.Value];
        }
        return val;
    }

    void Play()
    {
        m_state = GameState.Playing;
    }

    void Pause()
    {
        m_state = GameState.Paused;
    }

    #region scene changing and shit


    void OnSceneChanged(Scene last, Scene newScene)
    {
        m_transitionMat = FindObjectOfType<SimpleBlit>().TransitionMaterial;
        m_fadeIn = false;
        m_fadeInAmount = 1;


        if (newScene.name == "Gabe")
        {
            m_slotsArray = new List<GameObject>();
            m_items = new List<KeyValuePair<string, int>>();

            Transform[] arr = GameObject.Find("SlotsArray").GetComponentsInChildren<Transform>();
            foreach (Transform tr in arr)
            {
                if (tr.name != "SlotsArray")
                    m_slotsArray.Add(tr.gameObject);
            }

            foreach (GameObject go in m_slotsArray)
            {
                go.SetActive(false);
            }
        }
        if (newScene.name == "Plan")
        {

        }
    }

    public void ChangeSceneTo(string scene)
    {
        StartCoroutine(Change(scene));
        m_fadeIn = true;
        m_fadeInAmount = 0;
    }

    IEnumerator Change(string scene)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }

    IEnumerator EndPlanTime()
    {
        yield return new WaitForSeconds(5);
        ChangeSceneTo("DenInfo");
    }

    #endregion
}
