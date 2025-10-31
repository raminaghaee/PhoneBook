using System;
using System.Windows.Forms;
using System.Xml.Linq;

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
            if (string.IsNullOrWhiteSpace(_txtName.Text))
            {
                MessageBox.Show("لطفا نام را وارد کنید", 
                    "خطای اعتبارسنجی", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(_txtPhone.Text))
            {
                MessageBox.Show("لطفا شماره تلفن را وارد کنید", 
                    "خطای اعتبارسنجی", MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (var context = new PhoneBookContext())
                {
                    var contact = new Contact
                    {
                        Name = _txtName.Text.Trim(),
                        Phone = _txtPhone.Text.Trim(),
                        Address = _txtAddress.Text.Trim()
                    };

                    context.Contacts.Add(contact);
                    context.SaveChanges();

                    MessageBox.Show("مخاطب با موفقیت اضافه شد", "موفقیت", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);

                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در افزودن مخاطب: {ex.Message}", 
                    "خطا", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
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
