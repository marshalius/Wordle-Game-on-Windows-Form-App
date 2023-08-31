using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Media;
namespace Wordle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int sira = 0;
        int kutu = 0;
        int dictParameter;
        int saniye = 0, dakika = 0;
        bool siraSon = false;
        bool kelimeKontrol;
        string cevap,kelime;
        SoundPlayer uyari = new SoundPlayer();
        SoundPlayer galibiyet = new SoundPlayer();
        SoundPlayer maglubiyet = new SoundPlayer();
        SoundPlayer tus = new SoundPlayer();
        Button[,] kutular=new Button[6,5];
        Button[] harfler = new Button[29];
        Random rand = new Random();
        List<string> dict = new List<string>();
        private void Form1_Load(object sender, EventArgs e)
        {
            YeniOyun();
        }
        private void YeniOyun()
        {
            tus.SoundLocation = @"tus.wav";
            galibiyet.SoundLocation = @"galibiyet.wav";
            maglubiyet.SoundLocation = @"maglubiyet.wav";
            uyari.SoundLocation = @"uyari.wav";
            var dictionary = File.ReadLines(@"Dictionary.txt");
            foreach (string kelime in dictionary)
            {
                dict.Add(kelime);
            }
            dictParameter = rand.Next(0, dict.Count);
            cevap = dict[dictParameter];
            kutular[0, 0] = button1; kutular[0, 1] = button2; kutular[0, 2] = button3; kutular[0, 3] = button4; kutular[0, 4] = button5;
            kutular[1, 0] = button6; kutular[1, 1] = button7; kutular[1, 2] = button8; kutular[1, 3] = button9; kutular[1, 4] = button10;
            kutular[2, 0] = button11; kutular[2, 1] = button12; kutular[2, 2] = button13; kutular[2, 3] = button14; kutular[2, 4] = button15;
            kutular[3, 0] = button16; kutular[3, 1] = button17; kutular[3, 2] = button18; kutular[3, 3] = button19; kutular[3, 4] = button20;
            kutular[4, 0] = button21; kutular[4, 1] = button22; kutular[4, 2] = button23; kutular[4, 3] = button24; kutular[4, 4] = button25;
            kutular[5, 0] = button26; kutular[5, 1] = button27; kutular[5, 2] = button28; kutular[5, 3] = button29; kutular[5, 4] = button30;

            harfler[0] = button_e; harfler[1] = button_r; harfler[2] = button_t; harfler[3] = button_y; harfler[4] = button_u; harfler[5] = button_ii; harfler[6] = button_o; harfler[7] = button_p; harfler[8] = button_gg; harfler[9] = button_uu;
            harfler[10] = button_a; harfler[11] = button_s; harfler[12] = button_d; harfler[13] = button_f; harfler[14] = button_g; harfler[15] = button_h; harfler[16] = button_j; harfler[17] = button_k; harfler[18] = button_l; harfler[19] = button_ss; harfler[20] = button_i;
            harfler[21] = button_z; harfler[22] = button_c; harfler[23] = button_v; harfler[24] = button_b; harfler[25] = button_n; harfler[26] = button_m; harfler[27] = button_oo; harfler[28] = button_cc;
            for(int i=0;i<6;i++)
            {
                for(int j=0;j<5;j++)
                {
                    kutular[i, j].Text = "";
                    kutular[i, j].BackColor = Color.FromArgb(224, 224, 224);
                }
            }
            for (int i = 0; i < 29; i++)
            {
                harfler[i].Enabled = true;
                button_sil.Enabled = true;
                button_ok.Enabled = true;
            }
            for (int i=0;i<29;i++)
            {
                harfler[i].BackColor = Color.White;
            }
            timer1.Interval = 1000;
            timer1.Enabled = true;

            timer1.Enabled = true;
            saniye = 0;
            dakika = 0;
            label_zaman.Text = "00:00";
            label_mesaj.Text = "";

            sira = 0;
            kutu = 0;
            siraSon = false;
        }
        private void HarfEkle(string harf)
        {
            if(siraSon==false)
            {
                kutular[sira, kutu].Text = harf;
                if(kutu==4)
                {
                    siraSon = true;
                }
                else
                {
                    kutu++;
                }
                
            }
            label_mesaj.Text = "";
            tus.Play();

        }
        private void HarfKontrol(Button b, Color c)
        {
            for (int i = 0; i < 29; i++)
            {
                if(harfler[i].Text==b.Text)
                {
                    if(harfler[i].BackColor==Color.Green)
                    {
                        break;
                    }
                    else
                    {
                        harfler[i].BackColor = c;
                        break;
                    }
                    
                }
            }
        }
        private void Galibiyet()
        {
            kutular [sira, 0].BackColor = Color.LightGreen;
            kutular [sira, 1].BackColor = Color.LightGreen;
            kutular [sira, 2].BackColor = Color.LightGreen;
            kutular [sira, 3].BackColor = Color.LightGreen;
            kutular [sira, 4].BackColor = Color.LightGreen;
            label_mesaj.Location = new Point(122, 100);
            label_mesaj.Text = "KAZANDINIZ, TEBRİKLER!";
            label_mesaj.ForeColor = Color.Green;
            
            timer1.Enabled = false;

            for(int i=0; i<29; i++)
            {
                harfler[i].Enabled = false;
                button_sil.Enabled = false;
                button_ok.Enabled = false;
            }
            galibiyet.Play();
        }
        private void Maglubiyet()
        {
            label_mesaj.Location = new Point(139, 85);
            label_mesaj.Text = "KAYBETTİNİZ! CEVAP:\n" + cevap;
            label_mesaj.ForeColor = Color.Red;

            timer1.Enabled = false;

            for (int i = 0; i < 29; i++)
            {
                harfler[i].Enabled = false;
                button_sil.Enabled = false;
                button_ok.Enabled = false;
            }

            maglubiyet.Play();
        }
        private void KelimeKontrol()
        {
            kelime = kutular[sira, 0].Text + kutular[sira, 1].Text + kutular[sira, 2].Text + kutular[sira, 3].Text + kutular[sira, 4].Text;
            for(int i=0; i< dict.Count;i++)
            {
                if(kelime==dict[i])
                {
                    kelimeKontrol = true;
                    break;
                }
                kelimeKontrol = false;
            }
            if(kelime==cevap)
            {
                Galibiyet();
            }
            else if(kutular[sira, 4].Text=="")
            {
                label_mesaj.Location = new Point(67, 100);
                label_mesaj.Text = "LÜTFEN 5 HARFLİ BİR KELİME GİRİN!";
                label_mesaj.ForeColor = Color.Red;
                uyari.Play();
            }
            else if(kelimeKontrol==false)
            {
                label_mesaj.Location = new Point(25, 100);
                label_mesaj.Text = "LÜTFEN GEÇERLİ BİR TÜRKÇE KELİME GİRİN!";
                label_mesaj.ForeColor = Color.Red;
                uyari.Play();
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (kutular[sira, i].Text == cevap[j].ToString())
                        {
                            kutular[sira, i].BackColor = Color.Yellow;
                            HarfKontrol(kutular[sira, i], Color.Yellow);
                            break;
                        }
                        else
                        {
                            kutular[sira, i].BackColor = Color.Gray;
                            HarfKontrol(kutular[sira, i], Color.Gray);
                        }
                    }
                    if (kutular[sira, i].Text == cevap[i].ToString())
                    {
                        kutular[sira, i].BackColor = Color.LightGreen;
                        HarfKontrol(kutular[sira, i], Color.LightGreen);
                    }

                }
                if(sira==5)
                {
                    Maglubiyet();
                }
            }
            
        }
        private void button_e_Click(object sender, EventArgs e)
        {
            HarfEkle("E");
        }

        private void button_r_Click(object sender, EventArgs e)
        {
            HarfEkle("R");
        }

        private void button_t_Click(object sender, EventArgs e)
        {
            HarfEkle("T");
        }

        private void button_y_Click(object sender, EventArgs e)
        {
            HarfEkle("Y");
        }

        private void button_u_Click(object sender, EventArgs e)
        {
            HarfEkle("U");
        }

        private void button_ii_Click(object sender, EventArgs e)
        {
            HarfEkle("I");
        }

        private void button_o_Click(object sender, EventArgs e)
        {
            HarfEkle("O");
        }

        private void button_p_Click(object sender, EventArgs e)
        {
            HarfEkle("P");
        }

        private void button_gg_Click(object sender, EventArgs e)
        {
            HarfEkle("Ğ");
        }

        private void button_uu_Click(object sender, EventArgs e)
        {
            HarfEkle("Ü");
        }


        private void button_a_Click(object sender, EventArgs e)
        {
            HarfEkle("A");
        }

        private void button_s_Click(object sender, EventArgs e)
        {
            HarfEkle("S");
        }

        private void button_d_Click(object sender, EventArgs e)
        {
            HarfEkle("D");
        }

        private void button_f_Click(object sender, EventArgs e)
        {
            HarfEkle("F");
        }

        private void button_g_Click(object sender, EventArgs e)
        {
            HarfEkle("G");
        }

        private void button_h_Click(object sender, EventArgs e)
        {
            HarfEkle("H");
        }

        private void button_j_Click(object sender, EventArgs e)
        {
            HarfEkle("J");
        }

        private void button_k_Click(object sender, EventArgs e)
        {
            HarfEkle("K");
        }

        private void button_l_Click(object sender, EventArgs e)
        {
            HarfEkle("L");
        }

        private void button_ss_Click(object sender, EventArgs e)
        {
            HarfEkle("Ş");
        }

        private void button_i_Click(object sender, EventArgs e)
        {
            HarfEkle("İ");
        }

        private void button_z_Click(object sender, EventArgs e)
        {
            HarfEkle("Z");
        }

        private void button_c_Click(object sender, EventArgs e)
        {
            HarfEkle("C");
        }

        private void button_v_Click(object sender, EventArgs e)
        {
            HarfEkle("V");
        }

        private void button_b_Click(object sender, EventArgs e)
        {
            HarfEkle("B");
        }

        private void button_n_Click(object sender, EventArgs e)
        {
            HarfEkle("N");
        }

        private void button_m_Click(object sender, EventArgs e)
        {
            HarfEkle("M");
        }

        private void button_oo_Click(object sender, EventArgs e)
        {
            HarfEkle("Ö");
        }

        private void button_cc_Click(object sender, EventArgs e)
        {
            HarfEkle("Ç");
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (kutu == 4)
            {
                KelimeKontrol();
                if(kelimeKontrol==true)
                {
                    sira++;
                    kutu = 0;
                    siraSon = false;
                }
                
            }
            else
            {
                label_mesaj.Location = new Point(67, 100);
                label_mesaj.Text = "LÜTFEN 5 HARFLİ BİR KELİME GİRİN!";
                label_mesaj.ForeColor = Color.Red;
                uyari.Play();
            }
            

        }
        

        private void button32_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wordle, rastgele seçilen 5 harfli Türkçe bir kelimeyi tahmin etme oyunudur. " +
                "Bu kelimeyi tahmin etmek için 6 hakkınız vardır. " +
                "Her tahmininizde kullandığınız harflerin cevap niteliğindeki kelime ile ilişkisine göre ipuçları alırsınız. " +
                "Bu ipuçları şu şekildedir:\n\n" +
                "-Kullandığınız harf, doğru cevapta da yer alıyorsa ve harfin konumu da doğru cevaptaki ile aynıysa harfin bulunduğu kutucuk Yeşil olur.\n\n" +
                "-Kullandığınız harf, doğru cevapta da yer alıyorsa fakat harfin konumu yanlış ise harfin bulunduğu kutucuk Sarı olur.\n\n" +
                "-Kullandığınız harf, doğru cevapta yer almıyorsa harfin bulunduğu kutucuk Gri olur.\n\n" +
                "Kelime tahmininde bulunurken, kullandığınız kelimenin 5 harfli Türkçe bir kelime olamsı gerekmektedir. " +
                "Aksi takdirde tahmininiz kabul edilmez ve ipucu alamazsınız.","Wordle Nasıl Oynanır?");
        }

        private void button_yeni_Click(object sender, EventArgs e)
        {
            YeniOyun();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            saniye++;
            if(dakika<10)
            {
                if(saniye<10)
                {
                    label_zaman.Text = "0" + dakika + ":0" + saniye;
                }
                else if(saniye>=10 && saniye <60)
                {
                    label_zaman.Text = "0" + dakika + ":" + saniye;
                }
                else
                {
                    saniye = 0;
                    dakika++;
                    label_zaman.Text = "0" + dakika + ":0" + saniye;
                }
            }
            else
            {
                if (saniye < 10)
                {
                    label_zaman.Text = dakika + ":0" + saniye;
                }
                else if (saniye >= 10 && saniye < 60)
                {
                    label_zaman.Text = dakika + ":" + saniye;
                }
                else
                {
                    saniye = 0;
                    dakika++;
                    label_zaman.Text = dakika + ":0" + saniye;
                }
            }
        }

        private void button_sil_Click(object sender, EventArgs e)
        {
            if (siraSon == true)
            {
                kutular[sira, kutu].Text = "";
            }
            else if (kutu!=0)
            {
                kutu--;
                kutular[sira, kutu].Text = "";

            }
            
            siraSon = false;
        }
    }
}
