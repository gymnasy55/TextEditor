using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NoteTest
{
    public partial class AddFontForm : Form
    {
        public AddFontForm()
        {
            InitializeComponent();
        }

        public void btnOK_Click(object sender, EventArgs e)
        {
            MessageService service = new MessageService();
            try
            {
                string g = txtFont.Text;
            }
            catch(Exception ex)
            {
                service.ShowError(ex.Message);
            }

        }
    }
}
