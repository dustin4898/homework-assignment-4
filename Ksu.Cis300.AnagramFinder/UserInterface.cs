using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ksu.Cis300.TrieLibrary;
using System.IO;

namespace Ksu.Cis300.AnagramFinder
{
    public partial class UserInterface : Form
    {
        public UserInterface()
        {
            InitializeComponent();
        }

        private ITrie _words;

        private void uxFind_Click(object sender, EventArgs e)
        {
            string letters = _words.ToString();
            MessageBox.Show("n anagrams found.");
            ListBox listBox1 = new ListBox();
            List<LetterCounter1> temp = AnagramFinderClass.GetLetterCounters(letters);
            List<IList<ListBox>> list = new List<IList<ListBox>>();
            listBox1.BeginUpdate();
            list.Clear();
            AnagramFinderClass.GetAnagrams();
            listBox1.Items.Add(list);
            listBox1.EndUpdate();
        }

        private void uxSaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text files | *.txt | All files | *.*";
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        sw.WriteLine(listBox1.ToString());
                    }
                    MessageBox.Show("File written.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void UserInterface_Load(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files | *.txt | All files | *.*";
            openFileDialog.Title = "Open Word List";
            string file = openFileDialog.FileName;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _words = AnagramFinderClass.GetWordList(file);
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
