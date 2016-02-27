using System;
using System.Windows.Forms;
using CookComputing.XmlRpc;

namespace Wordpress_Post
{
    public partial class frmMain : Form
    {
        public XmlRpcClientProtocol clientProtocol;
        public IgetCatList categories;

        public struct blogInfo
        {
            public string title;
            public string description;
        }

        public interface IgetCatList
        {
            [XmlRpcMethod("metaWeblog.newPost")]
            string NewPage(int blogId, string strUserName, string strPassword, blogInfo content, int publish);
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            /*txtTitle.Clear();
            txtContent.Clear();*/
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
           /* blogInfo newBlogPost = default(blogInfo);
            newBlogPost.title = txtTitle.Text;
            newBlogPost.description = txtContent.Text;
            categories = (IgetCatList)XmlRpcProxyGen.Create(typeof(IgetCatList));
            clientProtocol = (XmlRpcClientProtocol)categories;
            clientProtocol.Url = "http://blog.uguraba.com/xmlrpc.php";
            string result = null;
            result = "";
            try
            {
                result = categories.NewPage(1, "uguraba", "BQJ8QTRJ7QRRW9VVHP9H", newBlogPost, 1);
                MessageBox.Show("Post başarıyla yollandı! Post ID : " + result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
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

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowseWordpressList_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveWordpressList_Click(object sender, EventArgs e)
        {

        }

        private void btnPostBrowse_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {

        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {

        }

        private void chcbxAgreement_CheckedChanged(object sender, EventArgs e)
        {

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