using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangingButton : MonoBehaviour {

    public string m_scene;

    public void GoToScene()
    {
        if (!FindObjectOfType<MainMenu>().m_changingScene)
        {
            FindObjectOfType<MainMenu>().m_changingScene = true;
            FindObjectOfType<PersistentData>().ChangeSceneTo(m_scene);
        }
    }
}
