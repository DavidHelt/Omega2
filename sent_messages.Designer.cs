namespace Omega
{
    partial class sent_messages
    {
        /// <summary> 
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód vygenerovaný pomocí Návrháře komponent

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sentmes = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // sentmes
            // 
            this.sentmes.Location = new System.Drawing.Point(3, 21);
            this.sentmes.Name = "sentmes";
            this.sentmes.Size = new System.Drawing.Size(708, 345);
            this.sentmes.TabIndex = 1;
            // 
            // sent_messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sentmes);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "sent_messages";
            this.Size = new System.Drawing.Size(816, 382);
            this.Load += new System.EventHandler(this.sent_messages_Load_1);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel sentmes;

        #endregion
    }
}
