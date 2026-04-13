
using GlmSharp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Capstone
{
    public class SceneManager
    {
        List<Texture> textures = new List<Texture>();
        List<Material> materials = new List<Material>();
        ShaderManager shaderManager;
        DatabaseManager databaseManager;
        Mesh boxMesh;
        Mesh planeMesh;
        Scene currentScene;

        public SceneManager(ShaderManager shaderManager)
        {
            this.shaderManager = shaderManager;
            boxMesh = Mesh.CreateBoxMesh();
            planeMesh = Mesh.CreatePlaneMesh();
            databaseManager = new DatabaseManager();
            currentScene = new Scene(HeightMap.GenerateNewMap((int)Keys.D1));
        }
        public void SetTransformations(Vector3 scaleVector, float rotationX, float rotationY, float rotationZ, Vector3 positionVector)
        {
            // Sets scale
            Matrix4 scale = Matrix4.CreateScale(scaleVector);
            // Applies rotation
            Matrix4 rotX = Matrix4.CreateRotationX(glm.Radians(rotationX));
            Matrix4 rotY = Matrix4.CreateRotationY(glm.Radians(rotationY));
            Matrix4 rotZ = Matrix4.CreateRotationZ(glm.Radians(rotationZ));
            // Sets position
            Matrix4 translation = Matrix4.CreateTranslation(positionVector);
            // Puts all matrices together into transformation matrix
            Matrix4 modelView = translation * rotZ * rotY * rotX * scale;
            shaderManager.SetModelMatrix(modelView);
        }
        public void SaveScene()
        {
            databaseManager.StoreHeightMap(currentScene.GetHeightMap());
        }
        public void LoadScene(int slot)
        {
            HeightMap? heightMap = databaseManager.GetStoredHeightMap(slot);
            if(heightMap != null)
            {
                currentScene = new Scene(heightMap);
            }
            else
            {
                Console.WriteLine("No height map saved for this slot");
            }
            
        }
        public void SetSlot(int slot)
        {
            currentScene.GetHeightMap().Slot = slot;
        }
        public void CreateNewScene()
        {
            currentScene = new Scene(HeightMap.GenerateNewMap(currentScene.GetHeightMap().Slot));
        }
        public void RenderScene()
        {
            Vector3 position = new Vector3(0.0f, 1.0f, 0.0f);
            Vector3 rotation = new Vector3(0f, 0f, 0f);
            Vector3 scale = new Vector3(1f, 1f, 1f);
            
            for(int x = 0;x < HeightMap.HEIGHT_MAP_SIZE;x++)
            {
                for(int y = 0;y < HeightMap.HEIGHT_MAP_SIZE;y++)
                {
                    int highestHeight = currentScene.GetHeight(x, y);
                    for(int height = 0; height <= highestHeight;height++)
                    {
                        position = new Vector3(x, height, y);
                        scale = new Vector3(1f, 1f, 1f);
                        if(height <= 6)
                        {
                            shaderManager.SetShaderColor(0f, 0f, 1f, 1f);
                        }
                        else if(height == 7)
                        {
                            shaderManager.SetShaderColor(0.6f, 0.6f, 0f, 1f);
                        }
                        else
                        {
                            shaderManager.SetShaderColor(0f, 0.8f, 0f, 1f);
                        }
                        SetTransformations(scale, rotation.X, rotation.Y, rotation.Z, position);
                        boxMesh.Draw();
                    }

                }
            }
        }
        // Binds each texture to a different slot
        private void BindTextures()
        {
            for(int i = 0;i < textures.Count;i++)
            {
                GL.ActiveTexture(TextureUnit.Texture0+i);
                textures[i].Bind();
            }
        }
        // Finds a texture ID based on the tag
        private int FindTextureID(string tag)
        {
            foreach(Texture texture in textures)
            {
                if(texture.GetTag() == tag)
                {
                    return texture.GetID();
                }
            }
            return -1;
        }
        // Finds a texture slot based on the tag
        private int FindTextureSlot(string tag)
        {
            for(int i = 0;i < textures.Count;i++)
            {
                if(textures[i].GetTag() == tag)
                {
                    return i;
                }
            }
            return -1;
        }
        private Material? FindMaterial(string tag)
        {
            foreach(Material material in materials)
            {
                if(material.Tag.Equals(tag))
                {
                    return material;
                }
            }
            return null;
        }
    }
}