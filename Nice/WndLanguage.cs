using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nice
{
    public partial class LocaleName : Form
    {
        /***************************************************************/
        // 
        Feature g_e = new Feature();
        /***************************************************************/

        public LocaleName()
        {
            InitializeComponent();
            InitText();
        }

        public void InitText()
        {
            RegShow.Text = g_e.GetReg();
        }

        private void Return_Click(object sender, EventArgs e)
        {
            MainPage mainPage = new MainPage();
            //Background background = new Background();

            LocaleName.ActiveForm.Close(); // 利用窗口Name来关闭窗口

            //background.Show(); // 主页窗口背景
            mainPage.Show(); // 主页窗口
        }

        private void RefreshRegShow()
        {
            RegShow.Text = g_e.GetReg();
        }

        private void SA_Click(object sender, EventArgs e)
        {
            g_e.SetReg(SA.Text);
            RefreshRegShow();
        }

        private void CZ_Click(object sender, EventArgs e)
        {
            g_e.SetReg(CZ.Text);
            RefreshRegShow();
        }

        private void DK_Click(object sender, EventArgs e)
        {
            g_e.SetReg(DK.Text);
            RefreshRegShow();
        }

        private void DE_Click(object sender, EventArgs e)
        {
            g_e.SetReg(DE.Text);
            RefreshRegShow();
        }

        private void en_GR_Click(object sender, EventArgs e)
        {
            g_e.SetReg(en_GR.Text);
            RefreshRegShow();
        }

        private void US_Click(object sender, EventArgs e)
        {
            g_e.SetReg(US.Text);
            RefreshRegShow();
        }

        private void GR_Click(object sender, EventArgs e)
        {
            g_e.SetReg(GR.Text);
            RefreshRegShow();
        }

        private void TR_Click(object sender, EventArgs e)
        {
            g_e.SetReg(TR.Text);
            RefreshRegShow();
        }

        private void TH_Click(object sender, EventArgs e)
        {
            g_e.SetReg(TH.Text);
            RefreshRegShow();
        }

        private void SV_Click(object sender, EventArgs e)
        {
            g_e.SetReg(SV.Text);
            RefreshRegShow();
        }

        private void RU_Click(object sender, EventArgs e)
        {
            g_e.SetReg(RU.Text);
            RefreshRegShow();
        }

        private void PT_Click(object sender, EventArgs e)
        {
            g_e.SetReg(PT.Text);
            RefreshRegShow();
        }

        private void PL_Click(object sender, EventArgs e)
        {
            g_e.SetReg(PL.Text);
            RefreshRegShow();
        }

        private void NO_Click(object sender, EventArgs e)
        {
            g_e.SetReg(NO.Text);
            RefreshRegShow();
        }

        private void NL_Click(object sender, EventArgs e)
        {
            g_e.SetReg(NL.Text);
            RefreshRegShow();
        }

        private void JP_Click(object sender, EventArgs e)
        {
            g_e.SetReg(JP.Text);
            RefreshRegShow();
        }

        private void KR_Click(object sender, EventArgs e)
        {
            g_e.SetReg(KR.Text);
            RefreshRegShow();
        }

        private void IT_Click(object sender, EventArgs e)
        {
            g_e.SetReg(IT.Text);
            RefreshRegShow();
        }

        private void FR_Click(object sender, EventArgs e)
        {
            g_e.SetReg(FR.Text);
            RefreshRegShow();
        }

        private void FI_Click(object sender, EventArgs e)
        {
            g_e.SetReg(FI.Text);
            RefreshRegShow();
        }

        private void MX_Click(object sender, EventArgs e)
        {
            g_e.SetReg(MX.Text);
            RefreshRegShow();
        }

        private void ES_Click(object sender, EventArgs e)
        {
            g_e.SetReg(ES.Text);
            RefreshRegShow();
        }

        private void CA_Click(object sender, EventArgs e)
        {
            g_e.SetReg(CA.Text);
            RefreshRegShow();
        }

        private void CN_Click(object sender, EventArgs e)
        {
            g_e.SetReg(CN.Text);
            RefreshRegShow();
        }

        private void RejectBtn(object sender, EventArgs e)
        {

        }
    }
}
