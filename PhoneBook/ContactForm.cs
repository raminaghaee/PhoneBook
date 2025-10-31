using System;
using System.Linq;
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
            LoadContacts();
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
        private void LoadContacts()
        {
            try
            {
                using (var context = new PhoneBookContext())
                {
                    var contacts = context.Contacts.ToList();
                    _dgvContacts.DataSource = contacts;

                    // تنظیم عناوین ستون‌ها
                    if (_dgvContacts.Columns.Count > 0)
                    {
                        _dgvContacts.Columns["Id"].HeaderText = "شناسه";
                        _dgvContacts.Columns["Name"].HeaderText = "نام";
                        _dgvContacts.Columns["Phone"].HeaderText = "تلفن";
                        _dgvContacts.Columns["Address"].HeaderText = "آدرس";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری مخاطبین: {ex.Message}", 
                    "خطا", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
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
            if (string.IsNullOrWhiteSpace(_txtId.Text))
            {
                MessageBox.Show("لطفا یک مخاطب را انتخاب کنید", "خطا", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(_txtName.Text))
            {
                MessageBox.Show("لطفا نام را وارد کنید", "خطای اعتبارسنجی", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(_txtPhone.Text))
            {
                MessageBox.Show("لطفا شماره تلفن را وارد کنید", 
                    "خطای اعتبارسنجی", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new PhoneBookContext())
                {
                    int id = int.Parse(_txtId.Text);
                    var contact = context.Contacts.Find(id);

                    if (contact != null)
                    {
                        contact.Name = _txtName.Text.Trim();
                        contact.Phone = _txtPhone.Text.Trim();
                        contact.Address = _txtAddress.Text.Trim();

                        context.SaveChanges();

                        MessageBox.Show("مخاطب با موفقیت ویرایش شد", 
                            "موفقیت", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                        LoadContacts();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("مخاطب یافت نشد", "خطا", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ویرایش مخاطب: {ex.Message}", 
                    "خطا", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void DgvContacts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = _dgvContacts.Rows[e.RowIndex];
                var contact = row.DataBoundItem as Contact;

                if (contact != null)
                {
                    _txtId.Text = contact.Id.ToString();
                    _txtName.Text = contact.Name;
                    _txtPhone.Text = contact.Phone;
                    _txtAddress.Text = contact.Address ?? "";
                }
            }
        }
        #endregion
    }
}