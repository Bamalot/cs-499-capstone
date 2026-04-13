using System.Security.Cryptography;
using NoiseDotNet;

namespace Capstone;

public class Scene
{
    public const int SCENE_SIZE = 32;
    const float NOISE_FREQUENCY = 0.1f;
    const int SCENE_HEIGHT = 8;
    int[,] heightMap = new int[SCENE_SIZE,SCENE_SIZE];
    public Scene()
    {
        GenerateHeightMap();
    }
    // Generates the voxel layout of the scene
    private void GenerateHeightMap()
    {
        heightMap = new int[SCENE_SIZE,SCENE_SIZE];
        // Obtain a seed from the current time
        int seed = (int)DateTime.Now.ToFileTime();

        // Initialize a grid for storing the noise values in
        int gridSize = SCENE_SIZE*SCENE_SIZE;
        float[] xValues = new float[gridSize];
        float[] yValues = new float[gridSize];
        int index = 0;
        for (int y = 0; y < SCENE_SIZE; ++y)
            for (int x = 0; x < SCENE_SIZE; ++x)
            {
                xValues[index] = x;
                yValues[index] = y;
                index++;
            }
        float[] output = new float[gridSize];
        // Calculate the noise
        NoiseSettings settings = new NoiseSettings(NOISE_FREQUENCY, NOISE_FREQUENCY, seed);
        Noise.GradientNoise2D(xValues, yValues, output, settings);


        // Stores the generated heights in the scene's height map
        for(int i = 0;i < index;i++)
        {
            int x = (int)xValues[i];
            int y = (int)yValues[i];
            // Convert the [-1,1] noise value to an actual height in the scene
            int height = (int)Math.Round(SCENE_HEIGHT*output[i] + SCENE_HEIGHT);
            heightMap[x,y] = height;
        }
    }
    public int GetHeight(int x, int y)
    {
        return heightMap[x,y];
    }
}