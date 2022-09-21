using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerPrefs/PlayerPrefKeys", order = 1, fileName = "PlayerPrefKeys")]
public class PlayerPrefKeys : ScriptableObject
{
    public string Coins = "NoOfCoins";
    public string Skips = "NoOfSkips";
    public string RemoveAds = "RemoveAds";
}
