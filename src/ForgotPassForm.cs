using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Chess
{
    public partial class ForgotPassForm : Form
    {
        private bool ShowPass = false, ShowPass1 = false;
        private const int maxPassLength = 40, maxEmailLength = 100;

        private Color formColor = Color.FromArgb(224, 204, 164),
            designColor = Color.FromArgb(82, 59, 38),
            buttonForeColor = Color.FromArgb(122, 90, 60),
            watermarkColor = Color.FromArgb(148, 121, 96),
            errorColor = Color.FromArgb(128, 41, 33);

        private string connStr = "Server=paul;Database=Chess;User Id=Paul;Password=Paul1234;";
        private Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public ForgotPassForm()
        {
            InitializeComponent();
            StyleForm();
        }

        private void StyleForm()
        {
            this.BackColor = formColor;

            lblForgotPass.ForeColor = designColor;
            lblErrorEmail.ForeColor = errorColor;
            lblErrorOldPass.ForeColor = errorColor;
            lblErrorNewPass.ForeColor = errorColor;
            lblErrorConfirmPass.ForeColor = errorColor;

            pnForgotPass.BackColor = designColor;
            pnForgotPass1.BackColor = designColor;
            pnForgotPass2.BackColor = designColor;
            pnForgetPass3.BackColor = designColor;

            StyleTextBoxes();
            StyleButtons();
        }

        private void StyleTextBoxes()
        {
            txtEmail.MaxLength = maxEmailLength;
            txtEmail.BackColor = formColor;
            txtEmail.ForeColor = watermarkColor;
            txtEmail.Text = "Email";

            txtOldPass.MaxLength = maxPassLength;
            txtOldPass.BackColor = formColor;
            txtOldPass.ForeColor = watermarkColor;
            txtOldPass.Text = "Old Password";

            txtNewPass.MaxLength = maxPassLength;
            txtNewPass.BackColor = formColor;
            txtNewPass.ForeColor = watermarkColor;
            txtNewPass.Text = "New Password";

            txtConfirmPass.MaxLength = maxPassLength;
            txtConfirmPass.BackColor = formColor;
            txtConfirmPass.ForeColor = watermarkColor;
            txtConfirmPass.Text = "Confirm Password";

            txtEmail.GotFocus += (sender, e) =>
            {
                if (txtEmail.ForeColor == watermarkColor)
                {
                    txtEmail.Clear();
                    txtEmail.ForeColor = designColor;
                }
            };

            txtEmail.LostFocus += (sender, e) =>
            {
                if (txtEmail.Text == "")
                {
                    txtEmail.ForeColor = watermarkColor;
                    txtEmail.Text = "Email";
                }
            };

            txtEmail.TextChanged += (sender, e) =>
            {
                if (txtEmail.TextLength == maxEmailLength)
                    lblErrorEmail.Text = "The email is too long!";
                else
                    lblErrorEmail.Text = "";

            };

            txtOldPass.GotFocus += (sender, e) =>
            {
                if (txtOldPass.ForeColor == watermarkColor)
                {
                    txtOldPass.Clear();
                    txtOldPass.ForeColor = designColor;

                    if (ShowPass1 == false) txtOldPass.PasswordChar = '*';
                }
            };

            txtOldPass.TextChanged += (sender, e) =>
            {
                if (txtOldPass.TextLength == maxPassLength)
                    lblErrorOldPass.Text = "The password is too long!";
                else
                    lblErrorOldPass.Text = "";
            };

            txtOldPass.LostFocus += (sender, e) =>
            {
                if (txtOldPass.Text == "" && btnShowHide1.ContainsFocus == false)
                {
                    txtOldPass.ForeColor = watermarkColor;
                    txtOldPass.PasswordChar = '\0';
                    txtOldPass.Text = "Old Password";
                }

                if(btnShowHide1.ContainsFocus == true)
                    txtOldPass.Focus();
            };

            txtNewPass.GotFocus += (sender, e) =>
            {
                if (txtNewPass.ForeColor == watermarkColor)
                {
                    txtNewPass.Clear();
                    txtNewPass.ForeColor = designColor;

                    if (ShowPass == false) txtNewPass.PasswordChar = '*';
                }
            };

            txtNewPass.TextChanged += (sender, e) =>
            {
                if (txtNewPass.TextLength == maxPassLength)
                    lblErrorNewPass.Text = "The password is too long!";
                else
                    lblErrorNewPass.Text = "";
            };

            txtNewPass.LostFocus += (sender, e) =>
            {
                if (txtNewPass.Text == "" && btnShowHide.ContainsFocus == false)
                {
                    txtNewPass.ForeColor = watermarkColor;
                    txtNewPass.PasswordChar = '\0';
                    txtNewPass.Text = "New Password";
                }

                if (btnShowHide.ContainsFocus == true)
                    txtNewPass.Focus();
            };

            txtConfirmPass.GotFocus += (sender, e) =>
            {
                if (txtConfirmPass.ForeColor == watermarkColor)
                {
                    txtConfirmPass.Clear();
                    txtConfirmPass.PasswordChar = '*';
                    txtConfirmPass.ForeColor = designColor;
                }
            };

            txtConfirmPass.LostFocus += (sender, e) =>
            {
                if (txtConfirmPass.Text == "")
                {
                    txtConfirmPass.ForeColor = watermarkColor;
                    txtConfirmPass.PasswordChar = '\0';
                    txtConfirmPass.Text = "Confirm Password";
                }
            };

            txtConfirmPass.TextChanged += (sender, e) =>
            {
                if (txtConfirmPass.TextLength == maxPassLength)
                    lblErrorConfirmPass.Text = "The password is too long!";
                else
                    lblErrorConfirmPass.Text = "";
            };
        }

        private void StyleButtons()
        {
            btnChangePass.ForeColor = buttonForeColor;
            btnChangePass.BackColor = formColor;

            btnBack.BackColor = formColor;
            btnBack.ForeColor = buttonForeColor;

            btnChangePass.FlatAppearance.MouseDownBackColor = formColor;
            btnChangePass.FlatAppearance.MouseOverBackColor = formColor;

            btnBack.FlatAppearance.MouseDownBackColor = formColor;
            btnBack.FlatAppearance.MouseOverBackColor = formColor;

            btnShowHide.FlatAppearance.MouseDownBackColor = formColor;
            btnShowHide.FlatAppearance.MouseOverBackColor = formColor;
            
            btnShowHide1.FlatAppearance.MouseDownBackColor = formColor;
            btnShowHide1.FlatAppearance.MouseOverBackColor = formColor;


            btnChangePass.MouseEnter += (sender, e) =>
            {
                btnChangePass.ForeColor = designColor;
            };

            btnChangePass.MouseLeave += (sender, e) =>
            {
                btnChangePass.ForeColor = buttonForeColor;
            };


            btnBack.MouseEnter += (sender, e) =>
            {
                btnBack.ForeColor = designColor;
            };

            btnBack.MouseLeave += (sender, e) =>
            {
                btnBack.ForeColor = buttonForeColor;
            };

            btnShowHide.MouseClick += (sender, e) =>
            {
                if (!ShowPass)
                {
                    btnShowHide.BackgroundImage = ChessGame.Properties.Resources.ShowPass;
                    ShowPass = true;
                    txtNewPass.PasswordChar = '\0';
                }
                else
                {
                    btnShowHide.BackgroundImage = ChessGame.Properties.Resources.HidePass;
                    ShowPass = false;

                    if (txtNewPass.ForeColor != watermarkColor)
                        txtNewPass.PasswordChar = '*';
                }
            };

            btnShowHide1.MouseClick += (sender, e) =>
            {
                if (!ShowPass1)
                {
                    btnShowHide1.BackgroundImage = ChessGame.Properties.Resources.ShowPass;
                    ShowPass1 = true;
                    txtOldPass.PasswordChar = '\0';
                }
                else
                {
                    btnShowHide1.BackgroundImage = ChessGame.Properties.Resources.HidePass;
                    ShowPass1 = false;

                    if (txtOldPass.ForeColor != watermarkColor)
                        txtOldPass.PasswordChar = '*';
                }
            };

            btnBack.MouseClick += (sender, e) =>
            {
                this.Close();
            };
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            lblErrorConfirmPass.Text = lblErrorOldPass.Text = lblErrorEmail.Text = "";

            if (txtEmail.ForeColor == watermarkColor)
                lblErrorEmail.Text = "Please enter an email address!";
            else
            {
                if (!emailRegex.IsMatch(txtEmail.Text))
                    lblErrorEmail.Text = "Wrong email format!";
                else
                {
                    if (!ExistingEmail())
                        lblErrorEmail.Text = "Email not found!";
                    else
                    {
                        if (txtOldPass.ForeColor == watermarkColor)
                            lblErrorOldPass.Text = "Please enter your old password first!";
                        else
                        {
                            if (!ValidOldPass())
                            {
                                txtOldPass.Clear();
                                txtOldPass.Focus();
                                lblErrorOldPass.Text = "The old password is not correct!";
                            }
                            else
                            {
                                if (txtNewPass.ForeColor == watermarkColor)
                                    lblErrorNewPass.Text = "Please enter a new password first!";
                                else
                                {
                                    if (txtConfirmPass.ForeColor == watermarkColor)
                                        lblErrorConfirmPass.Text = "Confirm your password!";
                                    else
                                    {
                                        if (txtConfirmPass.Text != txtNewPass.Text)
                                        {
                                            txtConfirmPass.Clear();
                                            txtConfirmPass.Focus();
                                            lblErrorConfirmPass.Text = "Incorrect password!";
                                        }
                                        else
                                        {
                                            ChangePassword();
                                            this.Close();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool ValidOldPass()
        {
            string oldPass = txtOldPass.Text;
            using (var con = new SqlConnection(connStr))
            {
                con.Open();
                string query = "SELECT * FROM Users WHERE Password = @password";

                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@password", oldPass);
                    if (cmd.ExecuteReader().HasRows)
                        return true;
                }
            }

            return false;
        }

        private void ChangePassword()
        {
            string email = txtEmail.Text;
            string pass = txtNewPass.Text;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE Users SET Password = @pass WHERE Email = @email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    cmd.ExecuteNonQuery();
                };
            }
        }

        private bool ExistingEmail()
        {
            string checkEmail = txtEmail.Text; ;

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Email = @email";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", checkEmail);
                    if (cmd.ExecuteReader().HasRows)
                        return true;
                }
            }

            return false;
        }
    }
}
