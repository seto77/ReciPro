using Crystallography.OpenGL;
//260530Cl Media Foundation へ移行: FFMediaToolkit は不使用 (旧 using をコメントアウト)。MediaFoundationVideoEncoder は Crystallography.Controls (Program.cs の global using 経由)
//using FFMediaToolkit; //260405Cl 追加
//using FFMediaToolkit.Encoding; //260405Cl 追加
//using FFMediaToolkit.Graphics; //260405Cl 追加
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic; //260405Cl 追加
using System.Threading.Tasks; //260405Cl 追加
using System.Windows.Forms;

namespace ReciPro;

public partial class FormMovie : FormBase
{
    public FormMain FormMain;
    public Form Caller;//呼び出し元

    public Control Target;

    public Func<Bitmap> Func;
    public Vector3DBase Direction { get; set; } = new Vector3DBase(0, 1, 0);

    public double Speed => numericBoxSpeed.Value;
    public double Duration => numericBoxDuration.Value;
    public int Framerate => numericBoxFps.ValueInteger; // (260629Ch) 動画の fps を GUI から指定する
    public bool IncludeFinalFrame => checkBoxIncludeFinalFrame.Checked; // (260629Ch) 終了時刻の姿勢を最後のフレームとして追加する
    public int EncodeQuality => numericBoxQuality.ValueInteger; // (260629Ch) Media Foundation の品質値 (1-100) を GUI から渡す
    public bool EnableRotation => checkBoxRotation.Checked; // (260629Ch) Rotation と Translation を独立指定にする
    //public bool EnableTranslation => translationModeAvailable && checkBoxTranslation.Checked; // (260629Ch) → 260703Cl フィールド削除
    public bool EnableTranslation => checkBoxTranslation.Checked; // 260703Cl 非 available 時は SetTranslationModeAvailability が Checked=false を強制するため Checked のみで判定できる
    public double TranslationSpeed => numericBoxTranslationSpeed.Value; // (260628Ch)

    public Vector3DBase A => FormMain.Crystal.A_Axis;
    public Vector3DBase B => FormMain.Crystal.B_Axis;
    public Vector3DBase C => FormMain.Crystal.C_Axis;

    public Matrix3D Rot => FormMain.Crystal.RotationMatrix;

    //260530Cl Media Foundation 移行で不要: private static bool ffmpegLoaded = false;
    private bool encoding = false; //260405Cl 追加: エンコード中フラグ
    //private bool translationModeAvailable = false; // (260629Ch) → 260703Cl 削除: checkBoxTranslation.Checked から導出可能な冗長状態だった

    public bool MillerBravaisIndex { set => indexControl.MillerBravais = value; }


    public FormMovie()
    {
        InitializeComponent();
        comboBoxSpeed.SelectedIndex = 7;
        // 260530Cl このマシンで H.265(HEVC) エンコーダが使えない場合は H.265 を選べないようにする
        if (!MediaFoundationVideoEncoder.IsHevcEncoderAvailable())
        {
            radioButtonH265.Enabled = false;
            radioButtonH264.Checked = true;
        }
    }

    private void buttonDirection_Click(object sender, EventArgs e)
    {
        Direction = (sender as Button).Name switch
        {
            "buttonTopRight" => new Vector3DBase(-1, 1, 0),
            "buttonRight" => new Vector3DBase(0, 1, 0),
            "buttonBottomRight" => new Vector3DBase(1, 1, 0),
            "buttonBottom" => new Vector3DBase(1, 0, 0),
            "buttonBottomLeft" => new Vector3DBase(1, -1, 0),
            "buttonLeft" => new Vector3DBase(0, -1, 0),
            "buttonTopLeft" => new Vector3DBase(-1, -1, 0),
            "buttonTop" => new Vector3DBase(-1, 0, 0),
            "buttonClock" => new Vector3DBase(0, 0, -1),
            "buttonAntiClock" => new Vector3DBase(0, 0, 1),
            _ => new Vector3DBase(0, 0, 1)
        };
        var buttons = new[] { buttonTopRight, buttonRight, buttonBottomRight, buttonBottom, buttonBottomLeft, buttonLeft, buttonTopLeft, buttonTop, buttonClock, buttonAntiClock };

        foreach (var b in buttons)
            b.ForeColor = (sender as Button).Name == b.Name ? Color.Blue : Color.Gray;
    }

