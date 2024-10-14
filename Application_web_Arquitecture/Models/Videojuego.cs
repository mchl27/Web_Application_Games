using System;
using System.Collections.Generic;

namespace Application_web_Arquitecture.Models;

public partial class Videojuego
{
    public int IdJuego { get; set; }

    public string Titulo { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public decimal Precio { get; set; }

    public bool Disponibilidad { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
