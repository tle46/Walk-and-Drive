using ColossalFramework.UI;
using UnityEngine;

namespace Walk_and_Drive;

internal class PlayerWalk : MonoBehaviour
{
	public static float speed;

	private float normalSpeed;

	private float rotSpeed = 2f;

	private Quaternion oldRot;

	public GameObject model;

	public UITextField field;

	public RaycastHit hit;

	private void Start()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		normalSpeed = 400f;
		speed = normalSpeed;
		model = ((Component)((Component)this).GetComponentsInChildren<Transform>()[1]).get_gameObject();
		oldRot = ((Component)this).get_transform().get_rotation();
		UIView aView = UIView.GetAView();
		ref UITextField reference = ref field;
		UIComponent obj = aView.AddUIComponent(typeof(UITextField));
		reference = (UITextField)(object)((obj is UITextField) ? obj : null);
		((UIComponent)field).set_width(300f);
		((Object)field).set_name("InfoField");
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKey((KeyCode)304))
		{
			speed = normalSpeed * 2f;
		}
		else
		{
			speed = normalSpeed;
		}
		Vector3 val = Vector3.get_zero();
		val.y = ((Component)this).GetComponent<Rigidbody>().get_velocity().y;
		if (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f)
		{
			val += ((Component)this).get_transform().get_forward() * (speed * Time.get_deltaTime()) * Input.GetAxis("Vertical");
			Quaternion rotation = ((Component)this).get_transform().get_rotation();
			Vector3 eulerAngles = ((Quaternion)(ref rotation)).get_eulerAngles();
			eulerAngles += new Vector3(0f, rotSpeed * Input.GetAxis("Horizontal"), 0f);
			((Component)this).get_transform().set_rotation(Quaternion.Euler(eulerAngles));
		}
		else
		{
			if (Input.GetKey((KeyCode)119))
			{
				val += ((Component)this).get_transform().get_forward() * (speed * Time.get_deltaTime());
				oldRot = ((Component)this).get_transform().get_rotation();
			}
			if (Input.GetKey((KeyCode)115))
			{
				val -= ((Component)this).get_transform().get_forward() * (speed * Time.get_deltaTime());
			}
			if (Input.GetKey((KeyCode)100))
			{
				val += ((Component)this).get_transform().get_right() * (speed * Time.get_deltaTime());
			}
			if (Input.GetKey((KeyCode)97))
			{
				val -= ((Component)this).get_transform().get_right() * (speed * Time.get_deltaTime());
			}
		}
		if (Input.GetKeyDown((KeyCode)32) && Physics.Raycast(new Ray(((Component)this).get_transform().get_position(), -((Component)this).get_transform().get_up()), ((Component)this).GetComponent<CapsuleCollider>().get_height() / 2f + 1f))
		{
			val += ((Component)this).get_transform().get_up() * 20f;
		}
		model.get_transform().set_rotation(oldRot);
		((Component)this).GetComponent<Rigidbody>().set_velocity(val);
	}

	private void OnDestroy()
	{
		UIView aView = UIView.GetAView();
		Object.Destroy((Object)(object)aView.FindUIComponent("InfoField"));
	}
}
