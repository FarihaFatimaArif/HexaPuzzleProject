using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RewardHandler : MonoBehaviour, IItemPurchase
{
    [SerializeField] AdSystem AdSystem;
    public UnityAction ShowRewardAction;
    [SerializeField] PlayerPrefKeys PrefKeys;
    [SerializeField] RewardGranted RewardGranted;
    [SerializeField] IAPShop IAPShop;
    [SerializeField] OneButtonPopupSo OneButtonPopupSo;
    public UnityEvent UpdateRewards;
    private void Start()
    {
    }
    void PurchaseFail(IAPItem iAPItem)
    {
        
    }
    public void RestorePurchase()
    {
        OneButtonPopupSo.PurchaseRestored.Invoke();
    }
    //add coin fun in reward granted, set pref fun in prefkeys
    public void RewardedAdPackSuccess()
    {
        RewardGranted.NoOfCoins += 100;
        PlayerPrefs.SetInt(PrefKeys.Coins, RewardGranted.NoOfCoins);
        PlayerPrefs.Save();
    }
    public void RewardedCoinPack()
    {
        AdSystem.RewardAction = RewardedAdPackSuccess;
        AdSystem.OnRewardedAdPack();
    }
    public void RemoveAds()
    {
        IAPShop.PurchaseItemWithId(IAPShop.RewardItems[0].SKU, this);
    }
    public void SmallCoinPack()
    {
        IAPShop.PurchaseItemWithId(IAPShop.RewardItems[1].SKU, this);
    }
    public void LargeCoinPack()
    {
        IAPShop.PurchaseItemWithId(IAPShop.RewardItems[2].SKU, this);
    }
    public void SpecialCoinPack()
    {
        IAPShop.PurchaseItemWithId(IAPShop.RewardItems[3].SKU, this);
    }

    public void PurchaseSuccess(IAPItem iAPItem)
    {
        foreach (var pair in iAPItem.Rewards)
        {
            if(pair.RewardType == RewardType.Coins)
            {
                RewardGranted.NoOfCoins += pair.Amount;
                PlayerPrefs.SetInt(PrefKeys.Coins, RewardGranted.NoOfCoins);
                PlayerPrefs.Save();
            }
            else if(pair.RewardType == RewardType.Skips)
            {
                RewardGranted.NoOfSkips += pair.Amount;
                PlayerPrefs.SetInt(PrefKeys.Skips, RewardGranted.NoOfSkips);
                PlayerPrefs.Save();
            }
            else if(pair.RewardType == RewardType.RemoveAds)
            {
                RewardGranted.RemoveAds = true;
                PlayerPrefs.SetInt(PrefKeys.RemoveAds, 1);
                PlayerPrefs.Save();
            }
        }
        OneButtonPopupSo.PurchaseSuccessfull.Invoke();
        UpdateRewards.Invoke();
    }

    void IItemPurchase.PurchaseFail(IAPItem iAPItem)
    {
        OneButtonPopupSo.PurchaseUnsuccessfull.Invoke();
        //throw new System.NotImplementedException();
    }
}
