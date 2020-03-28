using CodeWalker.GameFiles;
using SharpDX;
using System;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using Color = System.Drawing.Color;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Text;

namespace IDE2YTYP
{
    public partial class IDE2YTYP : MaterialForm
    {

        private string folderide;
        private string folderydr;
        private string folderout;

        private StringBuilder missing = new StringBuilder();

        private string modelName;
        private string textureDic;

        private string modelNamet;
        private string textureDict;

        private string lodDist;

        public string ModelName { get => modelName; set => modelName = value; }
        public string TextureDic { get => textureDic; set => textureDic = value; }
        public string ModelNamet { get => modelNamet; set => modelNamet = value; }
        public string TextureDict { get => textureDict; set => textureDict = value; }
        public StringBuilder Missing { get => missing; set => missing = value; }
        public string LodDist { get => lodDist; set => lodDist = value; }
               public string Folderide { get => folderide; set => folderide = value; }
        public string Folderydr { get => folderydr; set => folderydr = value; }
        public string Folderout { get => folderout; set => folderout = value; }

        public IDE2YTYP()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Missing.Clear();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

        }


        private void browse_ide_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select a folder that contains your IDEs files.";
            fbd.ShowDialog();
            ide_textbox.Text = fbd.SelectedPath;
        }

        private void browse_ydr_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd2 = new FolderBrowserDialog();
            fbd2.Description = "Select a folder that contains your YDRs files.";
            fbd2.ShowDialog();
            ydr_textbox.Text = fbd2.SelectedPath;
        }

        private void browse_out_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fbd3 = new FolderBrowserDialog();
            fbd3.Description = "Select a folder you want to output the files.";
            fbd3.ShowDialog();
            out_textbox.Text = fbd3.SelectedPath;
        }


        private async void convert_button_Click(object sender, EventArgs e)
        {

            if (Check(Folderide, Folderydr, Folderout).Item1 && Check(Folderide, Folderydr, Folderout).Item2 && Check(Folderide, Folderydr, Folderout).Item3)
            {

                CheckForIllegalCrossThreadCalls = false;

                if (cbOutputGame.SelectedIndex == 0)
                {
                    await ExportOutputV(Folderide, cbLOD.Checked).ConfigureAwait(false);

                }

                if (cbOutputGame.SelectedIndex == 1)
                {
                    await ExportOutputIV(Folderide).ConfigureAwait(false);
                }

                if (cbOutputGame.SelectedItem == null)
                {
                    MessageBox.Show("Error", "Select a output format", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                MessageBox.Show("Done", "Complete!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                EnableControls();
                txtIDE.ForeColor = Color.Black;
                txtIDE.Text = "Idle";
                txtEntities.ForeColor = Color.Black;
                txtEntities.Text = "No entity process";
               File.WriteAllText("MissingModels.txt", Missing.ToString());
            }
        }

        public async Task ExportOutputV(string idefolda, bool isLOD)
        {

            DisableControls();

            bool tobj = false;
            bool obj = false;
            int idecount = 0;

            bool issom = (tobj || obj);

            string[] idefiles = Directory.GetFiles(idefolda, "*.ide");

            MessageBox.Show(idefiles.Length + " IDEs detected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Task tk = Task.Run(() =>
            {
                foreach (var ide in idefiles)
                {

                    YtypFile ytf = new YtypFile();
                    YtypFile lodytf = new YtypFile();

                    string filename = Path.GetFileNameWithoutExtension(ide);
                    string filenamewithex = Path.GetFileName(ide);

                    Missing.AppendLine(filenamewithex);


                    idecount += 1;
                    txtIDE.ForeColor = Color.Red;
                    txtIDE.Text = idecount + " of " + idefiles.Length;

                    string[] idelines = File.ReadAllLines(ide);

                    foreach (var line in idelines)
                    {
                        string fixline = line.ToLowerInvariant();

                        string[] linesplitted = fixline.Split(',');

                        if (!issom)
                        {
                            if (string.IsNullOrEmpty(fixline))
                            {
                                continue;
                            }
                            if (fixline.StartsWith("#"))
                            {
                                continue; //ignore comments
                            }
                            if (fixline.StartsWith("objs")) { obj = true; continue; };
                            if (fixline.StartsWith("tobj")) { tobj = true; continue; };
                        }

                        if (fixline.StartsWith("end"))
                        {
                            tobj = false;
                            obj = false;
                            continue;
                        }

                        if (obj)
                        {

                            ModelName = linesplitted[1].Trim(); //ModelName in IDE
                            TextureDic = linesplitted[2].Trim(); //TextureDictionary in IDE 
                            txtEntities.ForeColor = Color.Green;
                            txtEntities.Text = "Processing " + ModelName + "...";


                            if (isLOD)
                            {
                                if (ModelName.Contains("lod"))
                                {
                                    Archetype lodarc = new Archetype();
                                    lodarc._BaseArchetypeDef.assetName = JenkHash.GenHash(ModelName);
                                    lodarc._BaseArchetypeDef.textureDictionary = JenkHash.GenHash(TextureDic);
                                    lodarc._BaseArchetypeDef.flags = 12582912;
                                    lodarc._BaseArchetypeDef.lodDist = float.Parse(lodist_textbox.Text);
                                    lodarc._BaseArchetypeDef.name = JenkHash.GenHash(ModelName);
                                    lodarc._BaseArchetypeDef.hdTextureDist = float.Parse(hdtexturedist_textbox.Text);
                                    lodarc._BaseArchetypeDef.assetType = rage__fwArchetypeDef__eAssetType.ASSET_TYPE_DRAWABLE;
                                    lodarc._BaseArchetypeDef.bbMin = GetYDR(ModelName).bbmin;
                                    lodarc._BaseArchetypeDef.bbMax = GetYDR(ModelName).bbmax;
                                    lodarc._BaseArchetypeDef.bsCentre = GetYDR(ModelName).bbcenter;
                                    lodarc._BaseArchetypeDef.bsRadius = GetYDR(ModelName).bbsphere;
                                    lodytf.AddArchetype(lodarc);
                                }
                                else
                                {
                                    Archetype arc = new Archetype();
                                    arc._BaseArchetypeDef.assetName = JenkHash.GenHash(ModelName);
                                    arc._BaseArchetypeDef.textureDictionary = JenkHash.GenHash(TextureDic);
                                    arc._BaseArchetypeDef.flags = 12582912;
                                    arc._BaseArchetypeDef.lodDist = float.Parse(lodist_textbox.Text);
                                    arc._BaseArchetypeDef.name = JenkHash.GenHash(ModelName);
                                    arc._BaseArchetypeDef.hdTextureDist = float.Parse(hdtexturedist_textbox.Text);
                                    arc._BaseArchetypeDef.assetType = rage__fwArchetypeDef__eAssetType.ASSET_TYPE_DRAWABLE;
                                    arc._BaseArchetypeDef.bbMin = GetYDR(ModelName).bbmin;
                                    arc._BaseArchetypeDef.bbMax = GetYDR(ModelName).bbmax;
                                    arc._BaseArchetypeDef.bsCentre = GetYDR(ModelName).bbcenter;
                                    arc._BaseArchetypeDef.bsRadius = GetYDR(ModelName).bbsphere;
                                    ytf.AddArchetype(arc);
                                }

                            }
                            else
                            {
                                Archetype arc = new Archetype();
                                arc._BaseArchetypeDef.assetName = JenkHash.GenHash(ModelName);
                                arc._BaseArchetypeDef.textureDictionary = JenkHash.GenHash(TextureDic);
                                arc._BaseArchetypeDef.flags = 12582912;
                                arc._BaseArchetypeDef.lodDist = float.Parse(lodist_textbox.Text);
                                arc._BaseArchetypeDef.name = JenkHash.GenHash(ModelName);
                                arc._BaseArchetypeDef.hdTextureDist = float.Parse(hdtexturedist_textbox.Text);
                                arc._BaseArchetypeDef.assetType = rage__fwArchetypeDef__eAssetType.ASSET_TYPE_DRAWABLE;
                                arc._BaseArchetypeDef.bbMin = GetYDR(ModelName).bbmin;
                                arc._BaseArchetypeDef.bbMax = GetYDR(ModelName).bbmax;
                                arc._BaseArchetypeDef.bsCentre = GetYDR(ModelName).bbcenter;
                                arc._BaseArchetypeDef.bsRadius = GetYDR(ModelName).bbsphere;
                                ytf.AddArchetype(arc);

                            }


                        }

                        if (tobj)
                        {

                            ModelNamet = linesplitted[1].Trim(); //ModelName in IDE
                            TextureDict = linesplitted[2].Trim(); //TextureDictionary in IDE 
                            txtEntities.Text = "Processing " + ModelNamet + "...";
                            TimeArchetype tarc = new TimeArchetype();

                            tarc._TimeArchetypeDef._BaseArchetypeDef.assetName = JenkHash.GenHash(ModelNamet);
                            tarc._TimeArchetypeDef._BaseArchetypeDef.textureDictionary = JenkHash.GenHash(TextureDict);
                            tarc._TimeArchetypeDef._BaseArchetypeDef.flags = 12582912;
                            tarc._TimeArchetypeDef._BaseArchetypeDef.lodDist = float.Parse(lodist_textbox.Text);
                            tarc._TimeArchetypeDef._BaseArchetypeDef.name = JenkHash.GenHash(ModelNamet);
                            tarc._TimeArchetypeDef._BaseArchetypeDef.hdTextureDist = float.Parse(hdtexturedist_textbox.Text);
                            tarc._TimeArchetypeDef._BaseArchetypeDef.assetType = rage__fwArchetypeDef__eAssetType.ASSET_TYPE_DRAWABLE;
                            tarc._TimeArchetypeDef._BaseArchetypeDef.bbMax = GetYDR(ModelNamet).bbmax;
                            tarc._TimeArchetypeDef._BaseArchetypeDef.bbMin = GetYDR(ModelNamet).bbmin;
                            tarc._TimeArchetypeDef._BaseArchetypeDef.bsCentre = GetYDR(ModelNamet).bbcenter;
                            tarc._TimeArchetypeDef._BaseArchetypeDef.bsRadius = GetYDR(ModelNamet).bbsphere;
                            tarc._TimeArchetypeDef._TimeArchetypeDef.timeFlags = 33030175;

                            ytf.AddArchetype(tarc);
                        }


                    }

                    if (isLOD)
                    {
                        string LODytfFolder = Folderout + "//" + filename + "_lod.ytyp";
                        byte[] newLodData = lodytf.Save();

                        if (File.Exists(LODytfFolder))
                        {
                            DialogResult result = MessageBox.Show("The file " + filename + "_lod.ytyp" + " already exists. \nDo you want to overwrite it?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {

                                File.WriteAllBytes(LODytfFolder, newLodData);

                            }


                        }
                        else
                        {
                            File.WriteAllBytes(LODytfFolder, newLodData);

                        }



                    }



                    string ytfFolder = Folderout + "//" + filename + ".ytyp";
                    byte[] newData = ytf.Save();

                    if (File.Exists(ytfFolder))
                    {
                        DialogResult result = MessageBox.Show("The file " + filename + ".ytyp" + " already exists. \nDo you want to overwrite it?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {

                            File.WriteAllBytes(ytfFolder, newData);

                        }


                    }
                    else
                    {
                        File.WriteAllBytes(ytfFolder, newData);

                    }


                }

            });
            await tk.ConfigureAwait(false);


        }

        public async Task ExportOutputIV(string idefolda)
        {

            DisableControls();

            bool tobj = false;
            bool obj = false;
            int idecount = 0;

            bool issom = (tobj || obj);

            string[] idefiles = Directory.GetFiles(idefolda, "*.ide");

            MessageBox.Show(idefiles.Length + " IDEs detected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Task tk = Task.Run(() =>
            {
                foreach (var ide in idefiles)
                {

                    StringBuilder ivIDE = new StringBuilder(); 

                    string filename = Path.GetFileNameWithoutExtension(ide);
                    string filenamewithex = Path.GetFileName(ide);

                    Missing.AppendLine(filenamewithex);


                    idecount += 1;
                    txtIDE.ForeColor = Color.Red;
                    txtIDE.Text = idecount + " of " + idefiles.Length;

                    string[] idelines = File.ReadAllLines(ide);

                    foreach (var line in idelines)
                    {
                        string fixline = line.ToLowerInvariant();

                        string[] linesplitted = fixline.Split(',');

                        if (!issom)
                        {
                            if (string.IsNullOrEmpty(fixline))
                            {
                                continue;
                            }
                            if (fixline.StartsWith("#"))
                            {
                                continue; //ignore comments
                            }
                            if (fixline.StartsWith("objs")) { obj = true; continue; };
                            if (fixline.StartsWith("tobj")) { tobj = true; continue; };
                        }

                        if (fixline.StartsWith("end"))
                        {
                            tobj = false;
                            obj = false;
                            continue;
                        }

                        if (obj)
                        {

                            ModelName = linesplitted[1].Trim(); //ModelName in IDE
                            TextureDic = linesplitted[2].Trim(); //TextureDictionary in IDE 
                            LodDist = linesplitted[4].Trim(); //Lod Distance in IDE 
                            txtEntities.ForeColor = Color.Green;
                            txtEntities.Text = "Processing " + ModelName + "...";
                            ivIDE.AppendLine(ModelName + ", " 
                                + TextureDic + ", "
                                + LodDist + ", 12582912, 0, " 
                                + GetODR(ModelName).AABBMin.X + ", " 
                                + GetODR(ModelName).AABBMin.Y + ", "
                                + GetODR(ModelName).AABBMin.Z + ", "
                                + GetODR(ModelName).AABBMax.X + ", "
                                + GetODR(ModelName).center.X + ", "
                                + GetODR(ModelName).center.Y + ", "
                                + GetODR(ModelName).center.Z + ", "
                                + GetODR(ModelName).radius + ", "
                                + "null");
                          
                        }

                        if (tobj)
                        {

                            ModelNamet = linesplitted[1].Trim(); //ModelName in IDE
                            TextureDict = linesplitted[2].Trim(); //TextureDictionary in IDE 
                            LodDist = linesplitted[4].Trim(); //Lod Distance in IDE 
                            txtEntities.Text = "Processing " + ModelNamet + "...";
                            ivIDE.AppendLine(ModelName + ", "
                                + TextureDic + ", "
                                + LodDist + ", 12582912, 0, "
                                + GetODR(ModelName).AABBMin.X + ", "
                                + GetODR(ModelName).AABBMin.Y + ", "
                                + GetODR(ModelName).AABBMin.Z + ", "
                                + GetODR(ModelName).AABBMax.X + ", "
                                + GetODR(ModelName).center.X + ", "
                                + GetODR(ModelName).center.Y + ", "
                                + GetODR(ModelName).center.Z + ", "
                                + GetODR(ModelName).radius + ", "
                                + "null" + ","
                                + "33423487");

                        }


                    }

                    //if (isLOD)
                    //{
                    //    string LODytfFolder = folderout + "//" + filename + "_lod.ytyp";
                    //    byte[] newLodData = lodytf.Save();

                    //    if (File.Exists(LODytfFolder))
                    //    {
                    //        DialogResult result = MessageBox.Show("The file " + filename + "_lod.ytyp" + " already exists. \nDo you want to overwrite it?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    //        if (result == DialogResult.Yes)
                    //        {

                    //            File.WriteAllBytes(LODytfFolder, newLodData);

                    //        }


                    //    }
                    //    else
                    //    {
                    //        File.WriteAllBytes(LODytfFolder, newLodData);

                    //    }



                    //}



                    string IDEIVFolder = Folderout + "//" + filename + ".ide";

                    if (File.Exists(IDEIVFolder))
                    {
                        DialogResult result = MessageBox.Show("The file " + filename + ".ide" + " already exists. \nDo you want to overwrite it?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {

                            //File.WriteAllBytes(IDEIVFolder, newData);

                        }


                    }
                    else
                    {
                        //File.WriteAllBytes(IDEIVFolder, newData);

                    }


                }

            });
            await tk.ConfigureAwait(false);


        }

        public (Vector3 center, Vector3 AABBMin, Vector3 AABBMax, float radius) GetODR(string file)
        {
            Vector3 AABBMax = Vector3.Zero;
            Vector3 AABBMin = Vector3.Zero;
            Vector3 center = Vector3.Zero;
            float radius = 0f;

            try
            {
                string[] odrfile = File.ReadAllLines(Folderydr + "//" + file + ".odr");
                foreach (var line in odrfile)
                {
                    if (line.Contains("Version 110 12"))
                    {
                        continue;
                    }

                    if (line.Contains("shadinggroup"))
                    {
                        continue;
                    }
                    if (line.Contains("{"))
                    {
                        continue;
                    }

                    string[] split1 = line.Split(' ');

                    if (line.Contains("	Shaders"))
                    {

                    }

                    for (int i = 0; i < Convert.ToInt32(split1[1] + 2); i++)
                    {
                        if (line.Contains(" "))
                        {
                            continue;
                        }
                    }

                    if (line.Contains("{"))
                    {
                        continue;
                    }

                    if (line.Contains("lodgroup"))
                    {
                        continue;
                    }

                    if (line.Contains("}"))
                    {
                        continue;
                    }

                    if (line.Contains("	high"))
                    {
                        continue;
                    }

                    if (line.Contains("	med"))
                    {
                        continue;
                    }

                    if (line.Contains("	low"))
                    {
                        continue;
                    }

                    if (line.Contains("	vlow"))
                    {
                        continue;
                    }

                    if (line.Contains("	high"))
                    {
                        continue;
                    }

                    if (line.Contains("center"))
                    {
                        string[] centervalues = line.Split(' ');
                        center = new Vector3(Convert.ToSingle(centervalues[1].Trim()), Convert.ToSingle(centervalues[2].Trim()), Convert.ToSingle(centervalues[3].Trim()));
                        continue;
                    }

                    if (line.Contains("AABBMin"))
                    {
                        string[] aabbminvalues = line.Split(' ');
                        AABBMin = new Vector3(Convert.ToSingle(aabbminvalues[1].Trim()), Convert.ToSingle(aabbminvalues[2].Trim()), Convert.ToSingle(aabbminvalues[3].Trim()));
                        continue;
                    }

                    if (line.Contains("AABBMax"))
                    {
                        string[] aabbmaxvalues = line.Split(' ');
                        AABBMax = new Vector3(Convert.ToSingle(aabbmaxvalues[1].Trim()), Convert.ToSingle(aabbmaxvalues[2].Trim()), Convert.ToSingle(aabbmaxvalues[3].Trim()));
                        continue;
                    }

                    if (line.Contains("radius"))
                    {
                        string[] radiusvalue = line.Split(' ');
                        radius = Convert.ToSingle(radiusvalue[1]);

                    }
                }

            }
            catch (Exception)
            {

                //Missing.AppendLine(" - " + file);
                string[] odrfile = File.ReadAllLines("default.odr");
                foreach (var line in odrfile)
                {
                    if (line.Contains("Version 110 12"))
                    {
                        continue;
                    }

                    if (line.Contains("shadinggroup"))
                    {
                        continue;
                    }
                    if (line.Contains("{"))
                    {
                        continue;
                    }


                    if (line.Contains("Shaders"))
                    {
                        string[] split1 = line.Split(' ');
                        int skipshaders = int.Parse(split1[1]);

                        for (int i = 1; i < Convert.ToInt32(skipshaders + 2); i++)
                        {
                                continue;
                        }
                    }



                    if (line.Contains("{"))
                    {
                        continue;
                    }

                    if (line.Contains("lodgroup"))
                    {
                        continue;
                    }

                    if (line.Contains("}"))
                    {
                        continue;
                    }

                    if (line.Contains("	high"))
                    {
                        continue;
                    }

                    if (line.Contains("	med"))
                    {
                        continue;
                    }

                    if (line.Contains("	low"))
                    {
                        continue;
                    }

                    if (line.Contains("	vlow"))
                    {
                        continue;
                    }

                    if (line.Contains("	high"))
                    {
                        continue;
                    }

                    if (line.Contains("center"))
                    {
                        string[] centervalues = line.Split(' ');
                        center = new Vector3(Convert.ToSingle(centervalues[1].Trim()), Convert.ToSingle(centervalues[2].Trim()), Convert.ToSingle(centervalues[3].Trim()));
                        continue;
                    }

                    if (line.Contains("AABBMin"))
                    {
                        string[] aabbminvalues = line.Split(' ');
                        AABBMin = new Vector3(Convert.ToSingle(aabbminvalues[1].Trim()), Convert.ToSingle(aabbminvalues[2].Trim()), Convert.ToSingle(aabbminvalues[3].Trim()));
                        continue;
                    }

                    if (line.Contains("AABBMax"))
                    {
                        string[] aabbmaxvalues = line.Split(' ');
                        AABBMax = new Vector3(Convert.ToSingle(aabbmaxvalues[1].Trim()), Convert.ToSingle(aabbmaxvalues[2].Trim()), Convert.ToSingle(aabbmaxvalues[3].Trim()));
                        continue;
                    }

                    if (line.Contains("radius"))
                    {
                        string[] radiusvalue = line.Split(' ');
                        radius = Convert.ToSingle(radiusvalue[1]);

                    }
                }


            }

            return (center, AABBMin, AABBMax, radius);
        }

        public (Vector3 bbmax, Vector3 bbmin, Vector3 bbcenter, float bbsphere) GetYDR(string file)
        {
            Vector3 bbmax;
            Vector3 bbmin;
            Vector3 bbcenter;
            float bbsphere;

            try
            {
                YdrFile yd = new YdrFile();
                byte[] data = File.ReadAllBytes(Folderydr + "//" + file + ".ydr");
                RpfFile.LoadResourceFile<YdrFile>(yd, data, 165);

                bbmax = yd.Drawable.BoundingBoxMax;
                bbmin = yd.Drawable.BoundingBoxMin;
                bbcenter = yd.Drawable.BoundingCenter;
                bbsphere = yd.Drawable.BoundingSphereRadius;

            }
            catch (Exception)
            {

                    Missing.AppendLine(" - " + file);


                YdrFile yd2 = new YdrFile();
                byte[] data2 = File.ReadAllBytes("default.ydr");
                RpfFile.LoadResourceFile<YdrFile>(yd2, data2, 165);

                bbmax = yd2.Drawable.BoundingBoxMax;
                bbmin = yd2.Drawable.BoundingBoxMin;
                bbcenter = yd2.Drawable.BoundingCenter;
                bbsphere = yd2.Drawable.BoundingSphereRadius;
            }

            return (bbmax, bbmin, bbcenter, bbsphere);
        }




        private void ide_textbox_TextChanged(object sender, EventArgs e)
        {
            Folderide = this.ide_textbox.Text;
        }

        private void ydr_textbox_TextChanged(object sender, EventArgs e)
        {
            Folderydr = this.ydr_textbox.Text;

        }

        private void out_textbox_TextChanged(object sender, EventArgs e)
        {
            Folderout = this.out_textbox.Text;

        }

        private static (bool, bool, bool) Check(string idepath, string ydrpath, string outpath)
        {
            bool Thereiside;
            bool Thereisydr;
            bool Thereisout;

            if (string.IsNullOrEmpty(idepath))
            {
                MessageBox.Show("IDE Path cannot be empty, please select a path and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Thereiside = false;
            }
            else
            {
                Thereiside = true;

            }

            if (string.IsNullOrEmpty(ydrpath))
            {
                MessageBox.Show("YDR Path cannot be empty, please select a path and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Thereisydr = false;
            }
            else
            {
                Thereisydr = true;

            }

            if (string.IsNullOrEmpty(outpath))
            {
                MessageBox.Show("Output Path cannot be empty, please select a path and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Thereisout = false;
            }
            else
            {
                Thereisout = true;

            }

            return (Thereiside, Thereisydr, Thereisout);
        }

        private void EnableControls()
        {
            ide_textbox.ReadOnly = false;
            ydr_textbox.ReadOnly = false;
            out_textbox.ReadOnly = false;
            lodist_textbox.ReadOnly = false;
            hdtexturedist_textbox.ReadOnly = false;
            convert_button.Enabled = true;
            cbLOD.Enabled = true;
            browse_ide.Enabled = true;
            browse_out.Enabled = true;
            browse_ydr.Enabled = true;
        }

        private void DisableControls()
        {
            ide_textbox.ReadOnly = true;
            ydr_textbox.ReadOnly = true;
            out_textbox.ReadOnly = true;
            lodist_textbox.ReadOnly = true;
            hdtexturedist_textbox.ReadOnly = true;
            convert_button.Enabled = false;
            cbLOD.Enabled = false;
            browse_ide.Enabled = false;
            browse_out.Enabled = false;
            browse_ydr.Enabled = false;
        }

        private void cbOutputGame_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbOutputGame.SelectedIndex == 0)
            {
                cbLOD.Visible = true;
            }

            if (cbOutputGame.SelectedIndex == 1)
            {
                cbLOD.Visible = false;

            }
        }
    }
}
