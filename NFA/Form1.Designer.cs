namespace DFA
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			this.Choose_file_btn = new System.Windows.Forms.Button();
			this.PrintLbl = new System.Windows.Forms.Label();
			this.StringBox = new System.Windows.Forms.TextBox();
			this.Checkbtn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.Minimizationbtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Choose_file_btn
			// 
			this.Choose_file_btn.Location = new System.Drawing.Point(251, 12);
			this.Choose_file_btn.Name = "Choose_file_btn";
			this.Choose_file_btn.Size = new System.Drawing.Size(166, 59);
			this.Choose_file_btn.TabIndex = 0;
			this.Choose_file_btn.Text = "Choose file";
			this.Choose_file_btn.UseVisualStyleBackColor = true;
			this.Choose_file_btn.Click += new System.EventHandler(this.Choose_file_btn_Click);
			// 
			// PrintLbl
			// 
			this.PrintLbl.AutoSize = true;
			this.PrintLbl.Location = new System.Drawing.Point(12, 82);
			this.PrintLbl.Name = "PrintLbl";
			this.PrintLbl.Size = new System.Drawing.Size(117, 17);
			this.PrintLbl.TabIndex = 1;
			this.PrintLbl.Text = "Print your string -";
			// 
			// StringBox
			// 
			this.StringBox.Location = new System.Drawing.Point(148, 82);
			this.StringBox.Name = "StringBox";
			this.StringBox.Size = new System.Drawing.Size(169, 22);
			this.StringBox.TabIndex = 2;
			// 
			// Checkbtn
			// 
			this.Checkbtn.Enabled = false;
			this.Checkbtn.Location = new System.Drawing.Point(323, 73);
			this.Checkbtn.Name = "Checkbtn";
			this.Checkbtn.Size = new System.Drawing.Size(69, 35);
			this.Checkbtn.TabIndex = 3;
			this.Checkbtn.Text = "Check";
			this.Checkbtn.UseVisualStyleBackColor = true;
			this.Checkbtn.Click += new System.EventHandler(this.Checkbtn_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(204, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Alphabet has 2 values: a and b";
			// 
			// Minimizationbtn
			// 
			this.Minimizationbtn.Enabled = false;
			this.Minimizationbtn.Location = new System.Drawing.Point(314, 128);
			this.Minimizationbtn.Name = "Minimizationbtn";
			this.Minimizationbtn.Size = new System.Drawing.Size(103, 31);
			this.Minimizationbtn.TabIndex = 5;
			this.Minimizationbtn.Text = "Minimization";
			this.Minimizationbtn.UseVisualStyleBackColor = true;
			this.Minimizationbtn.Click += new System.EventHandler(this.Minimizationbtn_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(438, 190);
			this.Controls.Add(this.Minimizationbtn);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Checkbtn);
			this.Controls.Add(this.StringBox);
			this.Controls.Add(this.PrintLbl);
			this.Controls.Add(this.Choose_file_btn);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Choose_file_btn;
        private System.Windows.Forms.Label PrintLbl;
        private System.Windows.Forms.TextBox StringBox;
        private System.Windows.Forms.Button Checkbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Minimizationbtn;
    }
}

