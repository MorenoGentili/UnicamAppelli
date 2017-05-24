using System.Collections.Generic;
using System.Threading.Tasks;
using UnicamAppelli.Modello;

namespace UnicamAppelli.Servizi {
    public interface IServizioCorsi {
        Task<IEnumerable<Corso>> Elenca();
        Task Crea(Corso corso);
    }
}