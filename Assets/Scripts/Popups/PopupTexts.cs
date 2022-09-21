using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Popup/PopupTexts", order = 1, fileName = "PopupTexts")]
public class PopupTexts : ScriptableObject
{
    public string AdNotLoaded= "Ad Not Loaded";
    public string Exit="Do you want to exit?";
    public string Fail = "Do you want to retry?";
    public string Reset = "Do you want to reset?";
    public string RewardGranted= "Reward Granted";
    public string Skip = "Do you want to watch an ad for skip?";
    public string PurchaseSuccessfull = "Purchase Successfull";
    public string PurchaseUnsuccessfull = "Purchase Unuccessfull";
    public string PurchaseRestored = "Purchase Restored";
    //skip ad show or not
    //do you want reset
    //purchase success and fail;
}
