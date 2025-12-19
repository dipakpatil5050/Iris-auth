using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAuth.ViewModels
{
    public class LoginTypeItem
    {
        public int Value { get; }
        public string Text { get; }

        public LoginTypeItem(int value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}
