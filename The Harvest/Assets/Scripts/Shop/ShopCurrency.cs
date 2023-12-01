using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopCurrency : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyText;

    void Update()
    {
        currencyText.text = Singleton.instance.currentShopCurrency.ToString();
    }
}
