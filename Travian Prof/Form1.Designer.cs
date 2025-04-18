namespace Travian_Prof
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            sunucutxt = new TextBox();
            nicknametxt = new TextBox();
            passwordtxt = new TextBox();
            girisbtn = new Button();
            formatbtn = new Button();
            bilgilbx = new ListBox();
            kaydetbtn = new Button();
            panel1 = new Panel();
            label19 = new Label();
            npckoycbx = new ComboBox();
            label10 = new Label();
            npcchk = new CheckBox();
            label4 = new Label();
            panel2 = new Panel();
            label11 = new Label();
            otoyagmachk = new CheckBox();
            label5 = new Label();
            panel3 = new Panel();
            label12 = new Label();
            otomacerachk = new CheckBox();
            label6 = new Label();
            panel4 = new Panel();
            label32 = new Label();
            label25 = new Label();
            label24 = new Label();
            binalarcbx = new ComboBox();
            label20 = new Label();
            label13 = new Label();
            buildkoycbx = new ComboBox();
            buildchk = new CheckBox();
            label7 = new Label();
            panel5 = new Panel();
            irklbl = new Label();
            label29 = new Label();
            label21 = new Label();
            atliadettxt = new TextBox();
            yayaadettxt = new TextBox();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            askerkoycmbx = new ComboBox();
            atlicbx = new ComboBox();
            yayacbx = new ComboBox();
            label14 = new Label();
            askeregitchk = new CheckBox();
            label8 = new Label();
            panel6 = new Panel();
            label30 = new Label();
            label15 = new Label();
            askerolmesinchk = new CheckBox();
            label9 = new Label();
            baslatbtn = new Button();
            tekrartxt = new TextBox();
            label22 = new Label();
            lisanstxt = new TextBox();
            label23 = new Label();
            lisansbtn = new Button();
            panel7 = new Panel();
            chatidtxt = new TextBox();
            label28 = new Label();
            label27 = new Label();
            saldirivarchk = new CheckBox();
            label26 = new Label();
            label31 = new Label();
            lblLisansDurumu2 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Location = new Point(20, 30);
            label1.Name = "label1";
            label1.Size = new Size(97, 20);
            label1.TabIndex = 0;
            label1.Text = "Sunucu URL:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 67);
            label2.Name = "label2";
            label2.Size = new Size(92, 20);
            label2.TabIndex = 1;
            label2.Text = "Mail Adresi:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 100);
            label3.Name = "label3";
            label3.Size = new Size(45, 20);
            label3.TabIndex = 2;
            label3.Text = "Şifre:";
            // 
            // sunucutxt
            // 
            sunucutxt.Font = new Font("Segoe UI Semibold", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sunucutxt.Location = new Point(128, 27);
            sunucutxt.Name = "sunucutxt";
            sunucutxt.Size = new Size(177, 25);
            sunucutxt.TabIndex = 3;
            // 
            // nicknametxt
            // 
            nicknametxt.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            nicknametxt.Location = new Point(128, 64);
            nicknametxt.Name = "nicknametxt";
            nicknametxt.Size = new Size(177, 27);
            nicknametxt.TabIndex = 4;
            // 
            // passwordtxt
            // 
            passwordtxt.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            passwordtxt.Location = new Point(128, 97);
            passwordtxt.Name = "passwordtxt";
            passwordtxt.Size = new Size(177, 27);
            passwordtxt.TabIndex = 5;
            // 
            // girisbtn
            // 
            girisbtn.Location = new Point(20, 130);
            girisbtn.Name = "girisbtn";
            girisbtn.Size = new Size(287, 43);
            girisbtn.TabIndex = 6;
            girisbtn.Text = "Giriş Yap ve Bilgi Çek";
            girisbtn.UseVisualStyleBackColor = true;
            girisbtn.Click += girisbtn_Click;
            // 
            // formatbtn
            // 
            formatbtn.ForeColor = Color.IndianRed;
            formatbtn.Location = new Point(231, 201);
            formatbtn.Name = "formatbtn";
            formatbtn.Size = new Size(74, 36);
            formatbtn.TabIndex = 7;
            formatbtn.Text = "Format";
            formatbtn.UseVisualStyleBackColor = true;
            formatbtn.Click += formatbtn_Click;
            // 
            // bilgilbx
            // 
            bilgilbx.Font = new Font("Segoe UI Semibold", 9F);
            bilgilbx.FormattingEnabled = true;
            bilgilbx.Items.AddRange(new object[] { "Programı ilk kez çalıştırırken mutlaka", "\"Giriş Yap ve Bilgi Çek\" butonuna tıklayın.", "Bilgi çekildikten sonra,", "\"Ayarları Kaydet\" butonuna tıklayın.", "Not: Bu işlem sadece 1 kez yapılacaktır..", "Bilgiler kayıt olduktan sonra her seferinde,", "bu işlemi yapmayınız.." });
            bilgilbx.Location = new Point(18, 257);
            bilgilbx.Name = "bilgilbx";
            bilgilbx.Size = new Size(287, 264);
            bilgilbx.TabIndex = 8;
            // 
            // kaydetbtn
            // 
            kaydetbtn.Location = new Point(20, 186);
            kaydetbtn.Name = "kaydetbtn";
            kaydetbtn.Size = new Size(205, 65);
            kaydetbtn.TabIndex = 9;
            kaydetbtn.Text = "Ayarları Kaydet";
            kaydetbtn.UseVisualStyleBackColor = true;
            kaydetbtn.Click += kaydetbtn_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(label19);
            panel1.Controls.Add(npckoycbx);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(npcchk);
            panel1.Controls.Add(label4);
            panel1.Location = new Point(316, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(239, 146);
            panel1.TabIndex = 10;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(13, 87);
            label19.Name = "label19";
            label19.Size = new Size(40, 20);
            label19.TabIndex = 14;
            label19.Text = "Köy:";
            // 
            // npckoycbx
            // 
            npckoycbx.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            npckoycbx.FormattingEnabled = true;
            npckoycbx.Location = new Point(77, 87);
            npckoycbx.Name = "npckoycbx";
            npckoycbx.Size = new Size(148, 28);
            npckoycbx.TabIndex = 13;
            npckoycbx.SelectedIndexChanged += npckoycbx_SelectedIndexChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(13, 36);
            label10.Name = "label10";
            label10.Size = new Size(87, 20);
            label10.TabIndex = 2;
            label10.Text = "Aktif/Değil:";
            // 
            // npcchk
            // 
            npcchk.AutoSize = true;
            npcchk.Location = new Point(120, 39);
            npcchk.Name = "npcchk";
            npcchk.Size = new Size(18, 17);
            npcchk.TabIndex = 1;
            npcchk.UseVisualStyleBackColor = true;
            npcchk.CheckedChanged += npcchk_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(53, 4);
            label4.Name = "label4";
            label4.Size = new Size(129, 23);
            label4.TabIndex = 0;
            label4.Text = "NPC KONTROL";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlLight;
            panel2.Controls.Add(label11);
            panel2.Controls.Add(otoyagmachk);
            panel2.Controls.Add(label5);
            panel2.Location = new Point(868, 186);
            panel2.Name = "panel2";
            panel2.Size = new Size(239, 87);
            panel2.TabIndex = 11;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(4, 40);
            label11.Name = "label11";
            label11.Size = new Size(87, 20);
            label11.TabIndex = 3;
            label11.Text = "Aktif/Değil:";
            // 
            // otoyagmachk
            // 
            otoyagmachk.AutoSize = true;
            otoyagmachk.Location = new Point(102, 43);
            otoyagmachk.Name = "otoyagmachk";
            otoyagmachk.Size = new Size(18, 17);
            otoyagmachk.TabIndex = 2;
            otoyagmachk.UseVisualStyleBackColor = true;
            otoyagmachk.CheckedChanged += otoyagmachk_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(55, 3);
            label5.Name = "label5";
            label5.Size = new Size(110, 23);
            label5.TabIndex = 1;
            label5.Text = "OTO YAĞMA";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ControlLight;
            panel3.Controls.Add(label12);
            panel3.Controls.Add(otomacerachk);
            panel3.Controls.Add(label6);
            panel3.Location = new Point(868, 293);
            panel3.Name = "panel3";
            panel3.Size = new Size(239, 80);
            panel3.TabIndex = 12;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(3, 37);
            label12.Name = "label12";
            label12.Size = new Size(87, 20);
            label12.TabIndex = 4;
            label12.Text = "Aktif/Değil:";
            // 
            // otomacerachk
            // 
            otomacerachk.AutoSize = true;
            otomacerachk.Location = new Point(102, 40);
            otomacerachk.Name = "otomacerachk";
            otomacerachk.Size = new Size(18, 17);
            otomacerachk.TabIndex = 3;
            otomacerachk.UseVisualStyleBackColor = true;
            otomacerachk.CheckedChanged += otomacerachk_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(55, 9);
            label6.Name = "label6";
            label6.Size = new Size(120, 23);
            label6.TabIndex = 2;
            label6.Text = "OTO MACERA";
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.GradientActiveCaption;
            panel4.Controls.Add(label32);
            panel4.Controls.Add(label25);
            panel4.Controls.Add(label24);
            panel4.Controls.Add(binalarcbx);
            panel4.Controls.Add(label20);
            panel4.Controls.Add(label13);
            panel4.Controls.Add(buildkoycbx);
            panel4.Controls.Add(buildchk);
            panel4.Controls.Add(label7);
            panel4.Location = new Point(316, 191);
            panel4.Name = "panel4";
            panel4.Size = new Size(239, 218);
            panel4.TabIndex = 13;
            panel4.Visible = false;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new Point(41, 28);
            label32.Name = "label32";
            label32.Size = new Size(154, 20);
            label32.TabIndex = 22;
            label32.Text = "HENÜZ ÇALIŞMIYOR";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label25.Location = new Point(3, 177);
            label25.Name = "label25";
            label25.Size = new Size(223, 20);
            label25.TabIndex = 21;
            label25.Text = "Hammadde yeterliyse +1 seviye";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(13, 93);
            label24.Name = "label24";
            label24.Size = new Size(42, 20);
            label24.TabIndex = 20;
            label24.Text = "Yapı:";
            // 
            // binalarcbx
            // 
            binalarcbx.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            binalarcbx.FormattingEnabled = true;
            binalarcbx.Items.AddRange(new object[] { "1. Akademi", "2. Ahır", "3. Ambar", "4. Atölye", "5. Belediye Binası", "6. Büyük Ahır", "7. Büyük Kışla", "8. Büyük Ambar", "9. Değirmen", "10. Demir Deposu", "11. Demir Madeni", "12. Elçilik", "13. Fırın", "14. Hazine", "15. Kahraman Kış.", "16. Kereste Deposu", "17. Kışla", "18. Köşk", "19. Merkez Binası", "20. Oduncu", "21. Pazar Yeri", "22. Saray", "23. Sığınak", "24. Sur", "25. Tahıl Tarlası", "26. Ticaret Binası", "27. Tuğla Deposu", "28. Tuğla Ocağı", "29. Tuzakçı" });
            binalarcbx.Location = new Point(77, 90);
            binalarcbx.Name = "binalarcbx";
            binalarcbx.Size = new Size(148, 28);
            binalarcbx.TabIndex = 19;
            binalarcbx.SelectedIndexChanged += binalarcbx_SelectedIndexChanged;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(13, 131);
            label20.Name = "label20";
            label20.Size = new Size(40, 20);
            label20.TabIndex = 14;
            label20.Text = "Köy:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(13, 52);
            label13.Name = "label13";
            label13.Size = new Size(87, 20);
            label13.TabIndex = 5;
            label13.Text = "Aktif/Değil:";
            // 
            // buildkoycbx
            // 
            buildkoycbx.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            buildkoycbx.FormattingEnabled = true;
            buildkoycbx.Location = new Point(77, 128);
            buildkoycbx.Name = "buildkoycbx";
            buildkoycbx.Size = new Size(148, 28);
            buildkoycbx.TabIndex = 13;
            buildkoycbx.SelectedIndexChanged += buildkoycbx_SelectedIndexChanged;
            // 
            // buildchk
            // 
            buildchk.AutoSize = true;
            buildchk.Location = new Point(120, 55);
            buildchk.Name = "buildchk";
            buildchk.Size = new Size(18, 17);
            buildchk.TabIndex = 4;
            buildchk.UseVisualStyleBackColor = true;
            buildchk.CheckedChanged += buildchk_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(65, 5);
            label7.Name = "label7";
            label7.Size = new Size(107, 23);
            label7.TabIndex = 2;
            label7.Text = "Bina Geliştir";
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.ControlLight;
            panel5.Controls.Add(irklbl);
            panel5.Controls.Add(label29);
            panel5.Controls.Add(label21);
            panel5.Controls.Add(atliadettxt);
            panel5.Controls.Add(yayaadettxt);
            panel5.Controls.Add(label18);
            panel5.Controls.Add(label17);
            panel5.Controls.Add(label16);
            panel5.Controls.Add(askerkoycmbx);
            panel5.Controls.Add(atlicbx);
            panel5.Controls.Add(yayacbx);
            panel5.Controls.Add(label14);
            panel5.Controls.Add(askeregitchk);
            panel5.Controls.Add(label8);
            panel5.Location = new Point(573, 27);
            panel5.Name = "panel5";
            panel5.Size = new Size(283, 251);
            panel5.TabIndex = 14;
            // 
            // irklbl
            // 
            irklbl.AutoSize = true;
            irklbl.Font = new Font("Arial", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            irklbl.ForeColor = Color.Black;
            irklbl.Location = new Point(90, 84);
            irklbl.Name = "irklbl";
            irklbl.Size = new Size(117, 19);
            irklbl.TabIndex = 22;
            irklbl.Text = "Henüz Irk Yok";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(13, 84);
            label29.Name = "label29";
            label29.Size = new Size(38, 20);
            label29.TabIndex = 21;
            label29.Text = "IRK:";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(224, 90);
            label21.Name = "label21";
            label21.Size = new Size(43, 20);
            label21.TabIndex = 16;
            label21.Text = "Adet";
            // 
            // atliadettxt
            // 
            atliadettxt.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            atliadettxt.Location = new Point(232, 166);
            atliadettxt.Name = "atliadettxt";
            atliadettxt.Size = new Size(34, 27);
            atliadettxt.TabIndex = 14;
            atliadettxt.TextChanged += atliadettxt_TextChanged;
            // 
            // yayaadettxt
            // 
            yayaadettxt.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            yayaadettxt.Location = new Point(232, 122);
            yayaadettxt.Name = "yayaadettxt";
            yayaadettxt.Size = new Size(34, 27);
            yayaadettxt.TabIndex = 13;
            yayaadettxt.TextChanged += yayaadettxt_TextChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(13, 211);
            label18.Name = "label18";
            label18.Size = new Size(40, 20);
            label18.TabIndex = 12;
            label18.Text = "Köy:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(13, 168);
            label17.Name = "label17";
            label17.Size = new Size(38, 20);
            label17.TabIndex = 11;
            label17.Text = "Atlı:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(13, 124);
            label16.Name = "label16";
            label16.Size = new Size(45, 20);
            label16.TabIndex = 10;
            label16.Text = "Yaya:";
            // 
            // askerkoycmbx
            // 
            askerkoycmbx.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            askerkoycmbx.FormattingEnabled = true;
            askerkoycmbx.Location = new Point(90, 208);
            askerkoycmbx.Name = "askerkoycmbx";
            askerkoycmbx.Size = new Size(176, 28);
            askerkoycmbx.TabIndex = 9;
            askerkoycmbx.SelectedIndexChanged += askerkoycmbx_SelectedIndexChanged;
            // 
            // atlicbx
            // 
            atlicbx.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            atlicbx.FormattingEnabled = true;
            atlicbx.Location = new Point(90, 165);
            atlicbx.Name = "atlicbx";
            atlicbx.Size = new Size(136, 28);
            atlicbx.TabIndex = 8;
            atlicbx.SelectedIndexChanged += atlicbx_SelectedIndexChanged;
            // 
            // yayacbx
            // 
            yayacbx.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            yayacbx.FormattingEnabled = true;
            yayacbx.Location = new Point(90, 121);
            yayacbx.Name = "yayacbx";
            yayacbx.Size = new Size(136, 28);
            yayacbx.TabIndex = 7;
            yayacbx.SelectedIndexChanged += yayacbx_SelectedIndexChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(4, 50);
            label14.Name = "label14";
            label14.Size = new Size(87, 20);
            label14.TabIndex = 6;
            label14.Text = "Aktif/Değil:";
            // 
            // askeregitchk
            // 
            askeregitchk.AutoSize = true;
            askeregitchk.Location = new Point(102, 53);
            askeregitchk.Name = "askeregitchk";
            askeregitchk.Size = new Size(18, 17);
            askeregitchk.TabIndex = 5;
            askeregitchk.UseVisualStyleBackColor = true;
            askeregitchk.CheckedChanged += askeregitchk_CheckedChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(90, 10);
            label8.Name = "label8";
            label8.Size = new Size(138, 23);
            label8.TabIndex = 3;
            label8.Text = "ASKER YETİŞTİR";
            // 
            // panel6
            // 
            panel6.BackColor = SystemColors.ControlLight;
            panel6.Controls.Add(label30);
            panel6.Controls.Add(label15);
            panel6.Controls.Add(askerolmesinchk);
            panel6.Controls.Add(label9);
            panel6.Location = new Point(868, 27);
            panel6.Name = "panel6";
            panel6.Size = new Size(239, 146);
            panel6.TabIndex = 12;
            // 
            // label30
            // 
            label30.Location = new Point(4, 70);
            label30.Name = "label30";
            label30.Size = new Size(203, 70);
            label30.TabIndex = 30;
            label30.Text = "Eksi üretimde olan köylerde 1 saatten az tahıl kalmışsa NPC yapar.";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(4, 36);
            label15.Name = "label15";
            label15.Size = new Size(87, 20);
            label15.TabIndex = 7;
            label15.Text = "Aktif/Değil:";
            // 
            // askerolmesinchk
            // 
            askerolmesinchk.AutoSize = true;
            askerolmesinchk.Location = new Point(103, 39);
            askerolmesinchk.Name = "askerolmesinchk";
            askerolmesinchk.Size = new Size(18, 17);
            askerolmesinchk.TabIndex = 6;
            askerolmesinchk.UseVisualStyleBackColor = true;
            askerolmesinchk.CheckedChanged += askerolmesinchk_CheckedChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(55, 9);
            label9.Name = "label9";
            label9.Size = new Size(143, 23);
            label9.TabIndex = 4;
            label9.Text = "ASKER ÖLMESİN";
            // 
            // baslatbtn
            // 
            baslatbtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            baslatbtn.ForeColor = Color.ForestGreen;
            baslatbtn.Location = new Point(316, 454);
            baslatbtn.Name = "baslatbtn";
            baslatbtn.Size = new Size(239, 67);
            baslatbtn.TabIndex = 15;
            baslatbtn.Text = "Programı Başlat";
            baslatbtn.UseVisualStyleBackColor = true;
            baslatbtn.Click += baslatbtn_Click;
            // 
            // tekrartxt
            // 
            tekrartxt.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            tekrartxt.Location = new Point(450, 421);
            tekrartxt.Name = "tekrartxt";
            tekrartxt.Size = new Size(102, 27);
            tekrartxt.TabIndex = 16;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(316, 425);
            label22.Name = "label22";
            label22.Size = new Size(119, 20);
            label22.TabIndex = 17;
            label22.Text = "Tekrar Dakikası:";
            // 
            // lisanstxt
            // 
            lisanstxt.Location = new Point(892, 452);
            lisanstxt.Name = "lisanstxt";
            lisanstxt.Size = new Size(214, 27);
            lisanstxt.TabIndex = 18;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(821, 455);
            label23.Name = "label23";
            label23.Size = new Size(56, 20);
            label23.TabIndex = 19;
            label23.Text = "Lisans:";
            // 
            // lisansbtn
            // 
            lisansbtn.Location = new Point(912, 485);
            lisansbtn.Name = "lisansbtn";
            lisansbtn.Size = new Size(176, 36);
            lisansbtn.TabIndex = 20;
            lisansbtn.Text = "Lisans Ekle!";
            lisansbtn.UseVisualStyleBackColor = true;
            lisansbtn.Click += lisansbtn_Click;
            // 
            // panel7
            // 
            panel7.BackColor = SystemColors.ControlLight;
            panel7.Controls.Add(chatidtxt);
            panel7.Controls.Add(label28);
            panel7.Controls.Add(label27);
            panel7.Controls.Add(saldirivarchk);
            panel7.Controls.Add(label26);
            panel7.Location = new Point(573, 293);
            panel7.Name = "panel7";
            panel7.Size = new Size(283, 125);
            panel7.TabIndex = 21;
            // 
            // chatidtxt
            // 
            chatidtxt.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            chatidtxt.Location = new Point(111, 66);
            chatidtxt.Name = "chatidtxt";
            chatidtxt.Size = new Size(156, 27);
            chatidtxt.TabIndex = 23;
            chatidtxt.TextChanged += chatidtxt_TextChanged;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(13, 69);
            label28.Name = "label28";
            label28.Size = new Size(65, 20);
            label28.TabIndex = 22;
            label28.Text = "Chat ID:";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label27.Location = new Point(12, 37);
            label27.Name = "label27";
            label27.Size = new Size(87, 20);
            label27.TabIndex = 21;
            label27.Text = "Aktif/Değil:";
            // 
            // saldirivarchk
            // 
            saldirivarchk.AutoSize = true;
            saldirivarchk.Location = new Point(111, 41);
            saldirivarchk.Name = "saldirivarchk";
            saldirivarchk.Size = new Size(18, 17);
            saldirivarchk.TabIndex = 20;
            saldirivarchk.UseVisualStyleBackColor = true;
            saldirivarchk.CheckedChanged += saldirivarchk_CheckedChanged;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label26.Location = new Point(90, 11);
            label26.Name = "label26";
            label26.Size = new Size(104, 23);
            label26.TabIndex = 20;
            label26.Text = "Saldırı Var !";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label31.Location = new Point(573, 452);
            label31.Name = "label31";
            label31.Size = new Size(0, 20);
            label31.TabIndex = 22;
            // 
            // lblLisansDurumu2
            // 
            lblLisansDurumu2.AutoSize = true;
            lblLisansDurumu2.Location = new Point(566, 501);
            lblLisansDurumu2.Name = "lblLisansDurumu2";
            lblLisansDurumu2.Size = new Size(0, 20);
            lblLisansDurumu2.TabIndex = 23;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1117, 537);
            Controls.Add(lblLisansDurumu2);
            Controls.Add(label31);
            Controls.Add(panel7);
            Controls.Add(lisansbtn);
            Controls.Add(label23);
            Controls.Add(lisanstxt);
            Controls.Add(label22);
            Controls.Add(tekrartxt);
            Controls.Add(baslatbtn);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(kaydetbtn);
            Controls.Add(bilgilbx);
            Controls.Add(formatbtn);
            Controls.Add(girisbtn);
            Controls.Add(passwordtxt);
            Controls.Add(nicknametxt);
            Controls.Add(sunucutxt);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Travian PRF BOT";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox sunucutxt;
        private TextBox nicknametxt;
        private TextBox passwordtxt;
        private Button girisbtn;
        private Button formatbtn;
        private ListBox bilgilbx;
        private Button kaydetbtn;
        private Panel panel1;
        private Label label4;
        private Panel panel2;
        private Label label5;
        private Panel panel3;
        private Label label6;
        private Panel panel4;
        private Label label7;
        private Panel panel5;
        private Panel panel6;
        private Label label8;
        private Label label10;
        private CheckBox npcchk;
        private Label label11;
        private CheckBox otoyagmachk;
        private Label label12;
        private CheckBox otomacerachk;
        private Label label13;
        private CheckBox buildchk;
        private Label label14;
        private CheckBox askeregitchk;
        private Label label15;
        private CheckBox askerolmesinchk;
        private Label label9;
        private Label label19;
        private ComboBox npckoycbx;
        private Label label20;
        private ComboBox buildkoycbx;
        private Label label18;
        private Label label17;
        private Label label16;
        private ComboBox askerkoycmbx;
        private ComboBox atlicbx;
        private ComboBox yayacbx;
        private TextBox yayaadettxt;
        private Label label21;
        private TextBox atliadettxt;
        private Button baslatbtn;
        private TextBox tekrartxt;
        private Label label22;
        private TextBox lisanstxt;
        private Label label23;
        private Button lisansbtn;
        private Label label25;
        private Label label24;
        private ComboBox binalarcbx;
        private Panel panel7;
        private TextBox chatidtxt;
        private Label label28;
        private Label label27;
        private CheckBox saldirivarchk;
        private Label label26;
        private Label label29;
        private Label irklbl;
        private Label label30;
        private Label label31;
        private Label lblLisansDurumu2;
        private Label label32;
    }
}
