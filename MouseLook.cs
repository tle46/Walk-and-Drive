using UnityEngine;

namespace Walk_and_Drive;

internal class MouseLook : MonoBehaviour
{
	public enum RotationAxes
	{
		MouseXAndY,
		MouseX,
		MouseY
	}

	public RotationAxes axes = RotationAxes.MouseXAndY;

	public float sensitivityX = 30f;

	public float sensitivityY = 30f;

	public float minimumX = -360f;

	public float maximumX = 360f;

	public float minimumY = -45f;

	public float maximumY = 60f;

	private float rotationY = 0f;

	private void Update()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		if (axes == RotationAxes.MouseXAndY)
		{
			float num = ((Component)this).get_transform().get_localEulerAngles().y + Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
			((Component)this).get_transform().set_localEulerAngles(new Vector3(0f - rotationY, num, 0f));
		}
		else if (axes == RotationAxes.MouseX)
		{
			((Component)this).get_transform().Rotate(0f, Input.GetAxis("Mouse X") * sensitivityX * Time.get_deltaTime(), 0f);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY * Time.get_deltaTime();
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
			((Component)this).get_transform().set_localEulerAngles(new Vector3(0f - rotationY, ((Component)this).get_transform().get_localEulerAngles().y, 0f));
		}
	}
}