    private void radioButtonCurrent_CheckedChanged(object sender, EventArgs e)
    {
        if (EnableTranslation)
        {
            if (indexControl.IsZero)
                indexControl.Values = (0, 1, 0); // (260629Ch) Translation を選んだだけで有効な既定方向にする
            if (!radioButtonAxis.Checked)
                radioButtonAxis.Checked = true; // (260629Ch) 平行移動を含む動画は Direction index [uvw] に限定する
        }

        indexControl.Mode = radioButtonAxis.Checked ? IndexControl.ModeEnum.Axis : IndexControl.ModeEnum.Plane;
        indexControl.Enabled = !radioButtonCurrent.Checked; // 260518Cl 旧 tableLayoutPanelAxis/Plane の Enabled 切替に対応: Current のとき入力欄を無効化
        tableLayoutPanelCurrent.Enabled = radioButtonCurrent.Checked && !EnableTranslation; // (260629Ch)
        numericBoxAxisU_ValueChanged(sender, e);
    }

    private void numericBoxAxisU_ValueChanged(object sender, EventArgs e)
    {
        var (x, y, z) = indexControl.Values;
        if (radioButtonAxis.Checked)
            Direction = Rot * (x * A + y * B + z * C);
        else if (radioButtonPlane.Checked)
        {
            var rot = new Matrix3D(A, B, C).Inverse();
            Direction = Rot * (x * rot.Row1 + y * rot.Row2 + z * rot.Row3);
        }
    }

