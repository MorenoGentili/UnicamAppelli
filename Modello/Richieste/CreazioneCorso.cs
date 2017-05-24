using System.ComponentModel.DataAnnotations;

namespace UnicamAppelli.Modello.Richieste {
    public class CreazioneCorso {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Docente { get; set; }

    }
}