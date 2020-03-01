using System;
using System.Collections.Generic;

namespace Crystallography
{
    public class CoordinatedAtom : System.IComparable
    {
        public string Label = "";
        public double Distance = 0;
        public Vector3D Position = new Vector3D();

        public CoordinatedAtom(string label, Vector3D position, double distance)
        {
            this.Label = label;
            this.Distance = distance;
            this.Position = position;
        }

        public int CompareTo(object obj)
        {
            return Distance.CompareTo(((CoordinatedAtom)obj).Distance);
        }
    }

    public static class AtomCoordination
    {
        public static List<CoordinatedAtom> Search(Crystal crystal, Atoms targetAtom, double maxLengthAngstrom)
        {
            Vector3D pos = (crystal.A_Axis * targetAtom.X + crystal.B_Axis * targetAtom.Y + crystal.C_Axis * targetAtom.Z) * 10.0;

            List<CoordinatedAtom> atoms = new List<CoordinatedAtom>();
            //まず、隣り合った単位格子の原子位置をすべて探索してCoordinatedAtom型のリストに全部入れる
            for (int max = 0; max < 8; max++)
            {
                bool flag = false;
                for (int xShift = -max; xShift <= max; xShift++)
                    for (int yShift = -max; yShift <= max; yShift++)
                        for (int zShift = -max; zShift <= max; zShift++)
                        {
                            if (Math.Abs(xShift) == max || Math.Abs(yShift) == max || Math.Abs(zShift) == max)
                            {
                                foreach (Atoms a in crystal.Atoms)
                                    foreach (var v in a.Atom)
                                    {
                                        Vector3D vTemp = v + new Vector3D(xShift, yShift, zShift, false);
                                        Vector3D tempPos = (crystal.A_Axis * vTemp.X + crystal.B_Axis * vTemp.Y + crystal.C_Axis * vTemp.Z) * 10;
                                        if (maxLengthAngstrom > (tempPos - pos).Length)
                                        {
                                            atoms.Add(new CoordinatedAtom(a.Label, tempPos, (tempPos - pos).Length));
                                            flag = true;//一個でも見つけられたら続行
                                        }
                                    }
                            }
                        }
                if (flag == false && max > 2)
                    break;
            }
            atoms.Sort();

            return atoms;
        }
    }
}