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
            this.convert_button = new MaterialSkin.Controls.MaterialButton();
            this.ide_textbox = new MaterialSkin.Controls.MaterialTextBox();
            this.browse_ide = new MaterialSkin.Controls.MaterialButton();
            this.ydr_textbox = new MaterialSkin.Controls.MaterialTextBox();
            this.browse_ydr = new MaterialSkin.Controls.MaterialButton();
            this.cbLOD = new MaterialSkin.Controls.MaterialCheckbox();
            this.out_textbox = new MaterialSkin.Controls.MaterialTextBox();
            this.browse_out = new MaterialSkin.Controls.MaterialButton();
            this.txtIDE = new MaterialSkin.Controls.MaterialLabel();
            this.txtEntities = new MaterialSkin.Controls.MaterialLabel();
            this.lodist_textbox = new MaterialSkin.Controls.MaterialTextBox();
            this.hdtexturedist_textbox = new MaterialSkin.Controls.MaterialTextBox();
            this.cbOutputGame = new MaterialSkin.Controls.MaterialComboBox();
            this.SuspendLayout();
            // 
            // convert_button
            // 
            this.convert_button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.convert_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.convert_button.Depth = 0;
            this.convert_button.DrawShadows = true;
            this.convert_button.Font = new System.Drawing.Font("Microsoft JhengHei Light", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convert_button.HighEmphasis = true;
            this.convert_button.Icon = null;
            this.convert_button.Location = new System.Drawing.Point(423, 375);
            this.convert_button.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.convert_button.MouseState = MaterialSkin.MouseState.HOVER;
            this.convert_button.Name = "convert_button";
            this.convert_button.Size = new System.Drawing.Size(88, 36);
            this.convert_button.TabIndex = 12;
            this.convert_button.Text = "Convert";
            this.convert_button.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.convert_button.UseAccentColor = false;
            this.convert_button.UseVisualStyleBackColor = true;
            this.convert_button.Click += new System.EventHandler(this.Convert_button_Click);
            // 
            // ide_textbox
            // 
            this.ide_textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ide_textbox.Depth = 0;
            this.ide_textbox.Font = new System.Drawing.Font("Roboto", 12F);
            this.ide_textbox.Hint = "IDE folder";
            this.ide_textbox.Location = new System.Drawing.Point(38, 159);
            this.ide_textbox.MaxLength = 200;
            this.ide_textbox.MouseState = MaterialSkin.MouseState.OUT;
            this.ide_textbox.Multiline = false;
            this.ide_textbox.Name = "ide_textbox";
            this.ide_textbox.Size = new System.Drawing.Size(824, 50);
            this.ide_textbox.TabIndex = 13;
            this.ide_textbox.Text = "";
            this.ide_textbox.WordWrap = false;
            this.ide_textbox.TextChanged += new System.EventHandler(this.Ide_textbox_TextChanged);
            // 
            // browse_ide
            // 
            this.browse_ide.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browse_ide.Depth = 0;
            this.browse_ide.DrawShadows = true;
            this.browse_ide.HighEmphasis = true;
            this.browse_ide.Icon = null;
            this.browse_ide.Location = new System.Drawing.Point(869, 166);
            this.browse_ide.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.browse_ide.MouseState = MaterialSkin.MouseState.HOVER;
            this.browse_ide.Name = "browse_ide";
            this.browse_ide.Size = new System.Drawing.Size(80, 36);
            this.browse_ide.TabIndex = 14;
            this.browse_ide.Text = "Browse";
            this.browse_ide.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.browse_ide.UseAccentColor = false;
            this.browse_ide.UseVisualStyleBackColor = true;
            this.browse_ide.Click += new System.EventHandler(this.Browse_ide_Click);
            // 
            // ydr_textbox
            // 
            this.ydr_textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ydr_textbox.Depth = 0;
            this.ydr_textbox.Font = new System.Drawing.Font("Roboto", 12F);
            this.ydr_textbox.Hint = "Models folder";
            this.ydr_textbox.Location = new System.Drawing.Point(38, 238);
            this.ydr_textbox.MaxLength = 200;
            this.ydr_textbox.MouseState = MaterialSkin.MouseState.OUT;
            this.ydr_textbox.Multiline = false;
            this.ydr_textbox.Name = "ydr_textbox";
            this.ydr_textbox.Size = new System.Drawing.Size(824, 50);
            this.ydr_textbox.TabIndex = 15;
            this.ydr_textbox.Text = "";
            this.ydr_textbox.WordWrap = false;
            this.ydr_textbox.TextChanged += new System.EventHandler(this.Ydr_textbox_TextChanged);
            // 
            // browse_ydr
            // 
            this.browse_ydr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browse_ydr.Depth = 0;
            this.browse_ydr.DrawShadows = true;
            this.browse_ydr.HighEmphasis = true;
            this.browse_ydr.Icon = null;
            this.browse_ydr.Location = new System.Drawing.Point(869, 245);
            this.browse_ydr.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.browse_ydr.MouseState = MaterialSkin.MouseState.HOVER;
            this.browse_ydr.Name = "browse_ydr";
            this.browse_ydr.Size = new System.Drawing.Size(80, 36);
            this.browse_ydr.TabIndex = 16;
            this.browse_ydr.Text = "Browse";
            this.browse_ydr.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.browse_ydr.UseAccentColor = false;
            this.browse_ydr.UseVisualStyleBackColor = true;
            this.browse_ydr.Click += new System.EventHandler(this.Browse_ydr_Click);
            // 
            // cbLOD
            // 
            this.cbLOD.AutoSize = true;
            this.cbLOD.Depth = 0;
            this.cbLOD.Location = new System.Drawing.Point(38, 104);
            this.cbLOD.Margin = new System.Windows.Forms.Padding(0);
            this.cbLOD.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cbLOD.MouseState = MaterialSkin.MouseState.HOVER;
            this.cbLOD.Name = "cbLOD";
            this.cbLOD.Ripple = true;
            this.cbLOD.Size = new System.Drawing.Size(154, 37);
            this.cbLOD.TabIndex = 17;
            this.cbLOD.Text = "Split to LOD type";
            this.cbLOD.UseVisualStyleBackColor = true;
            // 
            // out_textbox
            // 
            this.out_textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.out_textbox.Depth = 0;
            this.out_textbox.Font = new System.Drawing.Font("Roboto", 12F);
            this.out_textbox.Hint = "Output files";
            this.out_textbox.Location = new System.Drawing.Point(38, 316);
            this.out_textbox.MaxLength = 200;
            this.out_textbox.MouseState = MaterialSkin.MouseState.OUT;
            this.out_textbox.Multiline = false;
            this.out_textbox.Name = "out_textbox";
            this.out_textbox.Size = new System.Drawing.Size(824, 50);
            this.out_textbox.TabIndex = 18;
            this.out_textbox.Text = "";
            this.out_textbox.WordWrap = false;
            this.out_textbox.TextChanged += new System.EventHandler(this.Out_textbox_TextChanged);
            // 
            // browse_out
            // 
            this.browse_out.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browse_out.Depth = 0;
            this.browse_out.DrawShadows = true;
            this.browse_out.HighEmphasis = true;
            this.browse_out.Icon = null;
            this.browse_out.Location = new System.Drawing.Point(869, 323);
            this.browse_out.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.browse_out.MouseState = MaterialSkin.MouseState.HOVER;
            this.browse_out.Name = "browse_out";
            this.browse_out.Size = new System.Drawing.Size(80, 36);
            this.browse_out.TabIndex = 19;
            this.browse_out.Text = "Browse";
            this.browse_out.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.browse_out.UseAccentColor = false;
            this.browse_out.UseVisualStyleBackColor = true;
            this.browse_out.Click += new System.EventHandler(this.Browse_out_Click);
            // 
            // txtIDE
            // 
            this.txtIDE.Depth = 0;
            this.txtIDE.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtIDE.Location = new System.Drawing.Point(363, 429);
            this.txtIDE.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtIDE.Name = "txtIDE";
            this.txtIDE.Size = new System.Drawing.Size(199, 23);
            this.txtIDE.TabIndex = 20;
            this.txtIDE.Text = "Idle";
            this.txtIDE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEntities
            // 
            this.txtEntities.Depth = 0;
            this.txtEntities.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEntities.Location = new System.Drawing.Point(271, 467);
            this.txtEntities.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtEntities.Name = "txtEntities";
            this.txtEntities.Size = new System.Drawing.Size(386, 45);
            this.txtEntities.TabIndex = 21;
            this.txtEntities.Text = "No entity process";
            this.txtEntities.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lodist_textbox
            // 
            this.lodist_textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lodist_textbox.Depth = 0;
            this.lodist_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lodist_textbox.Hint = "LOD Distance";
            this.lodist_textbox.Location = new System.Drawing.Point(38, 430);
            this.lodist_textbox.MaxLength = 5;
            this.lodist_textbox.MouseState = MaterialSkin.MouseState.OUT;
            this.lodist_textbox.Multiline = false;
            this.lodist_textbox.Name = "lodist_textbox";
            this.lodist_textbox.Size = new System.Drawing.Size(118, 50);
            this.lodist_textbox.TabIndex = 22;
            this.lodist_textbox.Text = "20000";
            // 
            // hdtexturedist_textbox
            // 
            this.hdtexturedist_textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hdtexturedist_textbox.Depth = 0;
            this.hdtexturedist_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.hdtexturedist_textbox.Hint = "HD Tex. Distance";
            this.hdtexturedist_textbox.Location = new System.Drawing.Point(712, 428);
            this.hdtexturedist_textbox.MaxLength = 3;
            this.hdtexturedist_textbox.MouseState = MaterialSkin.MouseState.OUT;
            this.hdtexturedist_textbox.Multiline = false;
            this.hdtexturedist_textbox.Name = "hdtexturedist_textbox";
            this.hdtexturedist_textbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.hdtexturedist_textbox.Size = new System.Drawing.Size(150, 50);
            this.hdtexturedist_textbox.TabIndex = 23;
            this.hdtexturedist_textbox.Text = "200";
            // 
            // cbOutputGame
            // 
            this.cbOutputGame.AutoResize = false;
            this.cbOutputGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbOutputGame.Depth = 0;
            this.cbOutputGame.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbOutputGame.DropDownHeight = 174;
            this.cbOutputGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutputGame.DropDownWidth = 121;
            this.cbOutputGame.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cbOutputGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbOutputGame.FormattingEnabled = true;
            this.cbOutputGame.Hint = "Game Output";
            this.cbOutputGame.IntegralHeight = false;
            this.cbOutputGame.ItemHeight = 43;
            this.cbOutputGame.Items.AddRange(new object[] {
            "V",
            "IV"});
            this.cbOutputGame.Location = new System.Drawing.Point(710, 92);
            this.cbOutputGame.MaxDropDownItems = 4;
            this.cbOutputGame.MouseState = MaterialSkin.MouseState.OUT;
            this.cbOutputGame.Name = "cbOutputGame";
            this.cbOutputGame.Size = new System.Drawing.Size(152, 49);
            this.cbOutputGame.TabIndex = 25;
            this.cbOutputGame.SelectedIndexChanged += new System.EventHandler(this.CbOutputGame_SelectedIndexChanged);
            // 
            // IDE2YTYP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(987, 525);
            this.Controls.Add(this.cbOutputGame);
            this.Controls.Add(this.hdtexturedist_textbox);
            this.Controls.Add(this.lodist_textbox);
            this.Controls.Add(this.txtEntities);
            this.Controls.Add(this.txtIDE);
            this.Controls.Add(this.browse_out);
            this.Controls.Add(this.convert_button);
            this.Controls.Add(this.out_textbox);
            this.Controls.Add(this.cbLOD);
            this.Controls.Add(this.browse_ydr);
            this.Controls.Add(this.ydr_textbox);
            this.Controls.Add(this.browse_ide);
            this.Controls.Add(this.ide_textbox);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.Name = "IDE2YTYP";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IDE2YTYP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialButton convert_button;
        private MaterialSkin.Controls.MaterialTextBox ide_textbox;
        private MaterialSkin.Controls.MaterialButton browse_ide;
        private MaterialSkin.Controls.MaterialTextBox ydr_textbox;
        private MaterialSkin.Controls.MaterialButton browse_ydr;
        private MaterialSkin.Controls.MaterialCheckbox cbLOD;
        private MaterialSkin.Controls.MaterialTextBox out_textbox;
        private MaterialSkin.Controls.MaterialButton browse_out;
        private MaterialSkin.Controls.MaterialLabel txtIDE;
        private MaterialSkin.Controls.MaterialLabel txtEntities;
        private MaterialSkin.Controls.MaterialTextBox lodist_textbox;
        private MaterialSkin.Controls.MaterialTextBox hdtexturedist_textbox;
        private MaterialSkin.Controls.MaterialComboBox cbOutputGame;
    }
}

