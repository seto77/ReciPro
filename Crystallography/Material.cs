namespace Crystallography
{
    /// <summary>
    /// OpenGL用　材質
    /// </summary>
    public class AtomMaterial
    {
        public int Argb;
        public float Ambient = 0.1f;//環境光
        public float Diffusion = 0.8f;//拡散光
        public float Emission = 0.2f;//自己証明
        public float Shininess = 50f;//反射光の強度
        public float Specular = 0.7f;//反射光
        public float Transparency = 1f;

        public AtomMaterial(int argb, float ambient, float diffusion, float specular, float shininess, float emission, float transparency)
        {
            Argb = argb;
            Ambient = ambient;
            Diffusion = diffusion;
            Specular = specular;
            Shininess = shininess;
            Emission = emission;
            Transparency = transparency;
        }
    }
}