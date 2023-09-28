using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.UI;

public class IAPiOS : MonoBehaviour, IStoreListener
{

	static IStoreController storeController;                        // Purchasing システムの参照
	static IExtensionProvider storeExtensionProvider;               // 拡張した場合のPurchasing サブシステムの参照
	static string productIDNonConsumable = "nonconsumable";         // 非消費型製品の汎用ID
	static string productNameAppleNonConsumable = "Item01";       // Apple App Store identifier for the non-consumable product.

	public UIManager UI;

	public enum PURCHASE_STATE
	{
		NOT_PURCHASED = 0,
		PURCHASED = 1,
		PENDING = 2,
	};
	bool isInitialized = false;

	void Awake()
	{
		if (storeController == null)
		{
			// 初期化
			InitializePurchasing();
		}
	}

	/// <summary>
    /// 初期化処理
    /// </summary>
	public void InitializePurchasing()
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized())
		{
			// ... we are done here.
			return;
		}
		var module = StandardPurchasingModule.Instance();
		var builder = ConfigurationBuilder.Instance(module);
		builder.AddProduct(productIDNonConsumable, ProductType.NonConsumable, new IDs()
			{
				{ productNameAppleNonConsumable,       AppleAppStore.Name },
			});
		UnityPurchasing.Initialize(this, builder);
	}

	public bool IsInitialized()
	{
		return storeController != null && storeExtensionProvider != null;
	}


	/// <summary>
    /// 購入処理
    /// </summary>
	public void BuyNonConsumable()
	{
		BuyProductID(productIDNonConsumable);
	}

	public void BuyProductID(string productId)
	{
		try
		{
			if (IsInitialized())
			{
				Product product = storeController.products.WithID(productId);
				if (product != null && product.availableToPurchase)
				{
					Debug.Log(string.Format("Purchasing product asychronously: '{0}' - '{1}'", product.definition.id, product.definition.storeSpecificId));
					storeController.InitiatePurchase(product);
				}
				// Otherwise ...
				else
				{
					Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			else
			{
				Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}
		catch (Exception e)
		{
			Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
		}
	}

	/// <summary>
    /// 復元処理
    /// </summary>
	public void RestorePurchases()
	{
		if (!IsInitialized())
		{
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			UI.SpinnerPanel.SetActive(false);
			return;
		}
		// RestorePurchases started
		Debug.Log("RestorePurchases started ...");
		// Fetch the Apple store-specific subsystem.
		var apple = storeExtensionProvider.GetExtension<IAppleExtensions>();
		// Begin the asynchronous process of restoring purchases. Expect a confirmation response in the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
		apple.RestoreTransactions((result) => {
			// The first phase of restoration. If no more responses are received on ProcessPurchase then no purchases are available to be restored.
			Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
		});
		UI.SpinnerPanel.SetActive(false);
	}

	//
	PURCHASE_STATE checkAppleReceipt(string receipt)
	{
		PURCHASE_STATE resultstate = PURCHASE_STATE.NOT_PURCHASED;
		var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
			AppleTangle.Data(), Application.identifier);

		try
		{
			var result = validator.Validate(receipt);
			Debug.Log("Receipt is valid. Contents:");
			foreach (IPurchaseReceipt productReceipt in result)
			{
				Debug.Log(productReceipt.productID);
				Debug.Log(productReceipt.purchaseDate);
				Debug.Log(productReceipt.transactionID);
				AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;
				if (null != apple)
				{
					Debug.Log(apple.originalTransactionIdentifier);
					Debug.Log(apple.subscriptionExpirationDate);
					Debug.Log(apple.cancellationDate);
					Debug.Log(apple.quantity);
					resultstate = PURCHASE_STATE.PURCHASED;
				}
				else
				{
					resultstate = PURCHASE_STATE.NOT_PURCHASED;
				}
			}
		}
		catch (IAPSecurityException)
		{
			Debug.Log("Invalid receipt, not unlocking content");
			resultstate = PURCHASE_STATE.NOT_PURCHASED;
		}
		return resultstate;
	}

	public PURCHASE_STATE GetPurchaseState()
	{
		PURCHASE_STATE resultstate;
		if (storeController != null)
		{
			if (storeController.products.all[0].hasReceipt)
			{
				resultstate = checkAppleReceipt(storeController.products.all[0].receipt);
			}
			else
			{
				resultstate = PURCHASE_STATE.NOT_PURCHASED;
			}
		}
		else
		{
			resultstate = PURCHASE_STATE.NOT_PURCHASED;
		}
		return resultstate;
	}

	//
	// --- IStoreListener
	/// <summary>
    /// 購入失敗処理
    /// </summary>
    /// <param name="product"></param>
    /// <param name="failureReason"></param>
	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
		UI.SpinnerPanel.SetActive(false);
	}


	/// <summary>
    /// ローカル価格取得
    /// </summary>
    /// <returns></returns>
	public string GetlocalizedPriceString()
	{
		string retstr = "";
		if (storeController != null)
		{
			byte[] bytesData = System.Text.Encoding.UTF8.GetBytes(storeController.products.all[0].metadata.localizedPriceString);
			if (bytesData[0] == 0xC2 && bytesData[1] == 0xA5)
			{
				retstr = "\\";
				retstr += storeController.products.all[0].metadata.localizedPriceString.Substring(1, storeController.products.all[0].metadata.localizedPriceString.Length - 1);
				retstr += "(" + storeController.products.all[0].metadata.isoCurrencyCode + ")";
			}
			else
			{
				retstr = storeController.products.all[0].metadata.localizedPriceString;
				retstr += "(" + storeController.products.all[0].metadata.isoCurrencyCode + ")";
			}
		}
		else
		{
			retstr = null;

		}
		return retstr;
	}

	/// <summary>
    /// 初期化失敗
    /// </summary>
    /// <param name="error"></param>
	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}


	/// <summary>
	/// 購入成功時
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		if (String.Equals(args.purchasedProduct.definition.id, productIDNonConsumable, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			checkAppleReceipt(args.purchasedProduct.receipt);
		}
		else
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}
		//ゲーム側の処理
		Success();
		return PurchaseProcessingResult.Complete;
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		Debug.Log("OnInitialized: PASS");
		storeController = controller;
		storeExtensionProvider = extensions;
		var apple = storeExtensionProvider.GetExtension<IAppleExtensions>();

		if (storeController.products.all[0].hasReceipt)
		{
#if false
			Debug.Log("RefreshAppReceipt: START");
			apple.RefreshAppReceipt(result => {
				Debug.Log("RefreshAppReceipt OK");
				if (storeController.products.all[0].hasReceipt) {
					checkAppleReceipt(storeController.products.all[0].receipt);
				}
				isInitialized = true;
			},
			()=> {
				Debug.Log("RefreshAppReceipt Error");
				if (storeController.products.all[0].hasReceipt) {
					checkAppleReceipt(storeController.products.all[0].receipt);
				}
				isInitialized = true;
			});
#else
			checkAppleReceipt(storeController.products.all[0].receipt);
#endif
		}
		else
		{
			isInitialized = true;
		}
	}

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }

	//購入・復元成功時のゲーム処理
	private void Success()
	{
		GoogleAds.Instance.unRequestBanner();
		UI.SpinnerPanel.SetActive(false);
		UI.PurchasePanel.SetActive(false);
		SaveLoadSystem.Instance.gameData.isPurchase = true;
		SaveLoadSystem.Instance.Save();
	}
}