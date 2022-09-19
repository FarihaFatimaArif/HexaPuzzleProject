using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateCards : MonoBehaviour
{
    [SerializeField] IAPShop IAPShop;
    Vector2 initialPos;
    GameObject created;
    //[SerializeField] GameObject ShowRewardsPanel;
    //[SerializeField] GameObject CoinsIcon;
    //[SerializeField] TextMeshProUGUI CoinsText;
    //[SerializeField] GameObject SkipsIcon;
    //[SerializeField] TextMeshProUGUI SkipsText;
    //[SerializeField] RewardGranted RewardGranted;
    // Start is called before the first frame update
    void Start()
    {
        //created = Instantiate(CoinsIcon, initialPos, Quaternion.identity);
        //created.transform.SetParent(ShowRewardsPanel.transform);
        //CoinsText = Instantiate(CoinsText, initialPos, Quaternion.identity);
        //CoinsText.transform.SetParent(ShowRewardsPanel.transform);
        //created = Instantiate(SkipsIcon, initialPos, Quaternion.identity);
        //created.transform.SetParent(ShowRewardsPanel.transform);
        //SkipsText = Instantiate(SkipsText, initialPos, Quaternion.identity);
        //SkipsText.transform.SetParent(ShowRewardsPanel.transform);
        initialPos = new Vector2();
        foreach(var pair in IAPShop.RewardItems)
        {
            //GameObject cardCreated;
            created = Instantiate(pair.Card,initialPos, Quaternion.identity);
            created.transform.SetParent(this.transform);
            //transform.SetParent(cardCreated.transform);
        }
    }
    //public void UpdateRewards()
    //{
    //    CoinsText.text = RewardGranted.NoOfCoins.ToString();
    //    SkipsText.text = RewardGranted.NoOfSkips.ToString();
    //}
}
