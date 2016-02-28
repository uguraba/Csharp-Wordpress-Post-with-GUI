#region Define Namespaces
using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using CookComputing.XmlRpc;
#endregion

namespace Wordpress_Post
{
    public partial class frmMain : Form
    {
        #region Constructor
        public frmMain()
        {
            InitializeComponent();
            cmbbxPing.SelectedIndex = 0;
            cmbbxComment.SelectedIndex = 0;
        }
        #endregion

        #region Wordpress
        public postInfo _blogPost;

        public struct postInfo
        {
            public string[] categories;
            public string title;
            public string description;
            public string mt_allow_comments;
            public string mt_keywords;
            public DateTime dateCreated;
            public string mt_allow_pings;
        }

        public interface IcreatePost
        {
            [XmlRpcMethod("metaWeblog.newPost")]
            string NewPost(int blogId, string strUserName, string strPassword, postInfo content, int publish);
        }

        public void setPost(string _title, string _content, string _comment, string _category, string _tag, DateTime _publishDate, string _ping)
        {
            _blogPost = default(postInfo);
            _blogPost.title = _title;
            _blogPost.description = _content;
            _blogPost.mt_allow_comments = _comment; //According to received string, comments are allowed or not.
            _blogPost.categories = _category.Split(','); //Categories must be split with "," also if category does not exist, it will not create.
            _blogPost.mt_keywords = _tag; //Tags are split with "," but it xml-rpc do this for us.
            _blogPost.dateCreated = _publishDate;
            _blogPost.mt_allow_pings = _ping; //According to received string, ping is allowed or not.
        }

        public string sendPost(string _url, string _username, string _password)
        {
            IcreatePost _post = (IcreatePost)XmlRpcProxyGen.Create(typeof(IcreatePost));
            XmlRpcClientProtocol clientProtocol = (XmlRpcClientProtocol)_post;
            clientProtocol.Url = "http://" + _url + "/xmlrpc.php";
            string _postID = "";
            try
            {
                _postID = _post.NewPost(1, _username, _password, _blogPost, 1);
            }
            catch (Exception ex)
            {
                _postID = "Error";
            }
            return _postID;
        }
        #endregion

        #region WordpressGUISectionButtons
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
        #endregion

