using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace Walk_and_Drive;

internal class Main : MonoBehaviour
{
	private Camera cam;

	private Vector3 cameraOffset;

	private Camera origCam;

	private CameraController cc;

	private MouseLook cml;

	private MouseLook gml;

	private SimulationManager sm;

	public float cameraZoom = 0f;

	private readonly int NUM_BUILDING_COLLIDERS = 36;

	private readonly int NUM_VEHICLE_COLLIDERS = 100;

	private readonly int NUM_PARKED_VEHICLE_COLLIDERS = 50;

	private readonly float SCAN_DISTANCE = 300f;

	private bool active;

	private bool vehicleCollidersActive;

	private BuildingManager buildingManager;

	private NetManager netManager;

	private TerrainManager terrainManager;

	private VehicleManager vehicleManager;

	private ColliderContainer[] mBuildingColliders;

	private ColliderContainer[] mVehicleColliders;

	private ColliderContainer[] mParkedVehicleColliders;

	private void Awake()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		netManager = Singleton<NetManager>.get_instance();
		terrainManager = Singleton<TerrainManager>.get_instance();
		buildingManager = Singleton<BuildingManager>.get_instance();
		vehicleManager = Singleton<VehicleManager>.get_instance();
		mBuildingColliders = new ColliderContainer[NUM_BUILDING_COLLIDERS];
		mVehicleColliders = new ColliderContainer[NUM_VEHICLE_COLLIDERS];
		mParkedVehicleColliders = new ColliderContainer[NUM_PARKED_VEHICLE_COLLIDERS];
		for (int i = 0; i < NUM_BUILDING_COLLIDERS; i++)
		{
			ColliderContainer colliderContainer = new ColliderContainer();
			colliderContainer.colliderOwner = new GameObject("buildingCollider" + i);
			colliderContainer.meshCollider = colliderContainer.colliderOwner.AddComponent<MeshCollider>();
			colliderContainer.meshCollider.set_convex(true);
			((Collider)colliderContainer.meshCollider).set_enabled(true);
			mBuildingColliders[i] = colliderContainer;
		}
		for (int j = 0; j < NUM_VEHICLE_COLLIDERS; j++)
		{
			ColliderContainer colliderContainer2 = new ColliderContainer();
			colliderContainer2.colliderOwner = new GameObject("vehicleCollider" + j);
			colliderContainer2.boxCollider = colliderContainer2.colliderOwner.AddComponent<BoxCollider>();
			((Collider)colliderContainer2.boxCollider).set_enabled(true);
			mVehicleColliders[j] = colliderContainer2;
		}
		for (int k = 0; k < NUM_PARKED_VEHICLE_COLLIDERS; k++)
		{
			ColliderContainer colliderContainer3 = new ColliderContainer();
			colliderContainer3.colliderOwner = new GameObject("parkedVehicleCollider" + k);
			colliderContainer3.boxCollider = colliderContainer3.colliderOwner.AddComponent<BoxCollider>();
			((Collider)colliderContainer3.boxCollider).set_enabled(true);
			mParkedVehicleColliders[k] = colliderContainer3;
		}
	}

	private void Start()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		InitCams();
		Vector3 position = ((Component)origCam).get_transform().get_position();
		position.y = TerrainExtend.terrainManager.SampleTerrainHeight(position.x, position.z) + 2f;
		((Component)this).get_gameObject().get_transform().set_position(position);
		Quaternion rotation = ((Component)origCam).get_transform().get_rotation();
		rotation.x = 0f;
		rotation.z = 0f;
		((Component)this).get_gameObject().get_transform().set_rotation(rotation);
	}

	private void Update()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		sm.set_SimulationPaused(false);
		sm.set_SelectedSimulationSpeed(1);
		cameraZoom = Mathf.Clamp(cameraZoom + Input.get_mouseScrollDelta().y, -8f, 0f);
		((Component)cam).get_transform().set_localPosition(cameraOffset + new Vector3(0f, (0f - cameraZoom) / 4f, cameraZoom * 2f));
		Vector3 val = ((Component)this).get_transform().get_forward() * 2f;
		Quaternion rotation;
		if (cc.m_currentPosition.y - ((Component)cam).get_transform().get_position().y > 30f)
		{
			CameraController obj = cc;
			rotation = ((Component)this).get_transform().get_rotation();
			obj.m_targetAngle = new Vector2(((Quaternion)(ref rotation)).get_eulerAngles().y, 45f);
		}
		else
		{
			CameraController obj2 = cc;
			rotation = ((Component)this).get_transform().get_rotation();
			obj2.m_targetAngle = new Vector2(((Quaternion)(ref rotation)).get_eulerAngles().y, 0f);
		}
		RenderManager.set_LevelOfDetail(3);
		cc.m_targetPosition = ((Component)cam).get_transform().get_position() - val;
		cc.m_targetSize = 20f;
		ITerrain val2 = TerrainExtend.terrainManager;
		HandleInput();
		Cursor.set_lockState((CursorLockMode)1);
		if (((Component)this).GetComponent<Rigidbody>().get_position().y < -1f)
		{
			Object.Destroy((Object)(object)((Component)this).get_gameObject());
			ExceptionPanel val3 = UIView.get_library().ShowModal<ExceptionPanel>("ExceptionPanel");
			val3.SetMessage("Error", "Please try spawning in another location", false);
		}
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown((KeyCode)9) || Input.GetKeyDown((KeyCode)27))
		{
			Object.Destroy((Object)(object)((Component)this).get_gameObject());
		}
	}

	private void InitCams()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		origCam = Camera.get_main();
		((Behaviour)origCam).set_enabled(false);
		cc = Object.FindObjectOfType<CameraController>();
		cc.m_unlimitedCamera = true;
		cam = new GameObject("Player Cam").AddComponent<Camera>();
		((Component)cam).get_transform().set_parent(((Component)this).get_gameObject().get_transform());
		((Component)cam).set_tag("MainCamera");
		((Component)cam).get_transform().set_localRotation(Quaternion.Euler(0f, 0f, 0f));
		cam.set_allowHDR(true);
		cam.set_depth(origCam.get_depth() + 1f);
		cam.set_nearClipPlane(1f);
		Camera obj = cam;
		obj.set_cullingMask(obj.get_cullingMask() | (1 << Singleton<RenderManager>.get_instance().get_lightSystem().m_lightLayer));
		cml = ((Component)cam).get_gameObject().AddComponent<MouseLook>();
		cml.axes = MouseLook.RotationAxes.MouseY;
		gml = ((Component)this).get_gameObject().AddComponent<MouseLook>();
		gml.axes = MouseLook.RotationAxes.MouseX;
	}

	private void OnDestroy()
	{
		((Component)origCam).get_gameObject().SetActive(true);
		((Behaviour)origCam).set_enabled(true);
		Cursor.set_lockState((CursorLockMode)0);
		Cursor.set_visible(true);
		cc.m_freeCamera = false;
		cc.m_unlimitedCamera = false;
		Object.Destroy((Object)(object)((Component)this).get_gameObject());
	}
}
