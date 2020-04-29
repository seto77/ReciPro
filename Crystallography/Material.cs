namespace Crystallography
{
    /// <summary>
    /// OpenGL用　材質
    /// </summary>
    public class AtomMaterial
    {
        public int Argb { get; set; } = 0;
        public float Ambient { get; set; } = 0.1f;//環境光
        public float Diffusion { get; set; } = 0.8f;//拡散光
        public float Emission { get; set; } = 0.2f;//自己証明
        public float Shininess { get; set; } = 50f;//反射光の強度
        public float Specular { get; set; } = 0.7f;//反射光
        public float Transparency { get; set; } = 1f;

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

        public AtomMaterial(int argb, double ambient, double diffusion, double specular, double shininess, double emission, double transparency)
            :this(argb, (float) ambient, (float)diffusion, (float)specular, (float)shininess, (float)emission, (float) transparency)
        {
           
        }
    }
}