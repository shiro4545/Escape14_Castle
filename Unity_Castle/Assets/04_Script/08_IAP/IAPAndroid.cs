using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.UI;

public class IAPAndroid : MonoBehaviour, IStoreListener
{

	static IStoreController storeController;                            // Purchasing システムの参照
	static IExtensionProvider storeExtensionProvider;                   // 拡張した場合のPurchasing サブシステムの参照
	static string productIDNonConsumable = "nonconsumable";             // 非消費型製品の汎用ID
	static string productNameGooglePlayNonConsumable = "purchasing.nonconsumable"; // Google Play Store identifier for the non-consumable product.

	public enum PURCHASE_STATE
	{
		NOT_PURCHASED = 0,
		PURCHASED = 1,
		PENDING = 2,
	};
	PURCHASE_STATE purchaseState = 0;

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
			return;
		}
		var module = StandardPurchasingModule.Instance();
		var builder = ConfigurationBuilder.Instance(module);
		builder.AddProduct(productIDNonConsumable, ProductType.NonConsumable, new IDs()
			{
				{ productNameGooglePlayNonConsumable,  GooglePlay.Name }
			});
#if false
		/* for clearing consumable item for android */
		builder.AddProduct(productIDNonConsumable, ProductType.Consumable, new IDs()
			{
				{ productNameGooglePlayNonConsumable,  GooglePlay.Name }

			});
#endif
		UnityPurchasing.Initialize(this, builder);
	}
	public void OnInitializeFailed(InitializationFailureReason error)
	{
		/* 初期化失敗時の処理 */
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	private bool IsInitialized()
	{
		return storeController != null && storeExtensionProvider != null;
	}

	//****************************************************************************

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
					//購入処理
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

	//
	// --- IStoreListener
	//
	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");
		// Overall Purchasing system, configured with products for this application.
		storeController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		storeExtensionProvider = extensions;
		// レシートの検証
		if (storeController.products.all[0].hasReceipt)
		{
			//レシートあり
			purchaseState = checkGoogleReceipt(storeController.products.all[0].receipt);
		}
		else
		{
			// レシートなし
			purchaseState = PURCHASE_STATE.NOT_PURCHASED;
		}
		isInitialized = true;
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
			checkGoogleReceipt(args.purchasedProduct.receipt);

		}
		else
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}
		return PurchaseProcessingResult.Complete;
	}

	/// <summary>
    /// 購入失敗時
    /// </summary>
    /// <param name="product"></param>
    /// <param name="failureReason"></param>
	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		/* 購入失敗時の処理 */
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}



	PURCHASE_STATE checkGoogleReceipt(string receipt)
	{
		PURCHASE_STATE resultstate;
		// エディターの難読化ウィンドウで準備した機密を持つ
		// バリデーターを準備します。
		var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
			AppleTangle.Data(), Application.identifier);

		try
		{
			// Google Play で、result は 1 つの product ID を取得します
			// Apple stores で、receipts には複数のプロダクトが含まれます
			var result = validator.Validate(receipt);
			// 情報提供の目的で、ここにレシートをリストします
			Debug.Log("Receipt is valid. Contents:");

			GooglePurchaseState lastGoogleResult = GooglePurchaseState.Cancelled;
			DateTime lastData = DateTime.Today;
			bool setdata = false;
			foreach (IPurchaseReceipt productReceipt in result)
			{
				Debug.Log(productReceipt.productID);
				Debug.Log(productReceipt.purchaseDate);
				Debug.Log(productReceipt.transactionID);
				GooglePlayReceipt google = productReceipt as GooglePlayReceipt;
				if (null != google)
				{
					Debug.Log(google.purchaseState);
					if (!setdata)
					{
						setdata = true;
						lastData = productReceipt.purchaseDate;
						lastGoogleResult = google.purchaseState;
					}
					else
					{
						if (lastData < productReceipt.purchaseDate)
						{
							lastData = productReceipt.purchaseDate;
							lastGoogleResult = google.purchaseState;
						}
					}
				}
			}
			if (lastGoogleResult == GooglePurchaseState.Purchased)
			{
				resultstate = PURCHASE_STATE.PURCHASED;
			}
			else if ((int)lastGoogleResult == 4)
			{
				resultstate = PURCHASE_STATE.PENDING;
			}
			else
			{
				resultstate = PURCHASE_STATE.NOT_PURCHASED;
			}
		}
		catch (IAPSecurityException)
		{
			Debug.Log("Invalid receipt, not unlocking content");
			resultstate = PURCHASE_STATE.NOT_PURCHASED;
		}
		return resultstate;
	}


	/// <summary>
    /// ローカル価格の取得
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

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}