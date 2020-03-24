using CodeWalker.GameFiles;
using SharpDX;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace IDE2YTYP
{
    public partial class IDE2YTYP : Form
    {

        private static string folderide;
        private static string folderydr;
        private static string folderout;

        private static StringBuilder missing = new StringBuilder();


        private static bool ideempty;
        private static bool ydrempty;
        private static bool outempty;

        private static string modelName;
        private static string textureDic;

        private static string modelNamet;
        private static string textureDict;

        public static string ModelName { get => modelName; set => modelName = value; }
        public static string TextureDic { get => textureDic; set => textureDic = value; }
        public static string ModelNamet { get => modelNamet; set => modelNamet = value; }
        public static string TextureDict { get => textureDict; set => textureDict = value; }

        public IDE2YTYP()
        {
            InitializeComponent();
            missing.Clear();
        }


        private void browse_ide_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select a folder that contains your IDEs files.";
            fbd.ShowDialog();
            //ide_textbox.Text = fbd.SelectedPath;
        }

        private void browse_ydr_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd2 = new FolderBrowserDialog();
            fbd2.Description = "Select a folder that contains your YDRs files.";

            fbd2.ShowDialog();

            //ydr_textbox.Text = fbd2.SelectedPath;
        }

        private void browse_out_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fbd3 = new FolderBrowserDialog();
            fbd3.Description = "Select a folder you want to output the files.";
            fbd3.ShowDialog();
            out_textbox.Text = fbd3.SelectedPath;

        }

        public void OpenIDEs(string idefolda, string ydrfolda, string outfolda)
        {
            bool tobj = false;
            bool obj = false;

            bool issom = (tobj || obj);

            string[] idefiles = Directory.GetFiles(idefolda, "*.ide");

            MessageBox.Show(idefiles.Length + " IDEs detected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            foreach (var ide in idefiles)
            {

                YtypFile ytf = new YtypFile();

                string filename = Path.GetFileNameWithoutExtension(ide);

                missing.AppendLine(filename + " missing models: ");

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

                        modelName = linesplitted[1].ToString().Trim(); //ModelName in IDE
                        textureDic = linesplitted[2].ToString().Trim(); //TextureDictionary in IDE 

                        Archetype arc = new Archetype();

                        arc._BaseArchetypeDef.assetName = JenkHash.GenHash(modelName);
                        arc._BaseArchetypeDef.textureDictionary = JenkHash.GenHash(TextureDic);
                        arc._BaseArchetypeDef.flags = 12582912;
                        arc._BaseArchetypeDef.lodDist = float.Parse(lodist_textbox.Text);
                        arc._BaseArchetypeDef.name = JenkHash.GenHash(modelName);
                        arc._BaseArchetypeDef.hdTextureDist = float.Parse(hdtexturedist_textbox.Text);
                        arc._BaseArchetypeDef.assetType = rage__fwArchetypeDef__eAssetType.ASSET_TYPE_DRAWABLE;
                        arc._BaseArchetypeDef.bbMin = GetYDR(modelName).bbmin;
                        arc._BaseArchetypeDef.bbMax = GetYDR(modelName).bbmax;
                        arc._BaseArchetypeDef.bsCentre = GetYDR(modelName).bbcenter;
                        arc._BaseArchetypeDef.bsRadius = GetYDR(modelName).bbsphere;

                        ytf.AddArchetype(arc);
                    }

                    if (tobj)
                    {

                        modelNamet = linesplitted[1].ToString().Trim(); //ModelName in IDE
                        textureDict = linesplitted[2].ToString().Trim(); //TextureDictionary in IDE 

                        TimeArchetype tarc = new TimeArchetype();

                        tarc._TimeArchetypeDef._BaseArchetypeDef.assetName = JenkHash.GenHash(modelNamet);
                        tarc._TimeArchetypeDef._BaseArchetypeDef.textureDictionary = JenkHash.GenHash(TextureDict);
                        tarc._TimeArchetypeDef._BaseArchetypeDef.flags = 12582912;
                        tarc._TimeArchetypeDef._BaseArchetypeDef.lodDist = float.Parse(lodist_textbox.Text);
                        tarc._TimeArchetypeDef._BaseArchetypeDef.name = JenkHash.GenHash(modelNamet);
                        tarc._TimeArchetypeDef._BaseArchetypeDef.hdTextureDist = float.Parse(hdtexturedist_textbox.Text);
                        tarc._TimeArchetypeDef._BaseArchetypeDef.assetType = rage__fwArchetypeDef__eAssetType.ASSET_TYPE_DRAWABLE;
                        tarc._TimeArchetypeDef._BaseArchetypeDef.bbMax = GetYDR(modelNamet).bbmax;
                        tarc._TimeArchetypeDef._BaseArchetypeDef.bbMin = GetYDR(modelNamet).bbmin;
                        tarc._TimeArchetypeDef._BaseArchetypeDef.bsCentre = GetYDR(modelNamet).bbcenter;
                        tarc._TimeArchetypeDef._BaseArchetypeDef.bsRadius = GetYDR(modelNamet).bbsphere;
                        tarc._TimeArchetypeDef._TimeArchetypeDef.timeFlags = 33030175;

                        ytf.AddArchetype(tarc);
                    }

                    //bw.ReportProgress((int)(progresoide * 100 / idelines.Length));

                }
                byte[] newData = ytf.Save();
                File.WriteAllBytes(folderout + "//" + filename + ".ytyp", newData);
            }

        }

        private void convert_button_Click(object sender, EventArgs e)
        {

            if (Check(folderide, folderydr, folderout).Item1 && Check(folderide, folderydr, folderout).Item2 && Check(folderide, folderydr, folderout).Item3)
            {
                OpenIDEs(folderide, folderydr, folderout);
                MessageBox.Show("Done", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            folderide = ide_textbox.Text;
        }

        private void ydr_textbox_TextChanged(object sender, EventArgs e)
        {
            folderydr = ydr_textbox.Text;

        }

        private void out_textbox_TextChanged(object sender, EventArgs e)
        {
            folderout = out_textbox.Text;

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


    }
}
