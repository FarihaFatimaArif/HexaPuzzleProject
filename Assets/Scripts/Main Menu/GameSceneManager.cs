using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] TwoButtonPopupSO TwoButtonPopupSO;
    [SerializeField] AdSystem AdSystem;
    public void OnExitClick()
    {
        Debug.Log("here");
        TwoButtonPopupSO.ExitYes += LoadMainMenu;
        TwoButtonPopupSO.Exit.Invoke();
    }
    public void LoadMainMenu()
    {
        AdSystem.OnQuitAd();
        SceneManager.LoadScene("MainMenu");
        TwoButtonPopupSO.Hidepopup.Invoke();
    }
    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void OnResetClick()
    {
        TwoButtonPopupSO.ResetYes += Reset;
        TwoButtonPopupSO.Reset.Invoke();
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TwoButtonPopupSO.Hidepopup.Invoke();
    }
}
