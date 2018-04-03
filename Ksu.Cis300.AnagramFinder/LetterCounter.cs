using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ksu.Cis300.TrieLibrary;
using System.IO;

public struct LetterCounter1
{

    public LetterCounter1(List<LetterCounter1> letterCount, string letters, object next) : this()
    {
    }

    public int Count { get; }

    public char Letter { get; }

    public int Next { get; }

    public void LetterCounter(int count, char letter, int next)
    {
        int Count = count;
        char Letter = letter;
        int Next = next;
    }
}
