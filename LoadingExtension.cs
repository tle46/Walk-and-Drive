using ColossalFramework.UI;
using ICities;

namespace Walk_and_Drive;

public class LoadingExtension : ILoadingExtension
{
	public void OnCreated(ILoading loading)
	{
	}

	public void OnLevelLoaded(LoadMode mode)
	{
		ExceptionPanel val = UIView.get_library().ShowModal<ExceptionPanel>("ExceptionPanel");
		val.SetMessage("Test test", "fag please loading", false);
		UIView aView = UIView.GetAView();
		aView.AddUIComponent(typeof(UiMain));
		aView.AddUIComponent(typeof(UiMainButton));
	}

	public void OnLevelUnloading()
	{
	}

	public void OnReleased()
	{
	}
}
