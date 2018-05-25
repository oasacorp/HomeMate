using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMate
{
    class Dictionary
    {
        public static string iconDict(int id)
        {
            switch (id)
            {
                case 1:
                    return "\uf015";
                case 2:
                    return "\uf236";
                case 3:
                    return "\uf2cd";
                case 4:
                    return "\uf26c";

                case 5:
                    return "\uf2e7";
                case 6:
                    return "\uf462";

                case 7:
                    return "\uf494";
                case 8:
                    return "\uf007";
                case 9:
                    return "\uf069";
                default:
                    return "+";
            }

        }
    }
}
