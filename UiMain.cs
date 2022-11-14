using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace Walk_and_Drive;

public class UiMain : UIPanel
{
	public static UIPanel panel;

	public static bool visible;

	public override void Start()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		float width = 275f;
		panel = ((UIComponent)this).AddUIComponent<UIPanel>();
		UIHelper val = new UIHelper((UIComponent)(object)panel);
		((UIComponent)this).set_width(0f);
		UIHelperBase val2 = val.AddGroup(" \r\n  Walk n' Drive!");
		object self = ((UIHelper)((val2 is UIHelper) ? val2 : null)).get_self();
		((UIComponent)((self is UIComponent) ? self : null)).set_width(width);
		val2.AddButton("Select Spawn Location", new OnButtonClicked(LocationSelect));
		val2.AddButton("Spawn", new OnButtonClicked(OnEnterFPM));
		val2.AddButton("RemoveVehicles", new OnButtonClicked(OnDeletevehicleGO));
		panel.set_backgroundSprite("GenericTabDisabled");
		((UIComponent)panel).set_width(width);
		((UIComponent)panel).set_height(200f);
		((UIComponent)panel).set_relativePosition(new Vector3(675f, 250f));
		((UIComponent)panel).set_isVisible(false);
	}

	private void empty(string text)
	{
	}

	public static void ToggleSettings()
	{
		((UIComponent)panel).set_isVisible(!((UIComponent)panel).get_isVisible());
		if (((UIComponent)panel).get_isVisible())
		{
			visible = true;
		}
		else
		{
			visible = false;
		}
	}

	private void LocationSelect()
	{
	}

	private void OnEnterFPM()
	{
		Player player = new Player();
		player.player.SetActive(true);
		((UIComponent)panel).set_isVisible(false);
		ToggleSettings();
	}

	private void OnDeletevehicleGO()
	{
	}
}
