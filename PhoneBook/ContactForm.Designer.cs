using System.Drawing;
using System.Windows.Forms;

namespace PhoneBook
{
    partial class ContactForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            //ContactForm
            this.Text = "دفترچه تلفن";
            this.Size = new Size(800, 600);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;

            // Labels and TextBoxes
            Label lblId = new Label { Text = "شناسه:", Location = new Point(650, 20), AutoSize = true };
            _txtId = new TextBox { Location = new Point(500, 17), Width = 130, ReadOnly = true, BackColor = Color.LightGray };

            Label lblName = new Label { Text = "نام:", Location = new Point(650, 50), AutoSize = true };
            _txtName = new TextBox { Location = new Point(500, 47), Width = 130 };

            Label lblPhone = new Label { Text = "تلفن:", Location = new Point(650, 80), AutoSize = true };
            _txtPhone = new TextBox { Location = new Point(500, 77), Width = 130 };

            Label lblAddress = new Label { Text = "آدرس:", Location = new Point(650, 110), AutoSize = true };
            _txtAddress = new TextBox { Location = new Point(500, 107), Width = 130, Height = 60, Multiline = true };

            // Buttons
            _btnCreate = new Button { Text = "افزودن", Location = new Point(650, 180), Width = 80 };
            _btnUpdate = new Button { Text = "ویرایش", Location = new Point(560, 180), Width = 80 };
            _btnDelete = new Button { Text = "حذف", Location = new Point(470, 180), Width = 80 };
            _btnClear = new Button { Text = "پاک کردن", Location = new Point(380, 180), Width = 80 };

            _btnCreate.Click += BtnCreate_Click;
            _btnUpdate.Click += BtnUpdate_Click;
            _btnDelete.Click += BtnDelete_Click;
            _btnClear.Click += BtnClear_Click;

            // DataGridView
            _dgvContacts = new DataGridView
            {
                Location = new Point(20, 220),
                Size = new Size(740, 320),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoGenerateColumns = true
            };
            _dgvContacts.CellClick += DgvContacts_CellClick;
            // Add controls
            this.Controls.AddRange(new Control[]
            {
                lblId, _txtId, lblName, _txtName, lblPhone, _txtPhone,
                lblAddress, _txtAddress, _btnCreate, _btnUpdate, _btnDelete,
                _btnClear, _dgvContacts
            });
        }
        #endregion
    }
}

