using GlmSharp;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
namespace Capstone
{
    public class Camera
    {
        public float Zoom = 120;
        public float MovementSpeed = 200;
        public Vector3 Position = new Vector3(0f, 5f, 3);
        public Vector3 Front = new Vector3(0f, -3.0f, -2.0f);
        public Vector3 Up = new Vector3(0, 1.0f, 0);

        private int windowWidth;
        private int windowHeight;
        private ShaderManager shaderManager;
        public Camera(ShaderManager shaderManager, int width, int height)
        {
            this.shaderManager = shaderManager;
            windowWidth = width;
            windowHeight = height;
        }
        public Matrix4 GetViewProjectionMatrix()
        {
            Matrix4 view = Matrix4.LookAt(Position, Position+Front, Up);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(glm.Radians(Zoom), ((float)windowWidth)/((float)windowHeight), 0.001f, 100.0f);

            return view*projection;

        }
        public void Update()
        {
            Matrix4 view = Matrix4.LookAt(Position, Position+Front, Up);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(glm.Radians(Zoom), ((float)windowWidth)/((float)windowHeight), 0.0001f, 1000.0f);

            shaderManager.SetViewMatrix(view);
            shaderManager.SetProjectionMatrix(projection);
            shaderManager.SetViewPosition(Position);

        }
    }

}