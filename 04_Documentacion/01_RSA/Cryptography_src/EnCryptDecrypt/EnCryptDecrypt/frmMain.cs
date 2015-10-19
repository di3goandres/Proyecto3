using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EnCryptDecrypt
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (txtClearText.Text == "")
            {
                error.SetError(txtClearText, "Enter the text you want to encrypt");
            }
            else
            {
                error.Clear();
                string clearText = txtClearText.Text.Trim();
                string cipherText = CryptorEngine.Encrypt(clearText, true);
                txtDecryptedText.Visible = false;
                label3.Visible = false;
                txtCipherText.Text = cipherText;
                btnDecrypt.Enabled = true;
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string cipherText = txtCipherText.Text.Trim();
            string decryptedText = CryptorEngine.Decrypt(cipherText, true);
            txtDecryptedText.Text = decryptedText;
            txtDecryptedText.Visible = true;
            label3.Visible = true;            
        }
    }
}