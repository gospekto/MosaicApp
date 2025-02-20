namespace MosaicProgram
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnProcess = new Button();
            imageList1 = new ImageList(components);
            imageList2 = new ImageList(components);
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ClibSelector = new RadioButton();
            ASMlibSelector = new RadioButton();
            threadNumber = new TrackBar();
            tileSize = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnChoosePhoto = new Button();
            btnDownload = new Button();
            ThreadNumberCount = new Label();
            TileSizeCount = new Label();
            saveFileDialog1 = new SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)threadNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tileSize).BeginInit();
            SuspendLayout();
            // 
            // btnProcess
            // 
            btnProcess.Location = new Point(139, 502);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(298, 59);
            btnProcess.TabIndex = 0;
            btnProcess.Text = "Process";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // imageList2
            // 
            imageList2.ColorDepth = ColorDepth.Depth8Bit;
            imageList2.ImageSize = new Size(16, 16);
            imageList2.TransparentColor = Color.Transparent;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(561, 21);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(526, 323);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(561, 369);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(526, 323);
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // ClibSelector
            // 
            ClibSelector.AutoSize = true;
            ClibSelector.Checked = true;
            ClibSelector.Location = new Point(203, 184);
            ClibSelector.Name = "ClibSelector";
            ClibSelector.Size = new Size(40, 19);
            ClibSelector.TabIndex = 3;
            ClibSelector.TabStop = true;
            ClibSelector.Text = "C#";
            ClibSelector.UseVisualStyleBackColor = true;
            // 
            // ASMlibSelector
            // 
            ASMlibSelector.AutoSize = true;
            ASMlibSelector.Location = new Point(287, 184);
            ASMlibSelector.Name = "ASMlibSelector";
            ASMlibSelector.Size = new Size(50, 19);
            ASMlibSelector.TabIndex = 4;
            ASMlibSelector.TabStop = true;
            ASMlibSelector.Text = "ASM";
            ASMlibSelector.UseVisualStyleBackColor = true;
            // 
            // threadNumber
            // 
            threadNumber.Location = new Point(139, 299);
            threadNumber.Maximum = 32;
            threadNumber.Minimum = 1;
            threadNumber.Name = "threadNumber";
            threadNumber.Size = new Size(298, 45);
            threadNumber.TabIndex = 5;
            threadNumber.Value = 1;
            threadNumber.Scroll += threadNumber_Scroll;
            // 
            // tileSize
            // 
            tileSize.LargeChange = 1;
            tileSize.Location = new Point(139, 385);
            tileSize.Maximum = 100;
            tileSize.Minimum = 1;
            tileSize.Name = "tileSize";
            tileSize.Size = new Size(298, 45);
            tileSize.TabIndex = 6;
            tileSize.Value = 1;
            tileSize.Scroll += tileSize_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(139, 268);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 7;
            label1.Text = "Thread count";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(139, 356);
            label2.Name = "label2";
            label2.Size = new Size(47, 15);
            label2.TabIndex = 8;
            label2.Text = "Tile size";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(234, 156);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 9;
            label3.Text = "Library";
            label3.Click += label3_Click;
            // 
            // btnChoosePhoto
            // 
            btnChoosePhoto.Location = new Point(139, 36);
            btnChoosePhoto.Name = "btnChoosePhoto";
            btnChoosePhoto.Size = new Size(298, 59);
            btnChoosePhoto.TabIndex = 10;
            btnChoosePhoto.Text = "Select picture";
            btnChoosePhoto.UseVisualStyleBackColor = true;
            btnChoosePhoto.Click += btnChoosePhoto_Click;
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(139, 590);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(298, 59);
            btnDownload.TabIndex = 11;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += button1_Click;
            // 
            // ThreadNumberCount
            // 
            ThreadNumberCount.AutoSize = true;
            ThreadNumberCount.Location = new Point(86, 299);
            ThreadNumberCount.Name = "ThreadNumberCount";
            ThreadNumberCount.Size = new Size(38, 15);
            ThreadNumberCount.TabIndex = 12;
            ThreadNumberCount.Text = "label4";
            // 
            // TileSizeCount
            // 
            TileSizeCount.AutoSize = true;
            TileSizeCount.Location = new Point(86, 385);
            TileSizeCount.Name = "TileSizeCount";
            TileSizeCount.Size = new Size(38, 15);
            TileSizeCount.TabIndex = 13;
            TileSizeCount.Text = "label5";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1108, 720);
            Controls.Add(TileSizeCount);
            Controls.Add(ThreadNumberCount);
            Controls.Add(btnDownload);
            Controls.Add(btnChoosePhoto);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tileSize);
            Controls.Add(threadNumber);
            Controls.Add(ASMlibSelector);
            Controls.Add(ClibSelector);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(btnProcess);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)threadNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)tileSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnProcess;
        private ImageList imageList1;
        private ImageList imageList2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private RadioButton ClibSelector;
        private RadioButton ASMlibSelector;
        private TrackBar threadNumber;
        private TrackBar tileSize;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnChoosePhoto;
        private Button btnDownload;
        private Label ThreadNumberCount;
        private Label TileSizeCount;
        private SaveFileDialog saveFileDialog1;
    }
}
