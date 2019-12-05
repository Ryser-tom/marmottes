using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace marmottes
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEnCode_Click(object sender, RoutedEventArgs e)
        {
            char[] message = tbxText.Text.ToCharArray();
            int n = message.Length;
            var code = new Dictionary<char, int>();
            List<char> charSeen = new List<char>();

            int j = 0;
            foreach (var letter in message)
            {
                if (!charSeen.Contains(letter))
                {
                    charSeen.Add(letter);
                    int res = 0;

                    for (int i = 0; i < message.Length; i++)
                        if (letter == message[i])
                            res++;
                    code.Add(letter, res);
                    j++;
                    //tbxArrayResult.Text += letter + " = " + res + Environment.NewLine;
                }
            }
            var l = code.OrderBy(key => key.Key);
            var dic = l.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);

            show(code);
            //sort(code);
            huffman(sort(code));
        }

        static int countDistinct(char[] arr, int n)
        {
            int res = 1;

            // Pick all elements one by one 
            for (int i = 1; i < n; i++)
            {
                int j = 0;
                for (j = 0; j < i; j++)
                    if (arr[i] == arr[j])
                        break;

                // If not printed earlier,  
                // then print it 
                if (i == j)
                    res++;
            }
            return res;
        }

        public void show(Dictionary<char, int> code)
        {
            tbxArrayResult.Clear();
            foreach (KeyValuePair<char, int> value in code.OrderBy(key => key.Value))
            {
                tbxArrayResult.Text += value.Key + " = " + value.Value + Environment.NewLine;
            }

        }
        public Dictionary<string, int> sort(Dictionary<char, int> code)
        {
            var sorted = new Dictionary<string, int>();
            foreach (KeyValuePair<char, int> value in code.OrderBy(key => key.Value))
            {
                sorted.Add(value.Key.ToString(), value.Value);
            }
            return sorted;
        }

        public Dictionary<string, int> deleteFirstTwo(Dictionary<string, int> code)
        {
            var sorted = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> value in code.OrderBy(key => key.Value))
            {
                sorted.Add(value.Key.ToString(), value.Value);
            }
            sorted.Remove(code.ElementAt(0).Key);
            sorted.Remove(code.ElementAt(0).Key);
            return sorted;
        }

        public void huffman(Dictionary<string, int> code)
        {
            var compressed = new Dictionary<string, int>();
            code.Add(code.ElementAt(0).Key + " + " + code.ElementAt(1).Key, code.ElementAt(0).Value + code.ElementAt(1).Value);
            deleteFirstTwo(code);
            if (code.Count != 1)
            {
                huffman(code);
            }
            else
            {
                Console.WriteLine("done" + code);
            }
        }
    }
}
