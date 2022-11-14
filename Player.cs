using ColossalFramework;
using UnityEngine;

namespace Walk_and_Drive;

internal class Player
{
	public GameObject player = GameObject.CreatePrimitive((PrimitiveType)1);

	public Player()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Invalid comparison between Unknown and I4
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		player.AddComponent<Main>();
		player.AddComponent<PlayerWalk>();
		player.set_layer(2);
		Rigidbody val = player.AddComponent<Rigidbody>();
		player.GetComponent<CapsuleCollider>().set_radius(1f);
		val.set_useGravity(true);
		val.set_maxAngularVelocity(90f);
		val.set_angularDrag(0f);
		val.set_isKinematic(false);
		val.set_constraints((RigidbodyConstraints)112);
		Physics.set_gravity(new Vector3(0f, -5f, 0f));
		((Renderer)player.GetComponent<MeshRenderer>()).set_enabled(false);
		CitizenManager instance = Singleton<CitizenManager>.get_instance();
		bool flag = false;
		CitizenInstance val2 = instance.m_instances.m_buffer[(int)Random.Range(0f, (float)instance.m_instances.m_size)];
		while (!flag)
		{
			if ((int)((CitizenInstance)(ref val2)).get_Info().m_gender == 0 || (int)((CitizenInstance)(ref val2)).get_Info().m_gender == 1)
			{
				flag = true;
			}
			else
			{
				val2 = instance.m_instances.m_buffer[(int)Random.Range(0f, (float)instance.m_instances.m_size)];
			}
		}
		GameObject val3 = new GameObject();
		Mesh lodMesh = ((CitizenInstance)(ref val2)).get_Info().m_lodMesh;
		val3.AddComponent<MeshFilter>().set_mesh(lodMesh);
		((Renderer)val3.AddComponent<MeshRenderer>()).set_material(((CitizenInstance)(ref val2)).get_Info().m_lodMaterial);
		val3.get_transform().set_parent(player.get_transform());
		Transform transform = val3.get_transform();
		Bounds bounds = lodMesh.get_bounds();
		transform.set_localPosition(new Vector3(0f, 0f - ((Bounds)(ref bounds)).get_extents().y, 0f));
	}
}
