namespace Crystallography
{
    public class YusaGonio
    {
        private enum ScanMode
        { ZigzagScan, RotationalScan };

        //ScanMode Mode = ScanMode.ZigzagScan;

        public bool Valid = false;

        public bool Rx = false;

        /// <summary>
        /// Rx(Φ,回転)のモーター速度(deg/sec.)
        /// </summary>
        public double Rx_MotorSpeed = 18;

        /// <summary>
        /// Rz(θ, 首ふり)のモーター速度(deg/sec.)
        /// </summary>
        public double Rz_MotorSpeed = 2;

        /// <summary>
        /// Rz(θ, 首ふり)の揺動範囲(±deg.)
        /// </summary>
        public double Rz_OscillationRange = 4;

        /// <summary>
        /// Ry(ω,うなずき)のモーター速度(deg/sec.)
        /// </summary>
        public double Ry_MotorSpeed;

        /// <summary>
        /// Ry(ω,うなずき)の揺動範囲(±deg.)
        /// </summary>
        public double Ry_OscillationRange = 4;

        /// <summary>
        /// Ry(ω,うなずき)のステップ(deg.)
        /// </summary>
        public double Rt_Step = 0.2;
    }
}