        #region SendPostSectionComponents
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtContent.Clear();
            cmbbxPing.SelectedIndex = 0;
            cmbbxComment.SelectedIndex = 0;
            txtCategory.Clear();
            txtTag.Clear();
            dtp.Value = DateTime.Now;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (dgvWordpressInformationSettings.RowCount > 0)
            {
                if(!String.IsNullOrEmpty(txtTitle.Text) & !String.IsNullOrEmpty(txtContent.Text) & !String.IsNullOrEmpty(txtCategory.Text))
                { 
                    setPost(txtTitle.Text, txtContent.Text, cmbbxComment.Text, txtCategory.Text, txtTag.Text, dtp.Value, cmbbxPing.Text);
                    chcbxAgreement.Checked = false;
                    menuReport.PerformClick();
                    Application.DoEvents();
                    int _index = 0;
                    foreach (DataGridViewRow _row in dgvWordpressInformationSettings.Rows)
                    {
                        // This if condition is checking that url is selected or not (In or Out)
                        if(dgvSendPost.Rows[_index].Cells[0].Value.ToString() == "In")
                        { 
                            //I know without creating above three line, can do this but on that way the code is not readable.
                            string _url = _row.Cells["dgvSettingstxtURL"].Value.ToString();
                            string _username = _row.Cells["dgvSettingstxtUsername"].Value.ToString();
                            string _password = _row.Cells["dgvSettingstxtPassword"].Value.ToString();
                            dgvResult.Rows.Add();
                            dgvResult.Rows[dgvResult.RowCount - 1].Cells[0].Value = _url;
                            string _result = sendPost(_url, _username, _password);
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
                        _index++;
                    }
                }
                else
                {
                    MessageBox.Show("Title, content or category is missing.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("List is already empty.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPostBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog _fileDialog = new OpenFileDialog();
            _fileDialog.Filter = "Text File |*.txt";
            _fileDialog.Title = "Wordpress Post";
            if (_fileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader _read;
                _read = File.OpenText(_fileDialog.FileName);
                txtTitle.Text = _read.ReadLine(); //First line is title.
                txtCategory.Text = _read.ReadLine(); //Second line is category.
                txtTag.Text = _read.ReadLine(); //Third line is tag.
                cmbbxPing.Text = _read.ReadLine(); //Fourth line is ping. String will be only Open or Closed
                cmbbxComment.Text = _read.ReadLine(); //Fifth line is comment. String will be only Open or Closed
                dtp.Text = _read.ReadLine(); //Sixth line is publish date.
                string _content = "", _line;
                while ((_line = _read.ReadLine()) != null)
                {
                    _content += _line + Environment.NewLine; //Receiving content line by line.
                }
                txtContent.Text = _content;
                _read.Close();
            }
            else
            {
                MessageBox.Show("Didn't select any text file.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chcbxAgreement_CheckedChanged(object sender, EventArgs e)
        {
            if (chcbxAgreement.Checked)
                btnSend.Enabled = true;
            else
                btnSend.Enabled = false;
        }
        #endregion

        #region ReportSectionComponents
        private void btnStatistics_Click(object sender, EventArgs e)
        {
            if(dgvResult.RowCount > 0)
            { 
                int _success = 0, _failed = 0;
                foreach (DataGridViewRow _row in dgvResult.Rows)
                {
                    string _result = _row.Cells["dgvResulttxtResult"].Value.ToString();
                    if (_result == "Success.")
                        _success++;
                    else
                        _failed++;
                }
                MessageBox.Show(string.Format("Statistics for: {0}\nSuccess: {1}\nFailed: {2}", txtTitle.Text, _success.ToString(), _failed.ToString()), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Result list is already empty.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSaveToTxt_Click(object sender, EventArgs e)
        {
            if (dgvResult.RowCount > 0)
            {
                SaveFileDialog _saveDialog = new SaveFileDialog();
                _saveDialog.Filter = "Text File |*.txt";
                _saveDialog.OverwritePrompt = true;
                _saveDialog.CreatePrompt = true;
                if (_saveDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter _writer = new StreamWriter(_saveDialog.FileName);
                    string _line;
                    foreach (DataGridViewRow _row in dgvResult.Rows)
                    {
                        _line = _row.Cells["dgvResulttxtURL"].Value.ToString() + " " + _row.Cells["dgvResulttxtPostURL"].Value.ToString() + " " + _row.Cells["dgvResulttxtResult"].Value.ToString();
                        _writer.WriteLine(_line);
                    }
                    _writer.Close();
                }
            }
            else
            {
                MessageBox.Show("Result list is already empty.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            if(dgvResult.RowCount > 0)
            {
                string _mailBody = "";
                foreach (DataGridViewRow _row in dgvResult.Rows)
                {
                    string _url = _row.Cells["dgvResulttxtURL"].Value.ToString();
                    string _postUrl = _row.Cells["dgvResulttxtPostURL"].Value.ToString();
                    string _result = _row.Cells["dgvResulttxtResult"].Value.ToString();
                    _mailBody += _url + " " + _postUrl + " " + _result + "\n";
                }
                if ((!string.IsNullOrEmpty(txtSendMail.Text)))
                { 
                    try
                    { 
                        Mail.sendMail("csharpwordpresspost@gmail.com", "wordpressmailpassword", "C# Wordpress Application Report", txtSendMail.Text, _mailBody);
                        MessageBox.Show("Result list is sent your mail.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Result list is not sent your mail.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Email is missing.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Result list is already empty.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region AboutSectionButtons
        private void btnGNULicence_Click(object sender, EventArgs e)
        {
            openWebsite("http://www.gnu.org/licenses/gpl-3.0.en.html");
        }

        private void btnGithub_Click(object sender, EventArgs e)
        {
            openWebsite("http://www.github.com/uguraba");
        }

        private void btnTwitter_Click(object sender, EventArgs e)
        {
            openWebsite("http://www.twitter.com/uguraba");
        }
        #endregion

        #region openWebsite Function
        private void openWebsite(string _url)
        {
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command", false);
            Process.Start(((string)registryKey.GetValue(null, null)).Split('"')[1], _url);
        }
        #endregion

        #region Menu
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
            dgvSendPost.Rows.Clear();
            dgvSendPost.Refresh();
            foreach (DataGridViewRow _row in dgvWordpressInformationSettings.Rows)
            {
                dgvSendPost.Rows.Add();
                dgvSendPost.Rows[dgvSendPost.RowCount - 1].Cells[0].Value = "In";
                dgvSendPost.Rows[dgvSendPost.RowCount - 1].Cells[1].Value = _row.Cells["dgvSettingstxtURL"].Value;
            }
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

        private void menuSelect_Click(object sender, EventArgs e)
        {
            dgvSendPost.Rows[dgvSendPost.SelectedRows[0].Index].Cells[0].Value = "In";
        }

        private void menuSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvSendPost.RowCount; i++)
                dgvSendPost.Rows[i].Cells[0].Value = "In";
        }

        private void menuUnselect_Click(object sender, EventArgs e)
        {
            dgvSendPost.Rows[dgvSendPost.SelectedRows[0].Index].Cells[0].Value = "Out";
        }

        private void menuUnselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvSendPost.RowCount; i++)
                dgvSendPost.Rows[i].Cells[0].Value = "Out";
        }
        #endregion
    }
}