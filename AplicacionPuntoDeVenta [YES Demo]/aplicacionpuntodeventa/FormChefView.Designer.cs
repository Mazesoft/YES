namespace AplicacionPuntoDeVenta
{
    partial class FormChefView
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
            this.pnlThird = new System.Windows.Forms.Panel();
            this.btnOrdT = new System.Windows.Forms.Button();
            this.lbOrdT = new System.Windows.Forms.ListBox();
            this.lblOrderT = new System.Windows.Forms.Label();
            this.pnlSecond = new System.Windows.Forms.Panel();
            this.btnOrdS = new System.Windows.Forms.Button();
            this.lbOrdS = new System.Windows.Forms.ListBox();
            this.lblOrderS = new System.Windows.Forms.Label();
            this.pnlFirst = new System.Windows.Forms.Panel();
            this.lblOrdActual = new System.Windows.Forms.Label();
            this.btnOrdF = new System.Windows.Forms.Button();
            this.lbOrdF = new System.Windows.Forms.ListBox();
            this.lblOrderF = new System.Windows.Forms.Label();
            this.timerCheck = new System.Windows.Forms.Timer(this.components);
            this.pnlThird.SuspendLayout();
            this.pnlSecond.SuspendLayout();
            this.pnlFirst.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlThird
            // 
            this.pnlThird.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlThird.Controls.Add(this.btnOrdT);
            this.pnlThird.Controls.Add(this.lbOrdT);
            this.pnlThird.Controls.Add(this.lblOrderT);
            this.pnlThird.Location = new System.Drawing.Point(13, 12);
            this.pnlThird.Margin = new System.Windows.Forms.Padding(4);
            this.pnlThird.Name = "pnlThird";
            this.pnlThird.Size = new System.Drawing.Size(507, 849);
            this.pnlThird.TabIndex = 0;
            // 
            // btnOrdT
            // 
            this.btnOrdT.BackColor = System.Drawing.Color.LimeGreen;
            this.btnOrdT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrdT.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrdT.ForeColor = System.Drawing.Color.White;
            this.btnOrdT.Location = new System.Drawing.Point(35, 722);
            this.btnOrdT.Margin = new System.Windows.Forms.Padding(4);
            this.btnOrdT.Name = "btnOrdT";
            this.btnOrdT.Size = new System.Drawing.Size(437, 89);
            this.btnOrdT.TabIndex = 5;
            this.btnOrdT.Text = "Finalizar Orden";
            this.btnOrdT.UseVisualStyleBackColor = false;
            this.btnOrdT.Click += new System.EventHandler(this.btnOrdT_Click);
            // 
            // lbOrdT
            // 
            this.lbOrdT.Font = new System.Drawing.Font("Arial", 15.75F);
            this.lbOrdT.ForeColor = System.Drawing.Color.LimeGreen;
            this.lbOrdT.FormattingEnabled = true;
            this.lbOrdT.ItemHeight = 31;
            this.lbOrdT.Location = new System.Drawing.Point(35, 123);
            this.lbOrdT.Margin = new System.Windows.Forms.Padding(4);
            this.lbOrdT.Name = "lbOrdT";
            this.lbOrdT.Size = new System.Drawing.Size(436, 531);
            this.lbOrdT.TabIndex = 4;
            // 
            // lblOrderT
            // 
            this.lblOrderT.AutoSize = true;
            this.lblOrderT.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderT.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblOrderT.Location = new System.Drawing.Point(27, 49);
            this.lblOrderT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrderT.Name = "lblOrderT";
            this.lblOrderT.Size = new System.Drawing.Size(189, 42);
            this.lblOrderT.TabIndex = 3;
            this.lblOrderT.Text = "Orden No.";
            // 
            // pnlSecond
            // 
            this.pnlSecond.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlSecond.Controls.Add(this.btnOrdS);
            this.pnlSecond.Controls.Add(this.lbOrdS);
            this.pnlSecond.Controls.Add(this.lblOrderS);
            this.pnlSecond.Location = new System.Drawing.Point(533, 11);
            this.pnlSecond.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSecond.Name = "pnlSecond";
            this.pnlSecond.Size = new System.Drawing.Size(507, 849);
            this.pnlSecond.TabIndex = 1;
            // 
            // btnOrdS
            // 
            this.btnOrdS.BackColor = System.Drawing.Color.LimeGreen;
            this.btnOrdS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrdS.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrdS.ForeColor = System.Drawing.Color.White;
            this.btnOrdS.Location = new System.Drawing.Point(35, 724);
            this.btnOrdS.Margin = new System.Windows.Forms.Padding(4);
            this.btnOrdS.Name = "btnOrdS";
            this.btnOrdS.Size = new System.Drawing.Size(437, 89);
            this.btnOrdS.TabIndex = 5;
            this.btnOrdS.Text = "Finalizar Orden";
            this.btnOrdS.UseVisualStyleBackColor = false;
            this.btnOrdS.Click += new System.EventHandler(this.btnOrdS_Click);
            // 
            // lbOrdS
            // 
            this.lbOrdS.Font = new System.Drawing.Font("Arial", 15.75F);
            this.lbOrdS.ForeColor = System.Drawing.Color.LimeGreen;
            this.lbOrdS.FormattingEnabled = true;
            this.lbOrdS.ItemHeight = 31;
            this.lbOrdS.Location = new System.Drawing.Point(35, 124);
            this.lbOrdS.Margin = new System.Windows.Forms.Padding(4);
            this.lbOrdS.Name = "lbOrdS";
            this.lbOrdS.Size = new System.Drawing.Size(436, 531);
            this.lbOrdS.TabIndex = 4;
            // 
            // lblOrderS
            // 
            this.lblOrderS.AutoSize = true;
            this.lblOrderS.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderS.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblOrderS.Location = new System.Drawing.Point(27, 49);
            this.lblOrderS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrderS.Name = "lblOrderS";
            this.lblOrderS.Size = new System.Drawing.Size(189, 42);
            this.lblOrderS.TabIndex = 2;
            this.lblOrderS.Text = "Orden No.";
            // 
            // pnlFirst
            // 
            this.pnlFirst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlFirst.Controls.Add(this.lblOrdActual);
            this.pnlFirst.Controls.Add(this.btnOrdF);
            this.pnlFirst.Controls.Add(this.lbOrdF);
            this.pnlFirst.Controls.Add(this.lblOrderF);
            this.pnlFirst.Location = new System.Drawing.Point(1053, 12);
            this.pnlFirst.Margin = new System.Windows.Forms.Padding(4);
            this.pnlFirst.Name = "pnlFirst";
            this.pnlFirst.Size = new System.Drawing.Size(507, 849);
            this.pnlFirst.TabIndex = 2;
            // 
            // lblOrdActual
            // 
            this.lblOrdActual.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblOrdActual.Font = new System.Drawing.Font("Arial", 16F);
            this.lblOrdActual.ForeColor = System.Drawing.Color.Azure;
            this.lblOrdActual.Location = new System.Drawing.Point(-16, -1);
            this.lblOrdActual.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrdActual.Name = "lblOrdActual";
            this.lblOrdActual.Size = new System.Drawing.Size(537, 33);
            this.lblOrdActual.TabIndex = 4;
            this.lblOrdActual.Text = "Orden Actual";
            this.lblOrdActual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOrdF
            // 
            this.btnOrdF.BackColor = System.Drawing.Color.LimeGreen;
            this.btnOrdF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrdF.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrdF.ForeColor = System.Drawing.Color.White;
            this.btnOrdF.Location = new System.Drawing.Point(35, 722);
            this.btnOrdF.Margin = new System.Windows.Forms.Padding(4);
            this.btnOrdF.Name = "btnOrdF";
            this.btnOrdF.Size = new System.Drawing.Size(437, 89);
            this.btnOrdF.TabIndex = 3;
            this.btnOrdF.Text = "Finalizar Orden";
            this.btnOrdF.UseVisualStyleBackColor = false;
            this.btnOrdF.Click += new System.EventHandler(this.btnOrdF_Click);
            // 
            // lbOrdF
            // 
            this.lbOrdF.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOrdF.ForeColor = System.Drawing.Color.LimeGreen;
            this.lbOrdF.FormattingEnabled = true;
            this.lbOrdF.ItemHeight = 31;
            this.lbOrdF.Location = new System.Drawing.Point(35, 123);
            this.lbOrdF.Margin = new System.Windows.Forms.Padding(4);
            this.lbOrdF.Name = "lbOrdF";
            this.lbOrdF.Size = new System.Drawing.Size(436, 531);
            this.lbOrdF.TabIndex = 2;
            // 
            // lblOrderF
            // 
            this.lblOrderF.AutoSize = true;
            this.lblOrderF.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderF.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblOrderF.Location = new System.Drawing.Point(27, 49);
            this.lblOrderF.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrderF.Name = "lblOrderF";
            this.lblOrderF.Size = new System.Drawing.Size(189, 42);
            this.lblOrderF.TabIndex = 1;
            this.lblOrderF.Text = "Orden No.";
            // 
            // timerCheck
            // 
            this.timerCheck.Interval = 5000;
            this.timerCheck.Tick += new System.EventHandler(this.timerCheck_Tick);
            // 
            // FormChefView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AplicacionPuntoDeVenta.Properties.Resources._374296;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1579, 875);
            this.Controls.Add(this.pnlFirst);
            this.Controls.Add(this.pnlSecond);
            this.Controls.Add(this.pnlThird);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormChefView";
            this.Text = "FormChefView";
            this.Load += new System.EventHandler(this.FormChefView_Load);
            this.pnlThird.ResumeLayout(false);
            this.pnlThird.PerformLayout();
            this.pnlSecond.ResumeLayout(false);
            this.pnlSecond.PerformLayout();
            this.pnlFirst.ResumeLayout(false);
            this.pnlFirst.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlThird;
        private System.Windows.Forms.Panel pnlSecond;
        private System.Windows.Forms.Panel pnlFirst;
        private System.Windows.Forms.Label lblOrderF;
        private System.Windows.Forms.Button btnOrdT;
        private System.Windows.Forms.ListBox lbOrdT;
        private System.Windows.Forms.Label lblOrderT;
        private System.Windows.Forms.Button btnOrdS;
        private System.Windows.Forms.ListBox lbOrdS;
        private System.Windows.Forms.Label lblOrderS;
        private System.Windows.Forms.Button btnOrdF;
        private System.Windows.Forms.ListBox lbOrdF;
        private System.Windows.Forms.Label lblOrdActual;
        private System.Windows.Forms.Timer timerCheck;
    }
}