using System;
using System.Collections.Generic;
using System.IO;

namespace Crystallography;

[Serializable()]
public static class DiffractionOptics
{
    public static Parameter Read(string filename)
    {
        try
        {
            var fi = new FileInfo(filename);//FileInfoオブジェクトを作成
            var ct = fi.CreationTime;//作成日時の取得
            var lwt = fi.LastWriteTime;//更新日時の取得

            var sr = new StreamReader(filename);
            var str = sr.ReadToEnd();
            sr.Close();

            var replace = new Func<string, string, string, string>((string srcString, string oldValue, string newValue)
                => srcString.Replace("<" + oldValue, "<" + newValue).Replace("</" + oldValue, "</" + newValue));

            str = replace(str, "parameter", "Parameter");

            str = replace(str, "SACLA_EH5>", "FootMode>");

            str = replace(str, "centerX", "DirectSpotX");
            str = replace(str, "centerY", "DirectSpotY");
            str = replace(str, "cameraLength", "CameraLength1");

            str = replace(str, "SACLA_EH5_FootX", "FootX");
            str = replace(str, "SACLA_EH5_FootY", "FootY");
            str = replace(str, "SACLA_EH5_CameraLength2", "CameraLength2");

            var sw = new StreamWriter(filename);

            sw.Write(str);
            sw.Close();

            fi = new FileInfo(filename)//FileInfoオブジェクトを作成
            {
                CreationTime = ct,//作成日時の取得
                LastWriteTime = lwt//更新日時の取得
            };

            System.Xml.Serialization.XmlSerializer serializer = new(typeof(Parameter));
            var fs = new FileStream(filename, FileMode.Open);

            var prm = (Parameter)serializer.Deserialize(fs);
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

        public string FootMode="False";// 以前は SACLA_EH5 TrueであればFootモード、FalseであればDirectSpotモード

        public string DirectSpotX;//以前は centerX
        public string DirectSpotY;//以前は centerY
        public string CameraLength1;//以前は cameraLength
        
        public string FootX;//以前は SACLA_EH5_FootX
        public string FootY;//以前は SACLA_EH5_FootY
        public string CameraLength2;//以前は SACLA_EH5_CameraLength2

        public string SACLA_EH5_PixelWidth;//廃止予定
        public string SACLA_EH5_PixelHeight;//廃止予定
        public string SACLA_EH5_PixleSize;//廃止予定
        public string SACLA_EH5_TwoTheta;//=Tau 廃止予定
        public string SACLA_EH5_Tau;//=Tau 廃止予定
        public string SACLA_EH5_Phi;//=Phi 廃止予定
        public string SACLA_EH5_Distance;//＝CameraLength2 廃止予定
        public string pixSize;//廃止予定
        public string aspectRatio;//廃止予定

        public string waveSource;
        public string electronEnergy;

        public string xRayElement;
        public string xRayLine;
        public string waveLength;

        public string pixSizeX;
        public string pixSizeY;
        public string pixKsi;

        public string tiltPhi;
        public string tiltTau;

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
