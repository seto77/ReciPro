using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Crystallography
{
    public class Elasticity
    {
        public enum Mode
        {
            Stiffness, Compliance
        }

        public Matrix<double> Compliance = new DenseMatrix(6, 6);
        public Matrix<double> Stiffness = new DenseMatrix(6, 6);

        public Elasticity()
        {
        }

        public Elasticity(Matrix<double> m, Mode mode)
        {
            if (mode == Mode.Compliance)
            {
                Compliance = m;
                Stiffness = m.TryInverse();
            }
            else
            {
                Stiffness = m;
                Compliance = m.TryInverse();
            }
        }

        public Matrix3D GetStrain(Symmetry symmetry, Matrix3D stress)
        {
            return symmetry.CrystalSystemNumber switch
            {
                0 => GetStrainTriclinic(stress),
                1 => GetStrainTriclinic(stress),//tricrinic
                2 => GetStrainOrthorhombic(stress),//mono
                3 => GetStrainOrthorhombic(stress),//ortho
                4 => GetStrainTriclinic(stress),//tetra
                5 => GetStrainTrigonal(stress),//trigonal
                6 => GetStrainTriclinic(stress),//hexa
                7 => GetStrainCubic(stress),//cubic
                _ => new Matrix3D(),
            };
        }

        public Matrix3D GetStrainTriclinic(Matrix3D stress)
        {
            double sigma11 = Compliance[0, 0] * stress.E11 + Compliance[0, 1] * stress.E22 + Compliance[0, 2] * stress.E33 + 2 * (Compliance[0, 3] * stress.E23 + Compliance[0, 4] * stress.E31 + Compliance[0, 5] * stress.E12);
            double sigma22 = Compliance[1, 0] * stress.E11 + Compliance[1, 1] * stress.E22 + Compliance[1, 2] * stress.E33 + 2 * (Compliance[1, 3] * stress.E23 + Compliance[1, 4] * stress.E31 + Compliance[1, 5] * stress.E12);
            double sigma33 = Compliance[2, 0] * stress.E11 + Compliance[2, 1] * stress.E22 + Compliance[2, 2] * stress.E33 + 2 * (Compliance[2, 3] * stress.E23 + Compliance[2, 4] * stress.E31 + Compliance[2, 5] * stress.E12);
            double sigma23 = Compliance[3, 0] * stress.E11 + Compliance[3, 1] * stress.E22 + Compliance[3, 2] * stress.E33 + 2 * (Compliance[3, 3] * stress.E23 + Compliance[3, 4] * stress.E31 + Compliance[3, 5] * stress.E12);
            double sigma31 = Compliance[4, 0] * stress.E11 + Compliance[4, 1] * stress.E22 + Compliance[4, 2] * stress.E33 + 2 * (Compliance[4, 3] * stress.E23 + Compliance[4, 4] * stress.E31 + Compliance[4, 5] * stress.E12);
            double sigma12 = Compliance[5, 0] * stress.E11 + Compliance[5, 1] * stress.E22 + Compliance[5, 2] * stress.E33 + 2 * (Compliance[5, 3] * stress.E23 + Compliance[5, 4] * stress.E31 + Compliance[5, 5] * stress.E12);

            return new Matrix3D(sigma11, sigma12, sigma31, sigma12, sigma22, sigma23, sigma31, sigma23, sigma33);
        }

        public Matrix3D GetStrainTrigonal(Matrix3D stress)
        {
            double sigma11 = Compliance[0, 0] * stress.E11 + Compliance[0, 1] * stress.E22 + Compliance[0, 2] * stress.E33 + Compliance[0, 3] * stress.E23;
            double sigma22 = Compliance[1, 0] * stress.E11 + Compliance[1, 1] * stress.E22 + Compliance[1, 2] * stress.E33 + Compliance[1, 3] * stress.E23;
            double sigma33 = Compliance[2, 0] * (stress.E11 + stress.E22) + Compliance[2, 2] * stress.E33;
            double sigma23 = Compliance[3, 0] * (stress.E11 - stress.E22) + Compliance[3, 3] * stress.E23;
            double sigma31 = Compliance[4, 4] * stress.E31 + Compliance[4, 5] * stress.E12;
            double sigma12 = Compliance[4, 5] * stress.E31 + Compliance[5, 5] * stress.E12;

            return new Matrix3D(sigma11, sigma12 / 2.0, sigma31 / 2.0, sigma12 / 2.0, sigma22, sigma23 / 2.0, sigma31 / 2.0, sigma23 / 2.0, sigma33);
        }

        public Matrix3D GetStrainOrthorhombic(Matrix3D stress)
        {
            double sigma11 = Compliance[0, 0] * stress.E11 + Compliance[0, 1] * stress.E22 + Compliance[0, 2] * stress.E33;
            double sigma22 = Compliance[1, 0] * stress.E11 + Compliance[1, 1] * stress.E22 + Compliance[1, 2] * stress.E33;
            double sigma33 = Compliance[2, 0] * stress.E11 + Compliance[2, 1] * stress.E22 + Compliance[2, 2] * stress.E33;
            double sigma23 = Compliance[3, 3] * stress.E23 / 2;
            double sigma31 = Compliance[4, 4] * stress.E31 / 2;
            double sigma12 = Compliance[5, 5] * stress.E12 / 2;

            return new Matrix3D(sigma11, sigma12, sigma31, sigma12, sigma22, sigma23, sigma31, sigma23, sigma33);
        }

        public Matrix3D GetStrainCubic(Matrix3D stress)
        {
            double sigma11 = Compliance[0, 0] * stress.E11 + Compliance[0, 1] * (stress.E22 + stress.E33);
            double sigma22 = Compliance[0, 0] * stress.E22 + Compliance[0, 1] * (stress.E33 + stress.E11);
            double sigma33 = Compliance[0, 0] * stress.E33 + Compliance[0, 1] * (stress.E11 + stress.E22);
            double sigma23 = Compliance[3, 3] * stress.E23 / 2;
            double sigma31 = Compliance[3, 3] * stress.E31 / 2;
            double sigma12 = Compliance[3, 3] * stress.E12 / 2;

            return new Matrix3D(sigma11, sigma12, sigma31, sigma12, sigma22, sigma23, sigma31, sigma23, sigma33);
        }

        public Matrix3D GetStrain(Symmetry symmetry, Matrix3D rotation, Matrix3D stress)
        {
            Matrix3D m1 = rotation.Transpose() * stress * rotation;
            Matrix3D m2 = GetStrain(symmetry, m1);
            Matrix3D m = rotation * m2 * rotation.Transpose();
            return m;
        }

        public Matrix3D GetStrainByHill(Symmetry symmetry, Matrix3D rotation, Matrix3D stress, Matrix3D strain, double hillFactor)
        {
            return hillFactor * GetStrain(symmetry, rotation, stress) + (1 - hillFactor) * strain;
        }

        /*

        public double[] GetStrain(Symmetry symmetry, double e11, double e12, double e13, double e22, double e23, double e33)
        {
            switch (symmetry.CrystalSystemNumber)
            {
                case 0: return GetStrainTriclinic(e11,e12,e13,e22,e23,e33); break;
                case 1: return GetStrainTriclinic(e11, e12, e13, e22, e23, e33); break;//tricrinic
                case 2: return GetStrainOrthorhombic(e11, e12, e13, e22, e23, e33); break;//mono
                case 3: return GetStrainOrthorhombic(e11, e12, e13, e22, e23, e33); break;//ortho
                case 4: return GetStrainTriclinic(e11, e12, e13, e22, e23, e33); break;//tetra
                case 5: return GetStrainTrigonal(e11, e12, e13, e22, e23, e33); break;//trigonal
                case 6: return GetStrainTriclinic(e11, e12, e13, e22, e23, e33); break;//hexa
                case 7: return GetStrainCubic(e11, e12, e13, e22, e23, e33); break;//cubic
            }
            return new double[] { 0, 0, 0, 0, 0, 0 };
        }

        public double[] GetStrainTriclinic(double e11,double e12,double e13,double e22,double e23,double e33)
        {
            double sigma11 = Compliance.E[0, 0] * e11 + Compliance.E[0, 1] * e22 + Compliance.E[0, 2] * e33 + 2 * (Compliance.E[0, 3] * e23 + Compliance.E[0, 4] * e13 + Compliance.E[0, 5] * e12);
            double sigma22 = Compliance.E[1, 0] * e11 + Compliance.E[1, 1] * e22 + Compliance.E[1, 2] * e33 + 2 * (Compliance.E[1, 3] * e23 + Compliance.E[1, 4] * e13 + Compliance.E[1, 5] * e12);
            double sigma33 = Compliance.E[2, 0] * e11 + Compliance.E[2, 1] * e22 + Compliance.E[2, 2] * e33 + 2 * (Compliance.E[2, 3] * e23 + Compliance.E[2, 4] * e13 + Compliance.E[2, 5] * e12);
            double sigma23 = Compliance.E[3, 0] * e11 + Compliance.E[3, 1] * e22 + Compliance.E[3, 2] * e33 + 2 * (Compliance.E[3, 3] * e23 + Compliance.E[3, 4] * e13 + Compliance.E[3, 5] * e12);
            double sigma31 = Compliance.E[4, 0] * e11 + Compliance.E[4, 1] * e22 + Compliance.E[4, 2] * e33 + 2 * (Compliance.E[4, 3] * e23 + Compliance.E[4, 4] * e13 + Compliance.E[4, 5] * e12);
            double sigma12 = Compliance.E[5, 0] * e11 + Compliance.E[5, 1] * e22 + Compliance.E[5, 2] * e33 + 2 * (Compliance.E[5, 3] * e23 + Compliance.E[5, 4] * e13 + Compliance.E[5, 5] * e12);

            return new double[]{sigma11, sigma12, sigma31, sigma22, sigma23, sigma33};
        }

        public double[] GetStrainTrigonal(double e11, double e12, double e13, double e22, double e23, double e33)
        {
            double sigma11 = Compliance.E[0, 0] * e11 + Compliance.E[0, 1] * e22 + Compliance.E[0, 2] * e33 +  Compliance.E[0, 3] * e23;
            double sigma22 = Compliance.E[1, 0] * e11 + Compliance.E[1, 1] * e22 + Compliance.E[1, 2] * e33 +  Compliance.E[1, 3] * e23;
            double sigma33 = Compliance.E[2, 0] * (e11 + e22) + Compliance.E[2, 2] * e33;
            double sigma23 = Compliance.E[3, 0] * (e11 - e22) + Compliance.E[3, 3] * e23;
            double sigma31 = Compliance.E[4, 4] * e13 + Compliance.E[4, 5] * e12;
            double sigma12 = Compliance.E[4, 5] * e13 + Compliance.E[5, 5] * e12;

            return new double[] {sigma11, sigma12 / 2.0, sigma31 / 2.0, sigma22, sigma23 / 2.0,  sigma33};
        }

        public double[] GetStrainOrthorhombic(double e11, double e12, double e13, double e22, double e23, double e33)
        {
            double sigma11 = Compliance.E[0, 0] * e11 + Compliance.E[0, 1] * e22 + Compliance.E[0, 2] * e33;
            double sigma22 = Compliance.E[1, 0] * e11 + Compliance.E[1, 1] * e22 + Compliance.E[1, 2] * e33;
            double sigma33 = Compliance.E[2, 0] * e11 + Compliance.E[2, 1] * e22 + Compliance.E[2, 2] * e33;
            double sigma23 = Compliance.E[3, 3] * e23 / 2;
            double sigma31 = Compliance.E[4, 4] * e13 / 2;
            double sigma12 = Compliance.E[5, 5] * e12 / 2;

            return new double[] { sigma11, sigma12, sigma31, sigma22, sigma23, sigma33 };
        }

        public double[] GetStrainCubic(double e11, double e12, double e13, double e22, double e23, double e33)
        {
            double sigma11 = Compliance.E[0, 0] * e11 + Compliance.E[0, 1] * (e22 + e33);
            double sigma22 = Compliance.E[0, 0] * e22 + Compliance.E[0, 1] * (e33 + e11);
            double sigma33 = Compliance.E[0, 0] * e33 + Compliance.E[0, 1] * (e11 + e22);
            double sigma23 = Compliance.E[3, 3] * e23 / 2;
            double sigma31 = Compliance.E[3, 3] * e13 / 2;
            double sigma12 = Compliance.E[3, 3] * e12 / 2;

            return new double[] { sigma11, sigma12, sigma31, sigma22, sigma23, sigma33 };
        }

        //s: stress, r: rotation
        public double[] GetStrain(Symmetry symmetry,
            double r11, double r12, double r13, double r21, double r22, double r23, double r31, double r32, double r33,
            double s11, double s12, double s13, double s22, double s23, double s33)
        {
            //Matrix m1 = rotation.Transpose() * stress * rotation;
             //Matrix3D m2 = GetStrain(symmetry, m1);
            double[] m = GetStrain(symmetry,
                r11*r11*s11 + 2*r11*(r21*s12 + r31*s13) + r21*r21*s22 + 2*r21*r31*s23 + r31*r31*s33,
                r12*r21*s12 + r12*r31*s13 + r11*(r12*s11 + r22*s12 + r32*s13) + r21*r22*s22 + r22*r31*s23 + r21*r32*s23 + r31*r32*s33,
                r13*r21*s12 + r13*r31*s13 + r11*(r13*s11 + r23*s12 + r33*s13) + r21*r23*s22 + r23*r31*s23 + r21*r33*s23 + r31*r33*s33,
                r12*r12*s11 + 2*r12*(r22*s12 + r32*s13) + r22*r22*s22 + 2*r22*r32*s23 + r32*r32*s33,
                r13*r22*s12 + r13*r32*s13 + r12*(r13*s11 + r23*s12 + r33*s13) + r22*r23*s22 + r23*r32*s23 + r22*r33*s23 + r32*r33*s33,
                r13*r13*s11 + 2*r13*(r23*s12 + r33*s13) + r23*r23*s22 + 2*r23*r33*s23 + r33*r33*s33
                );

            //Matrix3D m = rotation * m2 * rotation.Transpose();
            //return m;
            return new double[]
            {
                r11*r11*m[0] + 2*r11*(r21*m[1] + r31*m[2]) + r21*r21*m[3] + 2*r21*r31*m[4] + r31*r31*m[5],
               r11*(r21*m[0] + r22*m[1] + r23*m[2]) + r12*(r21*m[1] + r22*m[3] + r23*m[4]) + r13*(r21*m[2] + r22*m[4] + r23*m[5]),
               r11*(r31*m[0] + r32*m[1] + r33*m[2]) + r12*(r31*m[1] + r32*m[3] + r33*m[4]) + r13*(r31*m[2] + r32*m[4] + r33*m[5]),
               r21*r21*m[0] + 2*r21*(r22*m[1] + r23*m[2]) + r22*r22*m[3] + 2*r22*r23*m[4] + r23*r23*m[5],
               r21*(r31*m[0] + r32*m[1] + r33*m[2]) + r22*(r31*m[1] + r32*m[3] + r33*m[4]) + r23*(r31*m[2] + r32*m[4] + r33*m[5]),
               r31*r31*m[0] + 2*r31*(r32*m[1] + r33*m[2]) + r32*r32*m[3] + 2*r32*r33*m[4] + r33*r33*m[5]
            };
        }

        public Matrix3D GetStrainByHill(Symmetry symmetry, Matrix3D rotation, Matrix3D stress, Matrix3D strain, double hillFactor)
        {
            double[] s=GetStrain(
                symmetry,
                rotation.E11,rotation.E12,rotation.E13,rotation.E21,rotation.E22,rotation.E23,rotation.E31,rotation.E32,rotation.E33,
                stress.E11,stress.E12,stress.E13,stress.E22,stress.E23,stress.E33);
            Matrix3D elasticStrain = new Matrix3D(s[0], s[1], s[2], s[1], s[3], s[4], s[2], s[3], s[5]);

            return hillFactor * elasticStrain + (1 - hillFactor) * strain;
        }
        */
    }
}