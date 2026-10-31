using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=LoginForm;Integrated Security=True;Encrypt=False;");



        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textEdit1.Text) || string.IsNullOrWhiteSpace(textEdit2.Text))
            {
                MessageBox.Show("الرجاء إدخال اسم المستخدم وكلمة المرور", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            con.Open();


            string query = "SELECT COUNT(*) FROM Users WHERE Username = @user AND Password = @pass";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@user", textEdit1.Text.Trim());
            cmd.Parameters.AddWithValue("@pass", textEdit2.Text.Trim());

            // تنفيذ الاستعلام وجلب النتيجة
            int count = Convert.ToInt32(cmd.ExecuteScalar());

            con.Close();

            if (count > 0)
            {
                MessageBox.Show("تم تسجيل الدخول بنجاح!", "مرحباً", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HomeForm frm = new HomeForm();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة!", "خطأ في تسجيل الدخول", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
