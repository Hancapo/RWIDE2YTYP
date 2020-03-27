using System;
using System.Threading.Tasks;
using CodeWalker.GameFiles;
using System.IO;
using SharpDX;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace IDE_Processor
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string folderide;
        private string foldermodels;
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

        public MainWindow()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private async void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (Check(folderide, foldermodels, folderout).Item1 && Check(folderide, foldermodels, folderout).Item2 && Check(folderide, foldermodels, folderout).Item3)
            {


                await ExportYTYP(folderide, cbLODtype.IsChecked.GetValueOrDefault()).ConfigureAwait(false);

                System.Windows.Forms.MessageBox.Show("Done", "Complete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EnableControls();
                //lbIDE.Foreground = new System.Windows.Media.Brush(System.Drawing.Color.White);
                //txtStatus.ForeColor = System.Drawing.Color.Black;
                lbEntitie.Content = "No entities";
                File.WriteAllText("missingmodels.txt", missing.ToString());
            }
        }


        public async Task ExportYTYP(string idefolda, bool isLOD)
        {

            DisableControls();

            bool tobj = false;
            bool obj = false;
            int idecount = 0;

            bool issom = (tobj || obj);

            string[] idefiles = Directory.GetFiles(idefolda, "*.ide");

            System.Windows.Forms.MessageBox.Show(idefiles.Length + " IDEs detected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Task tk = Task.Run(() =>
            {
                foreach (var ide in idefiles)
                {

                    YtypFile ytf = new YtypFile();
                    YtypFile lodytf = new YtypFile();

                    string filename = System.IO.Path.GetFileNameWithoutExtension(ide);

                    missing.AppendLine(filename + " missing models: ");

                    idecount += 1;
                    //txtProcess.ForeColor = Color.Red;
                    //lbIDE.Content = idecount + " of " + idefiles.Length;

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
                            //txtStatus.ForeColor = System.Drawing.Color.Green;
                            //lbEntitie.Content = "Processing " + ModelName + "...";


                            if (isLOD)
                            {
                                if (ModelName.Contains("lod"))
                                {
                                    Archetype lodarc = new Archetype();
                                    lodarc._BaseArchetypeDef.assetName = JenkHash.GenHash(ModelName);
                                    lodarc._BaseArchetypeDef.textureDictionary = JenkHash.GenHash(TextureDic);
                                    lodarc._BaseArchetypeDef.flags = 12582912;
                                    lodarc._BaseArchetypeDef.lodDist = float.Parse(this.tbLOD.Text);
                                    lodarc._BaseArchetypeDef.name = JenkHash.GenHash(ModelName);
                                    lodarc._BaseArchetypeDef.hdTextureDist = float.Parse(tbHDTex.Text);
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
                                    arc._BaseArchetypeDef.lodDist = float.Parse(this.tbLOD.Text);
                                    arc._BaseArchetypeDef.name = JenkHash.GenHash(ModelName);
                                    arc._BaseArchetypeDef.hdTextureDist = float.Parse(tbHDTex.Text);
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
                                arc._BaseArchetypeDef.lodDist = float.Parse(tbLOD.Text);
                                arc._BaseArchetypeDef.name = JenkHash.GenHash(ModelName);
                                arc._BaseArchetypeDef.hdTextureDist = float.Parse(tbHDTex.Text);
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
                            //lbEntitie.Content = "Processing " + ModelNamet + "...";
                            TimeArchetype tarc = new TimeArchetype();

                            tarc._TimeArchetypeDef._BaseArchetypeDef.assetName = JenkHash.GenHash(ModelNamet);
                            tarc._TimeArchetypeDef._BaseArchetypeDef.textureDictionary = JenkHash.GenHash(TextureDict);
                            tarc._TimeArchetypeDef._BaseArchetypeDef.flags = 12582912;
                            tarc._TimeArchetypeDef._BaseArchetypeDef.lodDist = float.Parse(tbLOD.Text);
                            tarc._TimeArchetypeDef._BaseArchetypeDef.name = JenkHash.GenHash(ModelNamet);
                            tarc._TimeArchetypeDef._BaseArchetypeDef.hdTextureDist = float.Parse(tbHDTex.Text);
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
                        string LODytfFolder = folderout + "//" + filename + "_lod.ytyp";
                        byte[] newLodData = lodytf.Save();

                        if (File.Exists(LODytfFolder))
                        {
                            DialogResult result = System.Windows.Forms.MessageBox.Show("The file " + filename + "_lod.ytyp" + " already exists. \nDo you want to overwrite it?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {

                                File.WriteAllBytes(LODytfFolder, newLodData);

                            }


                        }
                        else
                        {
                            File.WriteAllBytes(LODytfFolder, newLodData);

                        }



                    }



                    string ytfFolder = folderout + "//" + filename + ".ytyp";
                    byte[] newData = ytf.Save();

                    if (File.Exists(ytfFolder))
                    {
                        DialogResult result = System.Windows.Forms.MessageBox.Show("The file " + filename + " already exists. \nDo you want to overwrite it?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == System.Windows.Forms.DialogResult.Yes)
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

        public (Vector3 bbmax, Vector3 bbmin, Vector3 bbcenter, float bbsphere) GetYDR(string file)
        {

            Vector3 bbmax;
            Vector3 bbmin;
            Vector3 bbcenter;
            float bbsphere;

            try
            {
                YdrFile yd = new YdrFile();
                byte[] data = File.ReadAllBytes(foldermodels + "//" + file + ".ydr");
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

        private void EnableControls()
        {
            tbIDE.IsReadOnly = false;
            tbModels.IsReadOnly = false;
            tbOutput.IsReadOnly = false;
            tbHDTex.IsReadOnly = false;
            tbLOD.IsReadOnly = false;
            btnConvert.IsEnabled = true;
            btBrowseModels.IsEnabled = true;
            btBrowseIDE.IsEnabled = true;
            btBrowseOut.IsEnabled = true;

            cbLODtype.IsEnabled = true;
        }

        private void DisableControls()
        {
            tbIDE.IsReadOnly = true;
            tbModels.IsReadOnly = true;
            tbOutput.IsReadOnly = true;
            tbHDTex.IsReadOnly = true;
            tbLOD.IsReadOnly = true;
            btnConvert.IsEnabled = false;
            btBrowseModels.IsEnabled = false;
            btBrowseIDE.IsEnabled = false;
            btBrowseOut.IsEnabled = false;

            cbLODtype.IsEnabled = false;
        }

        private void btBrowseIDE_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select a folder that contains your IDE files.";
            fbd.ShowDialog();
            tbIDE.Text = fbd.SelectedPath;
        }

        private void btBrowseOut_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd3 = new FolderBrowserDialog();
            fbd3.Description = "Select a folder you want to output the files.";
            fbd3.ShowDialog();
            tbOutput.Text = fbd3.SelectedPath;
        }

        private void btBrowseModels_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd2 = new FolderBrowserDialog();
            fbd2.Description = "Select a folder that contains your Models files.";
            fbd2.ShowDialog();
            tbModels.Text = fbd2.SelectedPath;
        }

        private static (bool, bool, bool) Check(string idepath, string ydrpath, string outpath)
        {
            bool thereiside = false;
            bool thereisydr = false;
            bool thereisout = false;

            if (string.IsNullOrEmpty(idepath))
            {
                System.Windows.Forms.MessageBox.Show("IDE Path cannot be empty, please select a path and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                thereiside = false;
            }
            else
            {
                thereiside = true;

            }

            if (string.IsNullOrEmpty(ydrpath))
            {
                System.Windows.Forms.MessageBox.Show("YDR Path cannot be empty, please select a path and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                thereisydr = false;
            }
            else
            {
                thereisydr = true;

            }

            if (string.IsNullOrEmpty(outpath))
            {
                System.Windows.Forms.MessageBox.Show("Output Path cannot be empty, please select a path and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                thereisout = false;
            }
            else
            {
                thereisout = true;

            }

            return (thereiside, thereisydr, thereisout);
        }

        private void tbIDE_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            folderide = tbIDE.Text;
        }

        private void tbModels_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            foldermodels = tbModels.Text;

        }

        private void tbOutput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            folderout = tbOutput.Text;

        }
    }
}
