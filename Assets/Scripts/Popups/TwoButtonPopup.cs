using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TwoButtonPopup : MonoBehaviour
{
    [SerializeField] TwoButtonPopupSO TwoButtonPopupSO;
    [SerializeField] PopupTexts PopupTexts;
    [SerializeField] GameObject Popup;
    [SerializeField] TextMeshProUGUI PopupText;
    [SerializeField] Button NoBtn;
    [SerializeField] Button YesBtn;

    private void OnEnable()
    {
        TwoButtonPopupSO.Exit += Exit;
        TwoButtonPopupSO.Reset += Reset;
        TwoButtonPopupSO.Fail += Fail;
        TwoButtonPopupSO.Skip += Skip;
        NoBtn.onClick.AddListener(HidePopup);
        TwoButtonPopupSO.Hidepopup += HidePopup;
    }

    private void OnDisable()
    {
        //TwoButtonPopupSO.Exit -= Exit;
        //TwoButtonPopupSO.Reset -= Reset;
        //TwoButtonPopupSO.Fail -= Fail;
        //TwoButtonPopupSO.Skip -= Skip;
        //NoBtn.onClick.RemoveListener(HidePopup);
        //TwoButtonPopupSO.Hidepopup -= HidePopup;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }
    void Fail()
    {
        SetPopup(PopupTexts.Fail);
        Popup.SetActive(true);
        YesBtn.onClick.AddListener(TwoButtonPopupSO.FailYes);
        YesBtn.onClick.AddListener(HidePopup);
    }
    void SetPopup(string popupText)
    {
        PopupText.text = popupText;
    }
    void Exit()
    {
        Popup.SetActive(true);
        SetPopup(PopupTexts.Exit);
        //Popup.SetActive(true);
        YesBtn.onClick.AddListener(TwoButtonPopupSO.ExitYes);
        YesBtn.onClick.AddListener(HidePopup);
        //HidePopup();
    }
    void Reset()
    {
        SetPopup(PopupTexts.Reset);
        Popup.SetActive(true);
        //YesBtn.
        YesBtn.onClick.AddListener(TwoButtonPopupSO.ResetYes);
        YesBtn.onClick.AddListener(HidePopup);
        //HidePopup();
    }
    void Skip()
    {
        SetPopup(PopupTexts.Skip);
        Popup.SetActive(true);
        YesBtn.onClick.AddListener(TwoButtonPopupSO.SkipYes);
        YesBtn.onClick.AddListener(HidePopup);
    }    
    public void HidePopup()
    {
        Popup.SetActive(false);
        YesBtn.onClick.RemoveAllListeners();
    }
}
