using CRB.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CRB
{

    internal class Nav
    {
        public static Frame frame;
        public static void Visible(Button btn)
        {
            btn.Visibility = Visibility.Visible;
        }

        public static void Collapsed(Button btn)
        {
            btn.Visibility = Visibility.Collapsed;
        }

    }
	public partial class Electronic_medical_card
    {
		public virtual string MKBList { get { return string.Join(", ", MKB.Select(x => x.name_diagnosis)); } }
        public bool CheckUser
        {
            get
            {
                if (Right.curUser != null)
                {
                    if (Right.curUser.id_role == 1 && Right.curUser.id_role == 1)
                        return true;
                }
                return false;
            }
        }
    }

}
