using System.Security.Cryptography;
using NoiseDotNet;

namespace Capstone;

public class Scene
{
    HeightMap heightMap;

    public Scene(HeightMap map)
    {
        heightMap = map;
    }

    public int GetHeight(int x, int y)
    {
        return heightMap.GetValue(x, y);
    }
    public HeightMap GetHeightMap()
    {
        return heightMap;
    }
}