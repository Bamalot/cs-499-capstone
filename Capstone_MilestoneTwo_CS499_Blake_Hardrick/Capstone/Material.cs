
using GlmSharp;
using OpenTK.Mathematics;

namespace Capstone
{
    public class Material
    {
        public string Tag {get;}
        public Vector3 DiffuseColor {get;}
        public Vector3 SpecularColor {get;}
        public float Shininess {get;}

        public Material(string tag, Vector3 diffuseColor, Vector3 specularColor, float shininess)
        {
            Tag = tag;
            DiffuseColor = diffuseColor;
            SpecularColor = specularColor;
            Shininess = shininess;
        }
    }
}