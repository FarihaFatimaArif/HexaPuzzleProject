using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LifeSystem", order = 1)]
public class LifeSystem : ScriptableObject
{
    [SerializeField] int NoOfLives;
    [SerializeField] int RechargableTimeInMinutes;
    public void testingtime()
    {
        DateTimeOffset offset = new DateTimeOffset(2022, 9, 14, 0, 0, 0, new TimeSpan(2,0,0));
        long value = offset.ToUnixTimeSeconds();
        int time = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
        Debug.Log("timeeeeeee"+time);
    }

}
