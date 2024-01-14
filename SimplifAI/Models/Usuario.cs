using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifAI.Models
{
    class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Email { get; set; }  

        public string Senha { get; set; }

        public string SenhaConfirma { get; set; }

        public byte[] Foto { get; set; }
    }
}
