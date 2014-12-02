namespace SFC_Tools.Forms
{
    partial class ucWebServiceTest
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucWebServiceTest));
            this.btnTipTop = new System.Windows.Forms.Button();
            this.chkProduct = new System.Windows.Forms.CheckBox();
            this.btnGetData = new System.Windows.Forms.Button();
            this.dtWorkOrderInfo = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbTipTopRes = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtWorkOrderInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTipTop
            // 
            this.btnTipTop.Enabled = false;
            this.btnTipTop.Location = new System.Drawing.Point(387, 473);
            this.btnTipTop.Name = "btnTipTop";
            this.btnTipTop.Size = new System.Drawing.Size(173, 35);
            this.btnTipTop.TabIndex = 0;
            this.btnTipTop.Text = "Tiptop结单";
            this.btnTipTop.UseVisualStyleBackColor = true;
            this.btnTipTop.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkProduct
            // 
            this.chkProduct.AutoSize = true;
            this.chkProduct.Location = new System.Drawing.Point(622, 483);
            this.chkProduct.Name = "chkProduct";
            this.chkProduct.Size = new System.Drawing.Size(84, 16);
            this.chkProduct.TabIndex = 4;
            this.chkProduct.Text = "Production";
            this.chkProduct.UseVisualStyleBackColor = true;
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(89, 473);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(165, 35);
            this.btnGetData.TabIndex = 5;
            this.btnGetData.Text = "加载信息";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // dtWorkOrderInfo
            // 
            this.dtWorkOrderInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtWorkOrderInfo.Location = new System.Drawing.Point(9, 75);
            this.dtWorkOrderInfo.Name = "dtWorkOrderInfo";
            this.dtWorkOrderInfo.RowTemplate.Height = 23;
            this.dtWorkOrderInfo.Size = new System.Drawing.Size(727, 312);
            this.dtWorkOrderInfo.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label1.Font = new System.Drawing.Font("DFKai-SB", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(727, 52);
            this.label1.TabIndex = 7;
            this.label1.Text = "STOREDPROC ANALYSE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtbTipTopRes
            // 
            this.rtbTipTopRes.Location = new System.Drawing.Point(9, 393);
            this.rtbTipTopRes.Name = "rtbTipTopRes";
            this.rtbTipTopRes.Size = new System.Drawing.Size(727, 66);
            this.rtbTipTopRes.TabIndex = 8;
            this.rtbTipTopRes.Text = "";
            // 
            // ucWebServiceTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtbTipTopRes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtWorkOrderInfo);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.chkProduct);
            this.Controls.Add(this.btnTipTop);
            this.Name = "ucWebServiceTest";
            this.Size = new System.Drawing.Size(751, 520);
            ((System.ComponentModel.ISupportInitialize)(this.dtWorkOrderInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTipTop;
        private System.Windows.Forms.CheckBox chkProduct;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.DataGridView dtWorkOrderInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbTipTopRes;
    }
}
