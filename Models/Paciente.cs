namespace Parcial1_Programacion3_28_4.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string NombrePaciente { get; set; } 

        public int ObraSocialId { get; set; }

        public int Edad { get; set; }

        public string Sintomas { get; set; }

        public ObraSocial? ObraSocialElegida { get; set; }

    }
}
