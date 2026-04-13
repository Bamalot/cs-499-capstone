
using OpenTK.Graphics.OpenGL;

namespace Capstone
{
    public class Mesh
    {
        private int meshID;
        private int vertexCount;
        public Mesh(int id, int vertexCount)
        {
            meshID = id;
            this.vertexCount = vertexCount;
        }

        public void Draw()
        {
            GL.BindVertexArray(meshID);
            GL.DrawArrays(PrimitiveType.Triangles, 0, vertexCount);
        }
        public static Mesh CreatePlaneMesh()
        {
            int mesh = GL.GenVertexArray();
            GL.BindVertexArray(mesh);
            int buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            float[] meshData = { 
                -0.5f, 0f,  0.5f,
                -0.5f, -0f,  -0.5f,
                0.5f,  0f,  -0.5f,
                0.5f,  0f,  -0.5f,
                -0.5f,  0f,  0.5f,
                0.5f, -0f,  0.5f};

            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float)*108, meshData, BufferUsageHint.StaticDraw);
            GL.EnableVertexArrayAttrib(mesh, 0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            return new Mesh(mesh, 6);
        }
        public static Mesh CreateBoxMesh()
        {
            int mesh = GL.GenVertexArray();
            GL.BindVertexArray(mesh);
            int buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            float[] meshData = { 
                -0.5f, -0.5f,  0.5f,
                0.5f, -0.5f,  0.5f,
                0.5f,  0.5f,  0.5f,
                0.5f,  0.5f,  0.5f,
                -0.5f,  0.5f,  0.5f,
                -0.5f, -0.5f,  0.5f,

                -0.5f, -0.5f, -0.5f,
                -0.5f,  0.5f, -0.5f,
                0.5f,  0.5f, -0.5f,
                0.5f,  0.5f, -0.5f,
                0.5f, -0.5f, -0.5f,
                -0.5f, -0.5f, -0.5f,

                -0.5f,  0.5f,  0.5f,
                -0.5f,  0.5f, -0.5f,
                -0.5f, -0.5f, -0.5f,
                -0.5f, -0.5f, -0.5f,
                -0.5f, -0.5f,  0.5f,
                -0.5f,  0.5f,  0.5f,

                0.5f,  0.5f,  0.5f,
                0.5f, -0.5f, -0.5f,
                0.5f,  0.5f, -0.5f,
                0.5f, -0.5f, -0.5f,
                0.5f,  0.5f,  0.5f,
                0.5f, -0.5f,  0.5f,

                -0.5f,  0.5f, -0.5f,
                -0.5f,  0.5f,  0.5f,
                0.5f,  0.5f,  0.5f,
                0.5f,  0.5f,  0.5f,
                0.5f,  0.5f, -0.5f,
                -0.5f,  0.5f, -0.5f,

                -0.5f, -0.5f, -0.5f,
                0.5f, -0.5f,  0.5f,
                -0.5f, -0.5f,  0.5f,
                0.5f, -0.5f,  0.5f,
                -0.5f, -0.5f, -0.5f,
                0.5f, -0.5f, -0.5f};

            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float)*108, meshData, BufferUsageHint.StaticDraw);
            GL.EnableVertexArrayAttrib(mesh, 0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            return new Mesh(mesh, 36);
        }
    }
}