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
using System.Diagnostics;

namespace IDE2YTYP
{
    public partial class IDE2YTYP : MaterialForm
    {

        private string folderide;
        private string foldermodel;
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
        public string LodDist { get => lodDist; set => lodDist = value; }
        public string Folderide { get => folderide; set => folderide = value; }
        public string Foldermodel { get => foldermodel; set => foldermodel = value; }
        public string Folderout { get => folderout; set => folderout = value; }
        public StringBuilder Missing { get => missing; set => missing = value; }

        public IDE2YTYP()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red800, Primary.Red900, Primary.Red500, Accent.Red200, TextShade.WHITE);

        }

        private void Browse_ide_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "Select a folder that contains your IDEs files."
            };
            fbd.ShowDialog();
            ide_textbox.Text = fbd.SelectedPath;
        }

        private void Browse_model_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd2 = new FolderBrowserDialog
            {
                Description = "Select a folder that contains your YDRs files."
            };
            fbd2.ShowDialog();
            ydr_textbox.Text = fbd2.SelectedPath;
        }

        private void Browse_out_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fbd3 = new FolderBrowserDialog
            {
                Description = "Select a folder you want to output the files."
            };
            fbd3.ShowDialog();
            out_textbox.Text = fbd3.SelectedPath;
        }

        private async void Convert_button_Click(object sender, EventArgs e)
        {

            if (cbOutputGame.SelectedIndex == 0)
            {

                if (Check(Folderide, Foldermodel, Folderout).Item1 && Check(Folderide, Foldermodel, Folderout).Item2 && Check(Folderide, Foldermodel, Folderout).Item3)
                {
                    if (Directory.Exists(Folderide) && Directory.Exists(Foldermodel) && Directory.Exists(Folderout))
                    {
                        await ExportOutputV(Folderide, cbLOD.Checked).ConfigureAwait(false);
                        MessageBox.Show("Done", "Complete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EnableControls();
                        txtIDE.ForeColor = Color.Black;
                        txtIDE.Text = "Idle";
                        txtEntities.ForeColor = Color.Black;
                        txtEntities.Text = "No entity process";
                        File.WriteAllText(Folderout + "\\" + "MissingModels.txt", Missing.ToString());
                        MessageBox.Show("Please check MissingModels.txt to see if any models are missing in your output", "Atention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Missing.Clear();
                    }
                    else
                    {
                        MessageBox.Show("One or more of the selected paths aren't valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                else
                {
                    MessageBox.Show("The conversion cannot be done, check the paths and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }


            if (cbOutputGame.SelectedIndex == 1)
            {
                if (Check(Folderide, Foldermodel, Folderout).Item1 && Check(Folderide, Foldermodel, Folderout).Item2 && Check(Folderide, Foldermodel, Folderout).Item3)
                {
                    if (Directory.Exists(Folderide) && Directory.Exists(Foldermodel) && Directory.Exists(Folderout))
                    {
                        await ExportOutputIV(Folderide).ConfigureAwait(false);
                        MessageBox.Show("Done", "Complete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EnableControls();
                        txtIDE.ForeColor = Color.Black;
                        txtIDE.Text = "Idle";
                        txtEntities.ForeColor = Color.Black;
                        txtEntities.Text = "No entity process";
                        File.WriteAllText(Folderout + "\\" + "MissingModels.txt", Missing.ToString());
                        MessageBox.Show("Please check MissingModels.txt to see if any models are missing in your output", "Atention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Missing.Clear();
                    }
                    else
                    {
                        MessageBox.Show("The conversion cannot be done, check the paths and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
                else
                {
                    MessageBox.Show("The convertion cannot be done, check the paths and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

            if (cbOutputGame.SelectedItem == null)
            {
                MessageBox.Show("Please select a Output game and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

                    Missing.AppendLine(" - " + filenamewithex + " - ");


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
                            CheckModel(ModelName, ".ydr");
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
                            CheckModel(ModelNamet, ".ydr");
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

                        if (result == DialogResult.Yes) { File.WriteAllBytes(ytfFolder, newData); }
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
                    StringBuilder ivIDEtobj = new StringBuilder();
                    ivIDE.AppendLine("objs");
                    ivIDEtobj.AppendLine("tobj");

                    string filename = Path.GetFileNameWithoutExtension(ide);
                    string filenamewithex = Path.GetFileName(ide);

                    Missing.AppendLine(" - " + filenamewithex + " - ");


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
                            CheckModel(ModelName, ".odr");
                            if (linesplitted.Length < 6)
                            {
                                LodDist = linesplitted[3].Trim(); //Lod Distance in SA IDE

                            }
                            else
                            {
                                LodDist = linesplitted[4].Trim(); //Lod Distance in 3/LCS/VCS/VC IDE

                            }

                            txtEntities.ForeColor = Color.Green;
                            txtEntities.Text = "Processing " + ModelName + "...";

                            ivIDE.AppendLine(ModelName + ", "
                                + TextureDic + ", "
                                + LodDist + ", 12582912, 0, "
                                + GetODR(ModelName).ToString().Replace("\n", "").Replace("\r", "") + ", null");

                        }

                        if (tobj)
                        {

                            ModelNamet = linesplitted[1].Trim(); //ModelName in IDE
                            TextureDict = linesplitted[2].Trim(); //TextureDictionary in IDE 
                            CheckModel(ModelNamet, ".odr");

                            if (linesplitted.Length < 6)
                            {
                                LodDist = linesplitted[3].Trim(); //Lod Distance in SA IDE

                            }
                            else
                            {
                                LodDist = linesplitted[4].Trim(); //Lod Distance in 3/LCS/VCS/VC IDE

                            }

                            txtEntities.Text = "Processing " + ModelNamet + "...";
                            ivIDEtobj.AppendLine(ModelNamet + ", "
                                + TextureDict + ", "
                                + LodDist + ", 12582912, 0, "
                                + GetODR(ModelName).ToString().Replace("\n", "").Replace("\r", "") + ", null, 33423487");

                        }

                    }

                    ivIDE.AppendLine("end");
                    ivIDEtobj.AppendLine("end");

                    string IDEIVFolder = Folderout + "//" + filename + ".ide";

                    if (File.Exists(IDEIVFolder))
                    {
                        DialogResult result = MessageBox.Show("The file " + filename + ".ide" + " already exists. \nDo you want to overwrite it?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            File.WriteAllText(IDEIVFolder, ivIDE.ToString() + ivIDEtobj.ToString());

                        }
                    }
                    else
                    {
                        File.WriteAllText(IDEIVFolder, ivIDE.ToString() + ivIDEtobj.ToString());
                    }


                }

            });
            await tk.ConfigureAwait(false);





        }

        public string GetODR(string file)
        {
            string filefolder = Foldermodel + "//" + file + ".odr";

            string fulldata;
            if (File.Exists(filefolder))
            {
                FileStream fs = new FileStream(Foldermodel + "//" + file + ".odr", FileMode.Open);
                StringBuilder odr1 = new StringBuilder();
                StreamReader sr = new StreamReader(fs);

                //Reading ODR Header
                for (int i = 0; i < 3; i++)
                {
                    sr.ReadLine();
                }


                //Reading material count
                int shadernum = Convert.ToInt32(sr.ReadLine().Split(' ')[1].Trim());

                for (int z = 0; z < shadernum + 9; z++)
                {
                    sr.ReadLine();
                }

                string[] centerpart = sr.ReadLine().Split(' ');
                string[] aabbmapart = sr.ReadLine().Split(' ');
                string[] aabbmipart = sr.ReadLine().Split(' ');
                string radio = sr.ReadLine().Split(' ')[1];

                odr1.AppendLine(aabbmipart[1] + " ," + aabbmipart[2] + " ," + aabbmipart[3] + " ,"
                    + aabbmapart[1] + " ," + aabbmapart[2] + " ," + aabbmapart[3] + " ,"
                    + centerpart[1] + " ," + centerpart[2] + " ," + centerpart[3] + " ,"
                    + radio);

                fs.Close();

                fulldata = odr1.ToString();
            }
            else
            {
                //Missing.AppendLine(" - " + file);

                FileStream fs = new FileStream("default.odr", FileMode.Open);
                StringBuilder odr2 = new StringBuilder();

                StreamReader sr = new StreamReader(fs);

                //Reading ODR Header
                for (int i = 0; i < 3; i++)
                {
                    sr.ReadLine();
                }

                //Reading material count
                int shadernum = Convert.ToInt32(sr.ReadLine().Split(' ')[1].Trim());

                for (int z = 0; z < shadernum + 9; z++)
                {
                    sr.ReadLine();
                }

                string[] centerpart = sr.ReadLine().Split(' ');
                string[] aabbmapart = sr.ReadLine().Split(' ');
                string[] aabbmipart = sr.ReadLine().Split(' ');
                string radio = sr.ReadLine().Split(' ')[1];

                odr2.AppendLine(aabbmipart[1] + " ," + aabbmipart[2] + " ," + aabbmipart[3] + " ,"
                    + aabbmapart[1] + " ," + aabbmapart[2] + " ," + aabbmapart[3] + " ,"
                    + centerpart[1] + " ," + centerpart[2] + " ," + centerpart[3] + " ,"
                    + radio);

                fulldata = odr2.ToString();

                fs.Close();
            }
            return fulldata;
        }

        public (Vector3 bbmax, Vector3 bbmin, Vector3 bbcenter, float bbsphere) GetYDR(string file)
        {
            Vector3 bbmax;
            Vector3 bbmin;
            Vector3 bbcenter;
            float bbsphere;

            string filefolder = Foldermodel + "//" + file + ".ydr";

            if (File.Exists(filefolder))
            {
                YdrFile yd = new YdrFile();
                byte[] data = File.ReadAllBytes(Foldermodel + "//" + file + ".ydr");
                RpfFile.LoadResourceFile<YdrFile>(yd, data, 165);

                bbmax = yd.Drawable.BoundingBoxMax;
                bbmin = yd.Drawable.BoundingBoxMin;
                bbcenter = yd.Drawable.BoundingCenter;
                bbsphere = yd.Drawable.BoundingSphereRadius;
            }
            else
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

        private void Ide_textbox_TextChanged(object sender, EventArgs e)
        {
            Folderide = this.ide_textbox.Text;
        }

        private void Ydr_textbox_TextChanged(object sender, EventArgs e)
        {
            Foldermodel = this.ydr_textbox.Text;

        }

        private void Out_textbox_TextChanged(object sender, EventArgs e)
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
            browse_model.Enabled = true;
            cbOutputGame.Enabled = true;
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
            browse_model.Enabled = false;
            cbOutputGame.Enabled = false;

        }

        private void CbOutputGame_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbOutputGame.SelectedIndex == 0)
            {
                cbLOD.Visible = true;
                hdtexturedist_textbox.Visible = true;
                lodist_textbox.Visible = true;
            }

            if (cbOutputGame.SelectedIndex == 1)
            {
                cbLOD.Visible = false;
                hdtexturedist_textbox.Visible = false;
                lodist_textbox.Visible = false;


            }
        }

        private void CheckModel(string filename, string format)
        {
            string filefolder = Foldermodel + "//" + filename + format;

            if (!File.Exists(filefolder))
            {
                Missing.AppendLine(" -" + filename);
            }
            
        }


    }
}
