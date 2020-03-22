using System;
using System.Collections.Generic;
using System.IO;

namespace Crystallography
{
    [Serializable()]
    public static class DiffractionOptics
    {
        public static Parameter Read(string filename)
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(filename);//FileInfoオブジェクトを作成
                var ct = fi.CreationTime;//作成日時の取得
                var lwt = fi.LastWriteTime;//更新日時の取得

                StreamReader sr = new StreamReader(filename);
                List<string> strList = new List<string>();
                string tempstr;
                while ((tempstr = sr.ReadLine()) != null)
                {
                    tempstr = tempstr.Replace("<parameter", "<Parameter");
                    tempstr = tempstr.Replace("</parameter", "</Parameter");
                    strList.Add(tempstr);
                }
                sr.Close();

                StreamWriter sw = new StreamWriter(filename);

                for (int i = 0; i < strList.Count; i++)
                    sw.WriteLine(strList[i]);
                sw.Close();

                fi = new System.IO.FileInfo(filename);//FileInfoオブジェクトを作成
                fi.CreationTime = ct;//作成日時の取得
                fi.LastWriteTime = lwt;//更新日時の取得

                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DiffractionOptics.Parameter));
                System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open);

                DiffractionOptics.Parameter prm = (DiffractionOptics.Parameter)serializer.Deserialize(fs);
                fs.Close();

                return prm;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 回折光学系のパラメータ. 主にIPAnzlyzerとReciProから利用。
        /// </summary>
        [Serializable()]
        public class Parameter
        {
            /// <summary>
            /// "FlatPanel" か "Gandolfi"
            /// </summary>
            public string cameraMode;

            public string SACLA_EH5;
            public string SACLA_EH5_PixelWidth;
            public string SACLA_EH5_PixelHeight;
            public string SACLA_EH5_PixleSize;
            public string SACLA_EH5_TwoTheta = "0";//=Tau 廃止予定
            public string SACLA_EH5_Tau;
            public string SACLA_EH5_Phi;
            public string SACLA_EH5_Distance = "0";//＝CameraLength2 廃止予定
            public string SACLA_EH5_CameraLength2;
            public string SACLA_EH5_FootX;
            public string SACLA_EH5_FootY;

            public string waveSource;
            public string electronEnergy;

            public string xRayElement;
            public string xRayLine;
            public string waveLength;

            public string cameraLength;
            public string pixSize;
            public string pixSizeX;
            public string pixSizeY;
            public string pixKsi;
            public string aspectRatio;
            public string tiltPhi;
            public string tiltTau;
            public string centerX;
            public string centerY;
            public string sphericalRadiusInverse;
            public string GandolfiRadius;

            /// <summary>
            /// Concentric か Radial
            /// </summary>
            public string IntegrationMode;

            public string ConcentricStart;
            public string ConcentricEnd;
            public string ConcentricStep;

            /// <summary>
            /// Angle, Length, d-spacing
            /// </summary>
            public string ConcentricUnit;

            public string RadialRadius;
            public string RadialWidth;
            public string RadialStep;

            /// <summary>
            /// Angle, d-spacing
            /// </summary>
            public string RadialUnit;

            //Rectangle or Sector
            public string RegionMode;

            public string RectangleDirection;
            public string RectangleBandWidth;
            public string RectangleBothSide;
            public string RectangleAngle;

            public string SectorStart;
            public string SectorEnd;

            public string ExceptMaskedSpots;
            public string ExceptEdges;
            public string ExceptOver;
            public string ExceptUnder;

            public Parameter()
            {
            }
        }
    }
}