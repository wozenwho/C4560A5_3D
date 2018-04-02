using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;

namespace asgn5v1
{
    /// <summary>
    /// Summary description for Transformer.
    /// </summary>
    public class Transformer : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;
        //private bool GetNewData();

        // basic data for Transformer

        int numpts = 0;
        int numlines = 0;
        bool gooddata = false;
        double[,] verticesL;
        double[,] verticesR;
        double[,] scrnpts;
        double[,] scrnptsL;
        double[,] scrnptsR;
        double[,] ctrans = new double[4, 4];  //your main transformation matrix
        double[] offset = new double[2];
        private System.Windows.Forms.ImageList tbimages;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton transleftbtn;
        private System.Windows.Forms.ToolBarButton transrightbtn;
        private System.Windows.Forms.ToolBarButton transupbtn;
        private System.Windows.Forms.ToolBarButton transdownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton scaleupbtn;
        private System.Windows.Forms.ToolBarButton scaledownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        private System.Windows.Forms.ToolBarButton rotxby1btn;
        private System.Windows.Forms.ToolBarButton rotyby1btn;
        private System.Windows.Forms.ToolBarButton rotzby1btn;
        private System.Windows.Forms.ToolBarButton toolBarButton3;
        private System.Windows.Forms.ToolBarButton rotxbtn;
        private System.Windows.Forms.ToolBarButton rotybtn;
        private System.Windows.Forms.ToolBarButton rotzbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton4;
        private System.Windows.Forms.ToolBarButton shearrightbtn;
        private System.Windows.Forms.ToolBarButton shearleftbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton5;
        private System.Windows.Forms.ToolBarButton resetbtn;
        private System.Windows.Forms.ToolBarButton exitbtn;
        int[,] lines;

        private bool running;

        double[,] centreMat = new double[4, 4];
        double[,] originMat = new double[4, 4];
        double[,] scaleMat = new double[4, 4];
        double[,] translMat = new double[4, 4];
        double[,] reflectMat = new double[4, 4];
        double[,] translDownMat = new double[4, 4];
        double[,] translUpMat = new double[4, 4];
        double[,] translLeftMat = new double[4, 4];
        double[,] translRightMat = new double[4, 4];
        double[,] scaleUpMat = new double[4, 4];
        double[,] scaleDownMat = new double[4, 4];
        double[,] rotateXMat = new double[4, 4];
        double[,] rotateYMat = new double[4, 4];
        double[,] rotateZMat = new double[4, 4];

        double[,] initialMat = new double[4, 4];

        double[,] shearRightMat = new double[4, 4];
        double[,] shearLeftMat = new double[4, 4];

        double[] origin = new double[3];
        double[] shearingThings = new double[4];

        double maxW = 0;
        double maxH = 0;
        double minW = -999;
        double minH = -999;
        double midX;
        double midY;
        double midZ;
        double screenMidX;
        double screenMidY;

        //Timer variables
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        bool rotX = false;
        bool rotY = false;
        bool rotZ = false;

