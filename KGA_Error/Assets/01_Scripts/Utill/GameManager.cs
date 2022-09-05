using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public int CurrentScene;
    public int TurnIndex;
    private void Awake()
    {
        CurrentScene = 0;
        TurnIndex = 0;
    }
    public void UpdateScene()
    {
        TurnIndex = 0;
        //if (UIController.Instance.FadePannel != null)
        //{
        //    UIController.Instance.StartCoroutine("FadeInStart");
        //}
        AudioController.Instance.AudioPlay(0);
        if(CurrentScene == 3 || CurrentScene == 4 || CurrentScene == 5 || CurrentScene == 6)
        {
            //UIController.Instance.StartFadeOutCRT();
        }
        SceneManager.LoadScene(CurrentScene);
    }
}
