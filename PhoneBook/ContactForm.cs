using PhoneBook.BL;
using PhoneBook.BL.IRepositories;
using PhoneBook.BL.Services;
using PhoneBook.BL.Utilities;
using PhoneBook.DA.Repositories;
using System;
using System.Drawing;
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
        private ContactService _contactService;
        #endregion
        #region Constructor
        public ContactForm()
        {
            // ایجاد وابستگی‌ها
            IContactRepository repository = new ContactRepository();
            _contactService = new ContactService(repository);

            InitializeComponents();
            LoadContacts();
        }
        #endregion
        #region UtilityMethods
        private void InitializeComponents()
        {
            this.Text = "دفترچه تلفن - معماری سه لایه";
            this.Size = new Size(800, 600);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;

            // شناسه
            var lblId = new Label { Text = "شناسه:", Location = new Point(650, 20), AutoSize = true };
            _txtId = new TextBox { Location = new Point(500, 17), 
                Width = 130, ReadOnly = true, BackColor = Color.LightGray };

            // نام
            var lblName = new Label { Text = "نام:", Location = new Point(650, 50), AutoSize = true };
            _txtName = new TextBox { Location = new Point(500, 47), Width = 130 };

            // تلفن
            var lblPhone = new Label { Text = "تلفن:", Location = new Point(650, 80), AutoSize = true };
            _txtPhone = new TextBox { Location = new Point(500, 77), Width = 130 };

            // آدرس
            var lblAddress = new Label { Text = "آدرس:", Location = new Point(650, 110), AutoSize = true };
            _txtAddress = new TextBox { Location = new Point(500, 107), 
                Width = 130, Height = 60, Multiline = true };

            // دکمه‌ها
            _btnCreate = new Button { Text = "افزودن", Location = new Point(650, 180), Width = 80 };
            _btnUpdate = new Button { Text = "ویرایش", Location = new Point(560, 180), Width = 80 };
            _btnDelete = new Button { Text = "حذف", Location = new Point(470, 180), Width = 80 };
            _btnClear = new Button { Text = "پاک کردن", Location = new Point(380, 180), Width = 80 };

            _btnCreate.Click += (s, e) => CreateContact();
            _btnUpdate.Click += (s, e) => UpdateContact();
            _btnDelete.Click += (s, e) => DeleteContact();
            _btnClear.Click += (s, e) => ClearForm();

            // جدول
            _dgvContacts = new DataGridView
            {
                Location = new Point(20, 250),
                Size = new Size(790, 300),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoGenerateColumns = true
            };
           

            // ستون CheckBox برای انتخاب - اولین ستون
            var checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "IsSelected",
                HeaderText = "انتخاب",
                DataPropertyName = "IsSelected",
                Width = 60,
                ReadOnly = false  
            };
            _dgvContacts.Columns.Add(checkBoxColumn);
            _dgvContacts.CellClick += OnContactSelected;
            // افزودن کنترل‌ها به فرم
            this.Controls.AddRange(new Control[]
            {
                    lblId, _txtId, lblName, _txtName, lblPhone, _txtPhone,
                    lblAddress, _txtAddress, _btnCreate, _btnUpdate, _btnDelete,
                    _btnClear, _dgvContacts
            });
        }
        private void CreateContact()
        {
            var dto = GetContactFromForm();
            var result = _contactService.CreateContact(dto);

            ShowTlsResult(result);

            if (result.IsSuccess)
            {
                LoadContacts();
                ClearForm();
            }
        }

        private void UpdateContact()
        {
            var dto = GetContactFromForm();
            var result = _contactService.UpdateContact(dto);

            ShowTlsResult(result);

            if (result.IsSuccess)
            {
                LoadContacts();
                ClearForm();
            }
        }

        private void DeleteContact()
        {
            if (string.IsNullOrWhiteSpace(_txtId.Text))
            {
                ShowMessage("لطفا یک مخاطب را انتخاب کنید", MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                "آیا از حذف این مخاطب اطمینان دارید؟",
                "تأیید حذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult != DialogResult.Yes)
                return;

            int id = int.Parse(_txtId.Text);
            var result = _contactService.DeleteContact(id);

            ShowTlsResult(result);

            if (result.IsSuccess)
            {
                LoadContacts();
                ClearForm();
            }
        }

        private void LoadContacts()
        {
            try
            {
                var contacts = _contactService.GetAllContacts();
                _dgvContacts.DataSource = contacts;

                if (_dgvContacts.Columns.Count > 0)
                {
                    _dgvContacts.Columns["Id"].HeaderText = "شناسه";
                    _dgvContacts.Columns["Name"].HeaderText = "نام";
                    _dgvContacts.Columns["Phone"].HeaderText = "تلفن";
                    _dgvContacts.Columns["Address"].HeaderText = "آدرس";
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"خطا در بارگذاری مخاطبین: {ex.Message}", MessageBoxIcon.Error);
            }
        }

        private void OnContactSelected(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var allRows = _dgvContacts.Rows;
            var row = allRows[e.RowIndex];
            var contact = row.DataBoundItem as ContactDto;

            if (contact == null) return;

            // اگر روی ستون IsSelected کلیک شد
            if (e.ColumnIndex >= 0 && _dgvContacts.Columns[e.ColumnIndex].Name == "IsSelected")
            {
                // ابتدا همه رکوردها را Unselect کن
                foreach (DataGridViewRow c in allRows)
                {
                    (c.DataBoundItem as ContactDto).IsSelected = false;
                }

                // فقط رکورد کلیک شده را Select کن
                contact.IsSelected = true;
                _dgvContacts.Refresh();
            }

            if (contact != null)
            {
                _txtId.Text = contact.Id.ToString();
                _txtName.Text = contact.Name;
                _txtPhone.Text = contact.Phone;
                _txtAddress.Text = contact.Address;
            }
        }

        private void ClearForm()
        {
            _txtId.Clear();
            _txtName.Clear();
            _txtPhone.Clear();
            _txtAddress.Clear();
        }

        private ContactDto GetContactFromForm()
        {
            return new ContactDto
            {
                Id = string.IsNullOrWhiteSpace(_txtId.Text) ? 0 : int.Parse(_txtId.Text),
                Name = _txtName.Text,
                Phone = _txtPhone.Text,
                Address = _txtAddress.Text,
                IsSelected = false
            };
        }

        private void ShowTlsResult(TlsResult result)
        {
            var icon = result.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning;
            ShowMessage(result.Message, icon);
        }

        private void ShowMessage(string message, MessageBoxIcon icon)
        {
            MessageBox.Show(message, "پیام", MessageBoxButtons.OK, icon);
        }
        #endregion
    }
}