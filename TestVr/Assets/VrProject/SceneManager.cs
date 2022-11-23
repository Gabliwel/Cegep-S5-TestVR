using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void GoToFlyingScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ChuteLibre");
    }

    public void GoToGrapplingHook()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GrapplingHook");
    }

    public void GoToPunchingBag()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PunchingBag");
    }

    public void GoToStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene1");
    }
}
