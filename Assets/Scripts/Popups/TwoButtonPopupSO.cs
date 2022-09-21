using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/Popup/TwoButtonPopupSO", order = 1, fileName = "TwoButtonPopupSO")]
public class TwoButtonPopupSO : ScriptableObject
{
    public Action Exit;
    public UnityAction ExitYes;
    public UnityAction ResetYes;
    public UnityAction SkipYes;
    public UnityAction FailYes;
    public Action Reset;
    public Action Fail;
    public Action Skip;
    public Action Hidepopup;

}
