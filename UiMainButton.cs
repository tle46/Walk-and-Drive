using ColossalFramework.UI;
using UnityEngine;

namespace Walk_and_Drive;

internal class UiMainButton : UIButton
{
	private UIComponent BulldoserButton
	{
		get
		{
			UIComponent val = ((UIComponent)this).GetUIView().FindUIComponent<UIComponent>("MarqueeBulldozer");
			if ((Object)(object)val == (Object)null)
			{
				val = ((UIComponent)this).GetUIView().FindUIComponent<UIComponent>("BulldozerButton");
			}
			return val;
		}
	}

	public override void Start()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		((Object)this).set_name("WalknDrive");
		((UIInteractiveComponent)this).set_normalFgSprite("InfoIconTrafficCongestion");
		((UIInteractiveComponent)this).set_hoveredFgSprite("InfoIconTrafficCongestionHovered");
		((UIComponent)this).set_playAudioEvents(true);
		((UIComponent)this).set_size(new Vector2(42f, 42f));
		((UIComponent)this).set_absolutePosition(Vector2.op_Implicit(new Vector2(BulldoserButton.get_absolutePosition().x - 48f, BulldoserButton.get_parent().get_absolutePosition().y + 50f)));
	}

	protected override void OnClick(UIMouseEventParameter p)
	{
		UiMain.ToggleSettings();
		if (UiMain.visible)
		{
			((UIInteractiveComponent)this).set_normalFgSprite("InfoIconTrafficCongestionFocused");
		}
		else
		{
			((UIInteractiveComponent)this).set_normalFgSprite("InfoIconTrafficCongestion");
		}
	}
}
