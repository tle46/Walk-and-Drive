using ICities;

namespace Walk_and_Drive;

public class TerrainExtend : ITerrainExtension
{
	public static ITerrain terrainManager;

	public void OnCreated(ITerrain terrain)
	{
		terrainManager = terrain;
	}

	public void OnReleased()
	{
	}

	public void OnAfterHeightsModified(float minX, float minZ, float maxX, float maxZ)
	{
	}
}
