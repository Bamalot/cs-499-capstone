using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Capstone
{
    public class ShaderManager
    {
        const string MODEL_VARIABLE_NAME = "model";
        const string VIEW_VARIABLE_NAME = "view";
        const string PROJECTION_VARIABLE_NAME = "projection";
        const string VIEW_POSITION_VARIABLE_NAME = "viewPosition";

        const string COLOR_VARIABLE_NAME = "objectColor";
        const string TEXTURE_VARIABLE_NAME = "objectTexture";
        const string USE_TEXTURE_VARIABLE_NAME = "bUseTexture";
        const string USE_LIGHTING_VARIABLE_NAME = "bUseLighting";
        
        const string UV_SCALE_VARIABLE_NAME = "UVScale";
        const string MATERIAL_DIFFUSE_COLOR_VARIABLE_NAME = "material.diffuseColor";
        const string MATERIAL_SPECULAR_COLOR_VARIABLE_NAME = "material.specularColor";
        const string MATERIAL_SHININESS_VARIABLE_NAME = "material.shininess";

        int colorVariableLocation;
        int modelVariableLocation;
        int viewVariableLocation;
        int viewPositionVariableLocation;
        int projectionVariableLocation;
        int textureVariableLocation;
        int useTextureVariableLocation;
        int useLightingVariableLocation;
        int uvScaleVariableLocation;
        int materialDiffuseVariableLocation;
        int materialSpecularVariableLocation;
        int materialShininessVariableLocation;

        int shaderProgram;
        public ShaderManager()
        {
            
        }

        public void LoadShaders(string vertexFileName, string fragmentFileName)
        {
            try
            {
                // Create shader objects
                int vertexShader = GL.CreateShader(ShaderType.VertexShader);
                int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
                shaderProgram = GL.CreateProgram();

                GL.ShaderSource(vertexShader, File.ReadAllText(vertexFileName));
                GL.CompileShader(vertexShader);

                GL.ShaderSource(fragmentShader, File.ReadAllText(fragmentFileName));
                GL.CompileShader(fragmentShader);

                Console.WriteLine(GL.GetShaderInfoLog(vertexShader));
                Console.WriteLine(GL.GetShaderInfoLog(fragmentShader));

                GL.AttachShader(shaderProgram, vertexShader);
                GL.AttachShader(shaderProgram, fragmentShader);

                GL.LinkProgram(shaderProgram);
                
                // Checks for errors in OpenGL itself
                if(GL.GetError() != ErrorCode.NoError)
                {
                    throw new Exception($"OpenGL Error Code: {GL.GetError()}");
                }
                modelVariableLocation = GL.GetUniformLocation(shaderProgram, MODEL_VARIABLE_NAME);
                viewVariableLocation = GL.GetUniformLocation(shaderProgram, VIEW_VARIABLE_NAME);
                viewPositionVariableLocation = GL.GetUniformLocation(shaderProgram, VIEW_POSITION_VARIABLE_NAME);
                projectionVariableLocation = GL.GetUniformLocation(shaderProgram, PROJECTION_VARIABLE_NAME);
                colorVariableLocation = GL.GetUniformLocation(shaderProgram, COLOR_VARIABLE_NAME);
                useTextureVariableLocation = GL.GetUniformLocation(shaderProgram, USE_TEXTURE_VARIABLE_NAME);
                useLightingVariableLocation = GL.GetUniformLocation(shaderProgram, USE_LIGHTING_VARIABLE_NAME);
                textureVariableLocation = GL.GetUniformLocation(shaderProgram, TEXTURE_VARIABLE_NAME);
                uvScaleVariableLocation = GL.GetUniformLocation(shaderProgram, UV_SCALE_VARIABLE_NAME);
                materialDiffuseVariableLocation = GL.GetUniformLocation(shaderProgram, MATERIAL_DIFFUSE_COLOR_VARIABLE_NAME);
                materialSpecularVariableLocation = GL.GetUniformLocation(shaderProgram, MATERIAL_SPECULAR_COLOR_VARIABLE_NAME);
                materialShininessVariableLocation = GL.GetUniformLocation(shaderProgram, MATERIAL_SHININESS_VARIABLE_NAME);

            } catch(Exception e)
            {
                Console.WriteLine($"Error while loading shaders:\n {e.Message}");
            }

        }
        public void SetModelMatrix(Matrix4 matrix)
        {
            GL.UniformMatrix4(modelVariableLocation, false, ref matrix);
        }
        public void SetViewMatrix(Matrix4 matrix)
        {
            GL.UniformMatrix4(viewVariableLocation, false, ref matrix);
        }
        public void SetProjectionMatrix(Matrix4 matrix)
        {
            GL.UniformMatrix4(projectionVariableLocation, false, ref matrix);
        }
        public void SetViewPosition(Vector3 position)
        {
            GL.Uniform3(viewPositionVariableLocation, position);
        }
        // Sets the shader's color value
        public void SetShaderColor(float red, float green, float blue, float alpha)
        {
            GL.Uniform4(colorVariableLocation, red, green, blue, alpha);
            GL.Uniform1(useTextureVariableLocation, 0.0);
        }
        public void SetTexture(int textureSlot)
        {
            GL.Uniform1(useTextureVariableLocation, 1.0);
            GL.Uniform1(textureVariableLocation, textureSlot);
        }
        public void SetUVScale(float u, float v)
        {
            GL.Uniform2(uvScaleVariableLocation, new Vector2(u, v));
        }
        public void SetMaterial(Material material)
        {
            // Sets the material variables in the shader
            GL.Uniform3(materialDiffuseVariableLocation, material.DiffuseColor);
            GL.Uniform3(materialSpecularVariableLocation, material.SpecularColor);
            GL.Uniform1(materialShininessVariableLocation, material.Shininess);
        }
        public void UseShaderProgram()
        {
            GL.UseProgram(shaderProgram);
        }
    }
}