using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace muskit
{
	public class MasterUI : MonoBehaviour
	{
		// Controllable objects
		private AssetBundle bundle;
		private GameObject asymObject;
		private int curAsymType;
		private readonly string[] asymType = { "AsymFreecam", "AsymChasecam", "AsymDrone" };

		// UI elements
		private Button uiToggleButton;
		private Dropdown uiTypeDropdown;
		private Slider uiMouseSpeedSlider;
		private InputField uiMouseSpeedEntry;

		private bool desktopEnabled = false;

		// Use this for initialization
		void Start()
		{
			uiToggleButton = transform.GetChild(0).GetChild(0).GetComponent<Button>();
			uiToggleButton.onClick.AddListener(ToggleDesktop);
			uiTypeDropdown = transform.GetChild(0).GetChild(1).GetComponent<Dropdown>();
			uiTypeDropdown.onValueChanged.AddListener(SetAsymType);

			uiMouseSpeedSlider = transform.FindDeepChild("MouseSpeed Slider").GetComponent<Slider>();
			uiMouseSpeedSlider.onValueChanged.AddListener(OnMouseSpeedSliderChg);
			uiMouseSpeedEntry = transform.FindDeepChild("MouseSpeed Entry").GetComponent<InputField>();
			uiMouseSpeedEntry.onEndEdit.AddListener(OnMouseSpeedEntryDoneEditing);
			uiMouseSpeedEntry.text = MeatKitPlugin.mouseSpeed.ToString("F");
			uiMouseSpeedSlider.value = MeatKitPlugin.mouseSpeed;

			curAsymType = -1;
			SetDesktopUIEnable(false);
		}

		public void init(AssetBundle bundle)
		{
			this.bundle = bundle;
		}

		private void SetAsymType(int val)
		{
			if (curAsymType == val && asymObject != null)
				return;

			Destroy(asymObject);
			asymObject = Instantiate(bundle.LoadAsset<GameObject>(asymType[val]));
			curAsymType = val;
		}

		public void SetDesktopUIEnable(bool val)
		{
			desktopEnabled = val;
			Text btnText = uiToggleButton.GetComponentInChildren<Text>();
			if (desktopEnabled)
			{
				uiTypeDropdown.gameObject.SetActive(true);
				uiMouseSpeedSlider.gameObject.SetActive(true);
				uiMouseSpeedEntry.gameObject.SetActive(true);
				btnText.text = "Disable Desktop";
				SetAsymType(uiTypeDropdown.value);
			}
			else
			{
				uiTypeDropdown.gameObject.SetActive(false);
				uiMouseSpeedSlider.gameObject.SetActive(false);
				uiMouseSpeedEntry.gameObject.SetActive(false);
				curAsymType = -1;
				Destroy(asymObject);
				asymObject = null;
				btnText.text = "Enable Desktop";
			}
		}

		private void ToggleDesktop()
        {
			SetDesktopUIEnable(!desktopEnabled);
        }

		public void OnMouseSpeedSliderChg(float newValue)
        {
			Debug.Log("[mouse ui] Mouse speed slider changed!");
			MeatKitPlugin.mouseSpeed = newValue;
			uiMouseSpeedEntry.text = newValue.ToString("F");
		}
		
		public void OnMouseSpeedEntryDoneEditing(string newText)
        {
			Debug.Log("[mouse ui] Mouse speed entry changed!");
			try
			{
				float value = float.Parse(newText);
				value = Mathf.Clamp(value, uiMouseSpeedSlider.minValue, uiMouseSpeedSlider.maxValue);
				MeatKitPlugin.mouseSpeed = value;

				uiMouseSpeedEntry.text = value.ToString("F");
				uiMouseSpeedSlider.value = value;
			}
			catch
			{
				uiMouseSpeedEntry.text = uiMouseSpeedSlider.value.ToString("F");
			}
		}
	}
}