﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

namespace SE_Project
{
    public enum Pengguna { Head, body };
    public partial class Form_Client : Form
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private bool isConnected = false;

        private readonly object ClipboardLock = new object();

        public Form_Client()
        {
            InitializeComponent();
        }

        private void MessagePopup(string message, Pengguna user)
        {
            var time = DateTime.Now.ToString("HH:mm");
            var chatBubble = new Panel();

            var messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.Font = new Font("Segoe UI", 12);
            messageLabel.ForeColor = Color.Transparent;
            messageLabel.MaximumSize = new Size(250, 0);
            messageLabel.AutoSize = true;

            var timeLabel = new Label();
            timeLabel.Text = time;
            timeLabel.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            timeLabel.ForeColor = Color.Gray;
            timeLabel.AutoSize = true;

            //chatBubble.Controls.Add(messageLabel);
            //chatBubble.Controls.Add(timeLabel);

            richTextBox_Display.Invoke((MethodInvoker)(() =>
            {
                var sendColor = user == Pengguna.Head ? Color.Yellow : Color.Aqua;
                richTextBox_Display.BorderStyle = BorderStyle.Fixed3D;
                richTextBox_Display.AutoSize = true;
                richTextBox_Display.ShortcutsEnabled = true;
                //richTextBox_Display.MaximumSize = new Size(200, Height);
                richTextBox_Display.SelectionAlignment = user == Pengguna.Head ? HorizontalAlignment.Right : HorizontalAlignment.Left;
                //richTextBox_Display.Controls.Add(chatBubble);
                //chatBubble.BringToFront();
                richTextBox_Display.AppendText(" " + message + " ");
                richTextBox_Display.AppendText(" " + time);
                richTextBox_Display.AppendText(Environment.NewLine);
                richTextBox_Display.ScrollToCaret();
            }));
        }

        private void Form_Client_Load(object sender, EventArgs e)
        {
            // Initialize client connection
            client = new TcpClient();
            // Set up your IP address and port
            string ipAddress = "127.0.0.1";
            int port = 12345;
            try
            {
                client.Connect(ipAddress, port);
                NetworkStream networkStream = client.GetStream();
                reader = new StreamReader(networkStream);
                writer = new StreamWriter(networkStream);
                writer.AutoFlush = true;

                // Start listening for incoming messages from the server
                Task.Run(() => ReceiveMessages());

                // Set the connection status to true
                isConnected = true;

                button_Connect.BackColor = Color.Green;

                // Display the connection status message
                DisplayConnectionStatus("Successfully connected to the server!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the server: " + ex.Message);
                button_Connect.BackColor = Color.Red;
            }
        }
        private void DisplayConnectionStatus(string message)
        {
            richTextBox_Display.Invoke(new Action(() =>
            {
                richTextBox_Display.AppendText(message + Environment.NewLine);
            }));
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            Form_Client_Load(sender, e);
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            string message = textBox_Text.Text;
            SendMessage(message);
            //MessagePopup("sent", Pengguna.Head);
            textBox_Text.Clear();
        }

        private void textBox_Text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                button_Send_Click(sender, e);
                //MessagePopup("sent", Pengguna.Head);
            }
        }

        private void pictureBox_Image_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif) | *.jpg; *.jpeg; *.png; *.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                SendImage(imagePath);
                //MessagePopup(" ", Pengguna.Head);
            }
        }

        private void richTextBox_Display_TextChanged(object sender, EventArgs e)
        {
            // You can add any additional logic here if needed
        }

        private void textBox_Text_TextChanged(object sender, EventArgs e)
        {
            // You can add any additional logic here if needed
        }

        private void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    string message = reader.ReadLine();

                    if (IsBase64Image(message))
                    {
                        // Received image
                        DisplayImageFromBase64(message, true);
                        MessagePopup(" ", Pengguna.body);
                    }
                    else
                    {
                        // Received message
                        DisplayMessage("Server: " + message, Pengguna.body);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving message: " + ex.Message);
            }
        }

        private bool IsBase64Image(string message)
        {
            // Check if the message is a valid base64 encoded image
            try
            {
                byte[] imageBytes = Convert.FromBase64String(message);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image.FromStream(ms);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void DisplayImageFromBase64(string base64Image, bool isServerImage)
        {
            pictureBox_Image.Invoke(new Action(() =>
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image originalImage = Image.FromStream(ms);
                        Image resizedImage = ResizeImage(originalImage, 200, 200);

                        // Acquire lock to access the clipboard
                        lock (ClipboardLock)
                        {
                            Clipboard.SetImage(resizedImage);
                            if (isServerImage)
                            {
                                richTextBox_Display.SelectionAlignment = HorizontalAlignment.Left;
                                richTextBox_Display.AppendText("Server: (Image uploaded)" + Environment.NewLine);
                                richTextBox_Display.Paste();
                                richTextBox_Display.AppendText(Environment.NewLine);
                            }
                            else
                            {
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error displaying image: " + ex.Message);
                }
            })); 
        }



        private void SendMessage(string message)
        {
            if (isConnected)
            {
                writer.WriteLine(message);
                DisplayMessage("You: " + message, Pengguna.Head);
            }
            else
            {
                MessageBox.Show("It is not connected to the server. Press the connect button to connect to the server");
            }
        }
        private void SendImage(string imagePath)
        {
            if (isConnected)
            {
                try
                {
                    byte[] imageData = File.ReadAllBytes(imagePath);
                    string base64Image = Convert.ToBase64String(imageData);

                    // Acquire lock to access the clipboard
                    lock (ClipboardLock)
                    {
                        writer.WriteLine(base64Image);
                    }

                    MessagePopup("You: (Uploaded an image)"+ Environment.NewLine, Pengguna.Head);
                    DisplayImage(imagePath);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending image: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("It is not connected to the server. Press the connect button to connect to the server");
            }
        }

        private void DisplayImage(string imagePath)
        {

            pictureBox_Image.Invoke(new Action(() =>
            {
                try
                {
                    Image originalImage = Image.FromFile(imagePath);
                    Image resizedImage = ResizeImage(originalImage, 200, 200);

                    lock (ClipboardLock)
                    {
                        Clipboard.SetImage(resizedImage);
                        richTextBox_Display.Paste();
                        richTextBox_Display.AppendText(Environment.NewLine);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error displaying image: " + ex.Message);
                }
            }));
        }
        private Image ResizeImage(Image image, int width, int height)
        {
            // Create a new bitmap with the desired width and height
            Bitmap resizedImage = new Bitmap(width, height);

            // Create a graphics object from the resized bitmap
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        private void DisplayMessage(string message, Pengguna user)
        {
            richTextBox_Display.Invoke(new Action(() =>
            {
                MessagePopup(message + Environment.NewLine , user);
                //richTextBox_Display.Focus();
                richTextBox_Display.ScrollToCaret();
                
            }));
        }



        private void button_Upload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string chatHistory = richTextBox_Display.Text;
                File.WriteAllText(filePath, chatHistory);
                MessageBox.Show("Chat history saved successfully!");
            }
        }

        private void Form_Client_Load_1(object sender, EventArgs e)
        {

        }
    }
}
