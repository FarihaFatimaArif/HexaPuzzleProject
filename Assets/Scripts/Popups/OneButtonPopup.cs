using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OneButtonPopup : MonoBehaviour
{
    [SerializeField] OneButtonPopupSo OneButtonPopupSo;
    [SerializeField] PopupTexts PopupTexts;
    [SerializeField] GameObject Popup;
    [SerializeField] TextMeshProUGUI PopupText;
    [SerializeField] Button CloseBtn;

    private void OnEnable()
    {
        OneButtonPopupSo.RewardGranted += RewardGrantedPopup;
        OneButtonPopupSo.AdNotLoaded += AdNotLoaded;
        OneButtonPopupSo.PurchaseSuccessfull += PurchaseSuccessfull;
        OneButtonPopupSo.PurchaseUnsuccessfull += PurchaseUnsuccessfull;
        OneButtonPopupSo.PurchaseRestored += PurchaseRestored;
        CloseBtn.onClick.AddListener(HidePopup);
    }

    private void OnDisable()
    {
        OneButtonPopupSo.RewardGranted -= RewardGrantedPopup;
        OneButtonPopupSo.AdNotLoaded -= AdNotLoaded;
        OneButtonPopupSo.PurchaseSuccessfull -= PurchaseSuccessfull;
        OneButtonPopupSo.PurchaseUnsuccessfull -= PurchaseUnsuccessfull;
        OneButtonPopupSo.PurchaseRestored -= PurchaseRestored;
        CloseBtn.onClick.RemoveListener(HidePopup);
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }
    void AdNotLoaded()
    {
        SetPopup(PopupTexts.AdNotLoaded);
        Popup.SetActive(true);
    }
    void PurchaseSuccessfull()
    {
        SetPopup(PopupTexts.PurchaseSuccessfull);
        Popup.SetActive(true);
    }
    void PurchaseUnsuccessfull()
    {
        SetPopup(PopupTexts.PurchaseUnsuccessfull);
        Popup.SetActive(true);
    }
    void SetPopup(string popupText)
    {
        PopupText.text = popupText;
    }
    void RewardGrantedPopup()
    {
        SetPopup(PopupTexts.RewardGranted);
        Popup.SetActive(true);
    }
    void HidePopup()
    {
        Popup.SetActive(false);
    }
    void PurchaseRestored()
    {
        SetPopup(PopupTexts.PurchaseRestored);
        Popup.SetActive(true);
    }
}
