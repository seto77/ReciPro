namespace ReciPro
{
    partial class FormExport3DModel
    {
        /// <summary>必要なデザイナ変数です。</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>使用中のリソースをすべてクリーンアップします。</summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        //260804Cl 全面改稿: オプション選択フォーム化 (スケール/要素選択/多面体スタイル/増径/形式)。旧版は commit 9f5c3510 参照
        private void InitializeComponent()
        {
            captureExtender.SetCapture(this, true); // 260803Cl 追加: GUI監査キャプチャ対象 (フォーム全体)
            labelInfo = new System.Windows.Forms.Label();
            labelSizeAng = new System.Windows.Forms.Label();
            groupBoxScale = new System.Windows.Forms.GroupBox();
            radioButtonFit = new System.Windows.Forms.RadioButton();
            numericUpDownMaxSize = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            radioButtonScale = new System.Windows.Forms.RadioButton();
            numericUpDownScale = new System.Windows.Forms.NumericUpDown();
            label4 = new System.Windows.Forms.Label();
            labelResult = new System.Windows.Forms.Label();
            groupBoxInclude = new System.Windows.Forms.GroupBox();
            checkBoxAtoms = new System.Windows.Forms.CheckBox();
            checkBoxBonds = new System.Windows.Forms.CheckBox();
            checkBoxPolyhedra = new System.Windows.Forms.CheckBox();
            radioButtonPolySolid = new System.Windows.Forms.RadioButton();
            radioButtonPolyEdges = new System.Windows.Forms.RadioButton();
            radioButtonPolyMesh = new System.Windows.Forms.RadioButton(); // 260805Cl 追加: 透かし格子 (メッシュ) スタイル
            numericUpDownPolyEdgeDia = new System.Windows.Forms.NumericUpDown();
            numericUpDownPolyPitch = new System.Windows.Forms.NumericUpDown(); // 260805Cl 追加
            label5 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label(); // 260805Cl 追加
            checkBoxCellEdges = new System.Windows.Forms.CheckBox();
            numericUpDownEdgeDia = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            groupBoxPrintability = new System.Windows.Forms.GroupBox();
            checkBoxThicken = new System.Windows.Forms.CheckBox();
            numericUpDownMinBond = new System.Windows.Forms.NumericUpDown();
            label6 = new System.Windows.Forms.Label();
            labelWarning = new System.Windows.Forms.Label();
            groupBoxFormat = new System.Windows.Forms.GroupBox();
            radioButtonStl = new System.Windows.Forms.RadioButton();
            radioButton3mf = new System.Windows.Forms.RadioButton();
            buttonOK = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownMaxSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownPolyEdgeDia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownPolyPitch)).BeginInit(); // 260805Cl 追加
            ((System.ComponentModel.ISupportInitialize)(numericUpDownEdgeDia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownMinBond)).BeginInit();
            groupBoxScale.SuspendLayout();
            groupBoxInclude.SuspendLayout();
            groupBoxPrintability.SuspendLayout();
            groupBoxFormat.SuspendLayout();
            SuspendLayout();
            //
            // labelInfo
            //
            labelInfo.AutoSize = true;
            labelInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelInfo.Location = new System.Drawing.Point(10, 8);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new System.Drawing.Size(140, 15);
            labelInfo.TabIndex = 0;
            labelInfo.Text = "Objects: 0,  Triangles: 0";
            //
            // labelSizeAng
            //
            labelSizeAng.AutoSize = true;
            labelSizeAng.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelSizeAng.Location = new System.Drawing.Point(10, 26);
            labelSizeAng.Name = "labelSizeAng";
            labelSizeAng.Size = new System.Drawing.Size(120, 15);
            labelSizeAng.TabIndex = 1;
            labelSizeAng.Text = "Model size: 0 × 0 × 0 nm";//260805Cl 変更
            //
            // groupBoxScale
            //
            groupBoxScale.Controls.Add(radioButtonFit);
            groupBoxScale.Controls.Add(numericUpDownMaxSize);
            groupBoxScale.Controls.Add(label2);
            groupBoxScale.Controls.Add(radioButtonScale);
            groupBoxScale.Controls.Add(numericUpDownScale);
            groupBoxScale.Controls.Add(label4);
            groupBoxScale.Controls.Add(labelResult);
            groupBoxScale.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            groupBoxScale.Location = new System.Drawing.Point(10, 48);
            groupBoxScale.Name = "groupBoxScale";
            groupBoxScale.Size = new System.Drawing.Size(382, 100);
            groupBoxScale.TabIndex = 2;
            groupBoxScale.TabStop = false;
            groupBoxScale.Text = "Scale";
            //
            // radioButtonFit
            //
            radioButtonFit.AutoSize = true;
            radioButtonFit.Checked = true;
            radioButtonFit.Location = new System.Drawing.Point(12, 19);
            radioButtonFit.Name = "radioButtonFit";
            radioButtonFit.Size = new System.Drawing.Size(150, 19);
            radioButtonFit.TabIndex = 0;
            radioButtonFit.TabStop = true;
            radioButtonFit.Text = "Fit largest dimension:";
            radioButtonFit.UseVisualStyleBackColor = true;
            radioButtonFit.CheckedChanged += update;
            //
            // numericUpDownMaxSize
            //
            numericUpDownMaxSize.DecimalPlaces = 1;
            numericUpDownMaxSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            numericUpDownMaxSize.Location = new System.Drawing.Point(168, 18);
            numericUpDownMaxSize.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            numericUpDownMaxSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            numericUpDownMaxSize.Name = "numericUpDownMaxSize";
            numericUpDownMaxSize.Size = new System.Drawing.Size(62, 21);
            numericUpDownMaxSize.TabIndex = 1;
            numericUpDownMaxSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDownMaxSize.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            numericUpDownMaxSize.ValueChanged += update;
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(234, 20);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(28, 15);
            label2.TabIndex = 2;
            label2.Text = "mm";
            //
            // radioButtonScale
            //
            radioButtonScale.AutoSize = true;
            radioButtonScale.Location = new System.Drawing.Point(12, 44);
            radioButtonScale.Name = "radioButtonScale";
            radioButtonScale.Size = new System.Drawing.Size(95, 19);
            radioButtonScale.TabIndex = 3;
            radioButtonScale.Text = "Fixed scale:";
            radioButtonScale.UseVisualStyleBackColor = true;
            radioButtonScale.CheckedChanged += update;
            //
            // numericUpDownScale
            //
            numericUpDownScale.DecimalPlaces = 3;
            numericUpDownScale.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            numericUpDownScale.Location = new System.Drawing.Point(168, 43);
            numericUpDownScale.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            numericUpDownScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            numericUpDownScale.Name = "numericUpDownScale";
            numericUpDownScale.Size = new System.Drawing.Size(70, 21);
            numericUpDownScale.TabIndex = 4;
            numericUpDownScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDownScale.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            numericUpDownScale.ValueChanged += update;
            //
            // label4
            //
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(242, 45);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(40, 15);
            label4.TabIndex = 5;
            label4.Text = "mm/nm";//260805Cl 変更: 旧 "mm/Å" (実体は nm)
            //
            // labelResult
            //
            labelResult.AutoSize = true;
            labelResult.Location = new System.Drawing.Point(12, 72);
            labelResult.Name = "labelResult";
            labelResult.Size = new System.Drawing.Size(200, 15);
            labelResult.TabIndex = 6;
            labelResult.Text = "Scale: 1.000 mm/nm,   Output size: 0 × 0 × 0 mm";//260805Cl 変更
            //
            // groupBoxInclude
            //
            groupBoxInclude.Controls.Add(checkBoxAtoms);
            groupBoxInclude.Controls.Add(checkBoxBonds);
            groupBoxInclude.Controls.Add(checkBoxPolyhedra);
            groupBoxInclude.Controls.Add(radioButtonPolySolid);
            groupBoxInclude.Controls.Add(radioButtonPolyEdges);
            groupBoxInclude.Controls.Add(radioButtonPolyMesh); // 260805Cl 追加
            groupBoxInclude.Controls.Add(numericUpDownPolyEdgeDia);
            groupBoxInclude.Controls.Add(numericUpDownPolyPitch); // 260805Cl 追加
            groupBoxInclude.Controls.Add(label5);
            groupBoxInclude.Controls.Add(label7); // 260805Cl 追加
            groupBoxInclude.Controls.Add(checkBoxCellEdges);
            groupBoxInclude.Controls.Add(numericUpDownEdgeDia);
            groupBoxInclude.Controls.Add(label3);
            groupBoxInclude.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            groupBoxInclude.Location = new System.Drawing.Point(10, 154);
            groupBoxInclude.Name = "groupBoxInclude";
            groupBoxInclude.Size = new System.Drawing.Size(382, 128); // 260805Cl 変更: mesh 行の追加で 102→128
            groupBoxInclude.TabIndex = 3;
            groupBoxInclude.TabStop = false;
            groupBoxInclude.Text = "Include";
            //
            // checkBoxAtoms
            //
            checkBoxAtoms.AutoSize = true;
            checkBoxAtoms.Checked = true;
            checkBoxAtoms.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxAtoms.Location = new System.Drawing.Point(12, 20);
            checkBoxAtoms.Name = "checkBoxAtoms";
            checkBoxAtoms.Size = new System.Drawing.Size(62, 19);
            checkBoxAtoms.TabIndex = 0;
            checkBoxAtoms.Text = "Atoms";
            checkBoxAtoms.UseVisualStyleBackColor = true;
            checkBoxAtoms.CheckedChanged += update;
            //
            // checkBoxBonds
            //
            checkBoxBonds.AutoSize = true;
            checkBoxBonds.Checked = true;
            checkBoxBonds.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxBonds.Location = new System.Drawing.Point(100, 20);
            checkBoxBonds.Name = "checkBoxBonds";
            checkBoxBonds.Size = new System.Drawing.Size(60, 19);
            checkBoxBonds.TabIndex = 1;
            checkBoxBonds.Text = "Bonds";
            checkBoxBonds.UseVisualStyleBackColor = true;
            checkBoxBonds.CheckedChanged += update;
            //
            // checkBoxPolyhedra
            //
            checkBoxPolyhedra.AutoSize = true;
            checkBoxPolyhedra.Checked = true;
            checkBoxPolyhedra.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxPolyhedra.Location = new System.Drawing.Point(188, 20);
            checkBoxPolyhedra.Name = "checkBoxPolyhedra";
            checkBoxPolyhedra.Size = new System.Drawing.Size(80, 19);
            checkBoxPolyhedra.TabIndex = 2;
            checkBoxPolyhedra.Text = "Polyhedra";
            checkBoxPolyhedra.UseVisualStyleBackColor = true;
            checkBoxPolyhedra.CheckedChanged += update;
            //
            // radioButtonPolySolid
            //
            radioButtonPolySolid.AutoSize = true;
            radioButtonPolySolid.Checked = true;
            radioButtonPolySolid.Location = new System.Drawing.Point(28, 45);
            radioButtonPolySolid.Name = "radioButtonPolySolid";
            radioButtonPolySolid.Size = new System.Drawing.Size(140, 19);
            radioButtonPolySolid.TabIndex = 3;
            radioButtonPolySolid.TabStop = true;
            radioButtonPolySolid.Text = "Polyhedra: solid faces";
            radioButtonPolySolid.UseVisualStyleBackColor = true;
            radioButtonPolySolid.CheckedChanged += update;
            //
            // radioButtonPolyEdges
            //
            radioButtonPolyEdges.AutoSize = true;
            radioButtonPolyEdges.Location = new System.Drawing.Point(188, 45);
            radioButtonPolyEdges.Name = "radioButtonPolyEdges";
            radioButtonPolyEdges.Size = new System.Drawing.Size(95, 19);
            radioButtonPolyEdges.TabIndex = 4;
            radioButtonPolyEdges.Text = "edges,  dia.:";
            radioButtonPolyEdges.UseVisualStyleBackColor = true;
            radioButtonPolyEdges.CheckedChanged += update;
            //
            // numericUpDownPolyEdgeDia
            //
            numericUpDownPolyEdgeDia.DecimalPlaces = 1;
            numericUpDownPolyEdgeDia.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            numericUpDownPolyEdgeDia.Location = new System.Drawing.Point(282, 43);
            numericUpDownPolyEdgeDia.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            65536});
            numericUpDownPolyEdgeDia.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            numericUpDownPolyEdgeDia.Name = "numericUpDownPolyEdgeDia";
            numericUpDownPolyEdgeDia.Size = new System.Drawing.Size(52, 21);
            numericUpDownPolyEdgeDia.TabIndex = 5;
            numericUpDownPolyEdgeDia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDownPolyEdgeDia.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            //
            // label5
            //
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(338, 45);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(28, 15);
            label5.TabIndex = 6;
            label5.Text = "mm";
            //
            // radioButtonPolyMesh (260805Cl 追加: 透かし格子スタイル。バー径は稜線と同じ numericUpDownPolyEdgeDia)
            //
            radioButtonPolyMesh.AutoSize = true;
            radioButtonPolyMesh.Location = new System.Drawing.Point(28, 70);
            radioButtonPolyMesh.Name = "radioButtonPolyMesh";
            radioButtonPolyMesh.Size = new System.Drawing.Size(150, 19);
            radioButtonPolyMesh.TabIndex = 10;
            radioButtonPolyMesh.Text = "see-through mesh,  pitch:";
            radioButtonPolyMesh.UseVisualStyleBackColor = true;
            radioButtonPolyMesh.CheckedChanged += update;
            //
            // numericUpDownPolyPitch (260805Cl 追加)
            //
            numericUpDownPolyPitch.DecimalPlaces = 1;
            numericUpDownPolyPitch.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            numericUpDownPolyPitch.Location = new System.Drawing.Point(282, 68);
            numericUpDownPolyPitch.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            numericUpDownPolyPitch.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            numericUpDownPolyPitch.Name = "numericUpDownPolyPitch";
            numericUpDownPolyPitch.Size = new System.Drawing.Size(52, 21);
            numericUpDownPolyPitch.TabIndex = 11;
            numericUpDownPolyPitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDownPolyPitch.Value = new decimal(new int[] {
            50,
            0,
            0,
            65536});
            //
            // label7 (260805Cl 追加)
            //
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(338, 70);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(28, 15);
            label7.TabIndex = 12;
            label7.Text = "mm";
            //
            // checkBoxCellEdges
            //
            checkBoxCellEdges.AutoSize = true;
            checkBoxCellEdges.Location = new System.Drawing.Point(12, 98); // 260805Cl 変更: mesh 行の追加で 73→98
            checkBoxCellEdges.Name = "checkBoxCellEdges";
            checkBoxCellEdges.Size = new System.Drawing.Size(220, 19);
            checkBoxCellEdges.TabIndex = 7;
            checkBoxCellEdges.Text = "Unit cell edges as cylinders,  dia.:";
            checkBoxCellEdges.UseVisualStyleBackColor = true;
            checkBoxCellEdges.CheckedChanged += update;
            //
            // numericUpDownEdgeDia
            //
            numericUpDownEdgeDia.DecimalPlaces = 1;
            numericUpDownEdgeDia.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            numericUpDownEdgeDia.Location = new System.Drawing.Point(224, 96); // 260805Cl 変更: 71→96
            numericUpDownEdgeDia.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            65536});
            numericUpDownEdgeDia.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            numericUpDownEdgeDia.Name = "numericUpDownEdgeDia";
            numericUpDownEdgeDia.Size = new System.Drawing.Size(52, 21);
            numericUpDownEdgeDia.TabIndex = 8;
            numericUpDownEdgeDia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //260804Cl 変更: 既定⌀ 1.6→2.4mm (A1 mini 印刷テストで 1.6mm は細すぎと判明。1.5倍に)
            numericUpDownEdgeDia.Value = new decimal(new int[] {
            24,
            0,
            0,
            65536});
            //
            // label3
            //
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(280, 98); // 260805Cl 変更: 73→98
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(28, 15);
            label3.TabIndex = 9;
            label3.Text = "mm";
            //
            // groupBoxPrintability
            //
            groupBoxPrintability.Controls.Add(checkBoxThicken);
            groupBoxPrintability.Controls.Add(numericUpDownMinBond);
            groupBoxPrintability.Controls.Add(label6);
            groupBoxPrintability.Controls.Add(labelWarning);
            groupBoxPrintability.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            groupBoxPrintability.Location = new System.Drawing.Point(10, 288); // 260805Cl 変更: 262→288
            groupBoxPrintability.Name = "groupBoxPrintability";
            groupBoxPrintability.Size = new System.Drawing.Size(382, 88);
            groupBoxPrintability.TabIndex = 4;
            groupBoxPrintability.TabStop = false;
            groupBoxPrintability.Text = "Printability";
            //
            // checkBoxThicken
            //
            checkBoxThicken.AutoSize = true;
            checkBoxThicken.Checked = true;
            checkBoxThicken.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxThicken.Location = new System.Drawing.Point(12, 20);
            checkBoxThicken.Name = "checkBoxThicken";
            checkBoxThicken.Size = new System.Drawing.Size(190, 19);
            checkBoxThicken.TabIndex = 0;
            checkBoxThicken.Text = "Thicken bonds thinner than:";
            checkBoxThicken.UseVisualStyleBackColor = true;
            checkBoxThicken.CheckedChanged += update;
            //
            // numericUpDownMinBond
            //
            numericUpDownMinBond.DecimalPlaces = 1;
            numericUpDownMinBond.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            numericUpDownMinBond.Location = new System.Drawing.Point(200, 18);
            numericUpDownMinBond.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            65536});
            numericUpDownMinBond.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            numericUpDownMinBond.Name = "numericUpDownMinBond";
            numericUpDownMinBond.Size = new System.Drawing.Size(52, 21);
            numericUpDownMinBond.TabIndex = 1;
            numericUpDownMinBond.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDownMinBond.Value = new decimal(new int[] {
            12,
            0,
            0,
            65536});
            numericUpDownMinBond.ValueChanged += update;
            //
            // label6
            //
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(256, 20);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(28, 15);
            label6.TabIndex = 2;
            label6.Text = "mm";
            //
            // labelWarning
            //
            labelWarning.AutoSize = true;
            labelWarning.ForeColor = System.Drawing.Color.Crimson;
            labelWarning.Location = new System.Drawing.Point(12, 44);
            labelWarning.MaximumSize = new System.Drawing.Size(360, 0);
            labelWarning.Name = "labelWarning";
            labelWarning.Size = new System.Drawing.Size(0, 15);
            labelWarning.TabIndex = 3;
            //
            // groupBoxFormat
            //
            groupBoxFormat.Controls.Add(radioButtonStl);
            groupBoxFormat.Controls.Add(radioButton3mf);
            groupBoxFormat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            groupBoxFormat.Location = new System.Drawing.Point(10, 382); // 260805Cl 変更: 356→382
            groupBoxFormat.Name = "groupBoxFormat";
            groupBoxFormat.Size = new System.Drawing.Size(382, 46);
            groupBoxFormat.TabIndex = 5;
            groupBoxFormat.TabStop = false;
            groupBoxFormat.Text = "Format";
            //
            // radioButtonStl
            //
            radioButtonStl.AutoSize = true;
            radioButtonStl.Checked = true;
            radioButtonStl.Location = new System.Drawing.Point(12, 18);
            radioButtonStl.Name = "radioButtonStl";
            radioButtonStl.Size = new System.Drawing.Size(125, 19);
            radioButtonStl.TabIndex = 0;
            radioButtonStl.TabStop = true;
            radioButtonStl.Text = "STL (single color)";
            radioButtonStl.UseVisualStyleBackColor = true;
            radioButtonStl.CheckedChanged += update;
            //
            // radioButton3mf
            //
            radioButton3mf.AutoSize = true;
            radioButton3mf.Location = new System.Drawing.Point(150, 18);
            radioButton3mf.Name = "radioButton3mf";
            radioButton3mf.Size = new System.Drawing.Size(210, 19);
            radioButton3mf.TabIndex = 1;
            radioButton3mf.Text = "3MF (parts colored by element)";
            radioButton3mf.UseVisualStyleBackColor = true;
            radioButton3mf.CheckedChanged += update;
            //
            // buttonOK
            //
            buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonOK.Location = new System.Drawing.Point(216, 436); // 260805Cl 変更: 410→436
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(100, 25);
            buttonOK.TabIndex = 6;
            buttonOK.Text = "Save...";
            buttonOK.UseVisualStyleBackColor = true;
            //
            // buttonCancel
            //
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonCancel.Location = new System.Drawing.Point(322, 436); // 260805Cl 変更: 410→436
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(65, 25);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            //
            // FormExport3DModel
            //
            AcceptButton = buttonOK;
            //260805Cl 変更: 旧 AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F); AutoScaleMode = AutoScaleMode.Font;
            //Font スケーリングだと、UiFont が言語ごとに別フォント (ko/zh は Yu Gothic UI/YaHei) を当てた瞬間に
            //絶対配置の NumericUpDown と AutoSize ラベルがずれて重なる (ko/zh-Hans のキャプチャで実測)。他フォームと同じ Dpi にする。
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            CancelButton = buttonCancel;
            ClientSize = new System.Drawing.Size(402, 471); // 260805Cl 変更: mesh 行の追加で 445→471
            Controls.Add(labelInfo);
            Controls.Add(labelSizeAng);
            Controls.Add(groupBoxScale);
            Controls.Add(groupBoxInclude);
            Controls.Add(groupBoxPrintability);
            Controls.Add(groupBoxFormat);
            Controls.Add(buttonOK);
            Controls.Add(buttonCancel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormExport3DModel";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Export 3D Model";
            ((System.ComponentModel.ISupportInitialize)(numericUpDownMaxSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownPolyEdgeDia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownPolyPitch)).EndInit(); // 260805Cl 追加
            ((System.ComponentModel.ISupportInitialize)(numericUpDownEdgeDia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownMinBond)).EndInit();
            groupBoxScale.ResumeLayout(false);
            groupBoxScale.PerformLayout();
            groupBoxInclude.ResumeLayout(false);
            groupBoxInclude.PerformLayout();
            groupBoxPrintability.ResumeLayout(false);
            groupBoxPrintability.PerformLayout();
            groupBoxFormat.ResumeLayout(false);
            groupBoxFormat.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelSizeAng;
        private System.Windows.Forms.GroupBox groupBoxScale;
        private System.Windows.Forms.RadioButton radioButtonFit;
        public System.Windows.Forms.NumericUpDown numericUpDownMaxSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonScale;
        public System.Windows.Forms.NumericUpDown numericUpDownScale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.GroupBox groupBoxInclude;
        private System.Windows.Forms.CheckBox checkBoxAtoms;
        private System.Windows.Forms.CheckBox checkBoxBonds;
        private System.Windows.Forms.CheckBox checkBoxPolyhedra;
        private System.Windows.Forms.RadioButton radioButtonPolySolid;
        private System.Windows.Forms.RadioButton radioButtonPolyEdges;
        private System.Windows.Forms.RadioButton radioButtonPolyMesh; // 260805Cl 追加
        public System.Windows.Forms.NumericUpDown numericUpDownPolyEdgeDia;
        public System.Windows.Forms.NumericUpDown numericUpDownPolyPitch; // 260805Cl 追加
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7; // 260805Cl 追加
        private System.Windows.Forms.CheckBox checkBoxCellEdges;
        public System.Windows.Forms.NumericUpDown numericUpDownEdgeDia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxPrintability;
        private System.Windows.Forms.CheckBox checkBoxThicken;
        public System.Windows.Forms.NumericUpDown numericUpDownMinBond;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.GroupBox groupBoxFormat;
        private System.Windows.Forms.RadioButton radioButtonStl;
        private System.Windows.Forms.RadioButton radioButton3mf;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}
