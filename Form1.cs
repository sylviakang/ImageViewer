namespace ImageViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PictureBox[] pB;
        string[] fileNames;
        int startIndex = 0;
        int endIndex = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            pB =   new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5,pictureBox6 };
            txtRoute.Enabled = false;
            foreach (var pb in pB)
            {
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.BorderStyle = BorderStyle.FixedSingle;
            }
            pbBig.SizeMode = PictureBoxSizeMode.StretchImage;
            pbBig.BorderStyle = BorderStyle.FixedSingle;
            openFileDialog1.Multiselect = true;
            btnLeft.Enabled = false;
            btnRight.Enabled = false;
            //openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
           if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileNames = openFileDialog1.FileNames;
                startIndex = 0;
                endIndex = Math.Min(pB.Length, fileNames.Length) - 1;
                UpdatePictureBoxes();
                btnLeft.Enabled = false;
                btnRight.Enabled = endIndex < fileNames.Length - 1;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(sender is PictureBox pb && pb.Image != null)
            {
                //pbBig.Image = pb.Image;
                pbBig.Image = ((PictureBox)sender).Image;
                txtRoute.Text = fileNames[startIndex + Array.IndexOf(pB, pb)];
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            startIndex = Math.Max(0, startIndex - pB.Length);
            endIndex = Math.Min(endIndex - pB.Length, fileNames.Length - 1);          
            UpdatePictureBoxes();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            startIndex += pB.Length;
            endIndex = Math.Min(endIndex + pB.Length , fileNames.Length - 1);
            UpdatePictureBoxes();
        }

        private void UpdatePictureBoxes()
        {
            btnLeft.Enabled = startIndex > 0; 
            btnRight.Enabled = !(endIndex == fileNames.Length-1);
              
            for (int i = 0; i < pB.Length; i++)
            {
                if (startIndex + i < fileNames.Length)
                {
                    pB[i].Image = Image.FromFile(fileNames[startIndex + i]);
                }
                else
                {
                    pB[i].Image = null;
                }
            }
        }
    }
}