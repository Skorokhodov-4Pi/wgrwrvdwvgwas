using CRB.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB
{
	internal class CurElMedCard
	{
		public static Electronic_medical_card card;
	}

	public partial class Patient
	{
        public string fullname { get => $"{Surname} {First_name} {Patronymic} {date_of_birth.ToShortDateString()} {gender}"; }
        public string fullname2 { get => $"{Surname} {First_name} {Patronymic}"; }
        public string policy_number { get => ConnectDB.GetCont().OMS_policy.OrderByDescending(x => x.end_date).Where(x=> x.end_date >= DateTime.Now).FirstOrDefault(x => x.id_patient == id_patient)?.policy_number.ToString() ?? "Нет данных"; }


    }

    public partial class Staff
	{
		public string fullname_Staff { get { return $"{surname} {first_name} {patronymic}"; } }
	}
    public partial class beds
    {
        public string mesto { get { return $"Палата: {id_room} Койка: {num_bed} "; } }
    }
    
}
