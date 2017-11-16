using System;
using System.Drawing;
using System.Windows.Forms;

namespace NoteTest
{
    public interface IMainForm
    {
        string FilePath { get; }
        string Content { get; set; }
        void SetSymbolCount(int count);
        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
    }

    public partial class MainForm : Form, IMainForm
    {
        FontStyle b;
        public MainForm()
        {
            InitializeComponent();
            btnOpenFile.Click += BtnOpenFile_Click;
            btnSaveFile.Click += BtnSaveFile_Click;
            fldContent.TextChanged += fldContent_TextChanged;
            btnSelectFile.Click += BtnSelectFile_Click;
            numFont.ValueChanged += NumFont_ValueChanged;
            fldContent.Font = new Font("Calibri", 6);
            b = FontStyle.Regular;
        }

        #region Проброс событий
        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
        }

        private void BtnSaveFile_Click(object sender, EventArgs e)
        {
            if (FileSaveClick != null) FileSaveClick(this, EventArgs.Empty);
        }

        private void fldContent_TextChanged(object sender, EventArgs e)
        {
            if (ContentChanged != null) ContentChanged(this, EventArgs.Empty);
        }
        #endregion

        #region IMainForm
        public string FilePath
        {
            get { return fldFilePath.Text; }
        }

        public string Content
        {
            get { return fldContent.Text; }
            set { fldContent.Text = value; }
        }

        public void SetSymbolCount(int count)
        {
            lblSymbolCount.Text = count.ToString();
        }

        public event EventHandler FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler ContentChanged;
        #endregion

        public void BtnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Текстовый файлы|*.txt|Файлы кода C#|*.cs|Файлы кода C++|*.cpp|Файлы кода Pascal|*.pas|Все файлы|*.*";
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                fldFilePath.Text = dlg.FileName;
                if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
            }
        }

        public void NumFont_ValueChanged(object sender, EventArgs e)
        {
            fldContent.Font = new Font((string)fontBox.SelectedItem, (float)numFont.Value);
        }

        public void wrapBox_CheckedChanged(object sender, EventArgs e)
        {
            if(wrapBox.Checked == false) { fldContent.WordWrap = false; }
            else { fldContent.WordWrap = true; }    
        }

        private void boldBox_CheckedChanged(object sender, EventArgs e)
        {
            if(boldBox.Checked == false)
            {
                try
                {
                    fldContent.Font = new Font((string)fontBox.SelectedItem, (float)numFont.Value, FontStyle.Regular);
                    b = FontStyle.Regular;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    fldContent.Font = new Font((string)fontBox.SelectedItem, (float)numFont.Value, FontStyle.Bold);
                    b = FontStyle.Bold;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void fontBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fldContent.Font = new Font((string)fontBox.SelectedItem, (float)numFont.Value, b);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void btnAddFont_Click(object sender, EventArgs e)
        {
            AddFontForm form = new AddFontForm();
            form.ShowDialog();
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Текстовый файлы|*.txt|Все файлы|*.*";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnAddFont.Enabled = false;
            btnCreateFile.Enabled = false;
            fontBox.SelectedItem = "Calibri";
        }
    }
}