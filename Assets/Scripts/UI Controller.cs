using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void Play()
    {
        Scene_Controller.LoadScene(1);

    }

    public void Restart()
    {
        Scene_Controller.Restart();
    }

    public void NextLevel()
    {
        Scene_Controller.NextLevel();
    }

    public void SceneLoad(int sceneIndex)
    {
        Scene_Controller.LoadScene(sceneIndex);
    }


}
