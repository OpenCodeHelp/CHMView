using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;

using HtmlHelp;
using HtmlHelp.UIComponents;
using HtmlHelp.ChmDecoding;
using AxSHDocVw;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace HtmlHelp.UIComponents
{
	/// <summary>
	/// This class implements the main form for the htmlhelp viewer
	/// </summary>
	public class Viewer : System.Windows.Forms.UserControl, IHelpViewer
	{
		private static Viewer _current = null;
		private string LM_Key = @"Software\Klaus Weisser\HtmlHelpViewer\";

		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel3;
		private AxSHDocVw.AxWebBrowser axWebBrowser1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabContents;
		private System.Windows.Forms.TabPage tabIndex;
		private System.Windows.Forms.TabPage tabSearch;
		private HtmlHelp.UIComponents.TocTree tocTree1;
		private HtmlHelp.UIComponents.helpIndex helpIndex1;
		private HtmlHelp.UIComponents.helpSearch helpSearch2;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ImageList imgToolBar;
		private System.Windows.Forms.ToolBarButton btnBack;
		private System.Windows.Forms.ToolBarButton btnNext;
		private System.Windows.Forms.ToolBarButton btnStop;
		private System.Windows.Forms.ToolBarButton btnRefresh;
		private System.Windows.Forms.ToolBarButton btnHome;
		private System.Windows.Forms.ToolBarButton btnSep1;
		private System.Windows.Forms.ToolBarButton btnContents;
		private System.Windows.Forms.ToolBarButton btnIndex;
		private System.Windows.Forms.ToolBarButton btnSearch;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ToolBarButton btnSynch;

		HtmlHelpSystem _reader = null;
		DumpingInfo _dmpInfo=null;
		InfoTypeCategoryFilter _filter = new InfoTypeCategoryFilter();

		string _prefDumpOutput="";

		DumpCompression _prefDumpCompression = DumpCompression.Medium;

		DumpingFlags _prefDumpFlags = DumpingFlags.DumpBinaryTOC | DumpingFlags.DumpTextTOC | 
			DumpingFlags.DumpTextIndex | DumpingFlags.DumpBinaryIndex | 
			DumpingFlags.DumpUrlStr | DumpingFlags.DumpStrings;

		string _prefURLPrefix = "mk:@MSITStore:";
		bool _prefUseHH2TreePics = false;

		/// <summary>
		/// Gets the current viewer window
		/// </summary>
		public static Viewer Current
		{
			get { return _current; }
		}

		/// <summary>
		/// Constructor of the class
		/// </summary>
		public Viewer()
		{
			Viewer._current = this;

			// create a new instance of the classlibrary's main class
			_reader = new HtmlHelpSystem();
			HtmlHelpSystem.UrlPrefix = "mk:@MSITStore:";

			// use temporary folder for data dumping
			string sTemp = System.Environment.GetEnvironmentVariable("TEMP");
			if(sTemp.Length <= 0)
				sTemp = System.Environment.GetEnvironmentVariable("TMP");

			_prefDumpOutput = sTemp;

			// create a dump info instance used for dumping data
			_dmpInfo = 
				new DumpingInfo(DumpingFlags.DumpBinaryTOC | DumpingFlags.DumpTextTOC | 
				DumpingFlags.DumpTextIndex | DumpingFlags.DumpBinaryIndex | 
				DumpingFlags.DumpUrlStr | DumpingFlags.DumpStrings,
				sTemp, DumpCompression.Medium);

			LoadRegistryPreferences();

			HtmlHelpSystem.UrlPrefix = _prefURLPrefix;
			HtmlHelpSystem.UseHH2TreePics = _prefUseHH2TreePics;

			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabContents = new System.Windows.Forms.TabPage();
            this.tocTree1 = new HtmlHelp.UIComponents.TocTree();
            this.tabIndex = new System.Windows.Forms.TabPage();
            this.helpIndex1 = new HtmlHelp.UIComponents.helpIndex();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.helpSearch2 = new HtmlHelp.UIComponents.helpSearch();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.axWebBrowser1 = new AxSHDocVw.AxWebBrowser();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.btnBack = new System.Windows.Forms.ToolBarButton();
            this.btnNext = new System.Windows.Forms.ToolBarButton();
            this.btnStop = new System.Windows.Forms.ToolBarButton();
            this.btnRefresh = new System.Windows.Forms.ToolBarButton();
            this.btnHome = new System.Windows.Forms.ToolBarButton();
            this.btnSynch = new System.Windows.Forms.ToolBarButton();
            this.btnSep1 = new System.Windows.Forms.ToolBarButton();
            this.btnContents = new System.Windows.Forms.ToolBarButton();
            this.btnIndex = new System.Windows.Forms.ToolBarButton();
            this.btnSearch = new System.Windows.Forms.ToolBarButton();
            this.imgToolBar = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabContents.SuspendLayout();
            this.tabIndex.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "chm";
            this.openFileDialog1.Filter = "Compiled HTML-Help files (*.chm)|*.chm|All files|*.*";
            this.openFileDialog1.Title = "Open HTML-Help files";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 4);
            this.panel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(754, 2);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(360, 510);
            this.panel2.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabContents);
            this.tabControl1.Controls.Add(this.tabIndex);
            this.tabControl1.Controls.Add(this.tabSearch);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(360, 510);
            this.tabControl1.TabIndex = 8;
            // 
            // tabContents
            // 
            this.tabContents.Controls.Add(this.tocTree1);
            this.tabContents.Location = new System.Drawing.Point(4, 22);
            this.tabContents.Name = "tabContents";
            this.tabContents.Size = new System.Drawing.Size(352, 484);
            this.tabContents.TabIndex = 0;
            this.tabContents.Text = "Contents";
            // 
            // tocTree1
            // 
            this.tocTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tocTree1.Location = new System.Drawing.Point(0, 0);
            this.tocTree1.Name = "tocTree1";
            this.tocTree1.Padding = new System.Windows.Forms.Padding(2);
            this.tocTree1.Size = new System.Drawing.Size(352, 484);
            this.tocTree1.TabIndex = 0;
            this.tocTree1.TocSelected += new HtmlHelp.UIComponents.TocSelectedEventHandler(this.tocTree1_TocSelected);
            // 
            // tabIndex
            // 
            this.tabIndex.Controls.Add(this.helpIndex1);
            this.tabIndex.Location = new System.Drawing.Point(4, 22);
            this.tabIndex.Name = "tabIndex";
            this.tabIndex.Size = new System.Drawing.Size(352, 484);
            this.tabIndex.TabIndex = 1;
            this.tabIndex.Text = "Index";
            // 
            // helpIndex1
            // 
            this.helpIndex1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpIndex1.Location = new System.Drawing.Point(0, 0);
            this.helpIndex1.Name = "helpIndex1";
            this.helpIndex1.Size = new System.Drawing.Size(352, 484);
            this.helpIndex1.TabIndex = 0;
            this.helpIndex1.IndexSelected += new HtmlHelp.UIComponents.IndexSelectedEventHandler(this.helpIndex1_IndexSelected);
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.helpSearch2);
            this.tabSearch.Location = new System.Drawing.Point(4, 22);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(352, 484);
            this.tabSearch.TabIndex = 2;
            this.tabSearch.Text = "Search";
            // 
            // helpSearch2
            // 
            this.helpSearch2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpSearch2.Location = new System.Drawing.Point(0, 0);
            this.helpSearch2.Name = "helpSearch2";
            this.helpSearch2.Size = new System.Drawing.Size(352, 484);
            this.helpSearch2.TabIndex = 0;
            this.helpSearch2.FTSearch += new HtmlHelp.UIComponents.FTSearchEventHandler(this.helpSearch2_FTSearch);
            this.helpSearch2.HitSelected += new HtmlHelp.UIComponents.HitSelectedEventHandler(this.helpSearch2_HitSelected);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(360, 32);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 510);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.axWebBrowser1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(364, 32);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(390, 510);
            this.panel3.TabIndex = 10;
            // 
            // axWebBrowser1
            // 
            this.axWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWebBrowser1.Enabled = true;
            this.axWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.axWebBrowser1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser1.OcxState")));
            this.axWebBrowser1.Size = new System.Drawing.Size(390, 510);
            this.axWebBrowser1.TabIndex = 0;
            this.axWebBrowser1.DownloadComplete += new System.EventHandler(this.axWebBrowser1_DownloadComplete);
            this.axWebBrowser1.DownloadBegin += new System.EventHandler(this.axWebBrowser1_DownloadBegin);
            this.axWebBrowser1.CommandStateChange += new AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEventHandler(this.axWebBrowser1_CommandStateChanged);
            this.axWebBrowser1.ProgressChange += new AxSHDocVw.DWebBrowserEvents2_ProgressChangeEventHandler(this.axWebBrowser1_ProgressChanged);
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnBack,
            this.btnNext,
            this.btnStop,
            this.btnRefresh,
            this.btnHome,
            this.btnSynch,
            this.btnSep1,
            this.btnContents,
            this.btnIndex,
            this.btnSearch});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imgToolBar;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(754, 28);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // btnBack
            // 
            this.btnBack.ImageIndex = 0;
            this.btnBack.Name = "btnBack";
            this.btnBack.ToolTipText = "Back";
            // 
            // btnNext
            // 
            this.btnNext.ImageIndex = 1;
            this.btnNext.Name = "btnNext";
            this.btnNext.ToolTipText = "Forward";
            // 
            // btnStop
            // 
            this.btnStop.ImageIndex = 2;
            this.btnStop.Name = "btnStop";
            this.btnStop.ToolTipText = "Stop";
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageIndex = 3;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ToolTipText = "Refresh";
            // 
            // btnHome
            // 
            this.btnHome.ImageIndex = 4;
            this.btnHome.Name = "btnHome";
            this.btnHome.ToolTipText = "Default topic";
            // 
            // btnSynch
            // 
            this.btnSynch.ImageIndex = 8;
            this.btnSynch.Name = "btnSynch";
            this.btnSynch.ToolTipText = "Snychronize contents";
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnContents
            // 
            this.btnContents.ImageIndex = 5;
            this.btnContents.Name = "btnContents";
            this.btnContents.ToolTipText = "Show help contents";
            // 
            // btnIndex
            // 
            this.btnIndex.ImageIndex = 6;
            this.btnIndex.Name = "btnIndex";
            this.btnIndex.ToolTipText = "Show help index";
            // 
            // btnSearch
            // 
            this.btnSearch.ImageIndex = 7;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.ToolTipText = "Fulltext search";
            // 
            // imgToolBar
            // 
            this.imgToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolBar.ImageStream")));
            this.imgToolBar.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(211)))), ((int)(((byte)(206)))));
            this.imgToolBar.Images.SetKeyName(0, "");
            this.imgToolBar.Images.SetKeyName(1, "");
            this.imgToolBar.Images.SetKeyName(2, "");
            this.imgToolBar.Images.SetKeyName(3, "");
            this.imgToolBar.Images.SetKeyName(4, "");
            this.imgToolBar.Images.SetKeyName(5, "");
            this.imgToolBar.Images.SetKeyName(6, "");
            this.imgToolBar.Images.SetKeyName(7, "");
            this.imgToolBar.Images.SetKeyName(8, "");
            // 
            // Viewer
            // 
            this.ClientSize = new System.Drawing.Size(754, 542);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolBar1);
            this.Name = "Viewer";
            this.Text = "HtmlHelp - Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabContents.ResumeLayout(false);
            this.tabIndex.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// Navigates the embedded browser to the specified url
		/// </summary>
		/// <param name="url">url to navigate</param>
		private void NavigateBrowser(string url)
		{
			object flags = 0;
			object targetFrame = String.Empty;
			object postData = String.Empty;
			object headers = String.Empty;
			object oUrl = url;

			//axWebBrowser1.Navigate(url, ref flags, ref targetFrame, ref postData, ref headers);
			axWebBrowser1.Navigate2(ref oUrl, ref flags, ref targetFrame, ref postData, ref headers);
		}

		#region Registry preferences
		/// <summary>
		/// Loads viewer preferences from registry
		/// </summary>
		private void LoadRegistryPreferences()
		{
			RegistryKey regKey = Registry.LocalMachine.CreateSubKey(LM_Key);
			
			bool bEnable = bool.Parse(regKey.GetValue("EnableDumping", true).ToString());

			_prefDumpOutput = (string) regKey.GetValue("DumpOutputDir", _prefDumpOutput);
			_prefDumpCompression = (DumpCompression) ((int)regKey.GetValue("CompressionLevel", _prefDumpCompression));
			_prefDumpFlags = (DumpingFlags) ((int)regKey.GetValue("DumpingFlags", _prefDumpFlags));

			if(bEnable)
				_dmpInfo = new DumpingInfo(_prefDumpFlags, _prefDumpOutput, _prefDumpCompression);
			else
				_dmpInfo = null;

			_prefURLPrefix = (string) regKey.GetValue("ITSUrlPrefix", _prefURLPrefix);
			_prefUseHH2TreePics = bool.Parse(regKey.GetValue("UseHH2TreePics", _prefUseHH2TreePics).ToString());
		}
		/// <summary>
		/// Saves viewer preferences to registry
		/// </summary>
		private void SaveRegistryPreferences()
		{
			RegistryKey regKey = Registry.LocalMachine.CreateSubKey(LM_Key);

			regKey.SetValue("EnableDumping", (_dmpInfo!=null));
			regKey.SetValue("DumpOutputDir", _prefDumpOutput);
			regKey.SetValue("CompressionLevel", (int)_prefDumpCompression);
			regKey.SetValue("DumpingFlags", (int)_prefDumpFlags);

			regKey.SetValue("ITSUrlPrefix", _prefURLPrefix);
			regKey.SetValue("UseHH2TreePics", _prefUseHH2TreePics);
		}

		#endregion

		#region Eventhandlers for library usercontrols

		/// <summary>
		/// Called if the user hits the "Search" button on the full-text search pane
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event parameters</param>
		private void helpSearch2_FTSearch(object sender, SearchEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				DataTable dtResults = _reader.PerformSearch( e.Words, 500, e.PartialWords, e.TitlesOnly);
				helpSearch2.SetResults(dtResults);
			}
			finally
			{
				this.Cursor = Cursors.Arrow;
			}
		}

		/// <summary>
		/// Called if the user selects an entry from the search results.
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event parameters</param>
		private void helpSearch2_HitSelected(object sender, HitEventArgs e)
		{
			if( e.URL.Length > 0)
			{
				NavigateBrowser(e.URL);
			}
		}

		/// <summary>
		/// Called if the user selects a new table of contents item
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event parameters</param>
		private void tocTree1_TocSelected(object sender, TocEventArgs e)
		{
			if( e.Item.Local.Length > 0)
			{
				NavigateBrowser(e.Item.Url);
			}
		}

		/// <summary>
		/// Called if the user selects an index topic
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event parameters</param>
		private void helpIndex1_IndexSelected(object sender, IndexEventArgs e)
		{
			if(e.URL.Length > 0)
				NavigateBrowser(e.URL);
			
			
		}

		#endregion

		#region IHelpViewer interface implementation

		/// <summary>
		/// Navigates the helpviewer to a specific help url
		/// </summary>
		/// <param name="url">url</param>
		void IHelpViewer.NavigateTo(string url)
		{
			NavigateBrowser(url);
		}

		/// <summary>
		/// Shows help for a specific url
		/// </summary>
		/// <param name="namespaceFilter">namespace filter (used for merged files)</param>
		/// <param name="hlpNavigator">navigator value</param>
		/// <param name="keyword">keyword</param>
		void IHelpViewer.ShowHelp(string namespaceFilter, HelpNavigator hlpNavigator, string keyword)
		{
			((IHelpViewer)this).ShowHelp(namespaceFilter, hlpNavigator, keyword, "");
		}

		/// <summary>
		/// Shows help for a specific keyword
		/// </summary>
		/// <param name="namespaceFilter">namespace filter (used for merged files)</param>
		/// <param name="hlpNavigator">navigator value</param>
		/// <param name="keyword">keyword</param>
		/// <param name="url">url</param>
		void IHelpViewer.ShowHelp(string namespaceFilter, HelpNavigator hlpNavigator, string keyword, string url)
		{
			switch(hlpNavigator)
			{
				case HelpNavigator.AssociateIndex:
				{
					IndexItem foundIdx = _reader.Index.SearchIndex(keyword, IndexType.AssiciativeLinks);
					if(foundIdx != null)
					{
						if(foundIdx.Topics.Count > 0)
						{
							IndexTopic topic = foundIdx.Topics[0] as IndexTopic;

							if(topic.Local.Length>0)
								NavigateBrowser(topic.URL);
						}
					}
				};break;
				case HelpNavigator.Find:
				{
					this.Cursor = Cursors.WaitCursor;
					this.helpSearch2.SetSearchText(keyword);
					DataTable dtResults = _reader.PerformSearch(keyword, 500, true, false);
					this.helpSearch2.SetResults(dtResults);
					this.Cursor = Cursors.Arrow;
					this.helpSearch2.Focus();

				};break;
				case HelpNavigator.Index:
				{
					((IHelpViewer)this).ShowHelpIndex(url);
				};break;
				case HelpNavigator.KeywordIndex:
				{
					IndexItem foundIdx = _reader.Index.SearchIndex(keyword, IndexType.KeywordLinks);
					if(foundIdx != null)
					{
						if(foundIdx.Topics.Count == 1)
						{
							IndexTopic topic = foundIdx.Topics[0] as IndexTopic;

							if(topic.Local.Length>0)
								NavigateBrowser(topic.URL);
						} 
						else if(foundIdx.Topics.Count>1)
						{
							this.helpIndex1.SelectText(foundIdx.IndentKeyWord);
						}
					}
					this.helpIndex1.Focus();
				};break;
				case HelpNavigator.TableOfContents:
				{
					TOCItem foundTOC = _reader.TableOfContents.SearchTopic(keyword);
					if(foundTOC != null)
					{
						if(foundTOC.Local.Length>0)
							NavigateBrowser(foundTOC.Url);
					}
					this.tocTree1.Focus();
				};break;
				case HelpNavigator.Topic:
				{
					TOCItem foundTOC = _reader.TableOfContents.SearchTopic(keyword);
					if(foundTOC != null)
					{
						if(foundTOC.Local.Length>0)
							NavigateBrowser(foundTOC.Url);
					}
				};break;
			}
		}

		/// <summary>
		/// Shows the help index
		/// </summary>
		/// <param name="url">url</param>
		void IHelpViewer.ShowHelpIndex(string url)
		{
			if( url.Length == 0)
				url = HtmlHelpSystem.Current.DefaultTopic;

			NavigateBrowser(url);
		}

		/// <summary>
		/// Shows a help popup window
		/// </summary>
		/// <param name="parent">the parent control for the popup window</param>
		/// <param name="text">help text</param>
		/// <param name="location">display location</param>
		void IHelpViewer.ShowPopup(Control parent, string text, Point location)
		{
			// Display a native toolwindow and display the help string
			HelpToolTipWindow hlpTTip = new HelpToolTipWindow();
			hlpTTip.Location = location;
			hlpTTip.Text = text;
			hlpTTip.ShowShadow = true;
			hlpTTip.MaximumDuration = 300; // duration before hiding (after focus lost)

			hlpTTip.Show();
		}

		#endregion

		/// <summary>
		/// Called if the form loads
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event parameter</param>
		private void Form1_Load(object sender, System.EventArgs e)
		{
			tocTree1.Enabled = false;
			helpIndex1.Enabled = false;
			helpSearch2.Enabled = false;

			btnContents.Enabled = false;
			btnIndex.Enabled = false;
			btnSearch.Enabled = false;
           
			btnBack.Enabled = false;
			btnNext.Enabled = false;

			btnStop.Enabled = false;
			btnRefresh.Enabled = false;
			btnHome.Enabled = false;
			btnSynch.Enabled = false;
		}

		/// <summary>
		/// Called if the viewer is closing
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event parameter</param>
		private void Viewer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SaveRegistryPreferences();
		}

		/// <summary>
		/// Called if the browser changes its command state (forward/back button)
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event parameter</param>
		private void axWebBrowser1_CommandStateChanged(object sender, AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEvent e)
		{
			switch(e.command)
			{
				case 1: // forward command update
				{
					btnNext.Enabled = e.enable;
				};break;
				case 2:
				case 3: // back command update
				{
					btnBack.Enabled = e.enable;
				};break;
			}
		}

		/// <summary>
		/// Called wenn the download progress of the browser changes
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event parameter</param>
		private void axWebBrowser1_ProgressChanged(object sender, AxSHDocVw.DWebBrowserEvents2_ProgressChangeEvent e)
		{
			double dPercent = ((double)e.progress * 100.0)/(double)e.progressMax;
			dPercent = Math.Round(dPercent);

			//((IStatusBar)base.ServiceProvider.GetService(typeof(IStatusBar))).SetProgress((int)dPercent);
		}

		/// <summary>
		/// Called if the browser begins to download content
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event parameter</param>
		private void axWebBrowser1_DownloadBegin(object sender, System.EventArgs e)
		{
			btnStop.Enabled = true;

			btnHome.Enabled = false;

			btnBack.Enabled = false;
			btnNext.Enabled = false;
		}

		/// <summary>
		/// Called if the browser has finished downloading content
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event parameter</param>
		private void axWebBrowser1_DownloadComplete(object sender, System.EventArgs e)
		{
			btnStop.Enabled = false;

			btnHome.Enabled = true;
		}

        private string _FileName = "";
        /// <summary>
        /// 
        /// </summary>
        /// 
        //     要启动的应用程序的名称或某文件类型的文档的名称，该文件类型与应用程序关联并且拥有可用的默认打开操作。默认值为空字符串 ("")。
        [DefaultValue("")]
        [Editor("System.Diagnostics.Design.StartFileNameEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design")]
        public string FileName
        {
            get { return this._FileName; }
            set {
                this._FileName = value;
                if (System.IO.File.Exists(this._FileName))
                {
                    this.Cursor = Cursors.WaitCursor;

                    try
                    {
                        // clear current items
                        tocTree1.ClearContents();
                        helpIndex1.ClearContents();
                        helpSearch2.ClearContents();

                        // open the chm-file selected in the OpenFileDialog
                        _reader.OpenFile(this._FileName, _dmpInfo);

                        // Enable the toc-tree pane if the opened file has a table of contents
                        tocTree1.Enabled = _reader.HasTableOfContents;
                        // Enable the index pane if the opened file has an index
                        helpIndex1.Enabled = _reader.HasIndex;
                        // Enable the full-text search pane if the opened file supports full-text searching
                        helpSearch2.Enabled = _reader.FullTextSearch;

                        btnContents.Enabled = _reader.HasTableOfContents;
                        btnIndex.Enabled = _reader.HasIndex;
                        btnSearch.Enabled = _reader.FullTextSearch;

                        btnSynch.Enabled = _reader.HasTableOfContents;

                        tabControl1.SelectedIndex = 0;

                        btnRefresh.Enabled = true;
                        if (_reader.DefaultTopic.Length > 0)
                        {
                            btnHome.Enabled = true;
                        }

                        // Build the table of contents tree view in the classlibrary control
                        tocTree1.BuildTOC(_reader.TableOfContents, _filter);

                        // Build the index entries in the classlibrary control
                        if (_reader.HasKLinks)
                            helpIndex1.BuildIndex(_reader.Index, IndexType.KeywordLinks, _filter);
                        else if (_reader.HasALinks)
                            helpIndex1.BuildIndex(_reader.Index, IndexType.AssiciativeLinks, _filter);

                        // Navigate the embedded browser to the default help topic
                        NavigateBrowser(_reader.DefaultTopic);


                        this.Text = _reader.FileList[0].FileInfo.HelpWindowTitle + " - HtmlHelp - Viewer";


                        // Force garbage collection to free memory
                        GC.Collect();
                    }
                    finally
                    {
                        this.Cursor = Cursors.Arrow;
                    }

                    this.Cursor = Cursors.Arrow;
                }
            }
        }

		/// <summary>
		/// Called if the user clicks a toolbar button
		/// </summary>
		/// <param name="sender">sender of the event</param>
		/// <param name="e">event parameter</param>
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if( e.Button == btnContents ) // show help contents clicked
			{
				tabControl1.SelectedIndex = 0;
				tocTree1.Focus();
			}

			if( e.Button == btnIndex ) // show help index clicked
			{
				tabControl1.SelectedIndex = 1;
				helpIndex1.Focus();
			}

			if( e.Button == btnSearch ) // show help search clicked
			{
				tabControl1.SelectedIndex = 2;
				helpSearch2.Focus();
			}

			if( e.Button == btnRefresh ) // refresh clicked
			{
				try
				{
					axWebBrowser1.Refresh();
				}
				catch(Exception)
				{
				}
			}

			if( e.Button == btnNext ) // next/forward clicked
			{
				try
				{
					axWebBrowser1.GoForward();
				}
				catch(Exception)
				{
				}
			}

			if( e.Button == btnBack ) // back clicked
			{
				try
				{
					axWebBrowser1.GoBack();
				}
				catch(Exception)
				{
				}
			}

			if( e.Button == btnHome ) // home clicked
			{
				NavigateBrowser( _reader.DefaultTopic );
			}

			if( e.Button == btnStop ) // stop clicked
			{
				try
				{
					axWebBrowser1.Stop();
				}
				catch(Exception)
				{
				}
			}

			if( e.Button == btnSynch )
			{
				this.Cursor = Cursors.WaitCursor;
			 	tocTree1.Synchronize(axWebBrowser1.LocationURL);
				this.Cursor = Cursors.Arrow;
			}
		}

	}
    public class AppFilenameEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
            //return base.GetEditStyle(context);
        }
        //public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        //{
        //    IWindowsFormsEditorService wfes = provider.GetService(
        //        typeof(IWindowsFormsEditorService)) as
        //        IWindowsFormsEditorService;

        //    if (wfes != null)
        //    {
        //        OpenFileDialog fileDlg = new OpenFileDialog();
        //        fileDlg.Filter = "可执行程序 (*.exe)|*.exe";
        //        fileDlg.Multiselect = false;
        //        if (fileDlg.ShowDialog() == DialogResult.OK)
        //        {
        //            return fileDlg.FileName;
        //        }
        //        else
        //        {
        //            return string.Empty;
        //        }
        //    }
        //    return value;
        //    //return base.EditValue(context, provider, value);
        //}
    }
}
