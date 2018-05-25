using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMate
{
    class Split_Desplit
    {

        internal static Shortcut Split(string encText)
        {
            Shortcut s = new Shortcut();

            string decText = Crypto.Decrypt(encText);
            if (decText == "" || decText == "NaN")
            {
                s.Title = "NaN";
            }
            else
            {
                

                string[] fullText = decText.Split('!');
                s.Title = fullText[0];
                s.Address = fullText[1];
                s.Icon = Convert.ToInt32(fullText[2]);

            }
            return s;
            //to intgeers.icon


        }

        internal static string DeSplit(string title, string addr, int _icon)
        {
            string icon = _icon.ToString();
            string combinedText = title +"!" + addr + "!"+ icon;
            string encryptString = Crypto.Encrypt(combinedText);
            return encryptString;
        }
    }
}
