using NAudio.Wave;
using NAudio.Vorbis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsMusic1
{
    public partial class Form1 : Form
    {
        string[] files;
        List<string> localmusiclist = new List<string> { };
        public Form1()
        {
            InitializeComponent();
        }

        private void showmusic(string filename)
        {
            string extension = Path.GetExtension(filename);
             if (extension == ".ogg")
            {
                Console.WriteLine("this is ogg file.");
            }
             else
            {
                Console.WriteLine("this is not ogg file.");
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "选择音频|*mp3;*.wav;*.fllac";
            openFileDialog1.Multiselect = true;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                if (files!=null)
                {
                    Array.Clear(files,0, files.Length);
                }
                files =openFileDialog1.FileNames;

                string[] array = files;

                foreach(string x in array)
                { 
                    listBox1.Items.Add(x);
                    localmusiclist.Add(x);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (localmusiclist.Count > 0)
            {
                axWindowsMediaPlayer1.URL = localmusiclist[listBox1.SelectedIndex];
                showmusic(axWindowsMediaPlayer1.URL);
                label1.Text = Path.GetFileNameWithoutExtension(localmusiclist[listBox1.SelectedIndex]);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
            label2.Text = trackBar1.Value + "%";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
                if (localmusiclist.Count > 0)
                {

                    int index = listBox1.SelectedIndex + 1;

                    if (index >= localmusiclist.Count()) { index = 0; }

                    axWindowsMediaPlayer1.URL = localmusiclist[index];

                    showmusic(axWindowsMediaPlayer1.URL);
                    label1.Text = Path.GetFileNameWithoutExtension(localmusiclist[index]);

                    listBox1.SelectedIndex = index;


                }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string oggFilePath = "path_to_your_ogg_file.ogg"; // 替换为您的OGG文件路径  
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "播放音频|*.ogg";

            if (openFileDialog.ShowDialog()== DialogResult.OK)
            {
                oggFilePath = openFileDialog.FileName;
            }
            using (var vorbisreader = new VorbisWaveReader(oggFilePath))
            {
               using (var outputDevice = new WaveOutEvent())
               {
                  outputDevice.Init(vorbisreader);
                  outputDevice.Play();

                  // 等待播放完毕，或者您可以添加其他逻辑，比如用户输入来停止播放  
                  while (outputDevice.PlaybackState == PlaybackState.Playing)
                   {
                     System.Threading.Thread.Sleep(1000);
                   }
               }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
         
                if (localmusiclist.Count > 0)
                {
                    int index = listBox1.SelectedIndex;

                    // 如果当前没有选中任何项，或者已经是第一项，可以选择播放最后一项或者保持当前项不变  
                    if (index < 0)
                    {
                        index = 0; // 或者你可以选择播放列表中的最后一项：index = localmusiclist.Count - 1;  
                    }
                    else
                    {
                        index--; // 否则播放上一项  

                        // 如果现在索引是-1（即原本是第一项），可以选择播放列表中的最后一项或者保持当前项不变  
                        if (index < 0)
                        {
                            index = localmusiclist.Count - 1; // 循环到最后一项  
                        }
                    }

                    axWindowsMediaPlayer1.URL = localmusiclist[index];
                    showmusic(axWindowsMediaPlayer1.URL);
                    label1.Text = Path.GetFileNameWithoutExtension(localmusiclist[index]);

                    listBox1.SelectedIndex = index;
                }
            
        }
    }
 }
