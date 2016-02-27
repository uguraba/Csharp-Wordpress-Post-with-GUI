using System;
using System.Windows.Forms;
using System.IO;

namespace Wordpress_Post
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            cmbbxPing.SelectedIndex = 0;
            cmbbxComment.SelectedIndex = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtContent.Clear();
            cmbbxPing.SelectedIndex = 0;
            cmbbxComment.SelectedIndex = 0;
            txtCategory.Clear();
            txtTag.Clear();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (dgvWordpressInformationSettings.RowCount > 0)
            {
                Wordpress _wordpress = new Wordpress();
                _wordpress.setPost(txtTitle.Text, txtContent.Text, cmbbxComment.Text, txtCategory.Text, txtTag.Text, dtp.Value, cmbbxPing.Text);
                chcbxAgreement.Checked = false;
                menuReport.PerformClick();
                Application.DoEvents();
                foreach (DataGridViewRow _row in dgvWordpressInformationSettings.Rows)
                {
                    //I know without creating above three line, can do this but on that way the code is not readable.
                    string _url = _row.Cells["dgvSettingstxtURL"].Value.ToString();
                    string _username = _row.Cells["dgvSettingstxtUsername"].Value.ToString();
                    string _password = _row.Cells["dgvSettingstxtPassword"].Value.ToString();
                    dgvResult.Rows.Add();
                    dgvResult.Rows[dgvResult.RowCount - 1].Cells[0].Value = _url;
                    string _result = _wordpress.sendPost(_url, _username, _password);
                    if (_result != "Error")
                    {
                        dgvResult.Rows[dgvResult.RowCount - 1].Cells[1].Value = "http://" + _url + "/?p=" + _result;
                        dgvResult.Rows[dgvResult.RowCount - 1].Cells[2].Value = "Success.";
                    }
                    else
                    {
                        dgvResult.Rows[dgvResult.RowCount - 1].Cells[2].Value = "Failed.";
                    }
                }
            }
            else
            {
                MessageBox.Show("List is already empty.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void menuWordpressInformation_Click(object sender, EventArgs e)
        {
            grpbxAbout.Visible = false;
            grpbxSendPost.Visible = false;
            grpbxReport.Visible = false;
            grpbxWordpressInformation.Visible = true;
        }

        private void menuSendPost_Click(object sender, EventArgs e)
        {
            grpbxAbout.Visible = false;
            grpbxWordpressInformation.Visible = false;
            grpbxReport.Visible = false;
            grpbxSendPost.Visible = true;
        }

        private void menuReport_Click(object sender, EventArgs e)
        {
            grpbxAbout.Visible = false;
            grpbxSendPost.Visible = false;
            grpbxWordpressInformation.Visible = false;
            grpbxReport.Visible = true;
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            grpbxSendPost.Visible = false;
            grpbxWordpressInformation.Visible = false;
            grpbxReport.Visible = false;
            grpbxAbout.Visible = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool found = false;
            foreach (DataGridViewRow _row in dgvWordpressInformationSettings.Rows)
            {
                if (_row.Cells["dgvSettingstxtURL"].Value.ToString() == txtURL.Text && _row.Cells["dgvSettingstxtUsername"].Value.ToString() == txtUsername.Text && _row.Cells["dgvSettingstxtPassword"].Value.ToString() == txtPassword.Text)
                {
                    found = true;
                    break;
                }
            }
            if(!found)
            {
                if (!String.IsNullOrEmpty(txtURL.Text) && !String.IsNullOrEmpty(txtUsername.Text) && !String.IsNullOrEmpty(txtPassword.Text))
                {
                    dgvWordpressInformationSettings.Rows.Add();
                    dgvWordpressInformationSettings.Rows[dgvWordpressInformationSettings.RowCount - 1].Cells[0].Value = txtURL.Text;
                    dgvWordpressInformationSettings.Rows[dgvWordpressInformationSettings.RowCount - 1].Cells[1].Value = txtUsername.Text;
                    dgvWordpressInformationSettings.Rows[dgvWordpressInformationSettings.RowCount - 1].Cells[2].Value = txtPassword.Text;
                }
                else
                {
                    MessageBox.Show("URL, Username or Password is missing.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Information is already in the list.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvWordpressInformationSettings.RowCount > 0)
            {
                dgvWordpressInformationSettings.Rows.RemoveAt(this.dgvWordpressInformationSettings.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("List is already empty.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBrowseWordpressList_Click(object sender, EventArgs e)
        {
            OpenFileDialog _fileDialog = new OpenFileDialog();
            _fileDialog.Filter = "Text File |*.txt";
            _fileDialog.Title = "Wordpress Post";
            if (_fileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader _read;
                _read = File.OpenText(_fileDialog.FileName);
                string _line;
                while ((_line = _read.ReadLine()) != null)
                {
                    string[] _lines = _line.Split(' ');
                    bool found = false;
                    foreach (DataGridViewRow _row in dgvWordpressInformationSettings.Rows)
                    {
                        if (_row.Cells["dgvSettingstxtURL"].Value.ToString() == _lines[0] && _row.Cells["dgvSettingstxtUsername"].Value.ToString() == _lines[1] && _row.Cells["dgvSettingstxtPassword"].Value.ToString() == _lines[2])
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        dgvWordpressInformationSettings.Rows.Add();
                        dgvWordpressInformationSettings.Rows[dgvWordpressInformationSettings.RowCount - 1].Cells[0].Value = _lines[0];
                        dgvWordpressInformationSettings.Rows[dgvWordpressInformationSettings.RowCount - 1].Cells[1].Value = _lines[1];
                        dgvWordpressInformationSettings.Rows[dgvWordpressInformationSettings.RowCount - 1].Cells[2].Value = _lines[2];
                    }
                }
                _read.Close();
            }
            else
            {
                MessageBox.Show("Didn't select any text file.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveWordpressList_Click(object sender, EventArgs e)
        {
            SaveFileDialog _saveDialog = new SaveFileDialog();
            _saveDialog.Filter = "Text File |*.txt";
            _saveDialog.OverwritePrompt = true;
            _saveDialog.CreatePrompt = true;
            if (_saveDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter _writer = new StreamWriter(_saveDialog.FileName);
                foreach (DataGridViewRow _row in dgvWordpressInformationSettings.Rows)
                {
                    _writer.WriteLine(_row.Cells["dgvSettingstxtURL"].Value.ToString() + " " + _row.Cells["dgvSettingstxtUsername"].Value.ToString() + " " + _row.Cells["dgvSettingstxtPassword"].Value.ToString());
                }
                _writer.Close();
            }
        }

        private void btnPostBrowse_Click(object sender, EventArgs e)
        {

        }

        private void chcbxAgreement_CheckedChanged(object sender, EventArgs e)
        {
            if (chcbxAgreement.Checked)
                btnSend.Enabled = true;
            else
                btnSend.Enabled = false;
        }

        private void btnShowDetail_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveToTxt_Click(object sender, EventArgs e)
        {

        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {

        }

        private void btnGoogleDrive_Click(object sender, EventArgs e)
        {

        }

        private void btnGNULicence_Click(object sender, EventArgs e)
        {

        }

        private void btnGithub_Click(object sender, EventArgs e)
        {

        }

        private void btnTwitter_Click(object sender, EventArgs e)
        {

        }
    }
}