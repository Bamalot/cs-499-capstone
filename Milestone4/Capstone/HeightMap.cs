using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NoiseDotNet;
namespace Capstone
{
    [BsonIgnoreExtraElements]
    public class HeightMap
    {
        // Paramters for the height map
        public const int HEIGHT_MAP_SIZE = 32;
        public const int MAX_HEIGHT = 8;
        public const float NOISE_FREQUENCY = 0.1f;
        // Slot the height map is saved into
        public int Slot {get; set;}
        // Height data
        public int[,] Data {get; set;}

        public HeightMap()
        {
            Slot = 0;
            Data = new int[HEIGHT_MAP_SIZE,HEIGHT_MAP_SIZE];
        }
        public static HeightMap GenerateNewMap(int slot)
        {
            var map = new HeightMap();
            map.Slot = slot;
            map.GenerateValues();
            return map;
        }
        void GenerateValues()
        {
            // Obtain a seed from the current time
            int seed = (int)DateTime.Now.ToFileTime();

            // Initialize a grid for storing the noise values in
            int gridSize = HEIGHT_MAP_SIZE*HEIGHT_MAP_SIZE;
            float[] xValues = new float[gridSize];
            float[] yValues = new float[gridSize];
            int index = 0;
            for (int y = 0; y < HEIGHT_MAP_SIZE; ++y)
                for (int x = 0; x < HEIGHT_MAP_SIZE; ++x)
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
                int height = (int)Math.Round(MAX_HEIGHT*output[i] + MAX_HEIGHT);
                SetValue(x, y, height);
            }
        }
        public void SetValue(int x, int y, int value)
        {
            Data[x,y] = value;
        }

        public int GetValue(int x, int y)
        {
            return Data[x,y];
        }
    }
}