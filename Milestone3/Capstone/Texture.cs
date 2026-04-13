
using OpenTK.Graphics.OpenGL;
using StbImageSharp;

namespace Capstone
{
    public class Texture
    {
        int textureID;
        string tag;

        public Texture(string fileName, string tag)
        {
            this.tag = tag;
            // Flips images vertically to align with the OpenGL coordinate system
            StbImage.stbi_set_flip_vertically_on_load(1);
            using(FileStream imageData = new FileStream(fileName, FileMode.Open))
            {
                // Loads image data from file
                ImageResult image = ImageResult.FromStream(imageData);
                // Creates a texture with OpenGL
                textureID = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, textureID);
                // Set wrapping paramters
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                // Set filtering parameters
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                // Stores the texture data in the GPU
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
                // Generates mipmaps
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, textureID);
        }

        public string GetTag()
        {
            return tag;
        }

        public int GetID()
        {
            return textureID;
        }
    }
}