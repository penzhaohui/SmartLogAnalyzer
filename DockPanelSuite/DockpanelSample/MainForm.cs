using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DockpanelSample
{
    public partial class MainForm : Form
    {
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private DummySolutionExplorer m_solutionExplorer;
        private DummyPropertyWindow m_propertyWindow;
        private DummyToolbox m_toolbox;
        private DummyOutputWindow m_outputWindow;
        private DummyTaskList m_taskList;

        public MainForm()
        {
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.Dpi;

            //SetSplashScreen();
            CreateStandardControls();

            //showRightToLeft.Checked = (RightToLeft == RightToLeft.Yes);
            //RightToLeftLayout = showRightToLeft.Checked;
            m_solutionExplorer.RightToLeftLayout = RightToLeftLayout;
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);           

            visualStudioToolStripExtender.DefaultRenderer = _toolStripProfessionalRenderer;
            SetSchema(this.schemaVS2003ToolStripMenuItem, null);;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dockPanel.SuspendLayout(true);

            Assembly assembly = Assembly.GetAssembly(typeof(MainForm));
            Stream xmlStream = assembly.GetManifestResourceStream("DockSample.Resources.DockPanel.xml");
            dockPanel.LoadFromXml(xmlStream, m_deserializeDockContent);
            xmlStream.Close();

            dockPanel.ResumeLayout(true, true);
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(DummySolutionExplorer).ToString())
                return m_solutionExplorer;
            else if (persistString == typeof(DummyPropertyWindow).ToString())
                return m_propertyWindow;
            else if (persistString == typeof(DummyToolbox).ToString())
                return m_toolbox;
            else if (persistString == typeof(DummyOutputWindow).ToString())
                return m_outputWindow;
            else if (persistString == typeof(DummyTaskList).ToString())
                return m_taskList;
            else
            {
                // DummyDoc overrides GetPersistString to add extra information into persistString.
                // Any DockContent may override this value to add any needed information for deserialization.

                string[] parsedStrings = persistString.Split(new char[] { ',' });
                if (parsedStrings.Length != 3)
                    return null;

                if (parsedStrings[0] != typeof(DummyDoc).ToString())
                    return null;

                DummyDoc dummyDoc = new DummyDoc();
                if (parsedStrings[1] != string.Empty)
                    dummyDoc.FileName = parsedStrings[1];
                if (parsedStrings[2] != string.Empty)
                    dummyDoc.Text = parsedStrings[2];

                return dummyDoc;
            }
        }

        private readonly ToolStripRenderer _toolStripProfessionalRenderer = new ToolStripProfessionalRenderer();
        private void SetSchema(object sender, System.EventArgs e)
        {
            // Persist settings when rebuilding UI
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.temp.config");

            dockPanel.SaveAsXml(configFile);
            CloseAllContents();

            if (sender == this.schemaVS2005ToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2005Theme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2005, vs2005Theme);
            }
            else if (sender == this.schemaVS2003ToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2003Theme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2003, vs2003Theme);
            }
            else if (sender == this.schemaVS2010BlueToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2010BlueTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2010, vs2010BlueTheme);
            }
            else if (sender == this.schemaVS2012LightToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2012LightTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2012, vs2012LightTheme);
            }
            else if (sender == this.schemaVS2012BlueToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2012BlueTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2012, vs2012BlueTheme);
            }
            else if (sender == this.schemaVS2012DarkToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2012DarkTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2012, vs2012DarkTheme);
            }
            else if (sender == this.schemaVS2013BlueToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2013BlueTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2013, vs2013BlueTheme);
            }
            else if (sender == this.schemaVS2013LightToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2013LightTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2013, vs2013LightTheme);
            }
            else if (sender == this.schemaVS2013DarkToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2013DarkTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2013, vs2013DarkTheme);
            }
            else if (sender == this.schemaVS2015BlueToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2015BlueTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vs2015BlueTheme);
            }
            else if (sender == this.schemaVS2015LightToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2015LightTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vs2015LightTheme);
            }
            else if (sender == this.schemaVS2015DarkToolStripMenuItem)
            {
                this.dockPanel.Theme = this.vs2015DarkTheme;
                this.EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vs2015DarkTheme);
            }

            schemaVS2005ToolStripMenuItem.Checked = (sender == schemaVS2005ToolStripMenuItem);
            schemaVS2003ToolStripMenuItem.Checked = (sender == schemaVS2003ToolStripMenuItem);
            schemaVS2010BlueToolStripMenuItem.Checked = (sender == schemaVS2010BlueToolStripMenuItem);
            schemaVS2012LightToolStripMenuItem.Checked = (sender == schemaVS2012LightToolStripMenuItem);
            schemaVS2012BlueToolStripMenuItem.Checked = (sender == schemaVS2012BlueToolStripMenuItem);
            schemaVS2012DarkToolStripMenuItem.Checked = (sender == schemaVS2012DarkToolStripMenuItem);
            schemaVS2013LightToolStripMenuItem.Checked = (sender == schemaVS2013LightToolStripMenuItem);
            schemaVS2013BlueToolStripMenuItem.Checked = (sender == schemaVS2013BlueToolStripMenuItem);
            schemaVS2013DarkToolStripMenuItem.Checked = (sender == schemaVS2013DarkToolStripMenuItem);
            schemaVS2015LightToolStripMenuItem.Checked = (sender == schemaVS2015LightToolStripMenuItem);
            schemaVS2015BlueToolStripMenuItem.Checked = (sender == schemaVS2015BlueToolStripMenuItem);
            schemaVS2015DarkToolStripMenuItem.Checked = (sender == schemaVS2015DarkToolStripMenuItem);
            if (dockPanel.Theme.ColorPalette != null)
            {
                //this.statusStrip.BackColor = dockPanel.Theme.ColorPalette.MainWindowStatusBarDefault.Background;
            }

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }

        private void EnableVSRenderer(VisualStudioToolStripExtender.VsVersion version, ThemeBase theme)
        {
            this.visualStudioToolStripExtender.SetStyle(this.menuStrip, version, theme);
            this.visualStudioToolStripExtender.SetStyle(this.toolbarStrip, version, theme);
            //this.visualStudioToolStripExtender.SetStyle(this.statusStrip, version, theme);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (m_bSaveLayout)
                dockPanel.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);
        }

        private void toolbarStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.newToolStripButton)
                newToolStripMenuItem_Click(null, null);
            else if (e.ClickedItem == this.openToolStripButton)
                openToolStripMenuItem_Click(null, null);
            else if (e.ClickedItem == this.solutionExploreToolStripButton)
                solutionExploreToolStripMenuItem_Click(null, null);
            else if (e.ClickedItem == this.propertyWindowToolStripButton)
                propertyWindowToolStripMenuItem_Click(null, null);
            else if (e.ClickedItem == this.toolboxToolStripButton)
                toolboxToolStripMenuItem_Click(null, null);
            else if (e.ClickedItem == this.outputWindowToolStripButton)
                outputWindowToolStripMenuItem_Click(null, null);
            else if (e.ClickedItem == this.taskListToolStripButton)
                taskListToolStripMenuItem_Click(null, null);
            else if (e.ClickedItem == this.layoutByCodeToolStripButton)
                layoutByCodeToolStripMenuItem_Click(null, null);
            else if (e.ClickedItem == this.layoutByXMLToolStripButton)
                layoutByXMLToolStripMenuItem_Click(null, null);
        }

        private void solutionExploreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void propertyWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolboxToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void outputWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void taskListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void layoutByCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dockPanel.SuspendLayout(true);

            CloseAllContents();

            CreateStandardControls();

            m_solutionExplorer.Show(dockPanel, DockState.DockRight);
            m_propertyWindow.Show(m_solutionExplorer.Pane, m_solutionExplorer);
            m_toolbox.Show(dockPanel, DockState.DockLeft);
            m_outputWindow.Show(dockPanel, DockState.DockBottom);
            m_taskList.Show(m_outputWindow.Pane, m_outputWindow);

            DummyDoc doc1 = CreateNewDocument("Document1");
            DummyDoc doc2 = CreateNewDocument("Document2");
            DummyDoc doc3 = CreateNewDocument("Document3");
            DummyDoc doc4 = CreateNewDocument("Document4");
            doc1.Show(dockPanel, DockState.Document);
            doc2.Show(doc1.Pane, null);
            doc3.Show(doc1.Pane, DockAlignment.Bottom, 0.5);
            doc4.Show(doc3.Pane, DockAlignment.Right, 0.5);

            dockPanel.ResumeLayout(true, true);
        }
        private void CreateStandardControls()
        {
            m_solutionExplorer = new DummySolutionExplorer();
            m_propertyWindow = new DummyPropertyWindow();
            m_toolbox = new DummyToolbox();
            m_outputWindow = new DummyOutputWindow();
            m_taskList = new DummyTaskList();
        }

        private void CloseAllContents()
        {
            // we don't want to create another instance of tool window, set DockPanel to null
            m_solutionExplorer.DockPanel = null;
            m_propertyWindow.DockPanel = null;
            m_toolbox.DockPanel = null;
            m_outputWindow.DockPanel = null;
            m_taskList.DockPanel = null;

            // Close all other document windows
            CloseAllDocuments();

            // IMPORTANT: dispose all float windows.
            foreach (var window in dockPanel.FloatWindows.ToList())
                window.Dispose();

            System.Diagnostics.Debug.Assert(dockPanel.Panes.Count == 0);
            System.Diagnostics.Debug.Assert(dockPanel.Contents.Count == 0);
            System.Diagnostics.Debug.Assert(dockPanel.FloatWindows.Count == 0);
        }

        private void CloseAllDocuments()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                foreach (IDockContent document in dockPanel.DocumentsToArray())
                {
                    // IMPORANT: dispose all panes.
                    document.DockHandler.DockPanel = null;
                    document.DockHandler.Close();
                }
            }
        }

        private DummyDoc CreateNewDocument(string text)
        {
            DummyDoc dummyDoc = new DummyDoc();
            dummyDoc.Text = text;
            return dummyDoc;
        }

        private void layoutByXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
