using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class Pacijent
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int IdPacijenta { get; set; }
        [DataMember]
        public string Ime { get; set; }

        [DataMember]
        public string Jmbg { get; set; }
        [DataMember]
        public string Prezime { get; set; }
        

        public Pacijent()
        {
            
        }

        public Pacijent(string ime, string jmbg, string prezime)
        {
            Ime = ime;
            Jmbg = jmbg;
            Prezime = prezime;
        }


        public Pacijent KlonirajPacijenta()
        {
            Pacijent kopija = new Pacijent()
            {
                IdPacijenta = IdPacijenta,
                Ime = Ime,
                Prezime = Prezime,
                Jmbg = Jmbg
            };
            return kopija;
        }      
    }
}
