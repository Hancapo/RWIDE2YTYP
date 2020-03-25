namespace IDE2YTYP
{
    partial class IDE2YTYP
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IDE2YTYP));
            this.browse_ide = new System.Windows.Forms.Button();
            this.ide_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ydr_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.out_textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.browse_ydr = new System.Windows.Forms.Button();
            this.browse_out = new System.Windows.Forms.Button();
            this.convert_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lodist_textbox = new System.Windows.Forms.TextBox();
            this.hdtexturedist_textbox = new System.Windows.Forms.TextBox();
            this.s = new System.Windows.Forms.Label();
            this.d = new System.Windows.Forms.GroupBox();
            this.txtStatus = new System.Windows.Forms.Label();
            this.cbLOD = new System.Windows.Forms.CheckBox();
            this.txtProcess = new System.Windows.Forms.Label();
            this.d.SuspendLayout();
            this.SuspendLayout();
            // 
            // browse_ide
            // 
            this.browse_ide.Location = new System.Drawing.Point(546, 33);
            this.browse_ide.Name = "browse_ide";
            this.browse_ide.Size = new System.Drawing.Size(75, 20);
            this.browse_ide.TabIndex = 0;
            this.browse_ide.Text = "Browse";
            this.browse_ide.UseVisualStyleBackColor = true;
            this.browse_ide.Click += new System.EventHandler(this.browse_ide_Click);
            // 
            // ide_textbox
            // 
            this.ide_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ide_textbox.Location = new System.Drawing.Point(190, 33);
            this.ide_textbox.Name = "ide_textbox";
            this.ide_textbox.Size = new System.Drawing.Size(350, 20);
            this.ide_textbox.TabIndex = 1;
            this.ide_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ide_textbox.TextChanged += new System.EventHandler(this.ide_textbox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IDE Folder";
            // 
            // ydr_textbox
            // 
            this.ydr_textbox.Location = new System.Drawing.Point(190, 59);
            this.ydr_textbox.Name = "ydr_textbox";
            this.ydr_textbox.Size = new System.Drawing.Size(350, 20);
            this.ydr_textbox.TabIndex = 1;
            this.ydr_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ydr_textbox.TextChanged += new System.EventHandler(this.ydr_textbox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "YDR Folder";
            // 
            // out_textbox
            // 
            this.out_textbox.Location = new System.Drawing.Point(190, 85);
            this.out_textbox.Name = "out_textbox";
            this.out_textbox.Size = new System.Drawing.Size(350, 20);
            this.out_textbox.TabIndex = 1;
            this.out_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.out_textbox.TextChanged += new System.EventHandler(this.out_textbox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Output Folder";
            // 
            // browse_ydr
            // 
            this.browse_ydr.Location = new System.Drawing.Point(546, 59);
            this.browse_ydr.Name = "browse_ydr";
            this.browse_ydr.Size = new System.Drawing.Size(75, 20);
            this.browse_ydr.TabIndex = 3;
            this.browse_ydr.Text = "Browse";
            this.browse_ydr.UseVisualStyleBackColor = true;
            this.browse_ydr.Click += new System.EventHandler(this.browse_ydr_Click);
            // 
            // browse_out
            // 
            this.browse_out.Location = new System.Drawing.Point(546, 85);
            this.browse_out.Name = "browse_out";
            this.browse_out.Size = new System.Drawing.Size(75, 20);
            this.browse_out.TabIndex = 4;
            this.browse_out.Text = "Browse";
            this.browse_out.UseVisualStyleBackColor = true;
            this.browse_out.Click += new System.EventHandler(this.browse_out_Click);
            // 
            // convert_button
            // 
            this.convert_button.Location = new System.Drawing.Point(300, 122);
            this.convert_button.Name = "convert_button";
            this.convert_button.Size = new System.Drawing.Size(150, 50);
            this.convert_button.TabIndex = 5;
            this.convert_button.Text = "Convert";
            this.convert_button.UseVisualStyleBackColor = true;
            this.convert_button.Click += new System.EventHandler(this.convert_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(113, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "LOD Distance";
            // 
            // lodist_textbox
            // 
            this.lodist_textbox.Location = new System.Drawing.Point(102, 138);
            this.lodist_textbox.MaxLength = 5;
            this.lodist_textbox.Name = "lodist_textbox";
            this.lodist_textbox.Size = new System.Drawing.Size(100, 20);
            this.lodist_textbox.TabIndex = 8;
            this.lodist_textbox.Text = "20000";
            this.lodist_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // hdtexturedist_textbox
            // 
            this.hdtexturedist_textbox.Location = new System.Drawing.Point(517, 138);
            this.hdtexturedist_textbox.MaxLength = 5;
            this.hdtexturedist_textbox.Name = "hdtexturedist_textbox";
            this.hdtexturedist_textbox.Size = new System.Drawing.Size(100, 20);
            this.hdtexturedist_textbox.TabIndex = 8;
            this.hdtexturedist_textbox.Text = "200";
            this.hdtexturedist_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // s
            // 
            this.s.AutoSize = true;
            this.s.Location = new System.Drawing.Point(514, 122);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(107, 13);
            this.s.TabIndex = 7;
            this.s.Text = "HD Texture Distance";
            // 
            // d
            // 
            this.d.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.d.AutoSize = true;
            this.d.Controls.Add(this.txtProcess);
            this.d.Controls.Add(this.txtStatus);
            this.d.Controls.Add(this.cbLOD);
            this.d.Controls.Add(this.ide_textbox);
            this.d.Controls.Add(this.label1);
            this.d.Controls.Add(this.label2);
            this.d.Controls.Add(this.lodist_textbox);
            this.d.Controls.Add(this.hdtexturedist_textbox);
            this.d.Controls.Add(this.label3);
            this.d.Controls.Add(this.browse_out);
            this.d.Controls.Add(this.label4);
            this.d.Controls.Add(this.s);
            this.d.Controls.Add(this.out_textbox);
            this.d.Controls.Add(this.ydr_textbox);
            this.d.Controls.Add(this.convert_button);
            this.d.Controls.Add(this.browse_ide);
            this.d.Controls.Add(this.browse_ydr);
            this.d.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.d.Location = new System.Drawing.Point(12, 12);
            this.d.Name = "d";
            this.d.Size = new System.Drawing.Size(704, 371);
            this.d.TabIndex = 11;
            this.d.TabStop = false;
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.Location = new System.Drawing.Point(333, 218);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(61, 13);
            this.txtStatus.TabIndex = 12;
            this.txtStatus.Text = "No process";
            this.txtStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbLOD
            // 
            this.cbLOD.AutoSize = true;
            this.cbLOD.Location = new System.Drawing.Point(102, 185);
            this.cbLOD.Name = "cbLOD";
            this.cbLOD.Size = new System.Drawing.Size(106, 17);
            this.cbLOD.TabIndex = 11;
            this.cbLOD.Text = "Split to LOD type";
            this.cbLOD.UseVisualStyleBackColor = true;
            // 
            // txtProcess
            // 
            this.txtProcess.AutoSize = true;
            this.txtProcess.Location = new System.Drawing.Point(353, 189);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.Size = new System.Drawing.Size(24, 13);
            this.txtProcess.TabIndex = 14;
            this.txtProcess.Text = "Idle";
            this.txtProcess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IDE2YTYP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(723, 289);
            this.Controls.Add(this.d);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.Name = "IDE2YTYP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IDE2YTYP";
            this.d.ResumeLayout(false);
            this.d.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browse_ide;
        private System.Windows.Forms.TextBox ide_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ydr_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox out_textbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button browse_ydr;
        private System.Windows.Forms.Button browse_out;
        private System.Windows.Forms.Button convert_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lodist_textbox;
        private System.Windows.Forms.TextBox hdtexturedist_textbox;
        private System.Windows.Forms.Label s;
        private System.Windows.Forms.GroupBox d;
        private System.Windows.Forms.CheckBox cbLOD;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.Label txtProcess;
    }
}

