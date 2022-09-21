using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/RewardGrantedSO", order = 1, fileName = "RewardGranted")]
public class RewardGranted : ScriptableObject
{
    [SerializeField] PlayerPrefKeys PlayerPrefKeys;
    public bool RemoveAds=false;
    public int NoOfCoins;
    public int NoOfSkips;
    public void ReadRewards()
    {
        if (PlayerPrefs.HasKey(PlayerPrefKeys.Coins))
            NoOfCoins = PlayerPrefs.GetInt(PlayerPrefKeys.Coins);
        if (PlayerPrefs.HasKey(PlayerPrefKeys.Skips))
            NoOfSkips = PlayerPrefs.GetInt(PlayerPrefKeys.Skips);
        //if (PlayerPrefs.HasKey(PlayerPrefKeys.Coins))
        //    NoOfCoins = PlayerPrefs.GetInt(PlayerPrefKeys.Coins);
    }
    public void WriteRewards()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.Coins, NoOfCoins);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt(PlayerPrefKeys.Skips, NoOfSkips);
        PlayerPrefs.Save();
        if(RemoveAds)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.RemoveAds, 1);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.RemoveAds, 0);
            PlayerPrefs.Save();
        }
        
    }
}