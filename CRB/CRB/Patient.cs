//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            this.Electronic_medical_card = new HashSet<Electronic_medical_card>();
        }
    
        public int id_patient { get; set; }
        public string Surname { get; set; }
        public string First_name { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime date_of_birth { get; set; }
        public string gender { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string SNILS { get; set; }
        public string actual_address { get; set; }
        public string registered_address { get; set; }
        public int id_OMS_policy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Electronic_medical_card> Electronic_medical_card { get; set; }
        public virtual OMS_policy OMS_policy { get; set; }
    }
}
