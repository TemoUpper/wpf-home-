using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
     public  static class Validation
    {
       public static bool ValUsername(string userInput) 
        {
            if (userInput.Length > 50) return false;
            else return true;
        }
        public static bool ValUserEmail( string userInput)
        {
            if (!(userInput.Contains("@"))) return false;
            else return true;
        }
        public static bool ValUserPass(string userInput)
        {
            if (userInput.Length < 8 || userInput.Length < 8 || userInput.Length > 50) return false;
            else return true;
        }

        public static bool IsUserInputNumber(string instance) 
        {
            int num = -1;
            if ((int.TryParse(instance, out num))) return false;
            else return true;
        }
    }
}
