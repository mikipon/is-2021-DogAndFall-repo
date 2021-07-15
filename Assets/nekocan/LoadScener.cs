using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScener : MonoBehaviour
{
    string dogScene = "otameScene";
    string darumaScene = "otameScene";
    void Start()
    {
        DebugUIBuilder.instance.AddLabel("Load Scene");
        DebugUIBuilder.instance.AddButton("Load:" + dogScene, () => { OtameScene(); });
        DebugUIBuilder.instance.AddButton("Load:" + darumaScene, () => { Daruma(); });

    }

    void OtameScene()
    {
        SceneManager.LoadScene(dogScene);
    }

    void Daruma()
    {
        SceneManager.LoadScene(darumaScene);
    }
}
