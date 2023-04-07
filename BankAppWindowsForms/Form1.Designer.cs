namespace BankAppWindowsForms
{
    partial class Form1
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
            this.application_title = new System.Windows.Forms.Label();
            this.label_greeting = new System.Windows.Forms.Label();
            this.label_menu_question = new System.Windows.Forms.Label();
            this.button_create_account = new System.Windows.Forms.Button();
            this.button_login = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // application_title
            // 
            this.application_title.AutoSize = true;
            this.application_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.application_title.ForeColor = System.Drawing.SystemColors.GrayText;
            this.application_title.Location = new System.Drawing.Point(371, 32);
            this.application_title.Name = "application_title";
            this.application_title.Size = new System.Drawing.Size(197, 39);
            this.application_title.TabIndex = 0;
            this.application_title.Text = "BANK APP";
            this.application_title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label_greeting
            // 
            this.label_greeting.AutoSize = true;
            this.label_greeting.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_greeting.Location = new System.Drawing.Point(396, 93);
            this.label_greeting.Name = "label_greeting";
            this.label_greeting.Size = new System.Drawing.Size(152, 29);
            this.label_greeting.TabIndex = 1;
            this.label_greeting.Text = "Hello Guest.";
            this.label_greeting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_menu_question
            // 
            this.label_menu_question.AutoSize = true;
            this.label_menu_question.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_menu_question.Location = new System.Drawing.Point(309, 138);
            this.label_menu_question.Name = "label_menu_question";
            this.label_menu_question.Size = new System.Drawing.Size(316, 29);
            this.label_menu_question.TabIndex = 2;
            this.label_menu_question.Text = "What would you like to do?";
            this.label_menu_question.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_create_account
            // 
            this.button_create_account.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_create_account.Location = new System.Drawing.Point(314, 227);
            this.button_create_account.Name = "button_create_account";
            this.button_create_account.Size = new System.Drawing.Size(277, 78);
            this.button_create_account.TabIndex = 3;
            this.button_create_account.Text = "Create New Account";
            this.button_create_account.UseVisualStyleBackColor = true;
            this.button_create_account.Click += new System.EventHandler(this.button_create_account_Click);
            // 
            // button_login
            // 
            this.button_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_login.Location = new System.Drawing.Point(314, 330);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(277, 78);
            this.button_login.TabIndex = 4;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = true;
            // 
            // button_exit
            // 
            this.button_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_exit.Location = new System.Drawing.Point(314, 440);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(277, 78);
            this.button_exit.TabIndex = 5;
            this.button_exit.Text = "Exit";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(970, 686);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.button_create_account);
            this.Controls.Add(this.label_menu_question);
            this.Controls.Add(this.label_greeting);
            this.Controls.Add(this.application_title);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label application_title;
        private System.Windows.Forms.Label label_greeting;
        private System.Windows.Forms.Label label_menu_question;
        private System.Windows.Forms.Button button_create_account;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Button button_exit;
    }
}

