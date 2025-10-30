using System;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class ContactForm : Form
    {
        #region Fields
        private TextBox _txtId;
        private TextBox _txtName;
        private TextBox _txtPhone;
        private TextBox _txtAddress;
        private DataGridView _dgvContacts;
        private Button _btnCreate;
        private Button _btnUpdate;
        private Button _btnDelete;
        private Button _btnClear;
        #endregion
        #region Constructor
        public ContactForm()
        {
            InitializeComponent();
        } 
        #endregion
        #region UtilityMethods
        private void ClearForm()
        {
            _txtId.Clear();
            _txtName.Clear();
            _txtPhone.Clear();
            _txtAddress.Clear();
        } 
        #endregion
        #region Events
        private void BtnCreate_Click(object sender, EventArgs e) 
        {
        }
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        #endregion
    }
}
