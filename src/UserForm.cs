using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Chess
{
    public partial class UserForm : Form
    {
        bool ShowPass = false;

        private const int maxPassLength = 40, maxEmailLength = 100;

        private Color formColor = Color.FromArgb(224, 204, 164),
            designColor = Color.FromArgb(82, 59, 38),
            buttonForeColor = Color.FromArgb(122, 90, 60),
            watermarkColor = Color.FromArgb(148, 121, 96),
            errorColor = Color.FromArgb(128, 41, 33);

        private string connStr = "Server=paul;Database=Chess;User Id=Paul;Password=Paul1234;";

        private Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public UserForm()
        {
            InitializeComponent();
            StyleForm();
        }

        private void StyleForm()
        {
            this.BackColor = formColor;

            lblLogIn.ForeColor = designColor;
            lblOr.ForeColor = designColor;
            lblErrorPass.ForeColor = errorColor;
            lblErrorEmail.ForeColor = errorColor;

            pnLogIn.BackColor = designColor;
            pnPassword.BackColor = designColor;
            pnLeft.BackColor = designColor;
            pnRight.BackColor = designColor;

            StyleTextBoxes();
            StyleButtons();
        }

        private void StyleTextBoxes()
        {
            txtEmail.Text = "Email";
            txtEmail.BackColor = formColor;
            txtEmail.ForeColor = watermarkColor;
            txtEmail.MaxLength = maxEmailLength;

            txtPassword.Text = "Password";
            txtPassword.BackColor = formColor;
            txtPassword.ForeColor = watermarkColor;
            txtPassword.MaxLength = maxPassLength;

            txtEmail.GotFocus += (sender, e) =>
            {
                if (txtEmail.ForeColor == watermarkColor)
                {
                    txtEmail.Clear();
                    txtEmail.ForeColor = designColor;
                }
            };

            txtEmail.TextChanged += (sender, e) =>
            {
                if (txtEmail.TextLength == maxEmailLength)
                    lblErrorEmail.Text = "The email is too long!";
                else
                    lblErrorEmail.Text = "";
            };

            txtEmail.LostFocus += (sender, e) =>
            {
                if (txtEmail.Text == "")
                {
                    txtEmail.ForeColor = watermarkColor;
                    txtEmail.Text = "Email";
                }
            };

            txtPassword.GotFocus += (sender, e) =>
            {
                if (txtPassword.ForeColor == watermarkColor)
                {
                    txtPassword.Clear();
                    txtPassword.ForeColor = designColor;

                    if (ShowPass == false) txtPassword.PasswordChar = '*';
                }
            };

            txtPassword.TextChanged += (sender, e) =>
            {
                if (txtPassword.TextLength == maxPassLength)
                    lblErrorPass.Text = "The password is too long!";
                else
                    lblErrorPass.Text = "";
            };

            txtPassword.LostFocus += (sender, e) =>
            {
                if (txtPassword.Text == "" && btnShowHide.ContainsFocus == false)
                {
                    txtPassword.ForeColor = watermarkColor;
                    txtPassword.PasswordChar = '\0';
                    txtPassword.Text = "Password";
                }

                if(btnShowHide.ContainsFocus == true)
                    txtPassword.Focus();
            };
        }

        private void StyleButtons()
        {
            btnLogIn.BackColor = formColor;
            btnLogIn.ForeColor = buttonForeColor;

            btnSignUp.ForeColor = buttonForeColor;
            btnSignUp.BackColor = formColor;

            btnExit.BackColor = formColor;
            btnExit.ForeColor = buttonForeColor;

            btnForgotPass.BackColor = formColor;
            btnForgotPass.ForeColor = designColor;

            btnLogIn.FlatAppearance.MouseDownBackColor = formColor;
            btnLogIn.FlatAppearance.MouseOverBackColor = formColor;
            
            btnSignUp.FlatAppearance.MouseDownBackColor = formColor;
            btnSignUp.FlatAppearance.MouseOverBackColor = formColor;

            btnExit.FlatAppearance.MouseDownBackColor = formColor;
            btnExit.FlatAppearance.MouseOverBackColor = formColor;

            btnForgotPass.FlatAppearance.MouseDownBackColor = formColor;
            btnForgotPass.FlatAppearance.MouseOverBackColor = formColor;

            btnShowHide.FlatAppearance.MouseDownBackColor = formColor;
            btnShowHide.FlatAppearance.MouseOverBackColor = formColor;

            btnLogIn.MouseEnter += (sender, e) =>
            {
                btnLogIn.ForeColor = designColor;
            };

            btnLogIn.MouseLeave += (sender, e) =>
            {
                btnLogIn.ForeColor = buttonForeColor;
            };

            btnSignUp.MouseEnter += (sender, e) =>
            {
                btnSignUp.ForeColor = designColor;
            };

            btnSignUp.MouseLeave += (sender, e) =>
            {
                btnSignUp.ForeColor = buttonForeColor;
            };

            btnExit.MouseEnter += (sender, e) =>
            {
                btnExit.ForeColor = designColor;
            };

            btnExit.MouseLeave += (sender, e) =>
            {
                btnExit.ForeColor = buttonForeColor;
            };

            btnForgotPass.MouseEnter += (sender, e) =>
            {
                btnForgotPass.ForeColor = designColor;
            };

            btnForgotPass.MouseLeave += (sender, e) =>
            {
                btnForgotPass.ForeColor = buttonForeColor;
            };

            btnShowHide.MouseClick += (sender, e) =>
            {
                if (!ShowPass)
                {
                    btnShowHide.BackgroundImage = ChessGame.Properties.Resources.ShowPass;
                    ShowPass = true;
                    txtPassword.PasswordChar = '\0';
                }
                else
                {
                    btnShowHide.BackgroundImage = ChessGame.Properties.Resources.HidePass;
                    ShowPass = false;
                    if(txtPassword.ForeColor != watermarkColor)
                        txtPassword.PasswordChar = '*';
                }
            };

            btnExit.MouseClick += (sender, e) =>
            {
                this.Close();
            };
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            txtEmail.ForeColor = txtPassword.ForeColor = watermarkColor;
            txtEmail.Text = "Email";
            txtPassword.PasswordChar = '\0';
            txtPassword.Text = "Password";
            lblErrorEmail.Text = lblErrorPass.Text = string.Empty;

            this.Hide();
            SignUpForm signUpForm = new SignUpForm();
            signUpForm.FormClosed += (s, args) => this.Show();
            signUpForm.Show();
        }

        private void btnForgotPass_Click(object sender, EventArgs e)
        {
            txtEmail.ForeColor = txtPassword.ForeColor = watermarkColor;
            txtEmail.Text = "Email";
            txtPassword.PasswordChar = '\0';
            txtPassword.Text = "Password";
            lblErrorEmail.Text = lblErrorPass.Text = string.Empty;

            this.Hide();
            ForgotPassForm forgotPassForm = new ForgotPassForm();
            forgotPassForm.FormClosed += (s, args) => this.Show();
            forgotPassForm.Show();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            lblErrorPass.Text = lblErrorEmail.Text = "";

            if (txtEmail.ForeColor == watermarkColor)
                lblErrorEmail.Text = "Please enter an email address!";
            else
            {
                if(!emailRegex.IsMatch(txtEmail.Text))
                    lblErrorEmail.Text = "Wrong email format!";
                else
                {
                    if(txtPassword.ForeColor == watermarkColor)
                        lblErrorPass.Text = "Please enter a password first!";
                    else
                    {
                        if (!ValidData())
                            lblErrorPass.Text = "Wrong email or password!";
                        else
                        {
                            this.Hide();
                            ChessForm chessForm = new ChessForm();
                            chessForm.FormClosed += (s, args) => this.Close();
                            chessForm.Show();
                        }
                    }
                }
            }
        }

        private bool ValidData()
        {
            string email = txtEmail.Text;
            string pass = txtPassword.Text;

            using (var con = new SqlConnection(connStr))
            {
                con.Open();
                string query = "SELECT * FROM Users WHERE Email = @email AND Password = @pass";

                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    if (!cmd.ExecuteReader().HasRows)
                        return false;
                }
            }

            return true;
        }
    }
}
