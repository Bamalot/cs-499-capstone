
using GlmSharp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Capstone
{
    public class SceneManager
    {
        List<Texture> textures = new List<Texture>();
        List<Material> materials = new List<Material>();
        ShaderManager shaderManager;
        Mesh boxMesh;
        Mesh planeMesh;
        Scene currentScene;

        public SceneManager(ShaderManager shaderManager)
        {
            this.shaderManager = shaderManager;
            boxMesh = Mesh.CreateBoxMesh();
            planeMesh = Mesh.CreatePlaneMesh();
            currentScene = new Scene();
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
        public void CreateNewScene()
        {
            currentScene = new Scene();
        }
        public void RenderScene()
        {
            Vector3 position = new Vector3(0.0f, 1.0f, 0.0f);
            Vector3 rotation = new Vector3(0f, 0f, 0f);
            Vector3 scale = new Vector3(1f, 1f, 1f);
            
            for(int x = 0;x < Scene.SCENE_SIZE;x++)
            {
                for(int y = 0;y < Scene.SCENE_SIZE;y++)
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