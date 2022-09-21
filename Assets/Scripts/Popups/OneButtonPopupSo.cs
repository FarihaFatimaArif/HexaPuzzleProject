using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/Popup/OneButtonPopupSO", order = 1, fileName = "OneButtonPopupSO")]
public class OneButtonPopupSo : ScriptableObject
{
    public Action AdNotLoaded;
    public Action RewardGranted;
    public Action PurchaseSuccessfull;
    public Action PurchaseUnsuccessfull;
    public Action PurchaseRestored;
}
