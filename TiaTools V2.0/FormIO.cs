using System;
using CustomUtility_NET_F;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace TiaToolsV2_0
{
    public partial class FormIO : Form
    {
        #region Constructor

        public FormIO()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Workbook|*.xls; *xlsx";
            openFileDialog.Title = "Select An Excel file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                comboBoxSheetList.Items.Clear();
                textBoxFilePath.Text = openFileDialog.FileName;
            }

            //Fill combobox
            foreach (string str in ExcelDataTable.GetSheetsCollection(textBoxFilePath.Text))
            {
                comboBoxSheetList.Items.Add(str);
            }

            //Set default Sheet
            comboBoxSheetList.Text = comboBoxSheetList.Items[0].ToString();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            dataGridViewInput.DataSource = ExcelDataTable.ImportExcelToDataTable(textBoxFilePath.Text, comboBoxSheetList.SelectedIndex);
            dataGridViewInput.Update();
        }

        private void buttonCreateFiles_Click(object sender, EventArgs e)
        {
            //Select Folder To save File
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

            //New String To Save Filepath
            string filePath;
            string filePathDI;
            string filePathDQ;
            string filePathAI;
            string filePathAQ;
            string filePathTypes;
            string filePathFB;

            //New DataTable To Store DataGridView Paramaters
            DataTable dataTable = new DataTable();
            dataTable = (DataTable)dataGridViewInput.DataSource;

            //Create Source Files
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {

                //Create a New Directory To store New Files
                filePath = folderBrowserDialog.SelectedPath + @"\TIA_SourceFile_" + DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_");
                filePathDI = filePath + @"\DI";
                filePathAI = filePath + @"\AI";
                filePathDQ = filePath + @"\DQ";
                filePathAQ = filePath + @"\AQ";
                filePathTypes = filePath + @"\Types";
                filePathFB = filePath + @"\FB";
                Directory.CreateDirectory(filePath);

                #region DI
                //Input Files
                if (checkBoxD_IN.Checked)
                {
                    //Create Directory
                    Directory.CreateDirectory(filePathDI);

                    #region FC_Digital_IN
                    //New Stream For FC_Digital_IN
                    StreamWriter FC_DI = new StreamWriter(filePathDI + @"\FC_DI.scl", false);

                    //FC_Digital_IN Body
                    FC_DI.Write(TiaToolsV2_0.Properties.Resources.FC_DI);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("I") && !row["Logical Address"].ToString().Contains("W"))
                        {
                            FC_DI.Write(TiaToolsV2_0.Properties.Resources.FC_DI_Part.Replace("$VAR_NAME$", row["Name"].ToString()));
                            FC_DI.Write("\n");
                        }
                    }
                    FC_DI.Write("END_FUNCTION");
                    FC_DI.Close();
                    FC_DI.Dispose();
                    #endregion

                    #region DB_DIN
                    //New Stream For DB_DIN
                    StreamWriter DB_DI = new StreamWriter(filePathDI + @"\DB_DI.db", false);

                    //DB_IN Body
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("I") && !row["Logical Address"].ToString().Contains("W"))
                        {
                            DB_DI.Write(TiaToolsV2_0.Properties.Resources.DB_DI.Replace("$VAR_NAME$", row["Name"].ToString()));
                        }
                    }
                    DB_DI.Close();
                    DB_DI.Dispose();
                    #endregion

                    #region FC_Digital_IN_Config
                    //New Stream For FC_Digital_IN_Config
                    StreamWriter FC_DI_Config = new StreamWriter(filePathDI + @"\FC_DI_Config.scl", false);

                    //FC_Digital_IN_Config Body
                    FC_DI_Config.Write(TiaToolsV2_0.Properties.Resources.FC_DI_Config);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("I") && !row["Logical Address"].ToString().Contains("W"))
                        {
                            FC_DI_Config.Write(TiaToolsV2_0.Properties.Resources.FC_DI_Config_Part.Replace("$VAR_NAME$", row["Name"].ToString()));
                            FC_DI_Config.Write("\n");
                        }
                    }
                    FC_DI_Config.Write("END_FUNCTION");
                    FC_DI_Config.Close();
                    FC_DI_Config.Dispose();
                    #endregion
                }
                #endregion

                #region AI
                //Input Files
                if (checkBoxA_IN.Checked)
                {
                    //Create Directory
                    Directory.CreateDirectory(filePathAI);

                    #region FC_Analog_IN
                    //New Stream For FC_Analog_IN
                    StreamWriter FC_AI = new StreamWriter(filePathAI + @"\FC_AI.scl", false);

                    //FC_Analog_IN Body
                    FC_AI.Write(TiaToolsV2_0.Properties.Resources.FC_AI);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("I") && row["Logical Address"].ToString().Contains("W"))
                        {
                            FC_AI.Write(TiaToolsV2_0.Properties.Resources.FC_AI.Replace("$VAR_NAME$", row["Name"].ToString()));
                            FC_AI.Write("\n");
                        }
                    }
                    FC_AI.Write("END_FUNCTION");
                    FC_AI.Close();
                    FC_AI.Dispose();
                    #endregion

                    #region DB_AIN
                    //New Stream For DB_AIN
                    StreamWriter DB_AI = new StreamWriter(filePathAI + @"\DB_AI.db", false);

                    //DB_AIN Body
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("I") && row["Logical Address"].ToString().Contains("W"))
                        {
                            DB_AI.Write(TiaToolsV2_0.Properties.Resources.DB_AI.Replace("$VAR_NAME$", row["Name"].ToString()));
                        }
                    }
                    DB_AI.Close();
                    DB_AI.Dispose();
                    #endregion

                    #region FC_Analog_IN_Config
                    //New Stream For FC_Analog_IN_Config
                    StreamWriter FC_AI_Config = new StreamWriter(filePathAI + @"\FC_AI_Config.scl", false);

                    //FC_Digital_IN_Config Body
                    FC_AI_Config.Write(TiaToolsV2_0.Properties.Resources.FC_AI_Config);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("I") && row["Logical Address"].ToString().Contains("W"))
                        {
                            FC_AI_Config.Write(TiaToolsV2_0.Properties.Resources.FC_AI_Config_Part.Replace("$VAR_NAME$", row["Name"].ToString()));
                            FC_AI_Config.Write("\n");
                        }
                    }
                    FC_AI_Config.Write("END_FUNCTION");
                    FC_AI_Config.Close();
                    FC_AI_Config.Dispose();
                    #endregion
                }
                #endregion

                #region DO
                //Input Files
                if (checkBoxD_OUT.Checked)
                {
                    //Create Directory
                    Directory.CreateDirectory(filePathDQ);

                    #region FC_Digital_OUT
                    //New Stream For FC_Digital_OUT
                    StreamWriter FC_DQ = new StreamWriter(filePathDQ + @"\FC_DQ.scl", false);

                    //FC_Digital_OUT Body
                    FC_DQ.Write(TiaToolsV2_0.Properties.Resources.FC_DQ);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("Q") && !row["Logical Address"].ToString().Contains("W"))
                        {
                            FC_DQ.Write(TiaToolsV2_0.Properties.Resources.FC_DQ_Part.Replace("$VAR_NAME$", row["Name"].ToString()));
                            FC_DQ.Write("\n");
                        }
                    }
                    FC_DQ.Write("END_FUNCTION");
                    FC_DQ.Close();
                    FC_DQ.Dispose();
                    #endregion

                    #region DB_DOUT
                    //New Stream For DB_DOUT
                    StreamWriter DB_DQ = new StreamWriter(filePathDQ + @"\DB_DQ.db", false);

                    //DB_DOUT Body
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("Q") && !row["Logical Address"].ToString().Contains("W"))
                        {
                            DB_DQ.Write(TiaToolsV2_0.Properties.Resources.DB_DQ.Replace("$VAR_NAME$", row["Name"].ToString()));
                        }
                    }
                    DB_DQ.Close();
                    DB_DQ.Dispose();
                    #endregion

                    #region FC_Digital_OUT_Config
                    //New Stream For FC_Digital_OUT_Config
                    StreamWriter FC_DQ_Config = new StreamWriter(filePathDQ + @"\FC_DQ_Config.scl", false);

                    //FC_Digital_OUT_Config Body
                    FC_DQ_Config.Write(TiaToolsV2_0.Properties.Resources.FC_DQ_Config);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("Q") && !row["Logical Address"].ToString().Contains("W"))
                        {
                            FC_DQ_Config.Write(TiaToolsV2_0.Properties.Resources.FC_DQ_Config_Part.Replace("$VAR_NAME$", row["Name"].ToString()));
                            FC_DQ_Config.Write("\n");
                        }
                    }
                    FC_DQ_Config.Write("END_FUNCTION");
                    FC_DQ_Config.Close();
                    FC_DQ_Config.Dispose();
                    #endregion
                }
                #endregion

                #region AO
                //Input Files
                if (checkBoxA_OUT.Checked)
                {
                    //Create Directory
                    Directory.CreateDirectory(filePathAQ);

                    #region FC_Analog_OUT
                    //New Stream For FC_Analog_OUT
                    StreamWriter FC_AQ = new StreamWriter(filePathAQ + @"\FC_AQ.scl", false);

                    //FC_Digital_OUT Body
                    FC_AQ.Write(TiaToolsV2_0.Properties.Resources.FC_AQ);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("Q") && row["Logical Address"].ToString().Contains("W"))
                        {
                            FC_AQ.Write(TiaToolsV2_0.Properties.Resources.FC_AQ_Part.Replace("$VAR_NAME$", row["Name"].ToString()));
                            FC_AQ.Write("\n");
                        }
                    }
                    FC_AQ.Write("END_FUNCTION");
                    FC_AQ.Close();
                    FC_AQ.Dispose();
                    #endregion

                    #region DB_AOUT
                    //New Stream For DB_AOUT
                    StreamWriter DB_AQ = new StreamWriter(filePathAQ + @"\DB_AQ.db", false);

                    //DB_AOUT Body
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("Q") && row["Logical Address"].ToString().Contains("W"))
                        {
                            DB_AQ.Write(TiaToolsV2_0.Properties.Resources.DB_AQ.Replace("$VAR_NAME$", row["Name"].ToString()));
                        }
                    }
                    DB_AQ.Close();
                    DB_AQ.Dispose();
                    #endregion

                    #region FC_Analog_OUT_Config
                    //New Stream For FC_Analog_OUT_Config
                    StreamWriter FC_AQ_Config = new StreamWriter(filePathAQ + @"\FC_AQ_Config.scl", false);

                    //FC_Analog_OUT_Config Body
                    FC_AQ_Config.Write(TiaToolsV2_0.Properties.Resources.FC_AQ_Config);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Logical Address"].ToString().Contains("Q") && row["Logical Address"].ToString().Contains("W"))
                        {
                            FC_AQ_Config.Write(TiaToolsV2_0.Properties.Resources.FC_AQ_Config_Part.Replace("$VAR_NAME$", row["Name"].ToString()));
                            FC_AQ_Config.Write("\n");
                        }
                    }
                    FC_AQ_Config.Write("END_FUNCTION");
                    FC_AQ_Config.Close();
                    FC_AQ_Config.Dispose();
                    #endregion
                }
                #endregion

                #region Types
                if (checkBoxTypes.Checked)
                {
                    //Create Directory
                    Directory.CreateDirectory(filePathTypes);

                    //New Stream For FC_Analog_OUT
                    StreamWriter IO_Types = new StreamWriter(filePathTypes + @"\IO_Types.udt", false);


                    //Write From Source
                    IO_Types.Write(TiaToolsV2_0.Properties.Resources.IO_Types);

                    //Close Stream
                    IO_Types.Close();
                    IO_Types.Dispose();
                }
                #endregion

                #region IO_FB
                if (checkBoxFB.Checked)
                {
                    //Create Directory
                    Directory.CreateDirectory(filePathFB);

                    //New Stream For FC_Analog_OUT
                    StreamWriter FB_Analog_IN = new StreamWriter(filePathFB + @"\FB_AI.scl", false);
                    StreamWriter FB_Digital_IN = new StreamWriter(filePathFB + @"\FB_DI.scl", false);
                    StreamWriter FB_Analog_OUT = new StreamWriter(filePathFB + @"\FB_AQ.scl", false);
                    StreamWriter FB_Digital_OUT = new StreamWriter(filePathFB + @"\FB_DQ.scl", false);

                    //Write From Source
                    FB_Analog_IN.Write(TiaToolsV2_0.Properties.Resources.FB_AI);
                    FB_Digital_IN.Write(TiaToolsV2_0.Properties.Resources.FB_DI);
                    FB_Analog_OUT.Write(TiaToolsV2_0.Properties.Resources.FB_AQ);
                    FB_Digital_OUT.Write(TiaToolsV2_0.Properties.Resources.FB_DQ);

                    //Close Stream
                    FB_Analog_IN.Close();
                    FB_Analog_IN.Dispose();
                    FB_Analog_OUT.Close();
                    FB_Analog_OUT.Dispose();
                    FB_Digital_IN.Close();
                    FB_Digital_IN.Dispose();
                    FB_Digital_OUT.Close();
                    FB_Digital_OUT.Dispose();
                }
                #endregion
            }
            #endregion
        }
    }
}