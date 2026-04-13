using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Mathematics;

namespace Capstone
{
    public class Window : GameWindow
    {
        Camera camera;
        ShaderManager shaderManager;
        SceneManager sceneManager;
        float deltaTime;

        public Window(int width, int height, string title)
        : base(GameWindowSettings.Default, new NativeWindowSettings(){ClientSize=(width,height),Title=title,APIVersion=new Version(4,6),Profile=ContextProfile.Core})
        {
            
            shaderManager = new ShaderManager();
            camera = new Camera(shaderManager, width, height);
            // This captures the mouse cursor
            this.CursorState = CursorState.Grabbed;

            // This lets transparent objects be rendered
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            // Enable z-depth
            GL.Enable(EnableCap.DepthTest);
            GL.Disable(EnableCap.CullFace);
            // Specifies the color used for clearing
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            shaderManager.LoadShaders("shaders\\vertexShader.glsl","shaders\\fragmentShader.glsl");
            shaderManager.UseShaderProgram();
            sceneManager = new SceneManager(shaderManager);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            deltaTime = (float)args.Time;
            // Clears the color and depth buffers
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            camera.Update();
            sceneManager.RenderScene();
            // Display what is rendered
            ProcessEvents(0.0);
            SwapBuffers();
        }

        // This callback receives keyboard events
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            
            switch(e.Key)
            {
                // Close the window is escape is hit
                case Keys.Escape:
                    Close();
                    break;
                case Keys.W:
                    camera.Position += new Vector3(0f,0f,-camera.MovementSpeed*deltaTime);
                    break;
                case Keys.S:
                    camera.Position += new Vector3(0f,0f,camera.MovementSpeed*deltaTime);
                    break;
                case Keys.A:
                    camera.Position += new Vector3(-camera.MovementSpeed*deltaTime,0f,0f);
                    break;
                case Keys.D:
                    camera.Position += new Vector3(camera.MovementSpeed*deltaTime,0f,0f);
                    break;
                case Keys.E:
                    camera.Position += new Vector3(0f,-camera.MovementSpeed*deltaTime,0f);
                    break;
                case Keys.Q:
                    camera.Position += new Vector3(0f,camera.MovementSpeed*deltaTime,0f);
                    break;
            }
        }
    }
}