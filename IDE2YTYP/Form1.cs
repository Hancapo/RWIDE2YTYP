using CodeWalker.GameFiles;
using SharpDX;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDE2YTYP
{
    public partial class IDE2YTYP : Form
    {

        private string folderide;
        private string folderydr;
        private string folderout;

        private static StringBuilder missing = new StringBuilder();

        private string modelName;
        private string textureDic;

        private string modelNamet;
        private string textureDict;

        public string ModelName { get => modelName; set => modelName = value; }
        public string TextureDic { get => textureDic; set => textureDic = value; }
        public string ModelNamet { get => modelNamet; set => modelNamet = value; }
        public string TextureDict { get => textureDict; set => textureDict = value; }

        public IDE2YTYP()
        {
            InitializeComponent();
            missing.Clear();
            CheckForIllegalCrossThreadCalls = false;
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

        public async Task OpenIDEs(string idefolda, bool isLOD)
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

                    missing.AppendLine(filename + " missing models: ");

                    idecount += 1;
                    txtProcess.ForeColor = System.Drawing.Color.Red;
                    txtProcess.Text = idecount + " of " + idefiles.Length;

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
                            txtStatus.ForeColor = System.Drawing.Color.Green;
                            txtStatus.Text = "Processing " + ModelName + "...";


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
                            txtStatus.Text = "Processing " + ModelNamet + "...";
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
                        byte[] newLodData = lodytf.Save();
                        File.WriteAllBytes(folderout + "//" + filename + "_lod.ytyp", newLodData);
                    }



                    byte[] newData = ytf.Save();
                    File.WriteAllBytes(folderout + "//" + filename + ".ytyp", newData);
                }

            });
            await tk.ConfigureAwait(false);


        }

        private async void convert_button_Click(object sender, EventArgs e)
        {

            if (Check(folderide, folderydr, folderout).Item1 && Check(folderide, folderydr, folderout).Item2 && Check(folderide, folderydr, folderout).Item3)
            {
                

                await OpenIDEs(folderide, this.cbLOD.Checked);

                MessageBox.Show("Done", "Complete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EnableControls();
                txtProcess.ForeColor = System.Drawing.Color.Black;
                txtProcess.Text = "Idle";
                txtStatus.ForeColor = System.Drawing.Color.Black;
                txtStatus.Text = "No Process";
                File.WriteAllText("missingmodels.txt", missing.ToString());
            }
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
                byte[] data = File.ReadAllBytes(folderydr + "//" + file + ".ydr");
                RpfFile.LoadResourceFile<YdrFile>(yd, data, 165);

                bbmax = yd.Drawable.BoundingBoxMax;
                bbmin = yd.Drawable.BoundingBoxMin;
                bbcenter = yd.Drawable.BoundingCenter;
                bbsphere = yd.Drawable.BoundingSphereRadius;

            }
            catch (Exception)
            {

                missing.AppendLine(file);


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
            folderide = this.ide_textbox.Text;
        }

        private void ydr_textbox_TextChanged(object sender, EventArgs e)
        {
            folderydr = this.ydr_textbox.Text;

        }

        private void out_textbox_TextChanged(object sender, EventArgs e)
        {
            folderout = this.out_textbox.Text;

        }

        private static (bool, bool, bool) Check(string idepath, string ydrpath, string outpath)
        {
            bool thereiside = false;
            bool thereisydr = false;
            bool thereisout = false;

            if (string.IsNullOrEmpty(idepath))
            {
                MessageBox.Show("IDE Path cannot be empty, please select a path and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                thereiside = false;
            }
            else
            {
                thereiside = true;

            }

            if (string.IsNullOrEmpty(ydrpath))
            {
                MessageBox.Show("YDR Path cannot be empty, please select a path and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                thereisydr = false;
            }
            else
            {
                thereisydr = true;

            }

            if (string.IsNullOrEmpty(outpath))
            {
                MessageBox.Show("Output Path cannot be empty, please select a path and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                thereisout = false;
            }
            else
            {
                thereisout = true;

            }

            return (thereiside, thereisydr, thereisout);
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

    }
}
