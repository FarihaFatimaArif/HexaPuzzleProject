using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/AdSystemSO", order = 1, fileName = "AdSystem")]
public class AdSystem : ScriptableObject
{
    [SerializeField] RewardGranted RewardGranted;
    // [SerializeField] GameObject Popup;
    [SerializeField] OneButtonPopupSo OneButtonPopupSo;
    private const string MaxKey = "hlKffQFn1sKXRefAUUKG4o-i-OOURETonfImCKvE29oyDwftIiyhVZMlNNxwUFl8NgUmynX33XOEq5m09yb34Z";
    private const string RewardedAdUnit = "585f249ad115c420";
    private const string InterstitialAdUnit = "7d62e5180461f57a";
    private const string BannerAdUnit = "b56d58800dadb2d1";
    // bool counter = true;
    float startTime;
    float endTime;
    float difference;
    public UnityAction RewardAction;
    //public UnityEvent RewardedAdDone;
    //public UnityEvent ExitedAdDone;
    public void InitializingAdSystem()
    {
        startTime = Time.time;
        //difference = 30;
        Debug.Log("here");
        IntializingSdk();
        MaxSdk.LoadInterstitial(InterstitialAdUnit);
        MaxSdk.LoadRewardedAd(RewardedAdUnit);
        //StartCoroutine(Counter());
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialsAdClosed;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdClosed;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        //MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdDisplayFailed;
        //MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailed;

        //MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdDisplayFailed;
       // MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialAdLoadFailed;
        // MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailed;

    }
    //void OnRewardedAdDisplayFailed(string adUnitId, MaxSdk.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    //{
    //    OneButtonPopupSo.AdNotLoaded.Invoke();
    //}
    //void OnRewardedAdLoadFailed(string adUnitId, MaxSdk.ErrorInfo errorInfo)
    //{
    //    OneButtonPopupSo.AdNotLoaded.Invoke();
    //}
    //void OnInterstitialAdDisplayFailed(string adUnitId, MaxSdk.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    //{
    //    OneButtonPopupSo.AdNotLoaded.Invoke();
    //}
    //void OnInterstitialAdLoadFailed(string adUnitId, MaxSdk.ErrorInfo errorInfo)
    //{
    //    OneButtonPopupSo.AdNotLoaded.Invoke();
    //}
    void OnInterstitialsAdClosed(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        startTime = Time.time;
        MaxSdk.LoadInterstitial(InterstitialAdUnit);
        Debug.Log("here");
    }
    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        startTime = Time.time;
        //OneButtonPopupSo.RewardGranted.Invoke();
        if (RewardAction != null)
        {
            RewardAction();
        }
        //OneButtonPopupSo.RewardGranted?.Invoke();
        //Popup.SetActive(true);
    }

    void OnRewardedAdClosed(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        startTime = Time.time;
        MaxSdk.LoadRewardedAd(RewardedAdUnit);
        OneButtonPopupSo.RewardGranted?.Invoke();
    }
        public void IntializingSdk()
    {
        string[] adUnitIds = {
            // rewarded
            RewardedAdUnit,
            // interstitial
            InterstitialAdUnit,
            // banner
            BannerAdUnit
        };
        MaxSdk.SetSdkKey(MaxKey);
        MaxSdk.SetUserId(SystemInfo.deviceUniqueIdentifier);
        MaxSdk.SetVerboseLogging(true);
        MaxSdkCallbacks.OnSdkInitializedEvent += OnMaxInitialized;
        MaxSdk.InitializeSdk(adUnitIds);

    }
    private void OnMaxInitialized(MaxSdkBase.SdkConfiguration sdkConfiguration)
    {
        if (MaxSdk.IsInitialized())
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            MaxSdk.ShowMediationDebugger();
#endif
            Debug.Log("MaxSDK initialized");
        }
        else
        {
            Debug.Log("Failed to init MaxSDK");
        }
    }

    public void OnQuitAd()
    {
        if(MaxSdk.IsInitialized() && Counter()==true && RewardGranted.RemoveAds!=true)
        {
            if (MaxSdk.IsInterstitialReady(InterstitialAdUnit))
            {
                MaxSdk.ShowInterstitial(InterstitialAdUnit);
            }
        }
        startTime = Time.time;
       // ExitedAdDone.Invoke();
    }
    public void OnRewardAdSkip()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OneButtonPopupSo.AdNotLoaded.Invoke();
        }
        else if (MaxSdk.IsInitialized())
        {
            if (RewardGranted.NoOfSkips == 0)
            {
                if (MaxSdk.IsRewardedAdReady(RewardedAdUnit))
                {
                    MaxSdk.ShowRewardedAd(RewardedAdUnit);
                }
            }
            else
            {
                if (RewardAction != null)
                {
                    //Debug.Log("1st skip");
                    RewardGranted.NoOfSkips--;
                    RewardAction();
                }
                //RewardGranted.NoOfSkips--;
            }
        }
        //else
        //{
        //    OneButtonPopupSo.AdNotLoaded.Invoke();
        //}
        
    }
    public void OnRewardedAdPack()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OneButtonPopupSo.AdNotLoaded.Invoke();
        }
        else if (MaxSdk.IsInitialized())
        {
            if (MaxSdk.IsRewardedAdReady(RewardedAdUnit))
            {
                MaxSdk.ShowRewardedAd(RewardedAdUnit);
            }
        }
    }
    bool Counter()
    {
        endTime = Time.time;
        difference = endTime - startTime;
       if (difference>=30)
        {
            return true;
        }
        return false;
    }

}