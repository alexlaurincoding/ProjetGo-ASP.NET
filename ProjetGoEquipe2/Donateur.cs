//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjetGoEquipe2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Donateur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donateur()
        {
            this.Dons = new HashSet<Don>();
        }
    
        public int idDonateur { get; set; }
        public string nomDonateur { get; set; }
        public string prenomDonateur { get; set; }
        public string numeroTelDonateur { get; set; }
        public string adresseDonateur { get; set; }
        public string adresse2Donateur { get; set; }
        public string villeDonateur { get; set; }
        public string provinceDonateur { get; set; }
        public string cpDonateur { get; set; }
        public string emailDonateur { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Don> Dons { get; set; }
    }
}
