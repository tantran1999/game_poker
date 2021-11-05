namespace TienLen_Client
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.tb_Password = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox();
            this.tb_Account = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox();
            this.bunifuGradientPanel1 = new Bunifu.Framework.UI.BunifuGradientPanel();
            this.bt_Exit = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.bt_Signup = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.bt_SignIn = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.bunifuGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(59, 102);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(75, 20);
            this.bunifuCustomLabel2.TabIndex = 7;
            this.bunifuCustomLabel2.Text = "Mật khẩu";
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(56, 49);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(78, 20);
            this.bunifuCustomLabel1.TabIndex = 6;
            this.bunifuCustomLabel1.Text = "Tài khoản";
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.bunifuGradientPanel1;
            this.bunifuDragControl1.Vertical = true;
            // 
            // tb_Password
            // 
            this.tb_Password.AcceptsReturn = false;
            this.tb_Password.AcceptsTab = false;
            this.tb_Password.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tb_Password.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tb_Password.BackColor = System.Drawing.Color.Transparent;
            this.tb_Password.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tb_Password.BackgroundImage")));
            this.tb_Password.BorderColorActive = System.Drawing.Color.SteelBlue;
            this.tb_Password.BorderColorDisabled = System.Drawing.Color.LightSkyBlue;
            this.tb_Password.BorderColorHover = System.Drawing.Color.SteelBlue;
            this.tb_Password.BorderColorIdle = System.Drawing.Color.LightSkyBlue;
            this.tb_Password.BorderRadius = 1;
            this.tb_Password.BorderThickness = 2;
            this.tb_Password.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tb_Password.DefaultFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Password.DefaultText = "";
            this.tb_Password.FillColor = System.Drawing.Color.White;
            this.tb_Password.HideSelection = true;
            this.tb_Password.IconLeft = null;
            this.tb_Password.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.tb_Password.IconPadding = 10;
            this.tb_Password.IconRight = null;
            this.tb_Password.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.tb_Password.Location = new System.Drawing.Point(140, 93);
            this.tb_Password.MaxLength = 32767;
            this.tb_Password.MinimumSize = new System.Drawing.Size(100, 35);
            this.tb_Password.Modified = false;
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '●';
            this.tb_Password.ReadOnly = false;
            this.tb_Password.SelectedText = "";
            this.tb_Password.SelectionLength = 0;
            this.tb_Password.SelectionStart = 0;
            this.tb_Password.ShortcutsEnabled = true;
            this.tb_Password.Size = new System.Drawing.Size(145, 38);
            this.tb_Password.Style = Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox._Style.Bunifu;
            this.tb_Password.TabIndex = 15;
            this.tb_Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tb_Password.TextMarginLeft = 5;
            this.tb_Password.TextPlaceholder = "";
            this.tb_Password.UseSystemPasswordChar = true;
            // 
            // tb_Account
            // 
            this.tb_Account.AcceptsReturn = false;
            this.tb_Account.AcceptsTab = false;
            this.tb_Account.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tb_Account.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tb_Account.BackColor = System.Drawing.Color.Transparent;
            this.tb_Account.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tb_Account.BackgroundImage")));
            this.tb_Account.BorderColorActive = System.Drawing.Color.SteelBlue;
            this.tb_Account.BorderColorDisabled = System.Drawing.Color.LightSkyBlue;
            this.tb_Account.BorderColorHover = System.Drawing.Color.SteelBlue;
            this.tb_Account.BorderColorIdle = System.Drawing.Color.LightSkyBlue;
            this.tb_Account.BorderRadius = 1;
            this.tb_Account.BorderThickness = 2;
            this.tb_Account.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tb_Account.DefaultFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Account.DefaultText = "";
            this.tb_Account.FillColor = System.Drawing.Color.White;
            this.tb_Account.HideSelection = true;
            this.tb_Account.IconLeft = null;
            this.tb_Account.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.tb_Account.IconPadding = 10;
            this.tb_Account.IconRight = null;
            this.tb_Account.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.tb_Account.Location = new System.Drawing.Point(140, 40);
            this.tb_Account.MaxLength = 32767;
            this.tb_Account.MinimumSize = new System.Drawing.Size(100, 35);
            this.tb_Account.Modified = false;
            this.tb_Account.Name = "tb_Account";
            this.tb_Account.PasswordChar = '\0';
            this.tb_Account.ReadOnly = false;
            this.tb_Account.SelectedText = "";
            this.tb_Account.SelectionLength = 0;
            this.tb_Account.SelectionStart = 0;
            this.tb_Account.ShortcutsEnabled = true;
            this.tb_Account.Size = new System.Drawing.Size(145, 38);
            this.tb_Account.Style = Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox._Style.Bunifu;
            this.tb_Account.TabIndex = 14;
            this.tb_Account.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tb_Account.TextMarginLeft = 5;
            this.tb_Account.TextPlaceholder = "";
            this.tb_Account.UseSystemPasswordChar = false;
            this.tb_Account.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Account_KeyDown);
            // 
            // bunifuGradientPanel1
            // 
            this.bunifuGradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuGradientPanel1.BackgroundImage")));
            this.bunifuGradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuGradientPanel1.Controls.Add(this.bt_Exit);
            this.bunifuGradientPanel1.GradientBottomLeft = System.Drawing.Color.AliceBlue;
            this.bunifuGradientPanel1.GradientBottomRight = System.Drawing.Color.AliceBlue;
            this.bunifuGradientPanel1.GradientTopLeft = System.Drawing.Color.PowderBlue;
            this.bunifuGradientPanel1.GradientTopRight = System.Drawing.Color.White;
            this.bunifuGradientPanel1.Location = new System.Drawing.Point(2, 1);
            this.bunifuGradientPanel1.Name = "bunifuGradientPanel1";
            this.bunifuGradientPanel1.Quality = 10;
            this.bunifuGradientPanel1.Size = new System.Drawing.Size(391, 30);
            this.bunifuGradientPanel1.TabIndex = 13;
            // 
            // bt_Exit
            // 
            this.bt_Exit.BackColor = System.Drawing.Color.Transparent;
            this.bt_Exit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_Exit.BackgroundImage")));
            this.bt_Exit.ButtonText = "X";
            this.bt_Exit.ButtonTextMarginLeft = 0;
            this.bt_Exit.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.bt_Exit.DisabledFillColor = System.Drawing.Color.Gray;
            this.bt_Exit.DisabledForecolor = System.Drawing.Color.White;
            this.bt_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bt_Exit.ForeColor = System.Drawing.Color.Black;
            this.bt_Exit.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.bt_Exit.IconPadding = 10;
            this.bt_Exit.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.bt_Exit.IdleBorderColor = System.Drawing.Color.White;
            this.bt_Exit.IdleBorderRadius = 1;
            this.bt_Exit.IdleBorderThickness = 0;
            this.bt_Exit.IdleFillColor = System.Drawing.Color.AliceBlue;
            this.bt_Exit.IdleIconLeftImage = null;
            this.bt_Exit.IdleIconRightImage = null;
            this.bt_Exit.Location = new System.Drawing.Point(362, 0);
            this.bt_Exit.Name = "bt_Exit";
            stateProperties1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties1.BorderRadius = 1;
            stateProperties1.BorderThickness = 1;
            stateProperties1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties1.IconLeftImage = null;
            stateProperties1.IconRightImage = null;
            this.bt_Exit.onHoverState = stateProperties1;
            this.bt_Exit.Size = new System.Drawing.Size(29, 30);
            this.bt_Exit.TabIndex = 12;
            this.bt_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // bt_Signup
            // 
            this.bt_Signup.BackColor = System.Drawing.Color.Transparent;
            this.bt_Signup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_Signup.BackgroundImage")));
            this.bt_Signup.ButtonText = "Đăng kí";
            this.bt_Signup.ButtonTextMarginLeft = 0;
            this.bt_Signup.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.bt_Signup.DisabledFillColor = System.Drawing.Color.Gray;
            this.bt_Signup.DisabledForecolor = System.Drawing.Color.White;
            this.bt_Signup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bt_Signup.ForeColor = System.Drawing.Color.White;
            this.bt_Signup.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.bt_Signup.IconPadding = 10;
            this.bt_Signup.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.bt_Signup.IdleBorderColor = System.Drawing.Color.AliceBlue;
            this.bt_Signup.IdleBorderRadius = 1;
            this.bt_Signup.IdleBorderThickness = 0;
            this.bt_Signup.IdleFillColor = System.Drawing.Color.CornflowerBlue;
            this.bt_Signup.IdleIconLeftImage = null;
            this.bt_Signup.IdleIconRightImage = null;
            this.bt_Signup.Location = new System.Drawing.Point(229, 144);
            this.bt_Signup.Name = "bt_Signup";
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties2.BorderRadius = 1;
            stateProperties2.BorderThickness = 1;
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties2.IconLeftImage = null;
            stateProperties2.IconRightImage = null;
            this.bt_Signup.onHoverState = stateProperties2;
            this.bt_Signup.Size = new System.Drawing.Size(111, 39);
            this.bt_Signup.TabIndex = 11;
            this.bt_Signup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bt_Signup.Click += new System.EventHandler(this.bt_Signup_Click);
            // 
            // bt_SignIn
            // 
            this.bt_SignIn.BackColor = System.Drawing.Color.Transparent;
            this.bt_SignIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_SignIn.BackgroundImage")));
            this.bt_SignIn.ButtonText = "Đăng nhập";
            this.bt_SignIn.ButtonTextMarginLeft = 0;
            this.bt_SignIn.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.bt_SignIn.DisabledFillColor = System.Drawing.SystemColors.ActiveCaption;
            this.bt_SignIn.DisabledForecolor = System.Drawing.Color.White;
            this.bt_SignIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bt_SignIn.ForeColor = System.Drawing.Color.White;
            this.bt_SignIn.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.bt_SignIn.IconPadding = 10;
            this.bt_SignIn.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.bt_SignIn.IdleBorderColor = System.Drawing.Color.AliceBlue;
            this.bt_SignIn.IdleBorderRadius = 1;
            this.bt_SignIn.IdleBorderThickness = 0;
            this.bt_SignIn.IdleFillColor = System.Drawing.Color.CornflowerBlue;
            this.bt_SignIn.IdleIconLeftImage = null;
            this.bt_SignIn.IdleIconRightImage = null;
            this.bt_SignIn.Location = new System.Drawing.Point(60, 144);
            this.bt_SignIn.Name = "bt_SignIn";
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties3.BorderRadius = 1;
            stateProperties3.BorderThickness = 1;
            stateProperties3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties3.IconLeftImage = null;
            stateProperties3.IconRightImage = null;
            this.bt_SignIn.onHoverState = stateProperties3;
            this.bt_SignIn.Size = new System.Drawing.Size(111, 39);
            this.bt_SignIn.TabIndex = 10;
            this.bt_SignIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bt_SignIn.Click += new System.EventHandler(this.bt_SignIn_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(393, 204);
            this.Controls.Add(this.tb_Password);
            this.Controls.Add(this.tb_Account);
            this.Controls.Add(this.bunifuGradientPanel1);
            this.Controls.Add(this.bt_Signup);
            this.Controls.Add(this.bt_SignIn);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.bunifuGradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton bt_Exit;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton bt_Signup;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton bt_SignIn;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuGradientPanel bunifuGradientPanel1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox tb_Password;
        private Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox tb_Account;
    }
}