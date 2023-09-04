namespace SerialPlotter
{
    partial class SerialPlotter
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(60D, 3500D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(120D, 1000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(180D, 250D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(240D, 4000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(300D, 2000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(360D, 4500D);
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.serialPortTp = new System.Windows.Forms.TabPage();
            this.settingsGb = new System.Windows.Forms.GroupBox();
            this.portStatusPb = new System.Windows.Forms.ProgressBar();
            this.closeBtn = new System.Windows.Forms.Button();
            this.openBtn = new System.Windows.Forms.Button();
            this.flowControlLbl = new System.Windows.Forms.Label();
            this.flowControlCb = new System.Windows.Forms.ComboBox();
            this.parityLbl = new System.Windows.Forms.Label();
            this.parityCb = new System.Windows.Forms.ComboBox();
            this.stopBitsLbl = new System.Windows.Forms.Label();
            this.stopBitsCb = new System.Windows.Forms.ComboBox();
            this.dataBitsLbl = new System.Windows.Forms.Label();
            this.dataBitsCb = new System.Windows.Forms.ComboBox();
            this.baudrateLbl = new System.Windows.Forms.Label();
            this.comPortLbl = new System.Windows.Forms.Label();
            this.baudrateCb = new System.Windows.Forms.ComboBox();
            this.comPortCb = new System.Windows.Forms.ComboBox();
            this.graphTp = new System.Windows.Forms.TabPage();
            this.controlGb = new System.Windows.Forms.GroupBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.saveDataBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.graphGb = new System.Windows.Forms.GroupBox();
            this.lineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.serialPortTp.SuspendLayout();
            this.settingsGb.SuspendLayout();
            this.graphTp.SuspendLayout();
            this.controlGb.SuspendLayout();
            this.graphGb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.serialPortTp);
            this.tabControl1.Controls.Add(this.graphTp);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 533);
            this.tabControl1.TabIndex = 6;
            // 
            // serialPortTp
            // 
            this.serialPortTp.Controls.Add(this.settingsGb);
            this.serialPortTp.Location = new System.Drawing.Point(4, 22);
            this.serialPortTp.Name = "serialPortTp";
            this.serialPortTp.Padding = new System.Windows.Forms.Padding(3);
            this.serialPortTp.Size = new System.Drawing.Size(792, 507);
            this.serialPortTp.TabIndex = 0;
            this.serialPortTp.Text = "Serial Port";
            this.serialPortTp.UseVisualStyleBackColor = true;
            // 
            // settingsGb
            // 
            this.settingsGb.Controls.Add(this.portStatusPb);
            this.settingsGb.Controls.Add(this.closeBtn);
            this.settingsGb.Controls.Add(this.openBtn);
            this.settingsGb.Controls.Add(this.flowControlLbl);
            this.settingsGb.Controls.Add(this.flowControlCb);
            this.settingsGb.Controls.Add(this.parityLbl);
            this.settingsGb.Controls.Add(this.parityCb);
            this.settingsGb.Controls.Add(this.stopBitsLbl);
            this.settingsGb.Controls.Add(this.stopBitsCb);
            this.settingsGb.Controls.Add(this.dataBitsLbl);
            this.settingsGb.Controls.Add(this.dataBitsCb);
            this.settingsGb.Controls.Add(this.baudrateLbl);
            this.settingsGb.Controls.Add(this.comPortLbl);
            this.settingsGb.Controls.Add(this.baudrateCb);
            this.settingsGb.Controls.Add(this.comPortCb);
            this.settingsGb.Location = new System.Drawing.Point(209, 29);
            this.settingsGb.Name = "settingsGb";
            this.settingsGb.Size = new System.Drawing.Size(396, 379);
            this.settingsGb.TabIndex = 0;
            this.settingsGb.TabStop = false;
            this.settingsGb.Text = "Settings";
            // 
            // portStatusPb
            // 
            this.portStatusPb.Location = new System.Drawing.Point(31, 309);
            this.portStatusPb.Name = "portStatusPb";
            this.portStatusPb.Size = new System.Drawing.Size(335, 50);
            this.portStatusPb.TabIndex = 14;
            // 
            // closeBtn
            // 
            this.closeBtn.Enabled = false;
            this.closeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.Location = new System.Drawing.Point(210, 228);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(156, 59);
            this.closeBtn.TabIndex = 13;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // openBtn
            // 
            this.openBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openBtn.Location = new System.Drawing.Point(31, 228);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(162, 59);
            this.openBtn.TabIndex = 12;
            this.openBtn.Text = "Open";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // flowControlLbl
            // 
            this.flowControlLbl.AutoSize = true;
            this.flowControlLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowControlLbl.Location = new System.Drawing.Point(27, 180);
            this.flowControlLbl.Name = "flowControlLbl";
            this.flowControlLbl.Size = new System.Drawing.Size(98, 20);
            this.flowControlLbl.TabIndex = 11;
            this.flowControlLbl.Text = "Flow control:";
            // 
            // flowControlCb
            // 
            this.flowControlCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flowControlCb.FormattingEnabled = true;
            this.flowControlCb.Items.AddRange(new object[] {
            "None",
            "Xon/Xoff"});
            this.flowControlCb.Location = new System.Drawing.Point(186, 179);
            this.flowControlCb.Name = "flowControlCb";
            this.flowControlCb.Size = new System.Drawing.Size(180, 21);
            this.flowControlCb.TabIndex = 10;
            // 
            // parityLbl
            // 
            this.parityLbl.AutoSize = true;
            this.parityLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parityLbl.Location = new System.Drawing.Point(27, 153);
            this.parityLbl.Name = "parityLbl";
            this.parityLbl.Size = new System.Drawing.Size(52, 20);
            this.parityLbl.TabIndex = 9;
            this.parityLbl.Text = "Parity:";
            // 
            // parityCb
            // 
            this.parityCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parityCb.FormattingEnabled = true;
            this.parityCb.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.parityCb.Location = new System.Drawing.Point(186, 152);
            this.parityCb.Name = "parityCb";
            this.parityCb.Size = new System.Drawing.Size(180, 21);
            this.parityCb.TabIndex = 8;
            // 
            // stopBitsLbl
            // 
            this.stopBitsLbl.AutoSize = true;
            this.stopBitsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopBitsLbl.Location = new System.Drawing.Point(27, 126);
            this.stopBitsLbl.Name = "stopBitsLbl";
            this.stopBitsLbl.Size = new System.Drawing.Size(76, 20);
            this.stopBitsLbl.TabIndex = 7;
            this.stopBitsLbl.Text = "Stop bits:";
            // 
            // stopBitsCb
            // 
            this.stopBitsCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stopBitsCb.FormattingEnabled = true;
            this.stopBitsCb.Items.AddRange(new object[] {
            "1 bit",
            "1.5 bit",
            "2 bit"});
            this.stopBitsCb.Location = new System.Drawing.Point(186, 125);
            this.stopBitsCb.Name = "stopBitsCb";
            this.stopBitsCb.Size = new System.Drawing.Size(180, 21);
            this.stopBitsCb.TabIndex = 6;
            // 
            // dataBitsLbl
            // 
            this.dataBitsLbl.AutoSize = true;
            this.dataBitsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataBitsLbl.Location = new System.Drawing.Point(27, 99);
            this.dataBitsLbl.Name = "dataBitsLbl";
            this.dataBitsLbl.Size = new System.Drawing.Size(77, 20);
            this.dataBitsLbl.TabIndex = 5;
            this.dataBitsLbl.Text = "Data bits:";
            // 
            // dataBitsCb
            // 
            this.dataBitsCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataBitsCb.FormattingEnabled = true;
            this.dataBitsCb.Items.AddRange(new object[] {
            "7 bits",
            "8 bits"});
            this.dataBitsCb.Location = new System.Drawing.Point(186, 98);
            this.dataBitsCb.Name = "dataBitsCb";
            this.dataBitsCb.Size = new System.Drawing.Size(180, 21);
            this.dataBitsCb.TabIndex = 4;
            // 
            // baudrateLbl
            // 
            this.baudrateLbl.AutoSize = true;
            this.baudrateLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baudrateLbl.Location = new System.Drawing.Point(27, 72);
            this.baudrateLbl.Name = "baudrateLbl";
            this.baudrateLbl.Size = new System.Drawing.Size(79, 20);
            this.baudrateLbl.TabIndex = 3;
            this.baudrateLbl.Text = "Baudrate:";
            // 
            // comPortLbl
            // 
            this.comPortLbl.AutoSize = true;
            this.comPortLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comPortLbl.Location = new System.Drawing.Point(27, 45);
            this.comPortLbl.Name = "comPortLbl";
            this.comPortLbl.Size = new System.Drawing.Size(82, 20);
            this.comPortLbl.TabIndex = 1;
            this.comPortLbl.Text = "COM Port:";
            // 
            // baudrateCb
            // 
            this.baudrateCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudrateCb.FormattingEnabled = true;
            this.baudrateCb.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.baudrateCb.Location = new System.Drawing.Point(186, 71);
            this.baudrateCb.Name = "baudrateCb";
            this.baudrateCb.Size = new System.Drawing.Size(180, 21);
            this.baudrateCb.TabIndex = 2;
            // 
            // comPortCb
            // 
            this.comPortCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPortCb.FormattingEnabled = true;
            this.comPortCb.Location = new System.Drawing.Point(186, 44);
            this.comPortCb.Name = "comPortCb";
            this.comPortCb.Size = new System.Drawing.Size(180, 21);
            this.comPortCb.TabIndex = 0;
            // 
            // graphTp
            // 
            this.graphTp.Controls.Add(this.controlGb);
            this.graphTp.Controls.Add(this.graphGb);
            this.graphTp.Location = new System.Drawing.Point(4, 22);
            this.graphTp.Name = "graphTp";
            this.graphTp.Padding = new System.Windows.Forms.Padding(3);
            this.graphTp.Size = new System.Drawing.Size(792, 507);
            this.graphTp.TabIndex = 1;
            this.graphTp.Text = "Graph";
            this.graphTp.UseVisualStyleBackColor = true;
            // 
            // controlGb
            // 
            this.controlGb.Controls.Add(this.startBtn);
            this.controlGb.Controls.Add(this.saveDataBtn);
            this.controlGb.Controls.Add(this.stopBtn);
            this.controlGb.Controls.Add(this.clearBtn);
            this.controlGb.Location = new System.Drawing.Point(10, 386);
            this.controlGb.Name = "controlGb";
            this.controlGb.Size = new System.Drawing.Size(773, 113);
            this.controlGb.TabIndex = 5;
            this.controlGb.TabStop = false;
            this.controlGb.Text = "Control";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(6, 23);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(163, 78);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // saveDataBtn
            // 
            this.saveDataBtn.Location = new System.Drawing.Point(604, 23);
            this.saveDataBtn.Name = "saveDataBtn";
            this.saveDataBtn.Size = new System.Drawing.Size(163, 78);
            this.saveDataBtn.TabIndex = 4;
            this.saveDataBtn.Text = "Save Data";
            this.saveDataBtn.UseVisualStyleBackColor = true;
            this.saveDataBtn.Click += new System.EventHandler(this.saveDataBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(207, 23);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(163, 78);
            this.stopBtn.TabIndex = 2;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(408, 23);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(163, 78);
            this.clearBtn.TabIndex = 3;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // graphGb
            // 
            this.graphGb.Controls.Add(this.lineChart);
            this.graphGb.Location = new System.Drawing.Point(10, 6);
            this.graphGb.Name = "graphGb";
            this.graphGb.Size = new System.Drawing.Size(773, 374);
            this.graphGb.TabIndex = 1;
            this.graphGb.TabStop = false;
            this.graphGb.Text = "Graph";
            // 
            // lineChart
            // 
            chartArea1.AxisX.Crossing = -1.7976931348623157E+308D;
            chartArea1.AxisX.InterlacedColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.Interval = 100D;
            chartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.MajorGrid.Interval = 100D;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.Maximum = 500D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.Interval = 25D;
            chartArea1.AxisX.MinorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.MinorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.MinorTickMark.Enabled = true;
            chartArea1.AxisX.MinorTickMark.Interval = 50D;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea1.AxisX.Title = "Points";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            chartArea1.AxisY.Crossing = -1.7976931348623157E+308D;
            chartArea1.AxisY.InterlacedColor = System.Drawing.Color.LightGoldenrodYellow;
            chartArea1.AxisY.Interval = 1000D;
            chartArea1.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.Maximum = 4100D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.Interval = 500D;
            chartArea1.AxisY.MinorGrid.IntervalOffset = double.NaN;
            chartArea1.AxisY.MinorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.MinorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.MinorTickMark.Enabled = true;
            chartArea1.AxisY.MinorTickMark.Interval = 500D;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MinorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
            chartArea1.AxisY.Title = "ADC Value";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.Name = "ChartArea1";
            this.lineChart.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.BorderColor = System.Drawing.Color.Blue;
            legend1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            legend1.InterlacedRowsColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.lineChart.Legends.Add(legend1);
            this.lineChart.Location = new System.Drawing.Point(6, 19);
            this.lineChart.Name = "lineChart";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Blue;
            series1.Legend = "Legend1";
            series1.LegendText = "ADC Values";
            series1.Name = "adcSerie";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series1.Points.Add(dataPoint4);
            series1.Points.Add(dataPoint5);
            series1.Points.Add(dataPoint6);
            series1.Points.Add(dataPoint7);
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.lineChart.Series.Add(series1);
            this.lineChart.Size = new System.Drawing.Size(761, 349);
            this.lineChart.TabIndex = 0;
            this.lineChart.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            title1.Name = "Title1";
            title1.Text = "Line Chart";
            title1.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            this.lineChart.Titles.Add(title1);
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 5;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // SerialPlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 536);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SerialPlotter";
            this.Text = "Serial Plotter";
            this.tabControl1.ResumeLayout(false);
            this.serialPortTp.ResumeLayout(false);
            this.settingsGb.ResumeLayout(false);
            this.settingsGb.PerformLayout();
            this.graphTp.ResumeLayout(false);
            this.controlGb.ResumeLayout(false);
            this.graphGb.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage serialPortTp;
        private System.Windows.Forms.GroupBox settingsGb;
        private System.Windows.Forms.ProgressBar portStatusPb;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Label flowControlLbl;
        private System.Windows.Forms.ComboBox flowControlCb;
        private System.Windows.Forms.Label parityLbl;
        private System.Windows.Forms.ComboBox parityCb;
        private System.Windows.Forms.Label stopBitsLbl;
        private System.Windows.Forms.ComboBox stopBitsCb;
        private System.Windows.Forms.Label dataBitsLbl;
        private System.Windows.Forms.ComboBox dataBitsCb;
        private System.Windows.Forms.Label baudrateLbl;
        private System.Windows.Forms.Label comPortLbl;
        private System.Windows.Forms.ComboBox baudrateCb;
        private System.Windows.Forms.ComboBox comPortCb;
        private System.Windows.Forms.TabPage graphTp;
        private System.Windows.Forms.GroupBox controlGb;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button saveDataBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.GroupBox graphGb;
        private System.Windows.Forms.DataVisualization.Charting.Chart lineChart;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}

