using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace FormMainMenu
{
    public partial class Form1 : Form
    {
        //Fields
        private Button currentButton;

        //Açýk olan formu belirtir
        private Form activeForm;
        public Form1()
        {
            InitializeComponent();

            // kapatma butonunu gizle
            btnCloseChildForm.Visible = false;
            // Formun adýný sildi.
            this.Text = string.Empty;
            // açma kapama butonlarýný kaldýrdý.
            this.ControlBox = false;
            // windows bar gözüksün diye var.
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }



        // Fare yakalamayý býrakmak için kullanýlýr.
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        // Belirli bir pencereye belirli bir mesajý göndermek için kullanýlýr.
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = Color.Red;
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new Font("Uni Sans Heavy Italic CAPS", 13.2F, FontStyle.Bold);
                    // kapatma butonunu göster
                    btnCloseChildForm.Visible = true;
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.DarkMagenta;
                    previousBtn.ForeColor = Color.Coral;
                    previousBtn.Font = new Font("Uni Sans Heavy Italic CAPS", 10.2F, FontStyle.Bold);
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPanel.Controls.Add(childForm);
            this.panelDesktopPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;

        }

        private void BtnProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormProduct(), sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormBtn2(), sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                Reset();
            }
        }

        private void Reset()
        {
            DisableButton();
            lblTitle.Text = "HOME";
            panelTitleBar.BackColor = Color.FromArgb(0, 150, 156);
            currentButton = null;
            btnCloseChildForm.Visible = false; // kapatma butonunu gizle

        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