        public Transformer()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            Text = "COMP 4560:  Assignment 5 A00985710 I AM WILSON HU";
            ResizeRedraw = true;
            BackColor = Color.Black;
            MenuItem miNewDat = new MenuItem("New &Data...",
                new EventHandler(MenuNewDataOnClick));
            MenuItem miExit = new MenuItem("E&xit",
                new EventHandler(MenuFileExitOnClick));
            MenuItem miDash = new MenuItem("-");
            MenuItem miFile = new MenuItem("&File",
                new MenuItem[] { miNewDat, miDash, miExit });
            MenuItem miAbout = new MenuItem("&About",
                new EventHandler(MenuAboutOnClick));
            Menu = new MainMenu(new MenuItem[] { miFile, miAbout });


        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Transformer());
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.Red, 3);
            Pen pen2 = new Pen(Color.Cyan, 3);
            double temp;
            double temp2;
            int k;

            if (gooddata)
            {
                //create the screen coordinates:
                //scrnpts = vertices*ctrans

                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp = 0.0d;
                        temp2 = 0.0d;
                        for (k = 0; k < 4; k++)
                        {
                            temp += verticesL[i, k] * ctrans[k, j];
                            temp2 += verticesR[i, k] * ctrans[k, j];
                        }
                        scrnptsL[i, j] = temp;
                        scrnptsR[i, j] = temp2;
                    }
                }

                //now draw the lines

                for (int i = 0; i < numlines; i++)
                {
                    grfx.DrawLine(pen, (int)scrnptsL[lines[i, 0], 0], (int)scrnptsL[lines[i, 0], 1],
                        (int)scrnptsL[lines[i, 1], 0], (int)scrnptsL[lines[i, 1], 1]);
                    grfx.DrawLine(pen2, (int)scrnptsR[lines[i, 0], 0], (int)scrnptsR[lines[i, 0], 1],
                        (int)scrnptsR[lines[i, 1], 0], (int)scrnptsR[lines[i, 1], 1]);
                }


            } // end of gooddata block	
        } // end of OnPaint

        void MenuNewDataOnClick(object obj, EventArgs ea)
        {
            //MessageBox.Show("New Data item clicked.");
            gooddata = GetNewData();
            RestoreInitialImage();
        }

        void MenuFileExitOnClick(object obj, EventArgs ea)
        {
            Close();
        }

        void MenuAboutOnClick(object obj, EventArgs ea)
        {
            AboutDialogBox dlg = new AboutDialogBox();
            dlg.ShowDialog();
        }

        void RestoreInitialImage()
        {
            Array.Copy(initialMat, ctrans, initialMat.Length);
            running = false;
            timer.Stop();
            //Refresh();
            Invalidate();
        } // end of RestoreInitialImage

        bool GetNewData()
        {
            string strinputfile, text;
            ArrayList coorddata = new ArrayList();
            ArrayList linesdata = new ArrayList();
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Title = "Choose File with Coordinates of Vertices";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo coordfile = new FileInfo(strinputfile);
                StreamReader reader = coordfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) coorddata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeCoords(coorddata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Coordinates File***");
                return false;
            }

            opendlg.Title = "Choose File with Data Specifying Lines";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo linesfile = new FileInfo(strinputfile);
                StreamReader reader = linesfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) linesdata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeLines(linesdata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Line Data File***");
                return false;
            }
            scrnptsL = new double[numpts, 4];
            scrnptsR = new double[numpts, 4];

            setIdentity(ctrans, 4, 4);  //initialize transformation matrix to identity

            //Initial translate, scale, reflection, and translate matrices
            setIdentity(translMat, 4, 4);
            origin[0] = -verticesL[0, 0];
            origin[1] = -verticesL[0, 1];
            origin[2] = -verticesL[0, 2];
            origin[0] = -verticesR[0, 0];
            origin[1] = -verticesR[0, 1];
            origin[2] = -verticesR[0, 2];
            for (int i = 0; i < 3; i++)
                translMat[3, i] = origin[i];

            setIdentity(scaleMat, 4, 4);
            double scale = (this.Height / 2 / (maxH - minH))/2;
            scaleMat[0, 0] = scale;
            scaleMat[1, 1] = scale;
            scaleMat[2, 2] = scale;

            setIdentity(reflectMat, 4, 4);
            reflectMat[1, 1] = -1;

            setIdentity(centreMat, 4, 4);
            centreMat[3, 0] = this.Width / 2;
            centreMat[3, 1] = this.Height / 2;

            ctrans = MatrixMultiplicationForDays(translMat, ctrans);
            ctrans = MatrixMultiplicationForDays(scaleMat, ctrans);
            ctrans = MatrixMultiplicationForDays(reflectMat, ctrans);
            ctrans = MatrixMultiplicationForDays(centreMat, ctrans);


            Array.Copy(ctrans, initialMat, ctrans.Length);

            //Sets translate down matrix
            setIdentity(translDownMat, 4, 4);
            translDownMat[3, 1] = 10;

            //Sets translate up matrix
            setIdentity(translUpMat, 4, 4);
            translUpMat[3, 1] = -10;

            //Sets translate left matrix
            setIdentity(translLeftMat, 4, 4);
            translLeftMat[3, 0] = -10;

            //Sets translate right matrix
            setIdentity(translRightMat, 4, 4);
            translRightMat[3, 0] = 10;

            //Sets scale up matrix
            setIdentity(scaleUpMat, 4, 4);
            scaleUpMat[0, 0] = 1.1;
            scaleUpMat[1, 1] = 1.1;
            scaleUpMat[2, 2] = 1.1;

            //Sets scale down matrix
            setIdentity(scaleDownMat, 4, 4);
            scaleDownMat[0, 0] = 0.9;
            scaleDownMat[1, 1] = 0.9;
            scaleDownMat[2, 2] = 0.9;

            //Sets rotate x matrix
            setIdentity(rotateXMat, 4, 4);
            rotateXMat[1, 1] = Math.Cos(0.05);
            rotateXMat[1, 2] = -Math.Sin(0.05);
            rotateXMat[2, 1] = Math.Sin(0.05);
            rotateXMat[2, 2] = Math.Cos(0.05);

            //Sets rotate y matrix
            setIdentity(rotateYMat, 4, 4);
            rotateYMat[0, 0] = Math.Cos(0.05);
            rotateYMat[0, 2] = -Math.Sin(0.05);
            rotateYMat[2, 0] = Math.Sin(0.05);
            rotateYMat[2, 2] = Math.Cos(0.05);

            //Sets rotate z matrix
            setIdentity(rotateZMat, 4, 4);
            rotateZMat[0, 0] = Math.Cos(0.05);
            rotateZMat[0, 1] = -Math.Sin(0.05);
            rotateZMat[1, 0] = Math.Sin(0.05);
            rotateZMat[1, 1] = Math.Cos(0.05);

            //Sets shear left matrix
            setIdentity(shearLeftMat, 4, 4);
            shearLeftMat[1, 0] = 0.1;

            //Sets shear right matrix   
            setIdentity(shearRightMat, 4, 4);
            shearRightMat[1, 0] = -0.1;

            //shearing things
            shearingThings[0] = minW;
            shearingThings[1] = minH * scale;
            shearingThings[2] = 1;
            shearingThings[3] = 1;

            // MORE THINGS TO ADD.
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Interval = 20;


            return true;
        } // end of GetNewData

        void DecodeCoords(ArrayList coorddata)
        {
            //this may allocate slightly more rows that necessary
            verticesL = new double[coorddata.Count, 4];
            verticesR = new double[coorddata.Count, 4];
            numpts = 0;
            string[] text = null;

            for (int i = 0; i < coorddata.Count; i++)
            {
                text = coorddata[i].ToString().Split(' ', ',');
                verticesL[numpts, 0] = double.Parse(text[0]);
                //if (verticesL[numpts, 0] < 0.0d)
                //    break;
                verticesL[numpts, 1] = double.Parse(text[1]);
                verticesL[numpts, 2] = double.Parse(text[2]);
                verticesL[numpts, 3] = 1.0d;

                verticesR[numpts, 0] = double.Parse(text[5]);
                //if (verticesR[numpts, 0] < 0.0d)
                //    break;
                verticesR[numpts, 1] = double.Parse(text[6]);
                verticesR[numpts, 2] = double.Parse(text[7]);
                verticesR[numpts, 3] = 1.0d;


                if (minW == -999 && minH == -999)
                {
                    minW = verticesL[numpts, 0];
                    minH = verticesL[numpts, 1];
                }
                if (verticesL[numpts, 0] > maxW)
                {
                    maxW = (int)verticesL[numpts, 0];
                }
                if (verticesL[numpts, 1] > maxH && verticesL[numpts, 1] >= 0)
                {
                    maxH = (int)verticesL[numpts, 1];
                }
                if (verticesL[numpts, 0] < minW)
                {
                    minW = (int)verticesL[numpts, 0];
                }
                if (verticesL[numpts, 1] < minH && verticesL[numpts, 1] >= 0)
                {
                    minH = (int)verticesL[numpts, 1];
                }
                numpts++;
            }

            midX = (maxW - minW) / 2;
            midY = (maxH - minH) / 2;

        }// end of DecodeCoords

        void DecodeLines(ArrayList linesdata)
        {
            //this may allocate slightly more rows that necessary
            lines = new int[linesdata.Count, 2];
            numlines = 0;
            string[] text = null;
            for (int i = 0; i < linesdata.Count; i++)
            {
                text = linesdata[i].ToString().Split(' ', ',');
                lines[numlines, 0] = int.Parse(text[0]);
                if (lines[numlines, 0] < 0) break;
                lines[numlines, 1] = int.Parse(text[1]);
                numlines++;
            }
        } // end of DecodeLines

        void setIdentity(double[,] A, int nrow, int ncol)
        {
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++)
                    A[i, j] = 0.0d;
                A[i, i] = 1.0d;
            }

        }// end of setIdentity


        private void Transformer_Load(object sender, System.EventArgs e)
        {

        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (e.Button == transleftbtn)
            {

                ctrans = MatrixMultiplicationForDays(translLeftMat, ctrans);
                Refresh();
            }
            if (e.Button == transrightbtn)
            {
                ctrans = MatrixMultiplicationForDays(translRightMat, ctrans);
                Refresh();
            }
            if (e.Button == transupbtn)
            {
                ctrans = MatrixMultiplicationForDays(translUpMat, ctrans);
                Refresh();
            }

            if (e.Button == transdownbtn)
            {
                ctrans = MatrixMultiplicationForDays(translDownMat, ctrans);
                Refresh();
            }
            if (e.Button == scaleupbtn)
            {
                double[,] originMatrix = new double[4, 4];
                setIdentity(originMatrix, 4, 4);
                originMatrix[3, 0] = -scrnpts[0, 0];
                originMatrix[3, 1] = -scrnpts[0, 1];
                originMatrix[3, 2] = -scrnpts[0, 2];

                double[,] deOriginMatrix = new double[4, 4];
                setIdentity(deOriginMatrix, 4, 4);
                deOriginMatrix[3, 0] = scrnpts[0, 0];
                deOriginMatrix[3, 1] = scrnpts[0, 1];
                deOriginMatrix[3, 2] = scrnpts[0, 2];

                shearingThings[1] *= 1.1;

                ctrans = MatrixMultiplicationForDays(originMatrix, ctrans);
                ctrans = MatrixMultiplicationForDays(scaleUpMat, ctrans);
                ctrans = MatrixMultiplicationForDays(deOriginMatrix, ctrans);

                Refresh();
            }
            if (e.Button == scaledownbtn)
            {
                double[,] originMatrix = new double[4, 4];
                setIdentity(originMatrix, 4, 4);
                originMatrix[3, 0] = -scrnpts[0, 0];
                originMatrix[3, 1] = -scrnpts[0, 1];
                originMatrix[3, 2] = -scrnpts[0, 2];

                double[,] deOriginMatrix = new double[4, 4];
                setIdentity(deOriginMatrix, 4, 4);
                deOriginMatrix[3, 0] = scrnpts[0, 0];
                deOriginMatrix[3, 1] = scrnpts[0, 1];
                deOriginMatrix[3, 2] = scrnpts[0, 2];

                shearingThings[1] *= 0.9;

                ctrans = MatrixMultiplicationForDays(originMatrix, ctrans);
                ctrans = MatrixMultiplicationForDays(scaleDownMat, ctrans);
                ctrans = MatrixMultiplicationForDays(deOriginMatrix, ctrans);

                Refresh();
            }
            if (e.Button == rotxby1btn)
            {
                double[,] originMatrix = new double[4, 4];
                setIdentity(originMatrix, 4, 4);
                originMatrix[3, 0] = -scrnpts[0, 0];
                originMatrix[3, 1] = -scrnpts[0, 1];
                originMatrix[3, 2] = -scrnpts[0, 2];

                double[,] deOriginMatrix = new double[4, 4];
                setIdentity(deOriginMatrix, 4, 4);
                deOriginMatrix[3, 0] = scrnpts[0, 0];
                deOriginMatrix[3, 1] = scrnpts[0, 1];
                deOriginMatrix[3, 2] = scrnpts[0, 2];

                ctrans = MatrixMultiplicationForDays(originMatrix, ctrans);
                ctrans = MatrixMultiplicationForDays(rotateXMat, ctrans);
                ctrans = MatrixMultiplicationForDays(deOriginMatrix, ctrans);

                Refresh();

            }
            if (e.Button == rotyby1btn)
            {
                double[,] originMatrix = new double[4, 4];
                setIdentity(originMatrix, 4, 4);
                originMatrix[3, 0] = -scrnpts[0, 0];
                originMatrix[3, 1] = -scrnpts[0, 1];
                originMatrix[3, 2] = -scrnpts[0, 2];

                double[,] deOriginMatrix = new double[4, 4];
                setIdentity(deOriginMatrix, 4, 4);
                deOriginMatrix[3, 0] = scrnpts[0, 0];
                deOriginMatrix[3, 1] = scrnpts[0, 1];
                deOriginMatrix[3, 2] = scrnpts[0, 2];

                ctrans = MatrixMultiplicationForDays(originMatrix, ctrans);
                ctrans = MatrixMultiplicationForDays(rotateYMat, ctrans);
                ctrans = MatrixMultiplicationForDays(deOriginMatrix, ctrans);

                Refresh();
            }
            if (e.Button == rotzby1btn)
            {
                double[,] originMatrix = new double[4, 4];
                setIdentity(originMatrix, 4, 4);
                originMatrix[3, 0] = -scrnpts[0, 0];
                originMatrix[3, 1] = -scrnpts[0, 1];
                originMatrix[3, 2] = -scrnpts[0, 2];

                double[,] deOriginMatrix = new double[4, 4];
                setIdentity(deOriginMatrix, 4, 4);
                deOriginMatrix[3, 0] = scrnpts[0, 0];
                deOriginMatrix[3, 1] = scrnpts[0, 1];
                deOriginMatrix[3, 2] = scrnpts[0, 2];

                ctrans = MatrixMultiplicationForDays(originMatrix, ctrans);
                ctrans = MatrixMultiplicationForDays(rotateZMat, ctrans);
                ctrans = MatrixMultiplicationForDays(deOriginMatrix, ctrans);

                Refresh();
            }

            if (e.Button == rotxbtn)
            {
                timer.Stop();
                running = false;
                rotX = true;
                rotY = false;
                rotZ = false;
                timer.Start();
                running = true;

                while (running)
                {
                    Application.DoEvents();
                }

            }

            if (e.Button == rotybtn)
            {
                timer.Stop();
                running = false;
                rotX = false;
                rotY = true;
                rotZ = false;
                timer.Start();
                running = true;

                while (running)
                {
                    Application.DoEvents();
                }
            }

            if (e.Button == rotzbtn)
            {
                timer.Stop();
                running = false;
                rotX = false;
                rotY = false;
                rotZ = true;
                timer.Start();
                running = true;

                while (running)
                {
                    Application.DoEvents();
                }
            }

            if (e.Button == shearleftbtn)
            {
                double maxY = 0;

                for (int i = 0; i < scrnpts.GetLength(0); i++)
                {
                    if (scrnpts[i, 1] > maxY)
                        maxY = scrnpts[i, 1];
                }

                double[,] originMatrix = new double[4, 4];
                setIdentity(originMatrix, 4, 4);
                originMatrix[3, 1] = -maxY - shearingThings[1];

                double[,] deOriginMatrix = new double[4, 4];
                setIdentity(deOriginMatrix, 4, 4);
                deOriginMatrix[3, 1] = maxY + shearingThings[1];

                ctrans = MatrixMultiplicationForDays(originMatrix, ctrans);
                ctrans = MatrixMultiplicationForDays(shearLeftMat, ctrans);
                ctrans = MatrixMultiplicationForDays(deOriginMatrix, ctrans);

                Refresh();
            }

            if (e.Button == shearrightbtn)
            {

                double maxY = 0;

                for (int i = 0; i < scrnpts.GetLength(0); i++)
                {
                    if (scrnpts[i, 1] > maxY)
                        maxY = scrnpts[i, 1];
                }

                double[,] originMatrix = new double[4, 4];
                setIdentity(originMatrix, 4, 4);
                originMatrix[3, 1] = -maxY - shearingThings[1];

                double[,] deOriginMatrix = new double[4, 4];
                setIdentity(deOriginMatrix, 4, 4);
                deOriginMatrix[3, 1] = maxY + shearingThings[1];

                ctrans = MatrixMultiplicationForDays(originMatrix, ctrans);
                ctrans = MatrixMultiplicationForDays(shearRightMat, ctrans);
                ctrans = MatrixMultiplicationForDays(deOriginMatrix, ctrans);

                Refresh();
            }

            if (e.Button == resetbtn)
            {
                RestoreInitialImage();
            }

            if (e.Button == exitbtn)
            {
                Close();
            }

        }

        private double[,] MatrixMultiplicationForDays(double[,] transfMatrix, double[,] origMatrix)
        {

            double[,] newMatrix = new double[4, 4];

            double temp;
            double d1;
            double d2;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    temp = 0.0d;
                    for (int k = 0; k < 4; k++)
                    {
                        d1 = transfMatrix[row, k];
                        d2 = origMatrix[k, col];
                        temp += origMatrix[row, k] * transfMatrix[k, col];
                    }
                    newMatrix[row, col] = temp;
                }
            }

            //double temp2;
            //double[] newMatrix2 = new double[4];
            //for (int row = 0; row < 1; row++)
            //{
            //    for (int col = 0; col < 4; col++)
            //    {
            //        temp2 = 0.0d;
            //        for (int k = 0; k < 4; k++)
            //        {
            //            d1 = transfMatrix[row, k];
            //            d2 = origMatrix[k, col];
            //            temp2 += origMatrix[row, k] * transfMatrix[k, col];
            //        }
            //        newMatrix[row, col] = temp2;
            //    }
            //}
            //shearingThings = newMatrix2;

            return newMatrix;
        }

        private void rescale()
        {

        }

        private void rotateX()
        {
            double[,] originMatrix = new double[4, 4];
            setIdentity(originMatrix, 4, 4);
            originMatrix[3, 0] = -scrnpts[0, 0];
            originMatrix[3, 1] = -scrnpts[0, 1];
            originMatrix[3, 2] = -scrnpts[0, 2];

            double[,] deOriginMatrix = new double[4, 4];
            setIdentity(deOriginMatrix, 4, 4);
            deOriginMatrix[3, 0] = scrnpts[0, 0];
            deOriginMatrix[3, 1] = scrnpts[0, 1];
            deOriginMatrix[3, 2] = scrnpts[0, 2];

            ctrans = MatrixMultiplicationForDays(originMatrix, ctrans);
            ctrans = MatrixMultiplicationForDays(rotateXMat, ctrans);
            ctrans = MatrixMultiplicationForDays(deOriginMatrix, ctrans);
            Refresh();

        }

        private void rotateY()
        {
            double[,] originMatrix = new double[4, 4];
            setIdentity(originMatrix, 4, 4);
            originMatrix[3, 0] = -scrnpts[0, 0];
            originMatrix[3, 1] = -scrnpts[0, 1];
            originMatrix[3, 2] = -scrnpts[0, 2];

            double[,] deOriginMatrix = new double[4, 4];
            setIdentity(deOriginMatrix, 4, 4);
            deOriginMatrix[3, 0] = scrnpts[0, 0];
            deOriginMatrix[3, 1] = scrnpts[0, 1];
            deOriginMatrix[3, 2] = scrnpts[0, 2];

            ctrans = MatrixMultiplicationForDays(originMatrix, ctrans);
            ctrans = MatrixMultiplicationForDays(rotateYMat, ctrans);
            ctrans = MatrixMultiplicationForDays(deOriginMatrix, ctrans);
            Refresh();

        }

        private void rotateZ()
        {
            double[,] originMatrix = new double[4, 4];
            setIdentity(originMatrix, 4, 4);
            originMatrix[3, 0] = -scrnpts[0, 0];
            originMatrix[3, 1] = -scrnpts[0, 1];
            originMatrix[3, 2] = -scrnpts[0, 2];

            double[,] deOriginMatrix = new double[4, 4];
            setIdentity(deOriginMatrix, 4, 4);
            deOriginMatrix[3, 0] = scrnpts[0, 0];
            deOriginMatrix[3, 1] = scrnpts[0, 1];
            deOriginMatrix[3, 2] = scrnpts[0, 2];

            ctrans = MatrixMultiplicationForDays(originMatrix, ctrans);
            ctrans = MatrixMultiplicationForDays(rotateZMat, ctrans);
            ctrans = MatrixMultiplicationForDays(deOriginMatrix, ctrans);
            Refresh();

        }

        private void TimerEventProcessor(Object o, EventArgs ea)
        {
            if (rotX && !rotY && !rotZ)
            {
                rotateX();
            }
            if (!rotX && rotY && !rotZ)
            {
                rotateY();
            }
            if (!rotX && !rotY && rotZ)
            {
                rotateZ();
            }
        }
    } // End of Class	
} // End of Namespace
