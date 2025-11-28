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
    public partial class SignUpForm : Form
    {
        private bool ShowPass = false;
        private const int maxPassLength = 40, maxEmailLength = 100;

        private Color formColor = Color.FromArgb(224, 204, 164),
             designColor = Color.FromArgb(82, 59, 38),
             buttonForeColor = Color.FromArgb(122, 90, 60),
             watermarkColor = Color.FromArgb(148, 121, 96),
             errorColor = Color.FromArgb(128, 41, 33);

        private string connStr = "Server=paul;Database=Chess;User Id=Paul;Password=Paul1234;";
        private Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public SignUpForm()
        {
            InitializeComponent();
            StyleForm();
        }

        private void StyleForm()
        {
            this.BackColor = formColor;

            lblSignUp.ForeColor = designColor;
            lblErrorConfirmPass.ForeColor = errorColor;
            lblErrorPass.ForeColor = errorColor;
            lblErrorEmail.ForeColor = errorColor;

            pnSignUp.BackColor = designColor;
            pnSignUp1.BackColor = designColor;
            pnSignUp2.BackColor = designColor;

            StyleTextBoxes();
            StyleButtons();
        }

        private void StyleTextBoxes()
        {
            txtEmail.MaxLength = maxEmailLength;
            txtEmail.BackColor = formColor;
            txtEmail.ForeColor = watermarkColor;
            txtEmail.Text = "Email";

            txtPass.MaxLength = maxPassLength;
            txtPass.BackColor = formColor;
            txtPass.ForeColor = watermarkColor;
            txtPass.Text = "Password";

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
                if (txtEmail.TextLength == txtEmail.MaxLength)
                    lblErrorEmail.Text = "The email is too long!";
                else
                    lblErrorEmail.Text = "";
            };

            txtPass.GotFocus += (sender, e) =>
            {
                if (txtPass.ForeColor == watermarkColor)
                {
                    txtPass.Clear();
                    txtPass.ForeColor = designColor;

                    if (ShowPass == false) txtPass.PasswordChar = '*';
                }
            };

            txtPass.LostFocus += (sender, e) =>
            {
                if (txtPass.Text == "" && btnShowHide.ContainsFocus == false)
                {
                    txtPass.ForeColor = watermarkColor;
                    txtPass.PasswordChar = '\0';
                    txtPass.Text = "Password";
                }

                if(btnShowHide.ContainsFocus == true)
                    txtPass.Focus();
            };

            txtPass.TextChanged += (sender, e) =>
            {
                if (txtPass.TextLength == txtPass.MaxLength)
                    lblErrorPass.Text = "The password is too long!";
                else
                    lblErrorPass.Text = "";
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
            btnSignUp.ForeColor = buttonForeColor;
            btnSignUp.BackColor = formColor;

            btnBack.BackColor = formColor;
            btnBack.ForeColor = buttonForeColor;

            btnSignUp.FlatAppearance.MouseDownBackColor = formColor;
            btnSignUp.FlatAppearance.MouseOverBackColor = formColor;

            btnBack.FlatAppearance.MouseDownBackColor = formColor;
            btnBack.FlatAppearance.MouseOverBackColor = formColor;

            btnShowHide.FlatAppearance.MouseDownBackColor = formColor;
            btnShowHide.FlatAppearance.MouseOverBackColor = formColor;


            btnSignUp.MouseEnter += (sender, e) =>
            {
                btnSignUp.ForeColor = designColor;
            };

            btnSignUp.MouseLeave += (sender, e) =>
            {
                btnSignUp.ForeColor = buttonForeColor;
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
                    txtPass.PasswordChar = '\0';
                }
                else
                {
                    btnShowHide.BackgroundImage = ChessGame.Properties.Resources.HidePass;
                    ShowPass = false;

                    if(txtPass.ForeColor != watermarkColor)
                        txtPass.PasswordChar = '*';
                }
            };

            btnBack.MouseClick += (sender, e) =>
            {
                this.Close();
            };
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            lblErrorConfirmPass.Text = lblErrorPass.Text = lblErrorEmail.Text = "";

            if (txtEmail.ForeColor == watermarkColor)
                lblErrorEmail.Text = "Please enter an email address!";
            else
            {
                if (!emailRegex.IsMatch(txtEmail.Text))
                    lblErrorEmail.Text = "Wrong email format!";
                else
                {
                    if(ExistingEmail())
                        lblErrorEmail.Text = "Email already exists!";
                    else
                    {
                        if (txtPass.ForeColor == watermarkColor)
                            lblErrorPass.Text = "Please enter a password first!";
                        else
                        {
                            if (txtConfirmPass.ForeColor == watermarkColor)
                                lblErrorConfirmPass.Text = "Confirm your password!";
                            else
                            {
                                if (txtConfirmPass.Text != txtPass.Text)
                                {
                                    txtConfirmPass.Clear();
                                    txtConfirmPass.Focus();
                                    lblErrorConfirmPass.Text = "Incorrect password!";
                                }
                                else
                                {
                                    CreateAccount();
                                    this.Close();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CreateAccount()
        {
            string email = txtEmail.Text;
            string pass = txtPass.Text;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Users VALUES (@email, @pass)";

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
            string checkEmail = txtEmail.Text;

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