    //260405Cl 変更: ffmpeg.exe Process.Start → FFMediaToolkit API, async化
    //private void buttonOK_Click(object sender, EventArgs e) // 旧実装: ffmpeg.exe を Process.Start で呼び出し
    private async void buttonOK_Click(object sender, EventArgs e)
    {
        if (encoding) return; //260405Cl エンコード中は無視
        Visible = false;

        if (!EnableRotation && !EnableTranslation)
        {
            MessageBox.Show("Please select Rotation and/or Translation"); // (260629Ch)
            Visible = true; // (260629Ch)
            return;
        }

        //if (Direction.X == 0 && Direction.Y == 0 && Direction.Z == 0) // (260629Ch) 変更前: Translation 単独時も回転軸 Direction で判定していた
        if (EnableRotation && Direction.X == 0 && Direction.Y == 0 && Direction.Z == 0)
        {
            MessageBox.Show("Please input a valid orientation");
            Visible = true; // (260629Ch)
            return;
        }
        if (EnableTranslation && indexControl.IsZero)
        {
            MessageBox.Show("Please input a valid translation direction"); // (260629Ch)
            Visible = true; // (260629Ch)
            return;
        }

        var dlg = new SaveFileDialog() { Filter = "*.mp4|*.mp4" };
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            FormMain.Enabled = Caller.Enabled = false;

            // 260530Cl ffmpeg(GPL) の動的ロードを廃止。Media Foundation は OS 内蔵のため初期化不要。
            //          旧コード:
            //          if (!ffmpegLoaded) {
            //              var ffmpegDir = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "ffmpeg");
            //              foreach (var name in new[] { "libwinpthread-1", "libgcc_s_seh-1", "libstdc++-6", "zlib1",
            //                  "libx264-165", "libx265", "avutil-59", "swresample-5", "avcodec-61", "avformat-61", "swscale-8" })
            //                  NativeLibrary.Load(Path.Combine(ffmpegDir, name + ".dll"));
            //              FFmpegLoader.FFmpegPath = ffmpegDir; ffmpegLoaded = true;
            //          }

            //var framerate = 30; // (260629Ch) 変更前: 30 fps 固定
            var framerate = Math.Max(1, Framerate); // (260629Ch)
            var hevc = radioButtonH265.Checked; // 260530Cl VideoCodec→bool に変更 (H.265=true, H.264=false)
            var speed = (string)comboBoxSpeed.SelectedItem;
            var quality = EncodeQuality; // (260629Ch) 値が大きいほど高品質・大きめのファイルになる
            var rotateMovie = EnableRotation; // (260629Ch)
            //var translateMovie = EnableTranslation && Caller is FormStructureViewer; // (260629Ch) → 260703Cl 型チェックを 1 回にしてループ内でも再利用
            var structureViewer = Caller as FormStructureViewer; // 260703Cl
            var translateMovie = EnableTranslation && structureViewer != null; // 260703Cl
            var (transU, transV, transW) = indexControl.Values; // 260703Cl ループ不変の GUI 値は事前に読む (quality 等の hoist と同じ流儀)
            var translationSpeed = TranslationSpeed; // 260703Cl
            var duration = Duration; // 260703Cl
            var frameCount = Math.Max(1, (int)Math.Ceiling(duration * framerate)); // (260629Ch)
            if (IncludeFinalFrame)
                frameCount++; // (260629Ch) t = Duration の終了姿勢を最後に追加する

            //UIスレッドで全フレームのビットマップデータを収集 260405Cl
            var frames = new List<byte[]>();
            int width = 0, height = 0;
            try // 260703Cl 追加: fps/Duration 拡大で総フレームメモリが大きくなり得るため、OutOfMemoryException 等でもフォームが無効のまま残らないよう保護する
            {
                //for (int i = 0; i < Duration * framerate; i++) // (260629Ch) 変更前: 30 fps 固定かつ終了フレームを含められなかった
                for (int i = 0; i < frameCount; i++) // (260629Ch)
                {
                    // (260629Ch) 変更前: ここで姿勢を 1 step 進めてからキャプチャしていたため、先頭フレームが開始姿勢からずれていた
                    var bmp = Target is GLControlAlpha c ? c.GenerateBitmap() : Func();
                    if (bmp.Width % 2 != 0 || bmp.Height % 2 != 0)
                    {
                        var cropped = bmp.Clone(new Rectangle(0, 0, bmp.Width - bmp.Width % 2, bmp.Height - bmp.Height % 2), bmp.PixelFormat);
                        bmp.Dispose(); // 260530Cl Clone 元のビットマップを解放 (リーク防止)
                        bmp = cropped;
                    }
                    width = bmp.Width;
                    height = bmp.Height;
                    //260530Cl Bitmapを上から下・隙間なしの BGRA(32bpp) として byte[] に収集 (Media Foundation 入力用)
                    //         旧: Format24bppRgb で stride 付きデータを収集していた
                    var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                    var bits = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                    var data = new byte[bmp.Width * 4 * bmp.Height]; // 32bpp は stride==width*4 でパディングなし
                    System.Runtime.InteropServices.Marshal.Copy(bits.Scan0, data, 0, data.Length);
                    bmp.UnlockBits(bits);
                    bmp.Dispose(); // 260530Cl 収集後にビットマップを解放 (毎フレームのリーク防止)
                    frames.Add(data);

                    //FormMain.Rotate(Direction, Speed * Math.PI / framerate / 180.0); // (260628Ch) 変更前: 動画は常に回転のみ
                    // (260629Ch) キャプチャ後に次フレーム用の姿勢へ進める。1枚目は現在の開始姿勢そのものになる
                    //var currentTime = Math.Min(i / (double)framerate, Duration); // (260629Ch) → 260703Cl 三項分岐 (i+1==frameCount のとき Duration) は Math.Min が兼ねるため 1 式に集約
                    //var nextTime = i + 1 < frameCount ? Math.Min((i + 1) / (double)framerate, Duration) : Duration; // (260629Ch)
                    //var frameSeconds = nextTime - currentTime; // (260629Ch)
                    var frameSeconds = Math.Min((i + 1) / (double)framerate, duration) - Math.Min(i / (double)framerate, duration); // 260703Cl
                    if (frameSeconds <= 0)
                        continue;

                    //if (radioButtonTranslation.Checked && Caller is FormStructureViewer structureViewer) // (260629Ch) 変更前: 回転か平行移動のどちらか一方のみだった
                    //if (translateMovie && Caller is FormStructureViewer structureViewer) // (260629Ch) → 260703Cl 型チェックはループ前に 1 回だけ
                    if (translateMovie) // 260703Cl
                    {
                        //var step = TranslationSpeed / framerate; // (260629Ch) 変更前: 常に 1/fps 秒ぶん進めていた
                        var step = translationSpeed * frameSeconds; // (260629Ch)
                        if (step != 0)
                        {
                            var projectionCenter = structureViewer.ProjectionCenter;
                            structureViewer.SetProjectionCenter(projectionCenter.X + transU * step, projectionCenter.Y + transV * step, projectionCenter.Z + transW * step);
                        }
                    }
                    //else // (260629Ch) 変更前: Translation が ON のときは回転しなかった
                    if (rotateMovie) // (260629Ch)
                        FormMain.Rotate(Direction, Speed * Math.PI * frameSeconds / 180.0); // (260629Ch)
                }
            }
            catch (Exception ex) // 260703Cl 追加: キャプチャ失敗時はフォームを再有効化して復帰する (従来は例外でフォームが無効のまま残った)
            {
                frames.Clear();
                FormMain.Enabled = Caller.Enabled = true;
                MessageBox.Show($"Failed to capture frames: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //バックグラウンドスレッドでエンコード 260405Cl
            var fileName = dlg.FileName;
            // 260530Cl x264 由来の preset(ultrafast..veryslow) は Media Foundation では未使用
            //          (260629Ch) レート制御は MediaFoundationVideoEncoder 側で Quality 値から目標ビットレートを決める。combo は進捗表示ラベル用に残す。
            //          旧: var preset = speed switch { "ultrafast" => EncoderPreset.UltraFast, ... _ => EncoderPreset.Medium };

            encoding = true; //260405Cl

            //進捗ダイアログを表示 260405Cl
            var progressForm = new Form
            {
                Text = "Encoding...",
                AutoScaleMode = AutoScaleMode.Dpi,
                ClientSize = new System.Drawing.Size(500, 25),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ControlBox = false,
            };
            var codecName = hevc ? "H.265" : "H.264"; //260530Cl
            var progressBar = new ProgressBar { Dock = DockStyle.Fill, Maximum = frames.Count, Style = ProgressBarStyle.Continuous };
            progressForm.Text = $"{codecName} / Q{quality} / {speed} - 0.0% - 00:00:00 / --:--:--"; // (260629Ch)
            progressForm.Controls.Add(progressBar);

            try // 260530Cl 例外時もフォーム再有効化・progressForm 破棄・encoding 解除を保証する
            {
                progressForm.Show(this);

                var sw = System.Diagnostics.Stopwatch.StartNew(); //260405Cl
                var progress = new Progress<int>(v =>
                {
                    if (progressForm.IsDisposed) return; // 260530Cl Dispose 後の遅延 Report を無視 (ObjectDisposedException 防止)
                    progressBar.Value = v;
                    var percent = 100.0 * v / frames.Count;
                    var elapsed = sw.Elapsed;
                    var remaining = v > 0 ? TimeSpan.FromTicks(elapsed.Ticks * (frames.Count - v) / v) : TimeSpan.Zero;
                    progressForm.Text = $"{codecName} / Q{quality} / {speed} - {percent:0.0}% - {elapsed:hh\\:mm\\:ss} / {remaining:hh\\:mm\\:ss}"; // (260629Ch)
                });
                await Task.Run(() =>
                {
                    //260530Cl Media Foundation(OS 内蔵)で H.264/H.265 → MP4 を出力 (旧: FFMediaToolkit/ffmpeg)
                    //using var encoder = new MediaFoundationVideoEncoder(fileName, width, height, framerate, hevc); // (260629Ch) 変更前: 品質は既定値 70 固定
                    using var encoder = new MediaFoundationVideoEncoder(fileName, width, height, framerate, hevc, quality); // (260629Ch)
                    for (int i = 0; i < frames.Count; i++)
                    {
                        encoder.AddFrameBgra32(frames[i]);
                        frames[i] = null; // 260530Cl エンコード済みフレームを解放しピークメモリを抑える
                        ((IProgress<int>)progress).Report(i + 1);
                    }
                    encoder.Finish();
                });
            }
            finally
            {
                progressForm.Dispose(); // 260530Cl 非モーダル Form は Close では破棄されないため Dispose
                encoding = false; //260405Cl
                FormMain.Enabled = Caller.Enabled = true;
            }
        }
    }

    private void buttonCancel_Click(object sender, EventArgs e) => Visible = false;

    public void Execute(Control target, Form caller)
    {
        if (encoding) return; //260405Cl エンコード中は開かない
        Target = target;
        Caller = caller;
        Location = new Point(caller.Location.X + 10, caller.Location.Y + 10);
        TopMost = true;

        //radioButtonTranslation.Visible = caller.Name.Contains("Structure"); // (260628Ch) 変更前: 名前文字列で判定していた
        SetTranslationModeAvailability(caller is FormStructureViewer); // (260628Ch)

        Visible = true;
    }

    public void Execute(Func<Bitmap> func, Form caller)
    {
        if (encoding) return; //260405Cl エンコード中は開かない
        Target = null;
        Func = func;
        Caller = caller;
        Location = new Point(caller.Location.X + 10, caller.Location.Y + 10);
        TopMost = true;
        SetTranslationModeAvailability(false); // (260628Ch)
        Visible = true;
    }

    private void SetTranslationModeAvailability(bool available) // 260628Ch 追加
    {
        //radioButtonTranslation.Visible = available; // (260628Ch) 変更前
        //translationModeAvailable = available; // (260629Ch) → 260703Cl フィールド削除 (下の Checked 強制で不変条件が保たれる)
        checkBoxTranslation.Visible = available; // (260629Ch)
        if (!available)
        {
            checkBoxTranslation.Checked = false; // (260629Ch)
            if (!checkBoxRotation.Checked)
                checkBoxRotation.Checked = true; // (260629Ch)
        }

        //radioButtonRotation_CheckedChanged(this, EventArgs.Empty); // (260629Ch) 変更前
        motionCheckBox_CheckedChanged(this, EventArgs.Empty); // (260629Ch)
    }

    //private void radioButtonRotation_CheckedChanged(object sender, EventArgs e) // (260629Ch) 変更前: Rotation/Translation は排他ラジオボタンだった
    private void motionCheckBox_CheckedChanged(object sender, EventArgs e) // (260629Ch)
    {
        var rotationSelected = checkBoxRotation.Checked; // (260629Ch)
        var translationSelected = EnableTranslation; // (260629Ch)
        numericBoxSpeed.Visible = rotationSelected; // (260629Ch)
        numericBoxTranslationSpeed.Visible = translationSelected; // (260629Ch)

        //radioButtonAxis.Enabled = true; // (260629Ch) → 260703Cl 削除: どこにも false にするコードがなく no-op (Axis は常に選択可能のまま)
        radioButtonPlane.Enabled = radioButtonCurrent.Enabled = !translationSelected; // (260629Ch) Translation ON では他の方向種別を選べない

        //if (translationSelected && indexControl.IsZero) // (260629Ch) → 260703Cl 削除: 下の radioButtonCurrent_CheckedChanged 先頭に同一ロジックがあり二重実行だった
        //    indexControl.Values = (0, 1, 0); // (260629Ch)
        //if (translationSelected)
        //    radioButtonAxis.Checked = true;

        radioButtonCurrent_CheckedChanged(sender, e); // (260629Ch) Translation ON 時の既定方向 (0,1,0) と Axis 強制はこちらが担う
    }
}

