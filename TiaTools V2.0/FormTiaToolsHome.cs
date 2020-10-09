using System;
using System.Windows.Forms;
using CustomUtility_NET_F;

namespace TiaToolsV2_0
{
    public partial class FormToolsHome : Form
    {

        #region Constructor

        public FormToolsHome()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties


        #endregion

        #region Events

        private void buttonIO_Click(object sender, EventArgs e)
        {
            ChildForm.OpenChildFormOnPanel(new FormIO(), panelChildForm);
        }

        private void buttonMsq_Click(object sender, EventArgs e)
        {
            ChildForm.OpenChildFormOnPanel(new FormMessages(), panelChildForm);
        }

        private void buttonSM_Click(object sender, EventArgs e)
        {
            ChildForm.OpenChildFormOnPanel(new FormStateMachines(), panelChildForm);
        }

        #endregion

    }
}
