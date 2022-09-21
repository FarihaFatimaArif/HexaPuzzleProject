using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCards : MonoBehaviour
{
    [SerializeField] IAPShop IAPShop;
    [SerializeField] RewardGranted RewardGranted;
    [SerializeField] PlayerPrefKeys PrefKeys;
    Vector2 initialPos;
    GameObject created;
    // Start is called before the first frame update
    void Start()
    {
        int removeAdsBool;
        if (PlayerPrefs.HasKey(PrefKeys.Coins))
            RewardGranted.NoOfCoins = PlayerPrefs.GetInt(PrefKeys.Coins);
        if (PlayerPrefs.HasKey(PrefKeys.Skips))
            RewardGranted.NoOfSkips = PlayerPrefs.GetInt(PrefKeys.Skips);
        if (PlayerPrefs.HasKey(PrefKeys.RemoveAds))
        {
            removeAdsBool = PlayerPrefs.GetInt(PrefKeys.RemoveAds);
            if(removeAdsBool==1)
            {
                RewardGranted.RemoveAds = true;
            }
        }
        Debug.Log(RewardGranted.NoOfCoins);
        Debug.Log(RewardGranted.NoOfSkips);
        Debug.Log(RewardGranted.RemoveAds);
        initialPos = new Vector2();
        foreach (var pair in IAPShop.RewardItems)
        {
            if (pair.Rewards[0].RewardType == RewardType.RemoveAds && RewardGranted.RemoveAds == true)
            {

            }
            else
            {
                created = Instantiate(pair.Card, initialPos, Quaternion.identity);
                created.transform.SetParent(this.transform);
            }
        }
    }
}
