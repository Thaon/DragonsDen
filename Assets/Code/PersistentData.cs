using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EZCameraShake;
using FMODUnity;
using FMOD.Studio;

public enum GameState { Playing, Paused };

public class PersistentData : MonoBehaviour {

    #region member variables

    public GameState m_state = GameState.Paused;
    public Vector3 m_speed;
    public List<KeyValuePair<string, int>> m_items;
    public Sprite[] m_itemsIcons;
    public List<int> m_itemValuesByIndex;
    public List<GameObject> m_slotsArray;
    public string m_businessPlan;

    private Material m_transitionMat;
    private bool m_fadeIn = true;
    private float m_fadeInAmount = 0;
    private StudioEventEmitter m_fmodEmitter;
    private string m_activeSceneName;

    #endregion

    void Awake()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
        m_fmodEmitter = GetComponent<StudioEventEmitter>();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start ()
    {
        m_itemValuesByIndex = new List<int>();
        m_itemValuesByIndex.Add(Mathf.RoundToInt(10 * Random.Range(0.7f, 1.4f)));
        m_itemValuesByIndex.Add(Mathf.RoundToInt(10 * Random.Range(0.7f, 1.4f)));
        m_itemValuesByIndex.Add(Mathf.RoundToInt(10 * Random.Range(0.7f, 1.4f)));
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

        if (m_items!= null && m_items.Count > 0)
        {
            if (GetHigherAsset() == "Cattle")
                m_fmodEmitter.SetParameter("musicSlider", 0.3f);

            if (GetHigherAsset() == "Gem")
                m_fmodEmitter.SetParameter("musicSlider", 0.6f);

            if (GetHigherAsset() == "Share")
                m_fmodEmitter.SetParameter("musicSlider", 1f);
        }
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

    public string GetHigherAsset()
    {
        int cat = GetItemNumber("Cattle");
        int gem = GetItemNumber("Gem");
        int share = GetItemNumber("Share");
        List<int> vals = new List<int>() { cat, gem, share };

        int min = 0;

        foreach (int i in vals)
        {
            if (i > min)
                min = i;
        }

        if (min == cat)
            return "Cattle";
        if (min == gem)
            return "Gem";
        if (min == share)
            return "Share";

        return "";
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

    private int m_got, m_max, m_perc;
    public void SetDealResults(int percent, int moneyGotten, int possibleMoney)
    {
        m_got = moneyGotten;
        m_max = possibleMoney;
        m_perc = percent;
    }


    #region scene changing and shit

    void OnSceneChanged(Scene last, Scene newScene)
    {
        m_activeSceneName = newScene.name;
        m_transitionMat = FindObjectOfType<SimpleBlit>().TransitionMaterial;
        m_fadeIn = false;
        m_fadeInAmount = 1;
        m_fmodEmitter.Stop();

        if (newScene.name == "MainMenu")
        {
            Start();
        }

        if (newScene.name == "Gabe")
        {
            m_fmodEmitter.Play();

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
            StartCoroutine(EndPlanTime());
        }
        if (newScene.name == "DenInfo")
        {
            GameObject.Find("Document").GetComponent<Text>().text = m_businessPlan;
        }

        if (newScene.name == "EndGame")
        {
            GameObject.Find("Document").GetComponent<Text>().text = "You managed to sell " + m_perc + "% of your company for " + m_got + ". A decent amount, the best deal for you was " + m_max + "!";
        }
    }

    public void ChangeSceneTo(string scene)
    {
        StartCoroutine(Change(scene));
        m_fadeIn = true;
        m_fadeInAmount = 0;

        if (m_activeSceneName == "Gabe")
            GameObject.Find("Gong").GetComponent<StudioEventEmitter>().Play();
    }

    IEnumerator Change(string scene)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }

    IEnumerator EndPlanTime()
    {
        yield return new WaitForSeconds(5);
        m_businessPlan = GameObject.Find("Document").GetComponent<Text>().text;
        ChangeSceneTo("DenInfo");
    }

    #endregion
}
